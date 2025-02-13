namespace Library.Models
{
    public class Build
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int CPUId { get; set; }

        public int GPUId { get; set; }

        public int MotherboardId { get; set; }

        public int PowerSupplyId { get; set; }

        public int RAMId { get; set; }

        public int StorageId { get; set; }

        public int PCCaseId { get; set; }

        public int CPUCoolerId { get; set; }

        public double BuildTotalPrice { get; set; }
    }
}
