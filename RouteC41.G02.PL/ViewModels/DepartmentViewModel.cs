﻿using RouteC41.G02.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace RouteC41.G02.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "code is reuqired here !!")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Date of creation")]
        public DateTime DateOfCreation { get; set; }
        //Navigational property
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();


    }
}
