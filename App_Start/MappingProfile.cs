using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Models;
using Vidly.Dtos;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(customer => customer.Id, opt => opt.Ignore());
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>().ForMember(movie => movie.Id, opt => opt.Ignore());
            Mapper.CreateMap<Genre, GenreDto>();
            
            // Conditional 
            // Mapper.CreateMap<CustomerDto, Customer>()
            //       .ForMember(cust => cust.Id, opt => opt.Condition(custDTO => custDTO.Id > 0));

        }

    }
}