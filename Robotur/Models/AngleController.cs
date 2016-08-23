using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotur
{
    public class AngleController : PIDparameters
    {
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
