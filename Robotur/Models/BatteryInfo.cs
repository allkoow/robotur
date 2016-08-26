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

        private double cell1;
        public double Cell1
        {
            get { return cell1; }
            set
            {
                cell1 = value;
                RaisePropertyChanged(nameof(Cell1));
            }
        }

        private double cell2;
        public double Cell2
        {
            get { return cell2; }
            set
            {
                cell2 = value;
                RaisePropertyChanged(nameof(Cell2));
            }
        }

        private double battery;
        public double Battery
        {
            get
            {
                return cell1 + cell2;
            }
            set
            {
                battery = cell1 + cell2;
                RaisePropertyChanged(nameof(Battery));
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
