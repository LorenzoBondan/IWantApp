namespace IWantApp.Services.Exceptions;

public class RegistroDuplicadoException : Exception
{
    private static readonly string StandardMessage = "Existem outros registros iguais a este.";
    public string Detail { get; }

    public RegistroDuplicadoException()
        : base(StandardMessage)
    {
    }

    public RegistroDuplicadoException(string detail)
        : this()
    {
        Detail = detail;
    }
}
