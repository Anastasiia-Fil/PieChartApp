
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PieChartApp.Models;
using PieChartApp.Controls;

namespace PieChartApp
{
    public partial class Form1 : Form
    {
        private List<ChartItem> chartData =
            new List<ChartItem>();

        private PieChartControl pieChart;

        public Form1()
        {
            InitializeComponent();

            // Create our custom chart control
            pieChart = new PieChartControl();
            pieChart.Dock = DockStyle.Fill;

            pnlChart.Controls.Add(pieChart);

            // Connect buttons to methods
            btnAdd.Click += btnAdd_Click;
            btnClear.Click += btnClear_Click;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string label = txtLabel.Text.Trim();

            // Validate user input
            if (string.IsNullOrWhiteSpace(label))
            {
                MessageBox.Show("Please enter a category name.");
                return;
            }

            if (!float.TryParse(txtValue.Text, out float value)
                || float.IsNaN(value)
                || float.IsInfinity(value)
                || value <= 0)
            {
                MessageBox.Show("Please enter a positive number.");
                return;
            }

            // Add new item to the list
            ChartItem item = new ChartItem(label, value);
            chartData.Add(item);

            // Update list and chart
            lstData.Items.Add($"{label}: {value}");
            pieChart.SetData(chartData);

            // Clear input fields
            txtLabel.Clear();
            txtValue.Clear();
            txtLabel.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            chartData.Clear();
            lstData.Items.Clear();
            pieChart.SetData(chartData);
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblCategories_Click(object sender, EventArgs e)
        {

        }
    }
}
