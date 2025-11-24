using AutoMapper;
using Backend.Api.DTOs;
using Backend.Domain.Entities;

namespace Backend.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExpenseType, ExpenseTypeDto>();
            CreateMap<ExpenseTypeCreateDto, ExpenseType>();

            CreateMap<MonetaryFund, MonetaryFundDto>();
            CreateMap<MonetaryFundCreateDto, MonetaryFund>();

            CreateMap<Budget, BudgetDto>();
            CreateMap<BudgetCreateDto, Budget>();
        }
    }
}
