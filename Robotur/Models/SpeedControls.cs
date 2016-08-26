using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotur
{
    public class SpeedController : PIDparameters
    {
        private int signalPWM;
        public int SignalPWM
        {
            get { return signalPWM; }
            set
            {
                signalPWM = value;
                RaisePropertyChanged(nameof(SignalPWM));
            }
        }

        private double filterBeta;
        public double FilterBeta
        {
            get { return filterBeta; }
            set
            {
                filterBeta = value;
                RaisePropertyChanged(nameof(FilterBeta));
            }
        }

    }
}
