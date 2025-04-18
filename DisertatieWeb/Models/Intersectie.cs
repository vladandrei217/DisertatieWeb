namespace DisertatieWeb.Models
{
    public class Intersectie
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public double Latitudine { get; set; }
        public double Longitudine { get; set; }
        public string Status { get; set; } // ex: Activă / Inactivă / Alertă
        public int NumarVehicule { get; set; }
    }
}
