using Honoured.Enumerations;
using Honoured.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Deliveries
{
    public class Delivery : Entity<long>
    {
        public long NewArtWorkId { get; set; }

        public string NewArtworArtistName { get; set; }

        public string NewArtworkTitle { get; set; }

        public long OldArtWorkId { get; set; }

        public string OldArtworArtistName { get; set; }

        public string OldArtworkTitle { get; set; }

        public long ArtLoverId { get; set; }

        public string ArtLoverName { get; set; }

        public Address Address { get; set; }

        public string Phone { get; set; }

        public DeliveryStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public DeliveryType Type { get; set; }
    }
}
