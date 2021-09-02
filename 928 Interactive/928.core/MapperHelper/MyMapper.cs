using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace _928.Core.Wrappers {
    public static class MyMapper //: IMapper
    {

        public static object Map(object source, object destination, Type sourceType, Type destinationType) {
            return Mapper.Map(source, destination, sourceType, destinationType);
        }

        public static object Map(object source, Type sourceType, Type destinationType) {
            return Mapper.Map(source, sourceType, destinationType);
        }

        public static TDestination Map<TDestination>(object source) {
            return Mapper.Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source) {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination) {
            return Mapper.Map<TSource, TDestination>(source, destination);
        }
        public static IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>() {
            return Mapper.CreateMap<TSource, TDestination>();
        }

    }
}
