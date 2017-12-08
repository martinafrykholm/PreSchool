using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PreSchoolApp.Models.Entities;

namespace PreSchoolApp.Models.ViewModels
{
    public class EditUserVM
    {
       PreSchoolAppContext context;
        public EditUserVM(PreSchoolAppContext context)
        {
            this.context = context;
        }
       
        [Required(ErrorMessage ="Ange ditt förnamn")]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ange ditt efternamn")]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ange ett telefonnummer")]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Ange en mailadress")]
        [Display(Name = "E-mail")]
        public string EmailAdress { get; set; }

        public int ChildID { get; set; }

      

        //Lägger till en kommentar bara för att kunna pusha igen 
    }
}
