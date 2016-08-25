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

        // Measurements containing
        private ObservableCollection<Measurements> _measurements = new ObservableCollection<Measurements>();
        public ObservableCollection<Measurements> measurements
        {
            get { return _measurements; }
            set { _measurements = value; }
        }

        Measurements points1 = new Measurements();
        Measurements points2 = new Measurements();

        public Graphs graphs
        {
            get;
            private set;
        }

        // Timer for refreshing graphs
        private static Timer timerGraphs = null;

        // Graphs
        

        public MainViewModel()
        {
            // Timer for refreshing graphs initialization
            timerGraphs = new Timer(1000);
            timerGraphs.Elapsed += graphUpdate;

            double value = 0.0;

            for (int i=0; i<10; i++)
            {
                points1.X.Add(value + i);
                points1.Y.Add(value + i + 1);

                points2.X.Add(value + i);
                points2.Y.Add(value + i + 3);
            }

            List<Measurements> points = new List<Measurements>();
            points.Add(points1);
            points.Add(points2);

            graphs = new Graphs();

            graphs.draw(points);
        }

        private void graphUpdate(object sender, ElapsedEventArgs e)
        {
               
        }
    }
}