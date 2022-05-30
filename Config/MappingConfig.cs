using AutoMapper;
using Customers.API.Data.ValueObjects;
using Customers.API.Model;

namespace Customers.API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<CustomerVO, Customer>();
                config.CreateMap<Customer, CustomerVO>();
            });
            return mappingConfig;
        }
    }
}
