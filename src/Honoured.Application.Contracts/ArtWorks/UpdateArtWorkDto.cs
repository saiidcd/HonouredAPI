using Honoured.ArtDisciplines;
using Honoured.Enumerations;
using Honoured.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtWorks
{
    public class UpdateArtWorkDto : EntityDto<long>
    {
        public long ArtistId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Story { get; set; }

        public string ImageFile { get; set; }

        public bool IsFileUploaded { get; set; }

        public string DetailsFile { get; set; }

        public decimal Height { get; set; }

        public decimal Width { get; set; }

        public decimal Depth { get; set; }

        public string Dimensions { get=>$"{Width}x{Height}x{Depth}"; set=>setDimensions(value); }

        public double SalePrice { get; set; }

        private void setDimensions(string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            var entries = value.Split('x');
            
            if (entries.Length < 2 || entries.Length > 3) return;

            Width = decimal.Parse(entries[0]);
            Width = decimal.Parse(entries[1]);
            if (entries.Length > 2)
            {
                Width = decimal.Parse(entries[0]);
            }
        }

        public UnitOfMeasurement MeasurementUnit { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Today;

        public DateTime DateNextAvailable { get; set; } = DateTime.Today;

        public ArtStatus Status { get; set; } = ArtStatus.Submitted;

        public DateTime StatusDate { get; set; } = DateTime.Today;

        public List<TagDto> Tags { get; set; }

        public List<ArtDisciplineDTO> Categories { get; set; }

        public bool IsHasAdultContent { get; set; }
    }
}
