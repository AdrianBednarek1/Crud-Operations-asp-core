using System.ComponentModel.DataAnnotations;

namespace MVC.project.ViewModels.MakeViewModels
{
    public class MakeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abrv { get; set; }
    }
}
