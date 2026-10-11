using DAL_Arch.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace MVC_With_PL_Arch.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; } //PK

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Maxlength is 50")]
        [MinLength(5, ErrorMessage = "Minlength is 5")]
        public string Name { get; set; }

        [Range(22, 35, ErrorMessage = "Age should be 22 to 35")]
        public int? Age { get; set; }

        [RegularExpression("^[1-9]{1,3}-[a-zA-z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must be such as 123-street-city-country")]
        public string Address { get; set; }


        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }
        public IFormFile Image { get; set; }

        public string ImageName {  get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        [InverseProperty("Employees")]
        public Department Department { get; set; }
    }
}
