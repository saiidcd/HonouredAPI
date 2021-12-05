using Honoured.Enumerations;
using System;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtWorks
{
    public class ArtWorkDto : EntityDto<long>
    {
        public long ArtistId { get; set; }

        public string ArtisName { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Story { get; set; }

        public double SalePrice { get; set; }

        public string Dimensions { get; set; }

        public UnitOfMeasurement MeasurementUnit { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateNextAvailable { get; set; }

        public ArtStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public bool IsFileUploaded { get; set; }

        public string Icon64 { get; set; }

        public bool IsHasAdultContent { get; set; }
    }
}
