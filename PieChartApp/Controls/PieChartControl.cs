
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using PieChartApp.Models;

namespace PieChartApp.Controls
{
    public class PieChartControl : Control
    {
        // Data displayed in the chart
        private List<ChartItem> items = new List<ChartItem>();

        // Pastel colors for chart sections
        private readonly Color[] colors =
        {
            Color.FromArgb(185, 162, 215),
            Color.FromArgb(231, 180, 203),
            Color.FromArgb(169, 207, 200),
            Color.FromArgb(245, 205, 153),
            Color.FromArgb(168, 190, 225)
        };

        public PieChartControl()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
        }

        // Updates the chart data
        public void SetData(List<ChartItem> data)
        {
            items = new List<ChartItem>(data);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float total = items.Sum(item => item.Value);

            if (total <= 0)
            {
                g.DrawString("Add data to display chart",
                    Font, Brushes.Gray, 20, 20);
                return;
            }

            int diameter = Math.Min(
                Math.Min(Width - 180, Height - 40), 240);

            if (diameter <= 0)
                return;

            Rectangle pieArea = new Rectangle(
                20, 20, diameter, diameter);

            float startAngle = 0;

            for (int i = 0; i < items.Count; i++)
            {
                float sweepAngle =
                    items[i].Value / total * 360f;

                using (SolidBrush brush =
                    new SolidBrush(colors[i % colors.Length]))
                {
                    g.FillPie(brush, pieArea,
                        startAngle, sweepAngle);
                }

                startAngle += sweepAngle;
            }

            // Draw the legend
            int legendX = diameter + 40;

            for (int i = 0; i < items.Count; i++)
            {
                int y = 20 + i * 25;

                using (SolidBrush brush =
                    new SolidBrush(colors[i % colors.Length]))
                {
                    g.FillRectangle(brush,
                        legendX, y, 14, 14);
                }

                float percentage =
                    items[i].Value / total * 100f;

                string text =
                    $"{items[i].Label}: {percentage:F1}%";

                g.DrawString(text, Font,
                    Brushes.Black, legendX + 20, y);
            }
        }
    }
}
