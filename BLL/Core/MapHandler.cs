using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Text.Json.Serialization;
using DataModels.Sections.External.Booking.Room;
using Microsoft.Extensions.Configuration;
using DataModels.Common;
using BLL.Common;
using System.Data;

namespace BLL.Core
{
    public static class MapHandler
    {
        /// <summary>
        /// Mapping method that handle the data mapping between two generic Source-Target models by specifying 
        /// the source model class namespace path + name through the Source param and 
        /// the target model class namespace path + name through the Target param
        /// </summary>
        /// <param name="data">Source Data</param>
        /// <param name="Source">Source Model</param>
        /// <param name="Target">Target Model</param>
        /// <returns>Target Data Mapped</returns>
        public static Object Map(Object data, string Source, string Target)
        {
            var objSource = Activator.CreateInstance("DataModels", Source).Unwrap();
            var objTarget = Activator.CreateInstance("DataModels", Target).Unwrap();

            Type objSourceType = objSource.GetType();
            Type objTargetType = objTarget.GetType();
            
            // Assign data to the source model instance
            objSource = data;

            PropertyInfo[] objSourceProperties = objSourceType.GetProperties();
            PropertyInfo[] objTargetProperties = objTargetType.GetProperties();

            foreach (var sourceProperty in objSourceProperties)
            {
                var targetProperty = objTargetProperties.Where(a => a.Name == sourceProperty.Name 
                                                                && a.PropertyType == sourceProperty.PropertyType 
                                                                && a.CanWrite);
                if (targetProperty != null && targetProperty.Any())
                {
                    targetProperty = targetProperty.Where(a => a.PropertyType == sourceProperty.PropertyType);
                    if (targetProperty != null && targetProperty.Any())
                    {
                        targetProperty = targetProperty.Where(a => a.CanWrite);
                        if (targetProperty != null && targetProperty.Any())
                        {
                            object value = sourceProperty.GetValue(objSource);
                            var targetPropertyValue = targetProperty.First().GetValue(objTarget, null);
                            targetProperty.First().SetValue(objTarget, value, null);
                        }
                        else
                        {
                            throw new InvalidCastException(AppSettingsHelper.Setting("Errors").GetSection("Messages").GetValue<string>("Internal_Server_Error_Invalid_Mapping_ReadOnly"));
                        }
                    }
                    else
                    {
                        throw new InvalidDataException(AppSettingsHelper.Setting("Errors").GetSection("Messages").GetValue<string>("Internal_Server_Error_Invalid_Mapping_PropertyType"));
                    }
                }
            }

            return objTarget;
        }

        //internal static TDestination Map<TSource, TDestination>(TSource source, TDestination Target) 
        //                                                        where TSource : class
        //                                                        where TDestination : class
        //{
            
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<TSource, TDestination>();
        //    });

        //    config.AssertConfigurationIsValid();

        //    IMapper mapper = config.CreateMapper();
        //    mapper.Map(source, Target);

                
        //    return Target;
        //}
    }
}
