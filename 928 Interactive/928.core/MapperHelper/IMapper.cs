using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _928.Core.Wrappers
{
    public interface IMapper
    {
        object Map(object source, object destination, Type sourceType, Type destinationType);
        object Map(object source, Type sourceType, Type destinationType);
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
