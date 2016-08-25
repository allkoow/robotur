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

        public Connection Connection
        {
            get;
            set;
        }
        public Datas Datas
        {
            get;
            set;
        }

        #region Graphs
        public Graphs GraphAngle
        {
            get;
            set;
        }
        public Graphs GraphVelocity
        {
            get;
            set;
        }
        public Graphs GraphVoltage
        {
            get;
            set;
        }
        #endregion

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
            timerGraphs = new Timer(100);
            timerGraphs.Elapsed += graphUpdate;
            timerGraphs.Start();

            timerMessages = new Timer(100);
            timerMessages.Elapsed += messagesUpdate;
            timerMessages.Start();

            Connection = new Connection(messages);
            Datas = new Datas();

            GraphAngle = new Graphs();
            GraphVelocity = new Graphs();
            GraphVoltage = new Graphs();

            CommandConnection = new RelayCommand(Connection.Connect);
            CommandDisconnection = new RelayCommand(Connection.Disconnect);
            CommandRefreshListOfPorts = new RelayCommand(Connection.RefreshListOfPorts);
            CommandSendDatas = new RelayCommand(SendDatas);
        }

        private void SendDatas()
        {
            Connection.SendDatas(Datas.DatasToSend);
        }

        private void messagesUpdate(object sender, ElapsedEventArgs e)
        {
            RaisePropertyChanged(nameof(messages));
        }

        private void graphUpdate(object sender, ElapsedEventArgs e)
        {
            
            if (Datas.Settings.GetDatas)
            {
                Datas.DatasConversion(Connection.SerialDatas);
                GraphAngle.draw(new List<Measurements>() { Datas.Measurements[1], Datas.Measurements[2] });
                GraphVelocity.draw(new List<Measurements>() { Datas.Measurements[0], Datas.Measurements[6] });
                GraphVoltage.draw(new List<Measurements>() { Datas.Measurements[3] });
            }  
        }
    }
}