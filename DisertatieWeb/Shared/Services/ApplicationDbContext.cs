using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisertatieWeb.Shared.Services
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<PointOfInterest> Points_Of_Interest { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<TrafficSensor> TrafficSensors { get; set; }
        public DbSet<TrafficSensorComment> TrafficSensorComments { get; set; }
        public DbSet<TrafficFlow> TrafficFlows { get; set; }
        public DbSet<TrafficFlowMeasurement> TrafficFlowMeasurements { get; set; }
        public DbSet<VisitorFlow> VisitorFlows { get; set; }
        public DbSet<SensorInteraction> SensorInteractions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.PointOfInterest)
                .WithMany()
                .HasForeignKey(c => c.PoiId);

            modelBuilder.Entity<TrafficSensorComment>()
                .HasOne<TrafficSensor>()
                .WithMany(s => s.Comentarii)
                .HasForeignKey(c => c.SensorId);

            modelBuilder.Entity<TrafficFlow>()
                .HasMany(f => f.Masuratori)
                .WithOne(m => m.Flow)
                .HasForeignKey(m => m.FlowId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrafficFlowMeasurement>()
                .HasIndex(m => m.FlowId);

            modelBuilder.Entity<VisitorFlow>()
                .HasOne(v => v.PointOfInterest)
                .WithMany()
                .HasForeignKey(v => v.PoiId);
        }
    }

    //Models
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [Column("password_hash")] 
        public string PasswordHash { get; set; }

        [Column("reset_token")]
        public string? ResetToken { get; set; }

        [Column("reset_expiry")]
        public DateTime? ResetExpiry { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class Device
    {
        public int Id { get; set; }

        [Column("ble_id")]
        public string BleId { get; set; }

        public string? Description { get; set; }

        [Column("last_seen")]
        public DateTime? LastSeen { get; set; }
    }

    public class PointOfInterest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        [Column("ble_id")]
        public string BleId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("vizitatori_total")]
        public int VizitatoriTotal { get; set; }
        [Column("imagine_url")]
        public string ImagineUrl { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Column("poi_id")]
        public int PoiId { get; set; }
        [ForeignKey("PoiId")]
        public PointOfInterest PointOfInterest { get; set; }

        public string Content { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("device_id")]
        public string DeviceId { get; set; }
    }
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Tip { get; set; } 

        public string? Locatie { get; set; }

        public string? Descriere { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
    [Table("traffic_sensors")]
    public class TrafficSensor
    {
        public int Id { get; set; }

        [Required]
        [Column("nume")]
        public string Nume { get; set; }

        [Column("latitudine")]
        public double Latitudine { get; set; }

        [Column("longitudine")]
        public double Longitudine { get; set; }

        [Column("status")]
        public string Status { get; set; }
        [Column("detalii")]
        public string Detalii { get; set; }

        public List<TrafficSensorComment> Comentarii { get; set; } = new();
    }
    [Table("traffic_sensor_comments")]
    public class TrafficSensorComment
    {
        public int Id { get; set; }

        [Column("sensor_id")]
        public int SensorId { get; set; }

        [ForeignKey("SensorId")]
        public TrafficSensor Sensor { get; set; }

        [Column("autor")]
        public string Autor { get; set; }

        [Column("text")]
        public string Text { get; set; }

        [Column("data")]
        public DateTime Data { get; set; } = DateTime.UtcNow;
    }
    [Table("traffic_flows")]
    public class TrafficFlow
    {
        public int Id { get; set; }
        [Column("nume")] public string Nume { get; set; }
        [Column("descriere")] public string Descriere { get; set; }
        [Column("data_ultimei_actualizari")] public DateTime DataUltimeiActualizari { get; set; }

        public List<TrafficFlowMeasurement> Masuratori { get; set; } = new();
    }

    [Table("traffic_flow_measurements")]
    public class TrafficFlowMeasurement
    {
        public int Id { get; set; }
        [Column("flow_id")] public int FlowId { get; set; }
        [ForeignKey("FlowId")] public TrafficFlow Flow { get; set; }

        [Column("ora")] public DateTime Ora { get; set; }
        [Column("valoare")] public int Valoare { get; set; }
    }
    [Table("visitor_flow")]
    public class VisitorFlow
    {
        public int Id { get; set; }

        [Column("poi_id")]
        public int PoiId { get; set; }

        [ForeignKey("PoiId")]
        public PointOfInterest PointOfInterest { get; set; }

        [Column("numar_vizitatori")]
        public int NumarVizitatori { get; set; }

        [Column("ora_raportarii")]
        public DateTime OraRaportarii { get; set; }
    }
    [Table("sensor_interactions")]
    public class SensorInteraction
    {
        public int Id { get; set; }

        [Column("poi_id")]
        public int PoiId { get; set; }

        [ForeignKey("PoiId")]
        public PointOfInterest PointOfInterest { get; set; }

        [Column("notificare_trimisa")]
        public bool NotificareTrimisa { get; set; }
        [Column("ultima_detectie")]
        public DateTime? UltimaDetectie { get; set; }
        [Column("ultimul_comentariu")]
        public string? UltimulComentariu { get; set; }
        [Column("creeated_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
