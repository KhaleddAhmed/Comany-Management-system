using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RouteC41.G02.DAL.Models
{
    
    public enum Gender
    {
        [EnumMember(Value ="Male")]
        Male=1,
        [EnumMember(Value = "Female")]

        Female = 2
    }
    public enum EmpType
    {
        FullTime=1,
        PartTime=2
    }
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage ="Max Length OF Name is 50 Chars")]
        [MinLength(50, ErrorMessage = "Max Length OF Name is 50 Chars")]

        public string Name { get; set; }

        [Range(22,30)]
        public int Age { get; set; }


        [RegularExpression(@"^[0-9]{1,3}-[a-za-Z]{5,10}-[a-za-Z]{4-10}-[a-za-Z]{5,10}", ErrorMessage = "  Address Must Be Like 123-Street-City-Country")]
        public string  Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string Adrress { get; set; }

        [Display(Name ="Phone Number")]
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Hiring Of Data")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        public Gender Gneder { get; set; }

        public EmpType EmpType { get; set; }


    }
}
