using AutoMapper;
using PropiedadesMinimalAPI.Models;

namespace PropiedadesMinimalAPI.Mapper;

public class PropertiesMapper : Profile
{
    public PropertiesMapper()
    {
        CreateMap<Property, PropertyCreateDTO>().ReverseMap();
        CreateMap<Property, PropertyDTO>().ReverseMap();
    }
}
