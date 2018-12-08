﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab09
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрити програму?", "Вихід з програми",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBoxdX1.Clear();
            textBoxdX2.Clear();
            textBoxX1max.Clear();
            textBoxX1min.Clear();
            textBoxX2max.Clear();
            textBoxX2min.Clear();
            textBoxAver.Clear();

            dataGridViewFunct.Rows.Clear();
            dataGridViewFunct.Columns.Clear();
        }

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            double x1min = double.Parse(textBoxX1min.Text);
            double x1max = double.Parse(textBoxX1max.Text);
            double x2min = double.Parse(textBoxX2min.Text);
            double x2max = double.Parse(textBoxX2max.Text);
            double dx1 = double.Parse(textBoxdX1.Text);
            double dx2 = double.Parse(textBoxdX2.Text);

            int x1n = Convert.ToInt32(Math.Truncate(x1max - x1min) + 1);
            int x2n = Convert.ToInt32(Math.Truncate(x2max - x2min) + 1);

            dataGridViewFunct.ColumnCount = (int)Math.Ceiling((x2max - x2min) / dx2) + 2;
            dataGridViewFunct.RowCount = (int)Math.Ceiling((x1max - x1min) / dx1) + 2;

            for (int i = 0; i < dataGridViewFunct.RowCount - 1; i++)
                dataGridViewFunct.Rows[i].HeaderCell.Value = (x1min + i * dx1).ToString("0.000");
            dataGridViewFunct.RowHeadersWidth = 80;
            for (int i = 0; i < dataGridViewFunct.ColumnCount - 1; i++)
            {
                dataGridViewFunct.Columns[i].HeaderCell.Value = (x2min + i * dx2).ToString("0.000");
                dataGridViewFunct.Columns[i].Width = 60;
            }

            dataGridViewFunct.Rows[dataGridViewFunct.RowCount - 1].HeaderCell.Value = "Середнє";
            dataGridViewFunct.Columns[dataGridViewFunct.ColumnCount - 1].HeaderCell.Value = "Середнє";

            dataGridViewFunct.AutoResizeColumns();
            dataGridViewFunct.AutoResizeRows();
            int cl, rw;
            double x1, x2, y, sum = 0;

            double[] sumRows = new double[dataGridViewFunct.RowCount - 1],
                    sumColumns = new double[dataGridViewFunct.ColumnCount - 1];

            rw = 0;
            x1 = x1min;
            while (x1 <= x1max)
            {
                x2 = x2min;
                cl = 0;
                while (x2 <= x2max)
                {
                    y = Function(x1, x2);
                    if (y < 0)
                        sum += y;
                    sumColumns[cl] += y / x1n;
                    sumRows[rw] += y / x2n;
                    dataGridViewFunct.Rows[rw].Cells[cl].Value = y.ToString("0.000");
                    x2 += dx2;
                    cl++;
                }
                x1 += dx1;
                rw++;
            }

            textBoxAver.Text = (sum / (x1n * x2n)).ToString("0.000");

            for (int i = 0; i < dataGridViewFunct.ColumnCount - 1; i++)
            {
                dataGridViewFunct.Rows[dataGridViewFunct.RowCount - 1].Cells[i].Value = sumColumns[i];
            }
            for (int i = 0; i < dataGridViewFunct.RowCount - 1; i++)
            {
                dataGridViewFunct.Rows[i].Cells[dataGridViewFunct.ColumnCount - 1].Value = sumRows[i];
            }
        }

        private double Function(double x1, double x2)
        {
            return (0.1 * x1 * Math.Sin(x2) * Math.Pow(Math.Sin(x1), 4)) + 55;
        }

    }
}
