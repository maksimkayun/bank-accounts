using AutoMapper;
using BankAccount.DTO;
using DbContext.DataStorage.MongoModels;

namespace BankAccount.AutoMapperProfiles;

public class AccountDtoMongoProfile : Profile
{
    public AccountDtoMongoProfile()
    {
        CreateMap<Account, AccountDto>()
            .ForMember(e => e.OwnerId, opt => opt.MapFrom(m => m.Owner))
            .ForMember(e => e.AccountNumber, opt => opt.MapFrom(m => m.AccountNumber.ToString()))
            .ReverseMap()
            .ForMember(e => e.Owner, opt => opt.MapFrom(m => m.OwnerId))
            .ForMember(e => e.AccountNumber, opt => opt.MapFrom(m => int.Parse(m.AccountNumber)));
    }
}

public class AccountDtoPostgresProfile : Profile
{
    public AccountDtoPostgresProfile()
    {
        CreateMap<DbContext.DataStorage.PostgresModels.Account, AccountDto>()
            .ForMember(e => e.OwnerId, opt => opt.MapFrom(m => m.Owner))
            .ReverseMap()
            .ForMember(e => e.Owner, opt => opt.MapFrom(m => m.OwnerId));
    }
}