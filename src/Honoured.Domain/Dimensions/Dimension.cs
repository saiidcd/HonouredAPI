using Honoured.Enumerations;
using System;
using Volo.Abp.Domain.Entities;

namespace Honoured.Dimensions
{
    /// <summary>
    /// This class represents a dimesion. Each Artlover's ArtLoverSubscription
    /// will have a dimension assigned to it that would limit the size
    /// of the piece they can receive.
    /// Each dimension has a minimum/maximum height and width associated with it.
    /// </summary>
    public class Dimension : Entity<long>
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
