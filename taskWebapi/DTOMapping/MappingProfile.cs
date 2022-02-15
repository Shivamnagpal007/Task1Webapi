using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Models;
using taskWebapi.Models.Dtos;

namespace taskWebapi.DTOMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, empdepdto>().ReverseMap();      
            CreateMap<Employee, EmployeDisplayDto>().ReverseMap();
            CreateMap<empdepdto, EmployeeDepartment>().ReverseMap(); 
            CreateMap<EmployeeDepartment, empdepdto>().ReverseMap();

            CreateMap<findempdempdto, EmployeeDepartment>().ReverseMap();      
        }
    }
}
