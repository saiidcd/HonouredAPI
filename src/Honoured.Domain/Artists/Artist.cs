using Honoured.Enumerations;
using Honoured.Models;
using JetBrains.Annotations;
using System;
using Volo.Abp;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Honoured.ArtWorks;
using Honoured.Markets;
using Honoured.ArtistSubscriptions;
using System.Linq;
using Honoured.ArtDisciplines;

namespace Honoured.Artists
{
    public class Artist : FullAuditedAggregateRoot<long>
    {

        #region Properties
        public ArtistPersonalInfo PersonalDetails { get; set; }

        public ArtistStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public List<ArtWork> Portfolio { get; set; }

        public ContactPoint DefaultContactPoint { get; set; }

        public string ShortDesciption { get; set; }

        public string Bio { get; set; }

        public string Statement { get; set; }

        public List<Market> SubscribedMarkets { get; set; }

        public List<ArtDiscipline> Disciplines { get; set; }

        public ArtistSubscription ArtLoverSubscription { get; set; }

        public string Key 
        { 
            get 
            {
                return PersonalDetails==null ? "":
                        $"{PersonalDetails.First?.ToLower()};{PersonalDetails.Middle?.ToLower()};{PersonalDetails.Last?.ToLower()};{PersonalDetails.DOB.Date.ToShortDateString()}";
            } 
            set { } 
        }

        #endregion Properties

        #region Public Methods
        public string GetFullName()
        {
            return PersonalDetails.GetFullName();
        }

        public double GetMonthlyCost() 
        {
            return SubscribedMarkets.Count(m => m.Status == GeneralStatus.Active) * ArtLoverSubscription.Tier.Price;
        }
        #endregion Public Methods

        #region Ctors
        private Artist()
        {
            //Needed by the ORM
        }

        internal Artist([NotNull] string first, [NotNull] string middle, string last, [NotNull] DateTime dob)
        {
            PersonalDetails = new ArtistPersonalInfo { 
                                    First = Check.NotNullOrWhiteSpace(first,nameof(first)),
                                    Middle = Check.NotNullOrWhiteSpace(middle, nameof(middle)),
                                    Last = Check.NotNullOrWhiteSpace(last, nameof(last)),
                                    DOB = Check.NotNull(dob,nameof(dob))
                                };
        }
        internal Artist(long id)
        {
            Id = id;
        }
        #endregion Ctors
    }
}
