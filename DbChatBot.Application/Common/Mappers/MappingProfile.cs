using AutoMapper;
using DbChatBot.Contracts.Models;
using DbChatBot.Domain.Contracts;

namespace DbChatBot.Application.Common.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ITableSchema, TableSchemaDto>();
        CreateMap<IAiQuery, GeneratedQueryDto>();
    }
}