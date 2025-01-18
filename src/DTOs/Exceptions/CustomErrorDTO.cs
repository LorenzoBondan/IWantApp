namespace IWantApp.DTOs.Exceptions;

public class CustomErrorDTO
{
    public DateTime Timestamp { get; set; }
    public int Status { get; set; }
    public string Message { get; set; }
    public string Path { get; set; }

    public CustomErrorDTO(int status, string message, string path)
    {
        Timestamp = DateTime.UtcNow;
        Status = status;
        Message = message;
        Path = path;
    }
}
