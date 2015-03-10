using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace NorthwindCustomerService.Web.ExtensionMethods
{
    public static class AutoMapperExtension
    {
        public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source)
        {
            return Mapper.Map(source, destination);
        }
    }
}