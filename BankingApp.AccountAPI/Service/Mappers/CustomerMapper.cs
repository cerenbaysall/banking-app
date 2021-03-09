using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BankingApp.AccountAPI.Domain.Models;
using BankingApp.AccountAPI.Domain.Dto;

namespace BankingApp.AccountAPI.Service.Mappers
{
    public class CustomerMapper : ICustomerMapper
    { 
        private readonly IMapper _mapper;

        public CustomerMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Models.Customer, Domain.Dto.CustomerDto>();
                cfg.CreateMap<Domain.Models.Customer, Domain.Dto.CustomerDto>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dst => dst.Surname, opt => opt.MapFrom(src => src.Surname))
                    .ForMember(dst => dst.CustomerNo, opt => opt.MapFrom(src => src.CustomerNo));
            });

            _mapper = config.CreateMapper();
        }

        public CustomerDto MapCustomerDto(Domain.Models.Customer customer)
        {
            return _mapper.Map<Domain.Models.Customer, Domain.Dto.CustomerDto>(customer);
        }
    }
}
