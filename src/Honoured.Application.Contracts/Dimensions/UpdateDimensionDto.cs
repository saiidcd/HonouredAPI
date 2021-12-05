using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Dimensions
{
    public class UpdateDimensionDto: EntityDto<long>
    {

        #region Props
        public string Name { get; set; }

        public decimal MinHeight { get; set; }

        public decimal MinWidth { get; set; }

        public decimal MaxHeight { get; set; }

        public decimal MaxWidth { get; set; }

        public GeneralStatus Status { get; set; }

        public DateTime StatusDate { get; set; }
        #endregion Props
    }
}
