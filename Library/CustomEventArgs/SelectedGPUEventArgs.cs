using Library.Models;

namespace Library.CustomEventArgs
{
    public class SelectedGPUEventArgs : EventArgs
    {
        public GPU GPU { get; set; }

        public SelectedGPUEventArgs(GPU GPU)
        {
            this.GPU = GPU;
        }
    }
}
