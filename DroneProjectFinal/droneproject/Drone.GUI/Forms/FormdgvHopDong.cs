using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Drone.BUS;
using Drone.DAL.Entities;

namespace Drone.GUI.Forms
{
    public partial class FormdgvHopDong : Form
    {
        public FormdgvHopDong()
        {
            InitializeComponent();
        }
        private readonly ContractService contractService = new ContractService();
    
        private void BindGrid(List<Contract_> listContract)
        {
            dgvHD.Rows.Clear();
            foreach (var contract in listContract)
            {
                int index = dgvHD.Rows.Add();
                dgvHD.Rows[index].Cells[0].Value = contract.ContractID;
                dgvHD.Rows[index].Cells[1].Value = contract.NgayLap;
                dgvHD.Rows[index].Cells[2].Value = contract.TotalValue;
                dgvHD.Rows[index].Cells[3].Value = contract.CustomerID;
                dgvHD.Rows[index].Cells[4].Value = contract.TechnicianTeamID;
                dgvHD.Rows[index].Cells[5].Value = contract.PromotionID;
                dgvHD.Rows[index].Cells[6].Value = contract.Request;
            }
        }
        public void setGridViewStyle(DataGridView dgview)
        {
            //dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DeepPink;
            dgview.BackgroundColor = Color.LavenderBlush;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void FormdgvHopDong_Load(object sender, EventArgs e)
        {
            var contracts = contractService.GetAll();
            BindGrid(contracts);
            setGridViewStyle(dgvHD);
        }
    }
}
