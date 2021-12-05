using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.DTOs
{
    public class FileUploadDto : EntityDto<long>
    {
        public StreamContent Content { get; set; }

        public string Filename { get; set; }

        public long ArtWorkId { get; set; }
    }
}
