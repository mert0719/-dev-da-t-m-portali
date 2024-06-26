using System.ComponentModel.DataAnnotations;

namespace OdevPortal.Entity
{
    public class Odev
    {
        [Key]
        public int id { get; set; }
        public string odevAdi { get; set; }
        public string odevKonusu { get; set; }
        public int dersId { get; set; }
        public virtual Ders ders { get; set; }
    }
}
