namespace DevicesBackend.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login {  get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public required byte[] Salt { get; set; }
    }
}
