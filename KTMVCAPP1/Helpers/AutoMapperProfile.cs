using AutoMapper;
using KTMVCAPP1.Models;

namespace KTMVCAPP1.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }

    }
}
