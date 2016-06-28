﻿using AutoMapper;
using MailAdsApp.BLL.DTO;
using MailAdsApp.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailAdsApp.WEB.App_Start
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration MapperConfiguration;
        public static void RegisterMappings()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MailAddressDTO, MailAddressViewModel>();
            });
        }
    }
}