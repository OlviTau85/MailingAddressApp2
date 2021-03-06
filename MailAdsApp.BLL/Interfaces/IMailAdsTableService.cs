﻿using MailAdsApp.BLL.DTO;
using MailAdsApp.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAdsApp.BLL.Interfaces
{
    /// <summary>
    /// Interface for project services
    /// </summary>
    public interface IMailAdsTableService
    {
        FilterInfo Filter { get; set; }

        IEnumerable<MailAddressDTO> GetMailAddresses();

        void Dispose();

    }
}
