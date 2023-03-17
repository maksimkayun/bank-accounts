using AutoMapper;

namespace Mapper;

public static class MapUtil<S,D>
{
    public static D Map(S source, Action<IMappingExpression<S, D>> userSetup = null)
    {
        var mapper = MapperFactory<S, D>.CreateMapper(userSetup);
        return mapper.Map<D>(source);
    }
}