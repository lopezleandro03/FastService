using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FastService.Models
{
    public class Chart
    {
        public List<ChartDataSet> Datasets { get; set; }

        public string Labels
        {
            get
            {
                return Datasets.First().LabelsToJson();
            }
            set
            {
                Labels = value;
            }
        }

        public Chart()
        {
            Datasets = new List<ChartDataSet>();
        }
    }

    public class ChartDataSet
    {
        public List<string> DataPoints { get; set; }
        public List<decimal> DataValues { get; set; }

        public string Label { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string BorderWidth { get; set; }

        public ChartDataSet(string label, string bgColor, string brColor, string brWidth)
        {
            Label = label;
            BackgroundColor = bgColor;
            BorderColor = bgColor;
            BorderWidth = brWidth;
            DataPoints = new List<string>();
            DataValues = new List<decimal>();
        }

        public string LabelsToJson()
        {
            var json = JsonConvert.SerializeObject(DataPoints);
            return json.Replace("\"", "");
        }

        public string DataToJson()
        {
            var json = JsonConvert.SerializeObject(DataValues);
            return json;
        }

        public string Draw()
        {
            var charDef = "{" +
                            $"label: '{Label}'," +
                            $"data: {DataToJson()}," +
                            $"backgroundColor: ['{BackgroundColor}']," +
                            $"borderColor: ['{BorderColor}']," +
                            $"borderWidth: {BorderWidth}," +
                            $"fill: false" +
                          "},";

            return charDef;
        }

        public string DrawLast()
        {
            return Draw() + ",";
        }
    }
}