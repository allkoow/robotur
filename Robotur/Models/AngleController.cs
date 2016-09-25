using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotur
{
    public class AngleController : PIDparameters
    {
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

        public AngleController()
        {
            filterBeta = 0.95;
        }
    }
}
