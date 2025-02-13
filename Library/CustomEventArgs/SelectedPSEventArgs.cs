using Library.Models;

namespace Library.CustomEventArgs
{
    public class SelectedPSEventArgs : EventArgs
    {
        public PowerSupply PowerSupply { get; set; }

        public SelectedPSEventArgs(PowerSupply PowerSupply)
        {
            this.PowerSupply = PowerSupply;
        }
    }
}
