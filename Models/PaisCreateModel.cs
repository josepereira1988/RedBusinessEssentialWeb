using System.ComponentModel.DataAnnotations;

namespace RedBusinessEssentialWeb.Models
{
    public class PaisCreateModel
    {
        public string Nome { get; set; }
        [Display(Name = "Codigo Siscomex")]
        public int? CdSis { get; set; }
    }
}
