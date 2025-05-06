namespace DisertatieWeb.Models
{
    public class FluxVizitatoriModel
    {
        public string Locație { get; set; }
        public int NumarVizitatori { get; set; }
        public DateTime OraRaportarii { get; set; }
    }
    public class ZonaPopularaModel
    {
        public int Id { get; set; }
        public string NumeZona { get; set; }
        public int Vizitatori { get; set; }
        public double Latitudine { get; set; }
        public double Longitudine { get; set; }
    }
    public class PunctDeInteresModel
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Descriere { get; set; }
        public double Latitudine { get; set; }
        public double Longitudine { get; set; }
        public string? ImagineUrl { get; set; }
    }
    public class InteractiuneSenzorModel
    {
        public int Id { get; set; }
        public string PunctDeInteres { get; set; }
        public bool NotificareTrimisa { get; set; }
        public DateTime? UltimaDetectie { get; set; }
        public string UltimulComentariu { get; set; }
    }

}
