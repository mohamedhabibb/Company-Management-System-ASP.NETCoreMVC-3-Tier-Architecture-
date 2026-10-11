using AutoMapper;
using DAL_Arch.Models;
using MVC_With_PL_Arch.ViewModels;

namespace MVC_With_PL_Arch.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
