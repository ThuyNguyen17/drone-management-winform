using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ML;

namespace Drone.GUI.Forms
{
    public partial class Form2 : Form
    {
        private void btnPredict_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy bán kính từ TextBox
                double radius = double.Parse(txtRadius.Text);

                // Khoảng cách tối thiểu giữa các drone
                double minDistance = double.Parse(txtDistance.Text);

                // Tính số drone cần thiết
                double circumference = 2 * Math.PI * radius;
                int numDrones = (int)(circumference / minDistance);

                // Tạo mô phỏng trong RichTextBox
                richTextBoxDisplay.Text = GenerateCircleSimulation(radius, numDrones);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateCircleSimulation(double radius, int numDrones)
        {
            StringBuilder simulation = new StringBuilder();

            int size = (int)(radius * 2 + 1); // Kích thước lưới
            char[,] grid = new char[size, size];

            // Khởi tạo lưới
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    grid[i, j] = ' ';

            // Tính toán vị trí drone
            double centerX = radius;
            double centerY = radius;
            double angleStep = 2 * Math.PI / numDrones;

            for (int i = 0; i < numDrones; i++)
            {
                double angle = i * angleStep;
                int x = (int)Math.Round(centerX + radius * Math.Cos(angle));
                int y = (int)Math.Round(centerY + radius * Math.Sin(angle));

                if (x >= 0 && x < size && y >= 0 && y < size)
                    grid[y, x] = '.'; // Đặt dấu chấm cho drone
            }

            // Chuyển lưới thành chuỗi
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    simulation.Append(grid[i, j]);
                }
                simulation.AppendLine();
            }

            return simulation.ToString();
        }
}
}
