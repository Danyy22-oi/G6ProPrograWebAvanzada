namespace Proyecto_Final.Models
{
    public class UserRoleEditViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string Departamentos { get; set; }

        // Campos para editar la contraseña
     
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
