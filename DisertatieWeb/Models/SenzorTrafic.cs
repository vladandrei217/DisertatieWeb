namespace DisertatieWeb.Models
{
    public class SenzorTrafic
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public double Latitudine { get; set; }
        public double Longitudine { get; set; }
        public string Status { get; set; } // "Activ", "Inactiv", "Alertă"
        public List<ComentariuSenzor> Comentarii { get; set; }
    }
    public class ComentariuSenzor
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Autor { get; set; }
        public string Text { get; set; }
    }

}
