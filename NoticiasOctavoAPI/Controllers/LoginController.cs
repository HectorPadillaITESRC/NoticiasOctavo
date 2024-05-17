using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoticiasOctavoAPI.Helpers;
using NoticiasOctavoAPI.Models.DTOs;
using NoticiasOctavoAPI.Models.Entities;
using NoticiasOctavoAPI.Repositories;
using System.Security.Claims;

namespace NoticiasOctavoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IRepository<Usuarios> repository;
        private readonly JwtHelper jwtHelper;

        public LoginController(IRepository<Usuarios> repository, JwtHelper jwtHelper)
        {
            this.repository = repository;
            this.jwtHelper = jwtHelper;
        }

        [HttpPost]
        public IActionResult Authenticate(LoginDTO dto)
        {
            var usuario = repository.GetAll().FirstOrDefault(x => x.NombreUsuario == dto.Usuario && x.Contraseña == dto.Contraseña);

            if (usuario == null)
                return Unauthorized();

            var token = jwtHelper.GetToken(usuario.Nombre,
                usuario.EsAdmin == true ? "Admin" : "Periodista",
                new List<Claim> { new Claim("Id", usuario.Id.ToString())}
                );

            return Ok(token);
        }


    }
}
