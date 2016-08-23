using System.ComponentModel;

namespace Robotur
{
    public class PIDparameters : INotifyPropertyChanged
    {

        private double _setPoint;
        public double setPoint
        {
            get { return _setPoint; }
            set
            {
                _setPoint = value;
                RaisePropertyChanged(nameof(setPoint));
            }
        }

        private double _Ki;
        public double Ki
        {
            get { return _Ki; }
            set
            {
                _Ki = value;
                RaisePropertyChanged(nameof(Ki));
            }
        }

        private double _Kp;
        public double Kp
        {
            get { return _Kp; }
            set
            {
                _Kp = value;
                RaisePropertyChanged(nameof(Kp));
            }
        }

        private double _Kd;
        public double Kd
        {
            get { return _Kd; }
            set
            {
                _Kd = value;
                RaisePropertyChanged(nameof(Kd));
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
