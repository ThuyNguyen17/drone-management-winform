using Drone.BUS;
using Drone.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
namespace Drone.GUI.Forms
{ 
    public partial class FormAddHopDong : Form
    {
        public delegate void UpdateContractHandler(Contract_ contract);
        public event UpdateContractHandler OnContractUpdated;

        private readonly ContractService contractService = new ContractService();
        private readonly ContractDetailService contractDetailService = new ContractDetailService();
        private readonly DroneService droneService = new DroneService();
        private readonly CustomerService customerService = new CustomerService();
        private readonly TechTeamService techTeamService = new TechTeamService();
        private readonly PromotionService promotionService = new PromotionService();
        public FormAddHopDong()
        {
            InitializeComponent();
        }
        public FormAddHopDong(Contract_ contract = null)
        {
            InitializeComponent();
            editingContract = contract;
            this.Load += new EventHandler(FormAddHopDong_Load);
        }

        private void LoadCombo1(List<Customer> listCus)
        {
            cmbMaKH.DataSource = listCus;
            cmbMaKH.DisplayMember = "CustomerName"; 
            cmbMaKH.ValueMember = "CustomerID";
        }

        private void LoadCombo2(List<TechnicianTeam> listTech)
        {
            cmbDoiKyThuat.DataSource = listTech;
            cmbDoiKyThuat.DisplayMember = "TechnicianTeamName";
            cmbDoiKyThuat.ValueMember = "TechnicianTeamID";
        }

        private void LoadCombo3(List<Drone_> listDrones)
        {
            cmbMaDrone.DataSource = listDrones;
            cmbMaDrone.DisplayMember = "DroneName"; 
            cmbMaDrone.ValueMember = "DroneID";
        }

        private void LoadCombo4(List<Promotion> listPromotion)
        {
            cmbKhuyenMai.DataSource = listPromotion;
            cmbKhuyenMai.DisplayMember = "PromotionName";
            cmbKhuyenMai.ValueMember = "PromotionID";
        }
        private void btHuy_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            txtMaHopDong.Clear();
            cmbMaDrone.SelectedIndex = -1;
            cmbMaKH.SelectedIndex = -1;
            cmbKhuyenMai.SelectedIndex = -1;
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
            dtpLap.Value = DateTime.Now;
        }
        private bool ValidateContractInput(out string errorMessage)
        {
            errorMessage = "";

            if (txtMaHopDong.Text.Length > 5)
            {
                errorMessage = "Mã hợp đồng phải có dưới 5 ký tự.";
                return false;
            }

    
            if (droneService.FindById(cmbMaDrone.Text) == null)
            {
                errorMessage = "Không tìm thấy drone.";
                return false;
            }

            return true;
        }
        private Contract_ editingContract;
        private void FormAddHopDong_Load(object sender, EventArgs e)
        {
            try
            {
                var drones = droneService.GetAll();
                if (drones.Any())
                    LoadCombo3(drones);
                else
                    throw new Exception("Không có drone để hiển thị.");

                var customers = customerService.GetAll();
                if (customers.Any())
                    LoadCombo1(customers);
                else
                    throw new Exception("Không có khách hàng để hiển thị.");

                var techTeams = techTeamService.GetAll();
                if (techTeams.Any())
                    LoadCombo2(techTeams);
                else
                    throw new Exception("Không có đội kỹ thuật để hiển thị.");

                var promotions = promotionService.GetAll();
                if (promotions.Any())
                    LoadCombo4(promotions);
                else
                    throw new Exception("Không có chương trình khuyến mãi để hiển thị.");

                if (editingContract != null)
                {
                    txtMaHopDong.Text = editingContract.ContractID;
                    txtMaHopDong.Enabled = false;
                    dtpLap.Value = editingContract.NgayLap.GetValueOrDefault();

                    var contractDetail = contractDetailService.FindById(editingContract.ContractID);
                    if (contractDetail != null)
                    {
                        dtpStart.Value = contractDetail.NgayBatDau_.GetValueOrDefault();
                        dtpEnd.Value = contractDetail.NgayKetThuc.GetValueOrDefault();
                    }

                    cmbMaKH.SelectedValue = editingContract.CustomerID;
                    cmbKhuyenMai.SelectedValue = editingContract.PromotionID;
                    cmbDoiKyThuat.SelectedValue = editingContract.TechnicianTeamID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
     
        private void btDongY_Click(object sender, EventArgs e)
        {
            if (!ValidateContractInput(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string contractId = txtMaHopDong.Text;
                string droneId = cmbMaDrone.SelectedValue.ToString();
                string customerID = cmbMaKH.SelectedValue.ToString();
                string technicianTeamID = cmbDoiKyThuat.SelectedValue.ToString();
                string promotionID = cmbKhuyenMai.SelectedValue.ToString();
                DateTime dateLap = dtpLap.Value;
                DateTime startDate = dtpStart.Value;
                DateTime endDate = dtpEnd.Value;

                if (startDate >= endDate)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var drone = droneService.FindById(droneId);

                var existingContract = contractService.GetAll().FirstOrDefault(s => s.ContractID == contractId);
                var existingContractDetail = contractDetailService.GetAll().FirstOrDefault(s => s.ContractID == contractId && s.DroneID == droneId);

                if (existingContract != null)
                {
                    existingContract.NgayLap = dateLap;
                    existingContract.CustomerID = customerID;
                    existingContract.TechnicianTeamID = technicianTeamID;
                    existingContract.PromotionID = promotionID;

                    if (existingContractDetail != null)
                    {
                        existingContractDetail.Price = contractDetailService.CalculateTotalDaysAndPrice(droneId, startDate, endDate);
                        existingContractDetail.NgayBatDau_ = startDate;
                        existingContractDetail.NgayKetThuc = endDate;
                        contractDetailService.InsertUpdate(existingContractDetail);
                    }
                    else
                    {
                        // Nếu không tìm thấy chi tiết hợp đồng, tạo mới nó
                        var newContractDetail = new ContractDetail
                        {
                            ContractID = contractId,
                            DroneID = droneId,
                            Price = contractDetailService.CalculateTotalDaysAndPrice(droneId, startDate, endDate),
                            NgayBatDau_ = startDate,
                            NgayKetThuc = endDate
                        };
                        contractDetailService.InsertUpdate(newContractDetail);
                    }

                    contractService.InsertUpdate(existingContract);
                    MessageBox.Show("Cập nhật thông tin hợp đồng thành công!", "Thông báo");

                    OnContractUpdated?.Invoke(existingContract);
                }
                else
                {
                    var newContract = new Contract_
                    {
                        ContractID = contractId,
                        NgayLap = dateLap,
                        CustomerID = customerID,
                        PromotionID = promotionID,
                        TechnicianTeamID = technicianTeamID
                    };

                    var newContractDetail = new ContractDetail
                    {
                        ContractID = contractId,
                        DroneID = droneId,
                        Price = contractDetailService.CalculateTotalDaysAndPrice(droneId, startDate, endDate),
                        NgayBatDau_ = startDate,
                        NgayKetThuc = endDate
                    };

                    contractService.InsertUpdate(newContract);
                    contractDetailService.InsertUpdate(newContractDetail);
                    MessageBox.Show("Thêm mới hợp đồng và chi tiết thành công!", "Thông báo");

                    OnContractUpdated?.Invoke(newContract);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void txtMaHopDong_TextChanged(object sender, EventArgs e)
        {
            if (txtMaHopDong.Text.Length > 5)
            {
                lbError.Text = "Mã hợp đồng phải có dưới 5 ký tự.";
                lbError.ForeColor = Color.Red;
            }
            else
            {
                lbError.Text = string.Empty;
            }
        }

  
    }
}
