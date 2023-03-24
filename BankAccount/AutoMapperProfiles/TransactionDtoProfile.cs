using AutoMapper;
using BankAccount.DataStorage.PostgresModels;
using BankAccount.DTO;

namespace BankAccount.AutoMapperProfiles;

public class TransactionDtoPostgresProfile : Profile
{
    public TransactionDtoPostgresProfile()
    {
        CreateMap<Transaction, TransactionDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(m => m.Id.ToString()))
            .ForMember(e => e.SenderAccountId, opt => opt.MapFrom(m => m.Sender.Id))
            .ForMember(e => e.RecipientAccountId, opt => opt.MapFrom(m => m.Recipient.Id))
            .ForMember(e => e.Comment, opt => opt.MapFrom(_ => "MakeTransaction"))
            .ReverseMap()
            .ForMember(e => e.Id, opt => opt.MapFrom(m => int.Parse(m.Id)))
            .ForMember(e => e.Sender, opt => opt.MapFrom(m => new Account() {Id = int.Parse(m.SenderAccountId)}))
            .ForMember(e => e.Recipient, opt => opt.MapFrom(m => new Account() {Id = int.Parse(m.RecipientAccountId)}));
    }
}