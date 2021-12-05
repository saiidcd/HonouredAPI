using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.DTOs
{
    public class SubscriberDto : EntityDto<long>
    {
        public string First { get; set; }

        public string Middle { get; set; }

        public string Last { get; set; }

        public DateTime DOB { get; set; }

        public long ArtistId { get; set; }

        public ArtistStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public string ShortDesciption { get; set; }

        public string Bio { get; set; }

        public string Statement { get; set; }

        public string Email { get; set; }

        //public string Icon64 { get { return _icon64; } set { _icon64 = value; GetImage(); } }

        //public ImageSource Image { get => image; internal set => image = value; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public bool IsArtist { get; set; }

        public bool IsArtLover { get; set; }

        public string Icon64 { get; set; }
    }
}
