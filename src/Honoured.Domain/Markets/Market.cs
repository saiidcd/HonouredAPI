using Honoured.Artists;
using Honoured.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using UtilityLibrary.Models;
using Volo.Abp.Domain.Entities;

namespace Honoured.Markets
{
    public class Market : Entity<long>
    {

        #region Fields
        private List<int> _countryIds = new List<int>();
        #endregion Fields

        #region Props
        [NotMapped]
        public GpsArea Area { get; set; } = new GpsArea();

        public string Points {
            get=> Area.Points.Select(p=> $"{p.X}:{p.Y}").JoinAsString(";");
            set=>AdjustPoints(value);
        }

        public string Name { get; set; }

        public GeneralStatus Status { get; set; }

        public string CountryIds { 
            get=>_countryIds.JoinAsString(","); 
            set=> _countryIds = string.IsNullOrEmpty(value) ? new List<int>() :  value.Split(",").Select(i => int.Parse(i)).ToList();
        }

        public List<Artist> SubscribedArtists { get; set; }
        #endregion Props


        #region PrivateMethods
        private void AdjustPoints(string list)
        {
            var points = list.Split(";");
            Area.Points = new List<PointF>();
            foreach (var point in points)
            {
                var coords = point.Split(":");
                Area.Points.Add(new PointF(float.Parse(coords[0]), float.Parse(coords[1])));
            }
        }
        #endregion PrivateMethods
    }
}
