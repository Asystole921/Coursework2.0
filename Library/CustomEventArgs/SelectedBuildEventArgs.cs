using Library.Models;

namespace Library.CustomEventArgs
{
    public class SelectedBuildEventArgs : EventArgs
    {
        public Build Build { get; set; }

        public SelectedBuildEventArgs(Build Build)
        {
            this.Build = Build;
        }
    }

}
