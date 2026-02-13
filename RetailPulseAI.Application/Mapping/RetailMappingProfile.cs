using AutoMapper;
using RetailPulseAI.Application.DTOs;
using RetailPulseAI.Domain.Entities;

namespace RetailPulseAI.Application.Mapping;

public sealed class RetailMappingProfile : Profile
{
    public RetailMappingProfile()
    {
        CreateMap<RetailSale, SalesTrendPointDto>();
    }
}
