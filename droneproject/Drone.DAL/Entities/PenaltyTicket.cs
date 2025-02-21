namespace Drone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PenaltyTicket")]
    public partial class PenaltyTicket
    {
        [Key]
        [StringLength(5)]
        public string TicketID { get; set; }

        public int? PenaltyAmount { get; set; }

        [StringLength(100)]
        public string Reason_ { get; set; }

        [Required]
        [StringLength(5)]
        public string DroneID { get; set; }

        [Required]
        [StringLength(5)]
        public string ContractID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayLap { get; set; }

        public virtual Contract_ Contract_ { get; set; }
        
        public virtual Drone_ Drone { get; set; }
    }
}
