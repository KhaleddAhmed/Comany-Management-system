using RouteC41.G02.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace RouteC41.G02.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max length of name is 50 chars")]
        [MinLength(5, ErrorMessage = "Min length of name is 5 chars")]
        public string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }
        //[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage ="Address must be like 123 Street-City-Country")]
        public string Address { get; set; }
        public EmpType EmployeeType { get; set; }
        [DisplayName("Gender")]
        public Gender Gneder { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "IsActive")]
        public bool isActive;
        [EmailAddress]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Of Data")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        //ForeginKey
        public int? DepartmentId { get; set; }

        //Navigational property
        //[InverseProperty(nameof(Models.Department.Employees))]
        public Department Department { get; set; }

        public IFormFile Image  { get; set; }
        public string ImageName { get; set; }
    }
}
