namespace CalcoloSpeseBenzina.Components
{
    public class Percorso(string partenza = "", string arrivo = "")
    {
        public string Partenza { get; set; } = partenza;
        public string Arrivo { get; set; } = arrivo;
    }
}
