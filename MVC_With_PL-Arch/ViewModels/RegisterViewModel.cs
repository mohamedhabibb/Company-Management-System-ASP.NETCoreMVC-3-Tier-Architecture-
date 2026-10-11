using System.ComponentModel.DataAnnotations;

namespace MVC_With_PL_Arch.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage ="First name is required")]
        public string FName {get ;set;}


        [Required(ErrorMessage = "Last name is required")]
        public string LName {get ;set;}


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email {get ;set;}


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage ="Password dosen't match")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }   



    }
}      
       