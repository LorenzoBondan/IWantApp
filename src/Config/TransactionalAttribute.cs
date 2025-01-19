namespace IWantApp.Config;

using IWantApp.Models.Context;
using Microsoft.AspNetCore.Mvc.Filters;

public class TransactionalAttribute : ActionFilterAttribute
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionalAttribute(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var resultContext = await next(); // Executa a ação
                if (resultContext.Exception == null || resultContext.ExceptionHandled)
                {
                    await transaction.CommitAsync(); // Confirma a transação se não houver exceções
                }
                else
                {
                    await transaction.RollbackAsync(); // Reverte a transação em caso de exceção
                }
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
