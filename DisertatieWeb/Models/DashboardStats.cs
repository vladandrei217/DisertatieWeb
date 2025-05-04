namespace DisertatieWeb.Models
{
    public class DashboardStats
    {
        public int DispozitiveActive { get; set; }
        public int PuncteTuristice { get; set; }
        public int ComentariiTotal { get; set; }
        public int DispozitiveRecente { get; set; }
        public List<ComentariuRecent> ComentariiRecente { get; set; }
    }

    public class DailyTraffic
    {
        public DateTime Data { get; set; }
        public int Vehicule { get; set; }
    }
    public class EvenimentTrafic
    {
        public string Tip { get; set; }
        public string Locatie { get; set; }
        public DateTime Data { get; set; }
    }
    public class ComentariuRecent
    {
        public string UserEmail { get; set; }
        public string Continut { get; set; }
        public DateTime Data { get; set; }
        public string PunctInteres { get; set; }
    }
}
