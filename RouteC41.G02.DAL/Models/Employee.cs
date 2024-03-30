using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    public class Employee:ModelBase
    {


        public string Name { get; set; }

        public int Age { get; set; }

        public string  Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }


        public string EmailAddress { get; set; }

        public string Phone { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        
        public int? DepartmentId { get; set; } //FK column

        //[InverseProperty(nameof(Models.Department.Employees))]
        //Navigational Property=>one
        public Department Department { get; set; }



        [DisplayName("Gender")]
        public Gender Gneder { get; set; }

        public EmpType EmpType { get; set; }


    }
}
