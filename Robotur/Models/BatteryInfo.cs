using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotur
{
    public class BatteryInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double _cell1;
        public double cell1
        {
            get { return _cell1; }
            set
            {
                _cell1 = value;
                RaisePropertyChanged(nameof(cell1));
            }
        }

        private double _cell2;
        public double cell2
        {
            get { return _cell2; }
            set
            {
                _cell2 = value;
                RaisePropertyChanged(nameof(cell2));
            }
        }

        private double _battery;
        public double battery
        {
            get
            {
                return _cell1 + _cell2;
            }
            set
            {
                battery = _cell1 + _cell2;
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
