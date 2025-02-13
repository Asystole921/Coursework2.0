using Library.Models;

namespace Library.CustomEventArgs
{
    public class SelectedCPUEventArgs : EventArgs
    {
        public CPU CPU { get; set; }

        public SelectedCPUEventArgs(CPU CPU)
        {
            this.CPU = CPU;
        }
    }
}
