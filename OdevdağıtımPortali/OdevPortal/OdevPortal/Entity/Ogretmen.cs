using System.ComponentModel.DataAnnotations;

namespace OdevPortal.Entity
{
    public class Ogretmen
    {
        [Key]
        public int id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public int dersId { get; set; }
        public virtual Ders ders { get; set; }
    }
}
