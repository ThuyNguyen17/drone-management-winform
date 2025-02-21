namespace Drone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Drone")]
    public partial class Drone_
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Drone_()
        {
            ContractDetail = new HashSet<ContractDetail>();
            DroneMaintenance = new HashSet<DroneMaintenance>();
            Feedback = new HashSet<Feedback>();
            PenaltyTicket = new HashSet<PenaltyTicket>();
        }

        [StringLength(5)]
        [Key]
        public string DroneID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(10)]
        public string TypeID { get; set; }

        public int? RentalPrice { get; set; }

        public int? PurchasePrice { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractDetail> ContractDetail { get; set; }

        public virtual Type_ Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DroneMaintenance> DroneMaintenance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedback { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PenaltyTicket> PenaltyTicket { get; set; }
    }
}
