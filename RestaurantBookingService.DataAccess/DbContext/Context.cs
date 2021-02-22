using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;
using NpgsqlTypes;
using RestaurantBookingService.DataAccess.Models;

#nullable disable

namespace RestaurantBookingService.DataAccess.Context
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual List<Restaurant> GetRestaurantByUsersDietaryRestriction(List<long> userIds)
        {
            using var command = Database.GetDbConnection().CreateCommand();
            command.CommandText = "SELECT * FROM entities.fn_get_restaurant_by_criteria(@_user_ids)";
            command.Parameters.Add(new NpgsqlParameter("@_user_ids", userIds));
            var result = new List<Restaurant>();
            Database.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var restaurant = new Restaurant
                    {
                        Id = reader.GetInt64(0),
                        Name = reader.GetString(1),
                        LocationId = reader.GetInt64(2)
                    };
                    result.Add(restaurant);
                }
            }
            return result;
        }

        public virtual List<Table> GetAvailableTablesByRestaurantIds(List<long> restaurantIds, int required_seats)
        {
            using var command = Database.GetDbConnection().CreateCommand();
            command.CommandText = "SELECT * FROM entities.fn_get_available_table_list(@_user_ids, @_required_seats)";
            command.Parameters.Add(new NpgsqlParameter("@_user_ids", restaurantIds));
            command.Parameters.Add(new NpgsqlParameter("@_required_seats", required_seats));
            var result = new List<Table>();
            Database.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var table = new Table
                    {
                        Id = reader.GetInt64(0),
                        RestaurantId = reader.GetInt64(1)
                    };
                    result.Add(table);
                }
            }
            return result;
        }

        public virtual DbSet<DietaryEndorsement> DietaryEndorsements { get; set; }
        public virtual DbSet<DietaryRestriction> DietaryRestrictions { get; set; }
        public virtual DbSet<Diner> Diners { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationToUser> ReservationToUsers { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<RestaurantToDietaryRestriction> RestaurantToDietaryRestrictions { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserToDietaryRestriction> UserToDietaryRestrictions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<DietaryEndorsement>(entity =>
            {
                entity.ToTable("dietary_endorsement", "entities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    
                    .HasMaxLength(100)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<DietaryRestriction>(entity =>
            {
                entity.ToTable("dietary_restriction", "entities");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('entities.\"dietary_restriction_Id_seq\"'::regclass)");

                entity.Property(e => e.Name)
                    
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Diner>(entity =>
            {
                entity.ToTable("diner", "entities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                //entity.HasOne(d => d.Restaurant)
                //    .WithMany(p => p.Diners)
                //    .HasForeignKey(d => d.RestaurantId)
                //    .HasConstraintName("RestaurantId");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.Diners)
                //    .HasForeignKey(d => d.UserId)
                //    .HasConstraintName("UserId");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location", "location");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('location.\"location_Id_seq\"'::regclass)");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("reservation", "entities");

                entity.HasIndex(e => e.TableId, "fki_table_fkey");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('entities.\"reservation_Id_seq\"'::regclass)");

                entity.Property(e => e.DateTime)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("date_time");

                entity.Property(e => e.MarkForDelete).HasColumnName("mark_for_delete");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.Property(e => e.TableId).HasColumnName("table_id");

                //entity.HasOne(d => d.Restaurant)
                //    .WithMany(p => p.Reservations)
                //    .HasForeignKey(d => d.RestaurantId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("RestaurantId");

                //entity.HasOne(d => d.Table)
                //    .WithMany(p => p.Reservations)
                //    .HasForeignKey(d => d.TableId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("table_fk");
            });

            modelBuilder.Entity<ReservationToUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("reservation_to_user", "entities");

                entity.Property(e => e.ReservationId).HasColumnName("reservation_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                //entity.HasOne(d => d.Reservation)
                //    .WithMany()
                //    .HasForeignKey(d => d.ReservationId)
                //    .HasConstraintName("Reservation_pk");

                //entity.HasOne(d => d.ReservationNavigation)
                //    .WithMany()
                //    .HasForeignKey(d => d.ReservationId)
                //    .HasConstraintName("User_pk");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurant", "entities");

                entity.Property(e => e.Id)
                    .HasColumnName("id");
                    //.HasDefaultValueSql("nextval('entities.\"restaurant_Id_seq\"'::regclass)");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    
                    .HasMaxLength(100)
                    .HasColumnName("name");

                //entity.HasOne(d => d.Location)
                //    .WithMany(p => p.Restaurants)
                //    .HasForeignKey(d => d.LocationId)
                //    .HasConstraintName("LocationId");
            });

            modelBuilder.Entity<RestaurantToDietaryRestriction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("restaurant_to_dietary_restriction", "entities");

                entity.Property(e => e.DietaryRestrictionId).HasColumnName("dietary_restriction_id");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                //entity.HasOne(d => d.DietaryRestriction)
                //    .WithMany()
                //    .HasForeignKey(d => d.DietaryRestrictionId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("dietary_rest_fkey");

                //entity.HasOne(d => d.Restaurant)
                //    .WithMany()
                //    .HasForeignKey(d => d.RestaurantId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("rest_fkey");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.ToTable("table", "entities");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('entities.\"table_Id_seq\"'::regclass)");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                //entity.HasOne(d => d.Restaurant)
                //    .WithMany(p => p.Tables)
                //    .HasForeignKey(d => d.RestaurantId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("RestaurantId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "security");

                entity.HasIndex(e => e.ReservationId, "fki_reservation_fkey");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('security.\"users_Id_seq\"'::regclass)");

                entity.Property(e => e.Name)
                    
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ReservationId).HasColumnName("reservation_id");

                //entity.HasOne(d => d.Reservation)
                //    .WithMany(p => p.Users)
                //    .HasForeignKey(d => d.ReservationId)
                //    .HasConstraintName("reservation_fkey");
            });

            modelBuilder.Entity<UserToDietaryRestriction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_to_dietary_restriction", "entities");

                entity.Property(e => e.DietaryRestrictionId).HasColumnName("dietary_restriction_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                //entity.HasOne(d => d.DietaryRestriction)
                //    .WithMany()
                //    .HasForeignKey(d => d.DietaryRestrictionId)
                //    .HasConstraintName("DietaryRestrictionId_fk");

                //entity.HasOne(d => d.User)
                //    .WithMany()
                //    .HasForeignKey(d => d.UserId)
                //    .HasConstraintName("UserId_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
