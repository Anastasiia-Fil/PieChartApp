
namespace PieChartApp.Models
{
    // Represents one section of the pie chart
    public class ChartItem
    {
        public string Label { get; set; }
        public float Value { get; set; }

        public ChartItem(string label, float value)
        {
            Label = label;
            Value = value;
        }
    }
}
