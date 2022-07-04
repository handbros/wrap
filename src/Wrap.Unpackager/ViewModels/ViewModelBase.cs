using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wrap.Unpackager.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region ::INotifyPropertyChanged Supports::

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            // take a copy to prevent thread issues.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Set<T>(ref T variable, T newValue)
        {
            variable = newValue;
            NotifyPropertyChanged(nameof(variable));
        }

        #endregion
    }
}
