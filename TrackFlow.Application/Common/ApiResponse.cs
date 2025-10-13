namespace TrackFlow.Application.Common;

public class ApiResponse<T>
{
    public string Message { get; set; } =  string.Empty;
    public int StatusCode { get; set; } 
    public T? Payload  { get; set; }
    
    
    
}