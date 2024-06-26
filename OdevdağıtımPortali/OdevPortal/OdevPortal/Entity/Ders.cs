using System.ComponentModel.DataAnnotations;

namespace OdevPortal.Entity
{
    public class Ders
    {
        [Key]
        public int id { get; set; }
        public string dersAdi { get; set; }
       
       
    }
}
