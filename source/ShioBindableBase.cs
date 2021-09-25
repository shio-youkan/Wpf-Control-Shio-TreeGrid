using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Shio
{
    [Serializable]
    public abstract class ShioBindableBase : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public void ForcePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.OnPropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.OnPropertyChanged(propertyName);
        }

        protected bool NeedsRaisePropertyChanged<T>(T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;

            return true;
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ShioBindableBase()
        {
        }
    }
}