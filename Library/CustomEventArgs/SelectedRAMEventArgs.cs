using Library.Models;

namespace Library.CustomEventArgs
{
    public class SelectedRAMEventArgs : EventArgs
    {
        public RAM RAM { get; set; }

        public SelectedRAMEventArgs(RAM RAM)
        {
            this.RAM = RAM;
        }
    }
}
