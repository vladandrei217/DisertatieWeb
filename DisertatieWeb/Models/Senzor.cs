namespace DisertatieWeb.Models
{
    namespace DisertatieWeb.Models
    {
        public class Senzor
        {
            public int Id { get; set; }
            public string Nume { get; set; }
            public string Status { get; set; }
            public string Detalii { get; set; }
            public Senzor(int id, string nume, string status, string detalii)
            {
                Id = id;
                Nume = nume;
                Status = status;
                Detalii = detalii;
            }
            public Senzor()
            {
            }
        }
    }

}
