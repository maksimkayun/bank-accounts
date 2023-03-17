using AutoMapper;

namespace Mapper;

public static class MapperFactory<S,D> 
{
    internal static AutoMapper.Mapper CreateMapper(Action<IMappingExpression<S,D>> userSetup = null)
    {
        var config = new MapperConfiguration(cfg =>
        {
            var mappingExpr = cfg.CreateMap<S, D>();
            if(userSetup != null)
                userSetup(mappingExpr);
        });

        return new AutoMapper.Mapper(config);
    }
}