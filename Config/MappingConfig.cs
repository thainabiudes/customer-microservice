using AutoMapper;
using Model;
using Data.ValueObjects;

namespace Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CustomerVO, Customer>();
                config.CreateMap<Customer, CustomerVO>();
            });
            return mappingConfig;
        }
    }
}
