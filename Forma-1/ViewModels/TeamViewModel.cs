using Forma_1.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Forma_1.ViewModels
{
    public class TeamViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name has not added.")]
        [MinLength(1)]
        public string Name { get; set; }

        [Display(Name = "Founded")]
        [MinValue(1950)]
        [Required(ErrorMessage = "Founded has not added.")]
        public int Founded { get; set; }

        [Display(Name = "Wins")]
        [Required(ErrorMessage = "Wins has not added.")]
        public int Wins { get; set; }

        [Display(Name = "Paid")]
        [Required(ErrorMessage = "Paid has not added.")]
        public bool IsPaid { get; set; }
    }
}