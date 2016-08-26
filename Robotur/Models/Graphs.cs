using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotur
{
    public class Graphs
    {
        // Plot models
        private PlotModel plotModel = new PlotModel();
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set
            {
                plotModel = value;
            }
        } 

        // Parameters
        private List<LineSeries> series = new List<LineSeries>();

        public Graphs(string title)
        {
            // Definitions of axes
            var xaxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };

            var yaxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };

            // Add axes to the plot model
            plotModel.Axes.Add(xaxis);
            plotModel.Axes.Add(yaxis);

            plotModel.Title = title;
            plotModel.TitleFontSize = 16;
            plotModel.IsLegendVisible = true;
        }

        public void draw(List<Measurements> listOfMeasurements)
        {
            series.Clear();
            for (int i=0; i<listOfMeasurements.Count; i++)
            {
                var m = listOfMeasurements[i];
                series.Add(new LineSeries());

                for(int j=0; j<m.X.Count; j++)
                {
                    var x = m.X[j];
                    var y = m.Y[j];

                    series[i].Points.Add(new DataPoint(x, y));
                }
            }

            series[0].Title = "lala";
            

            plotModel.Series.Clear();
            
            foreach (LineSeries points in series)
                plotModel.Series.Add(points);

            plotModel.InvalidatePlot(true);
        }
    }
}
