using Honoured.ArtDisciplines;
using Honoured.Enumerations;
using Honoured.Tags;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtWorks
{
    public class CreateArtWorkDto : EntityDto<long>
    {
        public long ArtistId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Story { get; set; }

        public string ImageFile { get; set; }

        public string DetailsFile { get; set; }

        public decimal Height { get; set; }

        public decimal Width { get; set; }

        public decimal Depth { get; set; }

        public double SalePrice { get; set; }

        public UnitOfMeasurement MeasurementUnit { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Today;

        public DateTime DateNextAvailable { get; set; } = DateTime.Today;

        public ArtStatus Status { get; set; } = ArtStatus.Submitted;

        public DateTime StatusDate { get; set; } = DateTime.Today;

        public ArtDisciplineDTO MainCategory { get; set; }

        public List<TagDto> Tags { get; set; }

        public List<ArtDisciplineDTO> Categories { get; set; }

        public bool IsHasAdultContent { get; set; }
    }
}
