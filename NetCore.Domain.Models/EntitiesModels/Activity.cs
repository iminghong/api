using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCore.Domain.Models.EntitiesModels
{
    [Table("Activity")]
    public class Activity : SqlModelBase
    {
        [Key]
        [Required]
        public virtual Guid Act_ID { get; set; }
        public virtual string Name { get; set; }
        public virtual Nullable<DateTime> Start_Date { get; set; }
        public virtual Nullable<DateTime> End_Date { get; set; }
        public virtual Nullable<bool> IsGoing { get; set; }
    }
}
