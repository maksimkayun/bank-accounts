using AutoMapper;
using BankAccount.DataStorage.MongoModels;
using BankAccount.DTO;
using Account = BankAccount.DataStorage.PostgresModels.Account;
using Transaction = BankAccount.DataStorage.PostgresModels.Transaction;

namespace BankAccount.AutoMapperProfiles;

public class TransactionDtoPostgresProfile : Profile
{
    public TransactionDtoPostgresProfile()
    {
        CreateMap<Transaction, TransactionDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(m => m.Id.ToString()))
            .ForMember(e => e.SenderAccountNumber, opt => opt.MapFrom(m => m.Sender.AccountNumber))
            .ForMember(e => e.RecipientAccountNumber, opt => opt.MapFrom(m => m.Recipient.AccountNumber))
            .ForMember(e => e.Comment, opt => opt.MapFrom(_ => "MapTransaction"))
            .ReverseMap()
            .ForMember(e => e.Id, opt => opt.MapFrom(m => int.Parse(m.Id)))
            .ForMember(e => e.Sender, opt => opt.MapFrom(m => new Account() {AccountNumber = m.SenderAccountNumber.ToString()}))
            .ForMember(e => e.Recipient, opt => opt.MapFrom(m => new Account() {AccountNumber = m.RecipientAccountNumber.ToString()}));
    }
}

public class TransactionDtoMongoProfile : Profile
{
    public TransactionDtoMongoProfile()
    {
        CreateMap<DataStorage.MongoModels.Transaction, TransactionDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(m => m.Id))
            .ForMember(e => e.SenderAccountNumber, opt => opt.MapFrom(m => m.SenderAccountNumber))
            .ForMember(e => e.RecipientAccountNumber, opt => opt.MapFrom(m => m.RecipientAccountNumber))
            .ForMember(e=>e.Comment, opt=>opt.MapFrom(_=> "MapTransaction"))
            .IgnoreAllPropertiesWithAnInaccessibleSetter().IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
    }
}