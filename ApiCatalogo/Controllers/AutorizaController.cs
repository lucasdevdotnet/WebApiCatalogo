using ApiCatalogo.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers
{
    [Produces("application/json")] // definindo o formato json
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly IConfiguration _configuration;
        public AutorizaController(UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signManager,
             IConfiguration configuration)
        {
            this._userManager = userManager;                                      
            this._signManager = signManager;
            this._configuration = configuration;
        }
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController:: Acessado em : " +
           DateTime.Now.ToLongDateString();
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody]UsuarioDto usuarioDto)
        {
            var user = new IdentityUser
            {
                UserName = usuarioDto.Email,
                Email = usuarioDto.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, usuarioDto.Passwprd);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _signManager.SignInAsync(user, false);
            return Ok(GeraToken(usuarioDto));
        }
        private UsuarioToken GeraToken(UsuarioDto usuarioInfo)
        {
            #region Criando pacote para geração do token
            var claim = new[]
            {
                   new Claim(JwtRegisteredClaimNames.UniqueName, usuarioInfo.Email),
                   new Claim("meuPer", "pipos"),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };
            #endregion

            //gera ua chave com base no algoritimo simetrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // gera assinatura digital do token o algoritimo Hmac e chave privada
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var exipiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));
            JwtSecurityToken token = new JwtSecurityToken(
                 issuer: _configuration["TokenConfiguration:Issuer"],
               audience: _configuration["TokenConfiguration:Audience"],
                claims: claim,
                expires: exipiration,
                signingCredentials: credenciais);

            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = exipiration,
                Message = "Token"
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]UsuarioDto usuariInfo)
        { 
            var result = await _signManager.PasswordSignInAsync(usuariInfo.Email, usuariInfo.Passwprd, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok(GeraToken(usuariInfo));
            }
            ModelState.AddModelError(string.Empty, "Login Inválido......");
            return BadRequest(ModelState);
        }
    }
}