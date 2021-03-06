using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BankingApp.AccountAPI.Domain.Models;
using BankingApp.AccountAPI.Domain.Dto;

namespace BankingApp.AccountAPI.Service.Mappers
{
    public class AccountMapper : IAccountMapper
    { 
        private readonly IMapper _mapper;

        public AccountMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Models.Account, Domain.Dto.AccountDto>();
                cfg.CreateMap<Domain.Models.Account, Domain.Dto.AccountDto>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                    .ForMember(dst => dst.InitialCredit, opt => opt.MapFrom(src => src.InitialCredit));
            });

            _mapper = config.CreateMapper();
        }

        public AccountDto MapAccountDto(Domain.Models.Account account)
        {
            return _mapper.Map<Domain.Models.Account, Domain.Dto.AccountDto>(account);
        }
    }
}
