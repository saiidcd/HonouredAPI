using Honoured.ArtDisciplines;
using Honoured.Enumerations;
using Honoured.Models;
using Honoured.Placements;
using Honoured.Subscriptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Volo.Abp.Domain.Entities;

namespace Honoured.ArtLovers
{
    public class ArtLover : Entity<long>
    {

        #region Fields
        List<long> _previousArtIds = new List<long>();
        #endregion Fields


        #region Props
        public ArtLoverPersonalInfo Profile { get; set; }

        public ArtLoverStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public List<Placement> Placements { get; set; }

        public int ActivePlacements { get; set; }

        public List<ArtLoverSubscription> Subscriptions { get; set; }

        public DateTime NextPlacementDate { get; set; }

        public List<ArtLoverDiscipline> Categories { get; set; }

        protected string ArtHistory { 
            get => _previousArtIds.JoinAsString(","); 
            set=> _previousArtIds = value.Split(",").Select(i=>long.Parse(i)).ToList();
        }

        [NotMapped]
        public List<long> PreviousArtIds { get => _previousArtIds; set => _previousArtIds = value; }
        #endregion Props
        //TODO save in a NoSql DB a document with the fields
        /*
            ArtLoverId,
            ArtId1 : placementId1
            ArtId2 : placementId2
        */
        //With the idea of being able to look up by art id to determnine if an art has been linked with the artlover before

        #region Methods
        public void AddToArtHistory(long id)
        {
            if(!_previousArtIds.Contains(id)) _previousArtIds.Add(id);
        }

        #region Subscriptioin Operations
        public ArtLoverSubscription AddSubscription(ArtLoverSubscription toAdd)
        {
            throw new NotImplementedException("AddSubscription in ArtLover");
        }

        public void RemoveSubscription(ArtLoverSubscription toRemove)
        {
            throw new NotImplementedException("RemoveSubscription in ArtLover");
        }

        public void UpdateSubscription(ArtLoverSubscription toUpdate)
        {
            throw new NotImplementedException("UpdateSubscription in ArtLover");
        }

        #endregion Subscriptioin Operations

        #endregion Methods
    }
}
