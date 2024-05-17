using NoticiasOctavoAPI.Models.Entities;

namespace NoticiasOctavoAPI.Models.DTOs
{
    public class PeriodistaDTO
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Nombre { get; set; } = null!;

    }

    public class Periodista2DTO : PeriodistaDTO
    {
        public string Contraseña { get; set; } = null!;
    }
}
