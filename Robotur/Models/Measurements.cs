using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotur
{
    public class Measurements : INotifyPropertyChanged
    {
        private List<double> x = new List<double>();
        public List<double> X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
                RaisePropertyChanged(nameof(X));
            }
        }

        private List<double> y = new List<double>();
        public List<double> Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
                RaisePropertyChanged(nameof(Y));
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
