using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoticiasOctavoAPI.Models.DTOs;
using NoticiasOctavoAPI.Models.Entities;
using NoticiasOctavoAPI.Models.Validators;
using NoticiasOctavoAPI.Repositories;

namespace NoticiasOctavoAPI.Controllers
{

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodistasController : ControllerBase
    {

        private IRepository<Usuarios> usuariosRepository;
        private IMapper mapper;
        private PeriodistaValidator validator;

        public PeriodistasController(IRepository<Usuarios> usuariosRepository, IMapper mapper)
        {
            this.usuariosRepository = usuariosRepository;
            this.mapper = mapper;
            validator = new(usuariosRepository);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var datos = usuariosRepository.GetAll()
                .Where(x => x.EsAdmin == false).OrderBy(x => x.Nombre)
                .Select(x => mapper.Map<PeriodistaDTO>(x));


            return Ok(datos);
        }


        [HttpPost]
        public IActionResult Agregar(Periodista2DTO periodista)
        {

            var result = validator.Validate(periodista);

            if (result.IsValid)
            {
                var user = mapper.Map<Usuarios>(periodista);
                usuariosRepository.Insert(user);
                return Ok(mapper.Map<PeriodistaDTO>(user));
            }
            else
            {
                return BadRequest(result.Errors.Select(x => x.ErrorMessage));
            }
        }

        [HttpPut]
        public IActionResult Editar(Periodista2DTO periodista)
        {
            var result = validator.Validate(periodista);

            if (result.IsValid)
            {

                var p = usuariosRepository.Get(periodista.Id);

                if (p == null)
                {
                    return NotFound();
                }

                var newuser = mapper.Map<Usuarios>(p);
                usuariosRepository.Context.Attach(newuser);
                usuariosRepository.Context.Entry(newuser).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                usuariosRepository.Update(newuser);
                



                //p.Nombre= periodista.Nombre;
                //p.NombreUsuario = periodista.NombreUsuario;

                //usuariosRepository.Update(p);
                //return Ok(mapper.Map<PeriodistaDTO>(p));
            }
            else
            {
                return BadRequest(result.Errors.Select(x => x.ErrorMessage));
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var periodista = usuariosRepository.Get(id);
            if (periodista == null)
            {
                return NotFound();
            }

            usuariosRepository.Delete(periodista);
            return Ok();
        }
    }
}
