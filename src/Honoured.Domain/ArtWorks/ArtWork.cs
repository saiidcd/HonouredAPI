using Honoured.ArtDisciplines;
using Honoured.Artists;
using Honoured.CareInstructions;
using Honoured.Dimensions;
using Honoured.Enumerations;
using Honoured.Models;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Honoured.ArtWorks
{
    /// <summary>
    /// This class represents a piece of artwork. The piece will be associated with a number of tags
    /// to make it easier to matc it with a potential artlover.
    /// </summary>
    public class ArtWork : Entity<long>
    {

        #region Props
        public long ArtistId { get; set; }

        public bool IsFileUploaded { get; set; }

        public bool IsHasAdultContent { get; set; }

        public bool IsInsured { get; set; }

        public double SalePrice { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateNextAvailable { get; set; }

        public DateTime StatusDate { get; set; }

        public double Depth { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public UnitOfMeasurement MeasurementUnit { get; set; }

        public string ImageFile { get; set; }

        public string ShortDescription { get; set; }

        public string Story { get; set; }

        public string Title { get; set; }

        public ArtStatus Status { get; set; }

        public ArtDiscipline MainDiscipline { get; set; }

        public Artist Artist { get; set; }

        public List<ArtDiscipline> Categories { get; set; } = new List<ArtDiscipline>();

        public List<CareInstruction> CareInstructions { get; set; }

        public Dimension Dimension { get; set; }

        public List<Tag> Tags { get; set; }
        //TODO how to define insurance?
        //TODO Pallette? what to do about it? what is it exactly? how to define it?

        #endregion Props

        #region Public Methods
        public List<Tag> GetTagsFromStory() {
            throw new NotImplementedException("GetTagsFromStory in ArtWork.cs");
        }
        #endregion Public Methods

    }
}
