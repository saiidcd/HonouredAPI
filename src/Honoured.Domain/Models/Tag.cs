using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Volo.Abp.Domain.Entities;

namespace Honoured.Models
{
    public class Tag : Entity<long>
    {
        public string Value { get; set; }

        public string RelatedString { 
            get => Related.JoinAsString(";");
            set => Related = new List<string>(value.Split(";").ToList());
        }

        [NotMapped]
        public List<string> Related { get; set; }
    }
}
