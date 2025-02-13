using Library.Models;

namespace Library.CustomEventArgs
{
    public class SelectedCCEventArgs : EventArgs
    {
        public CPUCooler CPUCooler { get; set; }

        public SelectedCCEventArgs(CPUCooler CPUCooler)
        {
            this.CPUCooler = CPUCooler;
        }
    }
}
