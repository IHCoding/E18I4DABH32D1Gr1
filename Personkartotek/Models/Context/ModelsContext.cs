using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Personkartotek.Models.Context
{
    public class ModelsContext : DbContext
    {
        public ModelsContext () {}

        public ModelsContext(DbContextOptions<ModelsContext> options) : base(options) { }

       
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

        protected void OnConfiguration(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;" +
                //                            "Initial Catalog=Personkartotek;" +
                //                            "Integrated Security=True");

                optionsBuilder.UseSqlServer("Data Source=st-i4dab.uni.au.dk; " +
                                            "Initial Catalog=E18I4DABau461895;" +
                                            "User ID=E18I4DABau461895;" +
                                            "Password=E18I4DABau461895;" +
                                            "Connect Timeout=60;" +
                                            "Encrypt=False;" +
                                            "TrustServerCertificate=True;" +
                                            "ApplicationIntent=ReadWrite;" +
                                            "MultiSubnetFailover=False");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(a => a.AddressId).HasColumnName("AddressId");
                entity.Property(a => a.HouseNumber ).IsRequired();
                entity.Property(a => a.StreetName).IsRequired();
                //entity.Property(a => a.AddressType).IsRequired();

                entity.Property(a => a.CityId).HasColumnName("CityId");
                entity.HasOne(a => a.City)
                    .WithMany(a => a.Addresses)
                    .HasForeignKey(a => a.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Address");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(c => c.CityId).HasColumnName("CityId");
                entity.Property(c=> c.CityName).IsRequired();
                entity.Property(c=> c.Postalcode).IsRequired();
                entity.Property(c => c.ProvinceState).IsRequired();
                entity.Property(c => c.Country).IsRequired();
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasKey(e => e.EmailId);
                entity.Property(e => e.EmailId).HasColumnName("EmailId");
                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("Email");

                entity.Property(e => e.PersonId).HasColumnName("PersonId");
                //entity.Property(e => e.EmailType).IsRequired();

                entity.HasOne(e => e.Person)
                    .WithMany(e => e.Emails)
                    .HasForeignKey(e => e.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Email");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(p => p.PersonId).HasColumnName("PersonId");
                entity.Property(e => e.AddressId).HasColumnName("AddressId");

                entity.Property(p => p.FirstName).IsRequired();
                entity.Property(p => p.MiddleName).IsRequired();
                entity.Property(p => p.LastName).IsRequired();

                entity.Property(p => p.Description).IsRequired();
                entity.Property(p => p.Telephone).IsRequired();

                entity.HasOne(p => p.Address)
                    .WithMany(p => p.PersonsResidingAtAddress)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Person");
            });
        }
    }
}
