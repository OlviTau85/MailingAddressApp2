using AutoMapper;
using MailAdsApp.BLL.DTO;
using MailAdsApp.BLL.Infrastructure;
using MailAdsApp.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailAdsApp.WEB.App_Start
{
    /// <summary>
    /// Mapping configuration
    /// </summary>
    public static class AutoMapperConfig
    {
        public static MapperConfiguration MapperConfiguration;
        /// <summary>
        /// Creation of map dependencies
        /// </summary>
        public static void RegisterMappings()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MailAddressDTO, MailAddress>();
            });
        }
    }
}