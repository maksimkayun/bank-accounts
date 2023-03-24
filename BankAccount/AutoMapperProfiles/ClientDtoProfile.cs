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