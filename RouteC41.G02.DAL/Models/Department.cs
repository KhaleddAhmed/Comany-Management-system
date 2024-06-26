﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteC41.G02.DAL.Models
{
    public class Department:ModelBase
    {
       
        
        public string Code { get; set; }
       
        public string Name { get; set; }

        [Display(Name="Date Of Creation")]
        public DateTime DateOfCreation { get; set; }


        //Navigational Property =>Many
        public ICollection<Employee> Employees { get; set; }=new HashSet<Employee>();
    }
}
