using AutoMapper;
using BankingSystem.API.Dtos;
using BankingSystem.API.Entities;

namespace BankingSystem.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserReadDto>(); 
            CreateMap<UserCreateDto, User>();
            CreateMap<Account, AccountReadDto>();
            CreateMap<AccountCreateDto, Account>();
        }
    }
}
