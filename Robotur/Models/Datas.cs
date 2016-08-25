using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotur.Models
{
    public class Datas
    {
        private string datasToSend;
        public string DatasToSend
        {
            get { return datasToSend; }
            set { datasToSend = value; }
        }

        public Measurements[] measurements
        {
            get;
            set;
        }

        private Settings settings;
        private SpeedController speedController;
        private AngleController angleController;

        public Datas(Settings settings, SpeedController speedController, AngleController angleController)
        {
            this.settings = settings;
            this.speedController = speedController;
            this.angleController = angleController;

            measurements = new Measurements[7];
        }

        public void DatasConversion(string frame)
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
                if (i != 2)
                {
                    measurements[i].X.Add(measurements[i].X[measurements[i].X.Count - 1] + 1.0 * 0.1);
                    measurements[i].Y.Add(datasDouble[i]);
                }
            }

            int samples = 0;

            if(settings.OperatingMode == OperatingMode.cascadeController)
            {
                measurements[2].X.Add(measurements[2].X[measurements[2].X.Count - 1] + 1.0 * 0.1);
                measurements[2].Y.Add(datasDouble[2]);
            }
            else
            {
                measurements[2].X.Add(measurements[2].X[measurements[2].X.Count - 1] + 1.0 * 0.1);
                measurements[2].Y.Add(angleController.setPoint);
            }

            measurements[6].X.Add(measurements[4].X[measurements[4].X.Count - 1] + 1.0 * 0.1);
            measurements[6].Y.Add(speedController.setPoint);

            samples = settings.NumberOfSamples;


            #endregion

        }

    }
}
