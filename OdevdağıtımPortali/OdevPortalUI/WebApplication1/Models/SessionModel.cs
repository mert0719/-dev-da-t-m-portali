namespace WebApplication1.Models
{
    public class SessionModel
    {
        public string jwt { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string adiSoyadi { get; set; }
        public int dersId { get; set; }

    }
}
