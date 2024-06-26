using OdevPortal.Entity;

namespace OdevPortal.Model
{
    public class OgretmenModel
    {
        public int id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public int dersId { get; set; }
        public string dersAdi { get; set; }
    }
}
