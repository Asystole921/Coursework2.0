namespace Library.Models
{
    public class Motherboard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Chipset { get; set; }

        public string FormFactor { get; set; }

        public int SocketId { get; set; }

        public int RAMTypeId { get; set; }

        public int MaxRAM { get; set; }

        public int PCIeSlots { get; set; }

        public int USBPorts { get; set; }

        public int SATAPorts { get; set; }

        public double Price { get; set; }

        public string? ImgUrl { get; set; }
    }
}
