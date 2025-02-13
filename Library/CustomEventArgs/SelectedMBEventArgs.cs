using Library.Models;

namespace Library.CustomEventArgs
{
    public class SelectedMBEventArgs : EventArgs
    {
        public Motherboard Motherboard { get; set; }

        public SelectedMBEventArgs(Motherboard Motherboard)
        {
            this.Motherboard = Motherboard;
        }
    }
}
