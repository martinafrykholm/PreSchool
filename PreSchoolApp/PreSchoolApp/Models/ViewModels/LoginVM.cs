using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreSchoolApp.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Ange giltigt användarnamn")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Ange giltigt lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
