﻿using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopServer.Business.Ultis
{
    public class AutoMapperUtils
    {
        public static IMapper GetMapper<TSource, TDestination>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<TSource, TDestination>(MemberList.None);
            });

            IMapper mapper = new Mapper(config);
            return mapper;
        }
        private static IMapper GetMapper<TSource,TDestination>(string idString)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
                cfg.CreateMap<TSource, TDestination>(MemberList.None).ForSourceMember(idString, s => s.DoNotValidate()).ForMember(idString, s => s.Ignore());
            });
            IMapper mapper = new Mapper(config);
            return mapper;
        }
        #region Single
        public static TDestination AutoMap<TSource, TDestination>(TSource source)
        {
            var mapper = GetMapper<TSource, TDestination>();
            TDestination dest = mapper.Map<TDestination>(source);
            return dest;
        }

        public static TDestination AutoMap<TSource, TDestination>(TSource source, string idString)
        {
            var mapper = GetMapper<TSource, TDestination>(idString);
            TDestination dest = mapper.Map<TDestination>(source);
            return dest;
        }
        public static TDestination AutoMap<TSource, TDestination>(TSource source, TDestination dest)
        {
            var mapper = GetMapper<TSource, TDestination>();
            dest = mapper.Map(source, dest);
            return dest;
        }

        public static TDestination AutoMap<TSource, TDestination>(TSource source, TDestination dest, string idString)
        {
            var mapper = GetMapper<TSource, TDestination>(idString);
            dest = mapper.Map(source, dest);
            return dest;
        }
        #endregion
        #region List
        public static List<TDestination> AutoMap<TSource, TDestination>(List<TSource> source)
        {
            var mapper = GetMapper<TSource, TDestination>();
            List<TDestination> dest = mapper.Map<List<TDestination>>(source);
            return dest;
        }

        public static List<TDestination> AutoMap<TSource, TDestination>(List<TSource> source, string idString)
        {
            var mapper = GetMapper<TSource, TDestination>(idString);
            List<TDestination> dest = mapper.Map<List<TDestination>>(source);
            return dest;
        }
        public static List<TDestination> AutoMap<TSource, TDestination>(List<TSource> source, List<TDestination> dest)
        {
            var mapper = GetMapper<TSource, TDestination>();
            dest = mapper.Map(source, dest);
            return dest;
        }

        public static List<TDestination> AutoMap<TSource, TDestination>(List<TSource> source, List<TDestination> dest, string idString)
        {
            var mapper = GetMapper<TSource, TDestination>(idString);
            dest = mapper.Map(source, dest);
            return dest;
        }
        #endregion
    }
}
