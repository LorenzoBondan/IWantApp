namespace IWantApp.DTOs.Exceptions;

public class ValidationError : CustomErrorDTO
{
    public List<FieldErrorDTO> Errors { get; set; } = new();

    public ValidationError(int status, string message, string path)
        : base(status, message, path) { }

    public void AddError(string field, string errorMessage)
    {
        Errors.Add(new FieldErrorDTO { Field = field, ErrorMessage = errorMessage });
    }
}
