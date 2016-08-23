using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotur
{
    public enum OperatingMode
    {
        off, voltage, velocityController, angleController, cascadeController
    }

    public class Settings : INotifyPropertyChanged
    {
        private ObservableCollection<string> _portNumbers = new ObservableCollection<string>();
        public ObservableCollection<string> portNumbers
        {
            get { return _portNumbers; }
            set
            {
                _portNumbers = value;
                RaisePropertyChanged(nameof(portNumbers));
            }
        }

        private ObservableCollection<int> _baudRates = new ObservableCollection<int>();
        public ObservableCollection<int> baudRates
        {
            get { return _baudRates; }
            set
            {
                _baudRates = value;
                RaisePropertyChanged(nameof(baudRates));
            }
        }

        private OperatingMode _operatingMode;
        public OperatingMode operatingMode
        {
            get { return _operatingMode; }
            set
            {
                _operatingMode = value;
                RaisePropertyChanged(nameof(operatingMode));
            }
        }

        private bool _getDatas;
        public bool getDatas
        {
            get { return _getDatas; }
            set
            {
                _getDatas = value;
                RaisePropertyChanged(nameof(getDatas));
            }
        }

        private int _numberOfSamples;
        public int numberOfSamples
        {
            get { return _numberOfSamples; }
            set
            {
                _numberOfSamples = value;
                RaisePropertyChanged(nameof(numberOfSamples));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
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
