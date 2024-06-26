using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdevPortal.Context;
using OdevPortal.Entity;
using OdevPortal.Model;

namespace OdevPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdevlerController : ControllerBase
    {
        private readonly OdevDb _db;
        private readonly UserManager<Kullanicilar> _kullanici;
        public OdevlerController(OdevDb db , UserManager<Kullanicilar> kullanicilar)
        {
            _db = db;
            _kullanici = kullanicilar;
        }

        [HttpGet]
        public IActionResult GetList(int id)
        {
            var values = _db.odevs.Where(x=>x.dersId == id).Include(x=>x.ders).Select(x => new OdevModel
            {
                dersAdi = x.ders.dersAdi,
                dersId = x.dersId,
                id = x.id,
                odevAdi = x.odevAdi,
                odevKonusu = x.odevKonusu,
            }).ToList();
            return Ok(values);
        }

        [HttpGet("UserByHomeWork")]
        public IActionResult UserByHomeWork(int userId)
        {
            var values = _kullanici.Users.Where(x => x.Id == userId).Include(x=>x.odev).Include(x=>x.ders)
                .Select(x=>new OdevModel
                {
                    dersAdi = x.ders.dersAdi,
                    odevAdi = x.odev.odevAdi,
                    odevKonusu = x.odev.odevKonusu
                }).ToList();           
            return Ok(values);
        }

        [HttpPost]
        public IActionResult Add(OdevModel model)
        {
            Odev odev = new Odev();
            odev.dersId = model.dersId;
            odev.odevKonusu = model.odevKonusu;
            odev.odevAdi = model.odevAdi;
            _db.odevs.Add(odev);
            _db.SaveChanges();
            return Ok("Odev başarılı bir şekilde eklendi");
        }

        [HttpPut]
        public IActionResult Edit(int id ,OdevModel model)
        {
            var values =  _db.odevs.Find(id);
            values.dersId = values.dersId;
            values.odevKonusu = model.odevKonusu;
            values.odevAdi = model.odevAdi;
            _db.odevs.Update(values);
            _db.SaveChanges();
            return Ok("Odev başarılı bir şekilde güncellendi");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var values = _db.odevs.Find(id);
            _db.odevs.Remove(values);
            _db.SaveChanges();
            return Ok("Ödev silindi");
        }

        [HttpPut("GetHomeWork")]
        public async Task<IActionResult> GetHomeWork(string id , int odevId)
        {
            var values = await _kullanici.FindByIdAsync(id);
            values.odevId = odevId;
            var result = await _kullanici.UpdateAsync(values);
            if (result.Succeeded)
            {
                return Ok("Ödev alındı");
            }
            else
            {
                return BadRequest("Bir hata oluştu");
            }
           
        }
    }
}
