using Library.Models;

namespace Library.CustomEventArgs
{
    public class SelectedStorageEventArgs : EventArgs
    {
        public Storage Storage { get; set; }
        public SelectedStorageEventArgs(Storage Storage)
        {
            this.Storage = Storage;
        }
    }
}
