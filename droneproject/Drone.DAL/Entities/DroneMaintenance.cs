namespace Drone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DroneMaintenance")]
    public partial class DroneMaintenance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DroneMaintenance()
        {
            Technician = new HashSet<Technician>();
        }

        [Key]
        [StringLength(5)]
        public string MaintenanceID { get; set; }

        public DateTime? Date { get; set; }

        public int? Cost { get; set; }

        [Required]
        [StringLength(5)]
        public string DroneID { get; set; }

        public virtual Drone_ Drone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Technician> Technician { get; set; }
    }
}
