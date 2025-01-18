using IWantApp.DTOs.Exceptions;

namespace IWantApp.Services.Exceptions;

public class ValidationException : Exception
{
    public List<FieldErrorDTO> Errors { get; set; }

    public ValidationException(List<FieldErrorDTO> errors)
        : base("Validação falhou")
    {
        Errors = errors;
    }
}
