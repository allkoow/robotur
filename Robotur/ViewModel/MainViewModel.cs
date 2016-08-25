using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Timers;
using System;
using OxyPlot;
using System.Collections.Generic;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

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

        private StringBuilder messages = new StringBuilder();
        public StringBuilder Messages
        {
            get { return messages; }
            set
            {
                if (messages != value) messages = value;
                RaisePropertyChanged(nameof(Messages));
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

        private Connection connection;
        public Connection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        private Timer timerMessages;

        #region Commands
        public ICommand CommandConnection { get; private set; }
        public ICommand CommandDisconnection { get; private set; }
        public ICommand CommandRefreshListOfPorts { get; private set; }
        public ICommand CommandSendDatas { get; private set; }
        #endregion

        // Timer for refreshing graphs
        private static Timer timerGraphs = null;

        // Graphs
        

        public MainViewModel()
        {
            // Timer for refreshing graphs initialization
            timerGraphs = new Timer(1000);
            timerGraphs.Elapsed += graphUpdate;

            timerMessages = new Timer(100);
            timerMessages.Elapsed += messagesUpdate;
            timerMessages.Start();

            connection = new Connection(messages);

            CommandConnection = new RelayCommand(connection.Connect);
            CommandDisconnection = new RelayCommand(connection.Disconnect);
            CommandRefreshListOfPorts = new RelayCommand(connection.RefreshListOfPorts);
            CommandSendDatas = new RelayCommand(SendDatas);
        }

        private void SendDatas()
        {
            throw new NotImplementedException();
        }

        private void messagesUpdate(object sender, ElapsedEventArgs e)
        {
            RaisePropertyChanged(nameof(messages));
        }

        private void graphUpdate(object sender, ElapsedEventArgs e)
        {
               
        }
    }
}