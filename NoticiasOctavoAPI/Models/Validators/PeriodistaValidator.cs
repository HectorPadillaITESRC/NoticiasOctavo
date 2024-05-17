using FluentValidation;
using NoticiasOctavoAPI.Models.DTOs;
using NoticiasOctavoAPI.Models.Entities;
using NoticiasOctavoAPI.Repositories;

namespace NoticiasOctavoAPI.Models.Validators
{
    public class PeriodistaValidator:AbstractValidator<Periodista2DTO>
    {
        private IRepository<Usuarios> repository;

        public PeriodistaValidator(IRepository<Usuarios> repository)
        {
            this.repository = repository;

            RuleFor(x => x.Nombre).NotEmpty()
                .WithMessage("Debe escribir el nombre real del periodista");

            RuleFor(x => x.NombreUsuario).NotEmpty()
                .WithMessage("Debe escribir el nombre de usuario del periodista");

            RuleFor(x => x.Contraseña)
                .NotEmpty().WithMessage("Debe escribir la contraseña del periodista")
                .MinimumLength(5).WithMessage("La contraseña debe tener al menos 5 caracteres.")
                .Matches("[A-Z]").WithMessage("La contraseña debe tener al menos una letra mayúscula.");


            RuleFor(x => x).Must(NoExiste)
                .WithMessage("Ya existe un periodista con el mismo nombre de usuario");

        }


        private bool NoExiste(Periodista2DTO dto)
        {
            return !repository.GetAll().Any(x=> x.NombreUsuario==dto.NombreUsuario 
            && x.Id!=dto.Id);
        }

        
    }
}
