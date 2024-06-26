using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdevPortal.Context;
using OdevPortal.Entity;

namespace OdevPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DerslerController : ControllerBase
    {
        private readonly OdevDb _db;

        public DerslerController(OdevDb db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var values = _db.ders.ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult Add(Ders ders)
        {
            Ders o = new Ders();
            o.dersAdi = ders.dersAdi;
            _db.ders.Add(o);
            _db.SaveChanges();
            return Ok("Ders başarılı bir şekilde eklendi");
        }
        [HttpPut]
        public IActionResult Edit(int id , Ders ders)
        {
            var values = _db.ders.Find(id);
            values.dersAdi = ders.dersAdi;
            _db.ders.Update(values);
            _db.SaveChanges();
            return Ok("Ders başarılı bir şekilde güncellendi");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var values = _db.ders.Find(id);           
            _db.ders.Remove(values);
            _db.SaveChanges();
            return Ok("Ders başarılı bir şekilde silindi");
        }
    }
}
