using Library.Models;

namespace Library.CustomEventArgs
{
    public class SelectedPCCaseEventArgs : EventArgs
    {
        public PCCase PCCase { get; set; }

        public SelectedPCCaseEventArgs(PCCase PCCase)
        {
            this.PCCase = PCCase;
        }
    }
}
