namespace Drone.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContractDetail")]
    public partial class ContractDetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string DroneID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string ContractID { get; set; }

        public int? Price { get; set; }

        [Column("NgayBatDau ", TypeName = "smalldatetime")]
        public DateTime? NgayBatDau_ { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayKetThuc { get; set; }

        public virtual Contract_ Contract_ { get; set; }

        public virtual Contract_ Contract_1 { get; set; }

        public virtual Drone_ Drone { get; set; }
    }
}
