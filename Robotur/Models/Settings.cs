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
        off, pwm, velocityController, angleController, cascadeController
    }

    public class Settings : INotifyPropertyChanged
    {
        private OperatingMode operatingMode;
        public OperatingMode OperatingMode
        {
            get { return operatingMode; }
            set
            {
                operatingMode = value;
                RaisePropertyChanged(nameof(OperatingMode));
            }
        }

        private bool getDatas;
        public bool GetDatas
        {
            get { return getDatas; }
            set
            {
                getDatas = value;
                RaisePropertyChanged(nameof(GetDatas));
            }
        }

        private int numberOfSamples;
        public int NumberOfSamples
        {
            get { return numberOfSamples; }
            set
            {
                numberOfSamples = value;
                RaisePropertyChanged(nameof(NumberOfSamples));
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
