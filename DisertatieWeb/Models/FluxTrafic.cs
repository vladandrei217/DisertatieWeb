namespace DisertatieWeb.Models
{
    public class FluxTrafic
    {
        public int Id { get; set; }
        public string Nume { get; set; } // Numele fluxului (ex: "Flux Piața Victoriei - Bd. Unirii")
        public string Descriere { get; set; } // O descriere detaliată a fluxului
        public DateTime DataUltimeiActualizari { get; set; } // Data ultimei actualizări a fluxului
        public List<FluxTraficDetalii> Detalii { get; set; } = new List<FluxTraficDetalii>(); // Detalii asociate fluxului
    }

    public class FluxTraficDetalii
    {
        public int Id { get; set; }
        public DateTime Ora { get; set; } // Ora la care s-a măsurat fluxul
        public int Valoare { get; set; } // Valoare fluxului (ex: numărul de vehicule pe minut)
    }
}
