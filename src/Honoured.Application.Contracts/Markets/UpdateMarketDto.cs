using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using UtilityLibrary.Models;
using Volo.Abp.Application.Dtos;

namespace Honoured.Markets
{
    public class UpdateMarketDto : EntityDto<long>
    {
        #region Props
        public GpsArea Area { get; set; }

        public string Name { get; set; }

        public GeneralStatus Status { get; set; }

        public string URL { get; set; }


        #endregion Props
    }
}
