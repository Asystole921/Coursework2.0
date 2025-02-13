namespace Library.Models
{
    public class PowerSupply
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public int Wattage { get; set; }

        public string Efficiency { get; set; }

        public string Modularity { get; set; }

        public string FormFactor { get; set; }

        public string ConnectorTypes { get; set; }

        public double Price { get; set; }

        public string? ImgUrl { get; set; }
    }
}
