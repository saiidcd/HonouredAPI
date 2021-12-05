using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Tags
{
    public class TagDto : EntityDto<long>
    {
        public string Value { get; set; }

        public List<string> Related { get; set; }
    }
}
