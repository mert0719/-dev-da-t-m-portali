using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OdevPortal.Entity;
using OdevPortal.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OdevPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Kullanicilar> _userManager;
        private readonly SignInManager<Kullanicilar> _signInManager;
        private readonly RoleManager<AppRole> _role;

        public AuthController(UserManager<Kullanicilar> userManager, SignInManager<Kullanicilar> signInManager, RoleManager<AppRole> role)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginApiModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Giriş başarısız lütfen tekrar deneyin.");
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return BadRequest("Kullanıcı bulunamadı");
                }
                else
                {

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            };

                        var singInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("odevpapiyehosgel"));

                        var token = new JwtSecurityToken(
                            issuer: "http://localhost",
                            audience: "http://localhost",
                            expires: DateTime.UtcNow.AddHours(10),
                            claims: claims,
                            signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(singInKey, SecurityAlgorithms.HmacSha256)
                            );



                        return Ok(new
                        {
                            jwt = new JwtSecurityTokenHandler().WriteToken(token),
                            id = user.Id,
                            adiSoyadi = user.adiSoyadi,                            
                            email = user.Email,
                            token = true,
                            dersId = user.dersId,
                            role = roles.FirstOrDefault()
                        });

                    }
                    else
                    {
                        return BadRequest("Email veya şifreniz hatalı lütfen tekrar deneyin.");
                    }

                }
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Kayıt işlemi sırasında bir hata gerçekleşti. Lütfen tekrar deneyin");
            }
            else
            {
                var userControl = _userManager.Users.Where(x => x.Email == model.email).FirstOrDefault();
                if (userControl != null)
                {
                    return BadRequest("Bu email ile daha önceden kayıt yapılmış. Lütfen farklı bir email adresi girin.");
                }
                else
                {
                    var user = new Kullanicilar
                    {
                        adiSoyadi = model.adiSoyadi,                       
                        Email = model.email,
                        UserName = Guid.NewGuid().ToString(),                       
                        dersId = model.dersId,
                        numarasi = model.numarasi
                    };

                    var result = await _userManager.CreateAsync(user, model.sifre);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, model.roleName);
                        return Ok("Kaydınız oluşturuldu");
                    }
                    else
                    {
                        return BadRequest("Kayıt esnasında bir hata oluştu");
                    }

                }
            }
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole
                {
                    Name = model.name,
                };
                var result = await _role.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new { message = "Rol eklenemedi" });
                }
            }
            return Ok();
        }




    }
}
