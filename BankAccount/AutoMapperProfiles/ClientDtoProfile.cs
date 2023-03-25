using AutoMapper;
using BankAccount.DataStorage.MongoModels;
using BankAccount.DTO;

namespace BankAccount.AutoMapperProfiles;

public class ClientDtoMongoProfile : Profile
{
    public ClientDtoMongoProfile()
    {
        CreateMap<Client, ClientDto>();
    }
}

public class ClientDtoPostgresProfile : Profile
{
    public ClientDtoPostgresProfile()
    {
        CreateMap<DataStorage.PostgresModels.Client, ClientDto>().ForMember(e => e.AccountIds,
            opt => opt.MapFrom(m => m.Accounts.Select(i => i.Id.ToString()).ToList()));
    }
}