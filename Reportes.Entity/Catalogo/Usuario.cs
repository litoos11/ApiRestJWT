namespace Reportes.Entity
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Login { get; set; }
        public string Contrasenia { get; set; }

        public string Email { get; set; }

        public Usuario()
        {

        }

    }
}