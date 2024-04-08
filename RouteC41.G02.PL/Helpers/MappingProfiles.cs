using AutoMapper;
using RouteC41.G02.DAL.Models;
using RouteC41.G02.PL.ViewModels;

namespace RouteC41.G02.PL.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            /*.ForMember(d => d.Name , o => o.MapFrom(s => s.EmpName))*/
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }


    }
}
