using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SQLLibrary_new;

namespace PreSchoolApp.Models.ViewModels
{
    public class CreateUserVM
    {
        [Required(ErrorMessage ="Ange ett användarnamn")]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ange ett lösenord")]
        [Display(Name ="Lösenord")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Display(Name = "Kod")]
        public string ChildCode { get; set; }
    }
}
