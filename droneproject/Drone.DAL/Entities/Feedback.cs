namespace Drone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        [StringLength(5)]
        public string FeedbackID { get; set; }

        public short? Rating { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }

        [Required]
        [StringLength(5)]
        public string DroneID { get; set; }

        [Required]
        [StringLength(5)]
        public string ContractID { get; set; }

        public virtual Contract_ Contract_ { get; set; }

        public virtual Drone_ Drone { get; set; }
    }
}
