namespace Drone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Technician")]
    public partial class Technician
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Technician()
        {
            DroneMaintenance = new HashSet<DroneMaintenance>();
        }

        [StringLength(5)]
        public string TechnicianID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        [StringLength(5)]
        public string TechnicianTeamID { get; set; }
        public string Image { get; set; }
        public virtual TechnicianTeam TechnicianTeam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DroneMaintenance> DroneMaintenance { get; set; }
    }
}
