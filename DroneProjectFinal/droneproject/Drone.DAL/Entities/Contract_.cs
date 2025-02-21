namespace Drone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contract_
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contract_()
        {
            ContractDetail = new HashSet<ContractDetail>();
            Feedback = new HashSet<Feedback>();
            PenaltyTicket = new HashSet<PenaltyTicket>();
            ContractDetail1 = new HashSet<ContractDetail>();
        }

        [Key]
        [StringLength(5)]
        public string ContractID { get; set; }

        public DateTime? NgayLap { get; set; }

        public int? TotalValue { get; set; }

        [Required]
        [StringLength(5)]
        public string CustomerID { get; set; }

        [Required]
        [StringLength(5)]
        public string TechnicianTeamID { get; set; }

        [StringLength(5)]
        public string PromotionID { get; set; }

        [StringLength(200)]
        public string Request { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Promotion Promotion { get; set; }

        public virtual TechnicianTeam TechnicianTeam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractDetail> ContractDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedback { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PenaltyTicket> PenaltyTicket { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractDetail> ContractDetail1 { get; set; }
    }
}
