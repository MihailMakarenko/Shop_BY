using System.Text.Json;

namespace Entites.ErrorModel
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
