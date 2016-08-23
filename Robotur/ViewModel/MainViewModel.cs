using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Text;

namespace Robotur.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Settings _settings = new Settings();
        public Settings settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        private StringBuilder _messages = new StringBuilder();
        public StringBuilder messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                RaisePropertyChanged(nameof(messages));
            }
        }

        private BatteryInfo _batteryInfo = new BatteryInfo();
        public BatteryInfo batteryInfo
        {
            get { return _batteryInfo; }
            set
            {
                _batteryInfo = value;
                RaisePropertyChanged(nameof(batteryInfo));
            }
        }

        private AngleController _angleController = new AngleController();
        public AngleController angleController
        {
            get { return _angleController; }
            set
            {
                _angleController = value;
                RaisePropertyChanged(nameof(angleController));
            }
        }

        private SpeedController _speedController = new SpeedController();
        public SpeedController speedController
        {
            get { return _speedController; }
            set
            {
                _speedController = value;
                RaisePropertyChanged(nameof(speedController));
            }
        }

        public MainViewModel()
        {
            _settings.portNumbers.Add("COM1");
            _settings.baudRates.Add(9600);

            _batteryInfo.cell1 = 4.05;
        }
    }
}