using AutoMapper;
using BankAccount.DataStorage.MongoModels;
using BankAccount.DTO;

namespace BankAccount.AutoMapperProfiles;

public class AccountDtoMongoProfile : Profile
{
    public AccountDtoMongoProfile()
    {
        CreateMap<Account, AccountDto>()
            .ForMember(e => e.OwnerId, opt => opt.MapFrom(m => m.Owner))
            .ForMember(e => e.AccountNumber, opt => opt.MapFrom(m => m.AccountNumber.ToString()))
            .ForMember(e=>e.TransactionNumbers, opt=>opt.MapFrom(m=>m.TransactionNumbers))
            .ReverseMap()
            .ForMember(e => e.Owner, opt => opt.MapFrom(m => m.OwnerId))
            .ForMember(e => e.AccountNumber, opt => opt.MapFrom(m => int.Parse(m.AccountNumber)));
    }
}

public class AccountDtoPostgresProfile : Profile
{
    public AccountDtoPostgresProfile()
    {
        CreateMap<DataStorage.PostgresModels.Account, AccountDto>()
            .ForMember(e => e.OwnerId, opt => opt.MapFrom(m => m.Owner))
            .ForMember(e=>e.TransactionNumbers, opt=>opt.MapFrom(m=>GetTransactionNumbersByAccount(m)))
            .ReverseMap()
            .ForMember(e => e.Owner, opt => opt.MapFrom(m => m.OwnerId));
    }

    private List<int> GetTransactionNumbersByAccount(DataStorage.PostgresModels.Account acc) =>
        acc.IcomingTransactions.Select(e => e.Id).Concat(acc.OutgoingTransactions.Select(e => e.Id)).ToList();
}