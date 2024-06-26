using Microsoft.AspNetCore.Identity;

namespace OdevPortal.Entity
{
    public class Kullanicilar:IdentityUser<int>
    {
        public string adiSoyadi { get; set; }
        public string? numarasi { get; set; }
        public int dersId { get; set; }
        public virtual Ders ders { get; set; }

        public int? odevId { get; set; }
        public virtual Odev odev { get; set; }
    }
}
