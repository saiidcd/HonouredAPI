using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Artists
{
    public class UpdateArtistProfileDto : EntityDto<long>
    {
        public long ArtistId { get; set; }

        public string Name { get; set; }
        public string ShortDesciption { get; set; }

        public string Bio { get; set; }

        public string Statement { get; set; }
    }
}
