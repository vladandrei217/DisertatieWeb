namespace DisertatieWeb.Models
{
    public class DashboardStats
    {
        public int VehiculeActive { get; set; }
        public double EmisiiCo2 { get; set; }
        public int AlerteActive { get; set; }
        public double ConsumCombustibil { get; set; }

        public List<DailyTraffic> TraficZilnic { get; set; } = new();
    }

    public class DailyTraffic
    {
        public DateTime Data { get; set; }
        public int Vehicule { get; set; }
    }
}
