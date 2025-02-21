using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Drone.DAL.Entities;

namespace Drone.GUI.Forms
{
    public partial class FormCTHopDong : Form
    {
        public FormCTHopDong()
        {
            InitializeComponent();
        }

        public FormCTHopDong(Contract_ contract = null)
        {
            InitializeComponent();
            if (contract != null)
            {
                lbMaHD.Text = contract.ContractID;
                lbNgayBD.Text = contract.NgayLap?.ToString("dd/MM/yyyy") ?? "N/A";
                lbMaKH.Text = contract.CustomerID;
                lbDoiKT.Text = contract.TechnicianTeamID;
                lbThanhTien.Text = contract.TotalValue.ToString();
                lbMaKM.Text = contract.PromotionID;
            }
        }

        private void FormCTHopDong_Load(object sender, EventArgs e)
        {

        }
    }
}
