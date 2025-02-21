using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Drone.DAL.Entities
{
    public partial class DroneContextDB : DbContext
    {
        public DroneContextDB()
            : base("name=DroneContextDB")
        {
        }

        public virtual DbSet<Contract_> Contract_s { get; set; }
        public virtual DbSet<ContractDetail> ContractDetails { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Drone_> Drones { get; set; }
        public virtual DbSet<DroneMaintenance> DroneMaintenances { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<PenaltyTicket> PenaltyTickets { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Technician> Technicians { get; set; }
        public virtual DbSet<TechnicianTeam> TechnicianTeams{ get; set; }
        public virtual DbSet<Type_> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User", "dbo");
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID);

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(100);
            modelBuilder.Entity<Contract_>()
                .Property(e => e.ContractID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Contract_>()
                .Property(e => e.CustomerID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Contract_>()
                .Property(e => e.TechnicianTeamID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Contract_>()
                .Property(e => e.PromotionID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Contract_>()
                .HasMany(e => e.ContractDetail)
                .WithRequired(e => e.Contract_)
                .HasForeignKey(e => e.ContractID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract_>()
                .HasMany(e => e.Feedback)
                .WithRequired(e => e.Contract_)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract_>()
                .HasMany(e => e.PenaltyTicket)
                .WithRequired(e => e.Contract_)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract_>()
                .HasMany(e => e.ContractDetail1)
                .WithRequired(e => e.Contract_1)
                .HasForeignKey(e => e.ContractID);

            modelBuilder.Entity<ContractDetail>()
                .Property(e => e.DroneID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ContractDetail>()
                .Property(e => e.ContractID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Contract_)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Drone_>()
                .Property(e => e.DroneID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Drone_>()
                .Property(e => e.TypeID)
                .IsFixedLength();

            modelBuilder.Entity<Drone_>()
                .HasMany(e => e.ContractDetail)
                .WithRequired(e => e.Drone)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Drone_>()
                .HasMany(e => e.DroneMaintenance)
                .WithRequired(e => e.Drone)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Drone_>()
                .HasMany(e => e.Feedback)
                .WithRequired(e => e.Drone)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Drone_>()
                .HasMany(e => e.PenaltyTicket)
                .WithRequired(e => e.Drone)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DroneMaintenance>()
                .Property(e => e.MaintenanceID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DroneMaintenance>()
                .Property(e => e.DroneID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DroneMaintenance>()
                .HasMany(e => e.Technician)
                .WithMany(e => e.DroneMaintenance)
                .Map(m => m.ToTable("CTMaintenance").MapLeftKey("MaintenanceID").MapRightKey("TechnicianID"));

            modelBuilder.Entity<Feedback>()
                .Property(e => e.FeedbackID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.DroneID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.ContractID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PenaltyTicket>()
                .Property(e => e.TicketID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PenaltyTicket>()
                .Property(e => e.DroneID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PenaltyTicket>()
                .Property(e => e.ContractID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Promotion>()
                .Property(e => e.PromotionID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Promotion>()
                .Property(e => e.DiscountRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Technician>()
                .Property(e => e.TechnicianID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Technician>()
                .Property(e => e.TechnicianTeamID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TechnicianTeam>()
                .Property(e => e.TechnicianTeamID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TechnicianTeam>()
                .HasMany(e => e.Contract_)
                .WithRequired(e => e.TechnicianTeam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TechnicianTeam>()
                .HasMany(e => e.Technician)
                .WithRequired(e => e.TechnicianTeam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Type_>()
                .Property(e => e.TypeID)
                .IsFixedLength();
       
        }
    }
}
