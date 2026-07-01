namespace SIGEBI.Application.DTOs
{
    // DTO de usuario
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public int RolUsuarioId { get; set; }
        public int TipoUsuarioId { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}
