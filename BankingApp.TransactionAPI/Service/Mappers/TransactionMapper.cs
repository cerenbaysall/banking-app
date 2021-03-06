using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BankingApp.TransactionAPI.Domain.Models;
using BankingApp.TransactionAPI.Domain.Dto;

namespace BankingApp.TransactionAPI.Service.Mappers
{
    public class TransactionMapper : ITransactionMapper
    { 
        private readonly IMapper _mapper;

        public TransactionMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Models.Transaction, Domain.Dto.TransactionDto>();
                cfg.CreateMap<Domain.Models.Transaction, Domain.Dto.TransactionDto>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.AccountId, opt => opt.MapFrom(src => src.AccountId));
            });

            _mapper = config.CreateMapper();
        }

        public TransactionDto MapTransactionDto(Domain.Models.Transaction transaction)
        {
            return _mapper.Map<Domain.Models.Transaction, Domain.Dto.TransactionDto>(transaction);
        }
    }
}
