using System.ComponentModel.DataAnnotations;

namespace RedBusinessEssentialWeb.Models
{
    public class UsuariosCreateModel
    {
        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuario")]
        [StringLength(50, MinimumLength = 1)]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Informe o nome do usuário")]
        [Display(Name = "Nome do usuário")]
        [StringLength(50, MinimumLength = 1)]
        public string Nome { get; set; }
        [Display(Name = "Senha de acesso")]
        [Required(ErrorMessage = "Informe uma senha.")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Display(Name = "Usuario ativo")]
        public bool Ativo { get; set; }
    }
}
