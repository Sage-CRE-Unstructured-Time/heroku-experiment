using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Sage.MeteoriteLandingService.Models
{
    public partial class datasetsContext : DbContext
    {
        public datasetsContext()
        {
        }

        public datasetsContext(DbContextOptions<datasetsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActiveTrademarkRegistrations> ActiveTrademarkRegistrations { get; set; }
        public virtual DbSet<MeteoriteLandings> MeteoriteLandings { get; set; }
        public virtual DbSet<SalariesOfStateAgencies> SalariesOfStateAgencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp")
                .HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<ActiveTrademarkRegistrations>(entity =>
            {
                entity.ToTable("active_trademark_registrations");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Address1).HasColumnName("address_1");

                entity.Property(e => e.Address2).HasColumnName("address_2");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.CorrespondentName).HasColumnName("correspondent_name");

                entity.Property(e => e.ImageLink).HasColumnName("image_link");

                entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");

                entity.Property(e => e.RegistrationNumber).HasColumnName("registration_number");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.TrademarkDescription).HasColumnName("trademark_description");

                entity.Property(e => e.Zip).HasColumnName("zip");
            });

            modelBuilder.Entity<MeteoriteLandings>(entity =>
            {
                entity.ToTable("meteorite_landings");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Fall).HasColumnName("fall");

                entity.Property(e => e.GeoLocation).HasColumnName("geo_location");

                entity.Property(e => e.Mass).HasColumnName("mass");

                entity.Property(e => e.MeteoriteId).HasColumnName("meteorite_id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Nametype).HasColumnName("nametype");

                entity.Property(e => e.Recclass).HasColumnName("recclass");

                entity.Property(e => e.Reclat).HasColumnName("reclat");

                entity.Property(e => e.Reclong).HasColumnName("reclong");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<SalariesOfStateAgencies>(entity =>
            {
                entity.ToTable("salaries_of_state_agencies");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Agency).HasColumnName("agency");

                entity.Property(e => e.AgencyNumber).HasColumnName("agency_number");

                entity.Property(e => e.Classification).HasColumnName("classification");

                entity.Property(e => e.FiscalYear).HasColumnName("fiscal_year");

                entity.Property(e => e.FullOrPartTime).HasColumnName("full_or_part_time");

                entity.Property(e => e.SalaryAnnual).HasColumnName("salary_annual");

                entity.Property(e => e.ServiceType).HasColumnName("service_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
