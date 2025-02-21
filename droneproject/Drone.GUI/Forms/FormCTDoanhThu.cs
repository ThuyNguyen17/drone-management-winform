using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drone.GUI.Forms
{
    public partial class FormCTDoanhThu : Form
    {

        public FormCTDoanhThu(IEnumerable<dynamic> contractDetails)
        {
            InitializeComponent();
            dataGridView1.Columns.Add("MonthYear", "Tháng/Năm");
            dataGridView1.Columns.Add("Total", "Tổng Doanh Thu");

            foreach (var detail in contractDetails)
            {
                string label = $"{detail.Month}/{detail.Year}";
                decimal total = Convert.ToDecimal(detail.TotalMoney ?? 0);

             
                string formatted= total.ToString();  
                dataGridView1.Rows.Add(label, formatted);
            }


        }

        private void FormCTDoanhThu_Load(object sender, EventArgs e)
        {

        }
    }
}
