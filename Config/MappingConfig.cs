using AutoMapper;
using Custumers.API.Data.ValueObjects;
using Custumers.API.Model;

namespace Custumers.API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<CustumerVO, Custumer>();
                config.CreateMap<Custumer, CustumerVO>();
            });
            return mappingConfig;
        }
    }
}
