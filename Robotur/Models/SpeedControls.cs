using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotur
{
    public class SpeedController : PIDparameters
    {
        private double _voltage;
        public double voltage
        {
            get { return _voltage; }
            set
            {
                _voltage = value;
                RaisePropertyChanged(nameof(voltage));
            }
        }

        private double _filterBeta;
        public double filterBeta
        {
            get { return _filterBeta; }
            set
            {
                _filterBeta = value;
                RaisePropertyChanged(nameof(filterBeta));
            }
        }

    }
}
