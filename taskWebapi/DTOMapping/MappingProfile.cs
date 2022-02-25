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
            CreateMap<Employee, Models.Dtos.EmployeeDepartmentDto>().ReverseMap();      
            CreateMap<Employee, EmployeDisplayDto>().ReverseMap();
            CreateMap<EmployeeDepartment, EmployeeDepartment>().ReverseMap(); 
            CreateMap<EmployeeDepartment, EmployeeDepartment>().ReverseMap();

            CreateMap<FindEmployeeDepartment, Models.EmployeeDepartment>().ReverseMap();      
        }
    }
}
