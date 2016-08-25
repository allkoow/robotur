using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Robotur
{
    public class Connection : INotifyPropertyChanged
    {
        private SerialPort serialPort = new SerialPort();
        private BackgroundWorker bcgWorker = new BackgroundWorker();
        private static Timer timer;
        private static Timer timerCheckIsOpen;

        public StringBuilder messages
        {
            get;
            set;
        }

        #region connection notification
        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                if (isOpen != value) isOpen = value;
                RaisePropertyChanged(nameof(IsOpen));
            }
        }

        private bool connecting;
        public bool Connecting
        {
            get { return connecting; }
            set
            {
                if (connecting != value) connecting = value;
                RaisePropertyChanged(nameof(Connecting));
            }
        }
        #endregion

        #region parameters of connection
        private ObservableCollection<string> portNumbers = new ObservableCollection<string>();
        public ObservableCollection<string> PortNumbers
        {
            get { return portNumbers; }
            set
            {
                portNumbers = value;
                RaisePropertyChanged(nameof(PortNumbers));
            }
        }

        private ObservableCollection<int> baudRates = new ObservableCollection<int>();
        public ObservableCollection<int> BaudRates
        {
            get { return baudRates; }
            set
            {
                baudRates = value;
                RaisePropertyChanged(nameof(BaudRates));
            }
        }

        public int SelectedPortIndex
        {
            get;
            set;
        }
        public int SelectedBaudRateIndex
        {
            get;
            set;
        }
        #endregion

        private string serialDatas;
        public string SerialDatas
        {
            get { return serialDatas; }
            set
            {
                serialDatas = value;
            }
        }

        public Connection(StringBuilder messages)
        {
            serialPort.DataReceived += new SerialDataReceivedEventHandler(GetDatas);
            serialPort.RtsEnable = true;
            serialPort.DtrEnable = true;
            serialPort.Handshake = Handshake.None;

            this.messages = messages;
            RefreshListOfPorts();

            timer = new Timer(5000);
            timer.Elapsed += tooLong;

            timerCheckIsOpen = new Timer(1000);
            timerCheckIsOpen.Elapsed += checkIsOpen;

            bcgWorker.DoWork += new DoWorkEventHandler(DoConnection);
            bcgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Connected);
            bcgWorker.WorkerSupportsCancellation = true;

            BaudRates.Add(9600);
            BaudRates.Add(230400);

        }

        public void RefreshListOfPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            PortNumbers.Clear();

            foreach (string port in ports)
                PortNumbers.Add(port);

            messages.AppendLine("Odświeżono listę portów.");
        }

        public void Connect()
        {
            messages.AppendLine("Nawiązywanie połączenia...");
            Connecting = true;

            bcgWorker.RunWorkerAsync();
            timer.Start();
        }

        private void DoConnection(object sender, DoWorkEventArgs e)
        {
            serialPort.PortName = PortNumbers[SelectedPortIndex];
            serialPort.BaudRate = BaudRates[SelectedBaudRateIndex];

            try
            {
                if(!serialPort.IsOpen)
                {
                    serialPort.Open();
                    timer.Stop();
                }
                else
                {
                    messages.AppendLine("Port jest już otwarty.");
                    Connecting = false;
                    IsOpen = false;
                }
            }
            catch
            {
                messages.AppendLine("Nie można nawiązać połączenia.");
                Connecting = false;
            }
        }

        private void Connected(object sender, RunWorkerCompletedEventArgs e)
        {
            if(serialPort.IsOpen)
            {
                messages.AppendLine("Nawiązano połączenie.");
                Connecting = false;
                IsOpen = true;
                timerCheckIsOpen.Start();
            }
        }

        private void checkIsOpen(object sender, ElapsedEventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                IsOpen = false;
                messages.AppendLine("Utracono połączenie.");
                timerCheckIsOpen.Stop();
            }
        }

        private void tooLong(object sender, ElapsedEventArgs e)
        {
            messages.AppendLine("Nawiązywanie połączenia trwa dłużej niż zwykle. Spróbuj ponownie." + Environment.NewLine);

            bcgWorker.CancelAsync();
            timer.Stop();
        }

        public void Disconnect()
        {
            if(serialPort.IsOpen)
            {
                try
                {
                    serialPort.Close();
                    messages.AppendLine("Zamknięto połączenie.");
                    IsOpen = false;
                    timerCheckIsOpen.Stop();
                }
                catch
                {
                    messages.AppendLine("Nie można zamknąć połączenia." + Environment.NewLine);
                }

                serialPort.Dispose();
            }
        }

        private void GetDatas(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialDatas = serialPort.ReadLine();
                }
                    
            }
            catch
            {
                messages.AppendLine("Odebranie danych nie powiodło się.");
            }
        }

        public void SendDatas(string datas)
        {
            try
            {
                if (serialPort.IsOpen)
                    serialPort.WriteLine(datas);
            }
            catch
            {
                messages.AppendLine("Wysłanie danych nie powiodło się.");
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        #endregion
    }
}
