using System.Text.Json.Serialization;


namespace DevicesBackend.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
