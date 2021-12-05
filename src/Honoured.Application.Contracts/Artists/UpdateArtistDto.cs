using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Artists
{
    public class UpdateArtistDto : EntityDto<long>
    {
        public string First { get; set; }

        public string Middle { get; set; }

        public string Last { get; set; }

        public DateTime DOB { get; set; }

        public long ArtistId { get; set; }

        public ArtistStatus Status { get; set; }

        public string ImageFile { get; set; }

        public bool IsIconUploaded { get; set; }
    }
}
