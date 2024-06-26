using System.ComponentModel.DataAnnotations;

namespace OdevPortal.Entity
{
    
    public class Ogrenci
    {
        [Key]
        public int id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public int odevId { get; set; }
        public virtual Odev odev { get; set; }
    }
}
