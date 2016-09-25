using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotur
{
    public class Datas
    {
        private string datasToSend;
        public string DatasToSend
        {
            get
            {
                return datasToSend = PrepareDatasToSend();
            }
            set { datasToSend = value; }
        }

        public Measurements[] Measurements
        {
            get;
            set;
        }

        private Settings settings = new Settings();
        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        private SpeedController speedController = new SpeedController();
        public SpeedController SpeedController
        {
            get { return speedController; }
            set { speedController = value; }
        }

        private AngleController angleController = new AngleController();
        public AngleController AngleController
        {
            get { return angleController; }
            set { angleController = value; }
        }

        private BatteryInfo batteryInfo = new BatteryInfo();
        public BatteryInfo BatteryInfo
        {
            get { return batteryInfo; }
            set { batteryInfo = value; }
        }

        public Datas()
        {
            Measurements = new Measurements[13];
            for(int i=0; i<Measurements.Length; i++)
            {
                Measurements[i] = new Measurements();
                Measurements[i].X.Add(0);
                Measurements[i].Y.Add(0);
            }

            Settings.NumberOfSamples = 100;
        }

        public void DatasConversion(string frame)
        {
            if(frame != null)
            {
                #region zamiana kropek na przecinki, potrzebna do konwersji na typ double
                string frameConverted = frame.Replace('.', ',');
                #endregion
                #region wyodrębnienie danych z ramki
                string[] datasString = frameConverted.Split(new char[] { ';' }); // odebrane dane muszą być oddzielone średnikami
                #endregion
                #region konwersja danych na typ double
                double[] datasDouble = new double[datasString.Length];

                int i = 0;
                foreach (string str in datasString)
                {
                    datasDouble[i] = Convert.ToDouble(str);
                    i++;
                }
                #endregion

                #region przypisanie danych 
                for (i = 0; i < datasDouble.Length; i++)
                {
                    Measurements[i].X.Add(Measurements[i].X[Measurements[i].X.Count - 1] + 1.0);
                    Measurements[i].Y.Add(datasDouble[i]);
                }

                batteryInfo.Cell1 = Measurements[11].Y[0];
                batteryInfo.Cell2 = Measurements[12].Y[0];
                
                #endregion
                #region obcinanie zbyt dużej ilości danych
                foreach (Measurements m in Measurements)
                {
                    if (m.X.Count > settings.NumberOfSamples)
                    {
                        m.X.RemoveAt(0);
                        m.Y.RemoveAt(0);
                    }
                }
                #endregion
            }
        }

        private string PrepareDatasToSend()
        {
            string frame = Convert.ToString((int)settings.OperatingMode) + ";"
                         + Convert.ToString(speedController.SignalPWM) + ";"
                         + string.Format("{0:N4}", angleController.SetPoint) + ";"
                         + string.Format("{0:N4}", angleController.Kp) + ";"
                         + string.Format("{0:N4}", angleController.Ki) + ";"
                         + string.Format("{0:N4}", angleController.Kd) + ";"
                         + string.Format("{0:N4}", angleController.FilterBeta) + ";"
                         + string.Format("{0:N4}", speedController.SetPoint) + ";"
                         + string.Format("{0:N4}", speedController.Kp) + ";"
                         + string.Format("{0:N4}", speedController.Ki) + ";"
                         + string.Format("{0:N4}", speedController.Kd) + ";"
                         + string.Format("{0:N4}", speedController.FilterBeta);

            frame = frame.Replace(",", ".");
            return frame;
        }
    }
}
