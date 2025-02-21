using Drone.BUS;
using Drone.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Drone.GUI.Forms
{
    public partial class FormAddDrone : Form
    {
        private DroneService droneService;
        private TypeService typeService;
        private string avatarFilePath = string.Empty;
        public event EventHandler DroneDataChanged;
        public FormAddDrone()
        {
            droneService = new DroneService();
            typeService = new TypeService();
            InitializeComponent();
        }
        private void OnDroneDataChanged()
        {
            DroneDataChanged?.Invoke(this, EventArgs.Empty);
        }
        public void FormAdd_Load(object sender, EventArgs e)
        {
            List<Drone_> listDrone = droneService.GetAll();
            List<Type_> listType = typeService.GetAll();
            FillTypeCombobox(listType);
            BindGrid(listDrone);
            setGridViewStyle(dgvDrone);
        }

        private void FillTypeCombobox(List<Type_> listType)
        {
            cmbLoai.DataSource = listType;
            cmbLoai.DisplayMember = "TypeID";
            cmbLoai.ValueMember = "TypeID";
        }

        private void BindGrid(List<Drone_> listDrone)
        {
            dgvDrone.Rows.Clear();
            foreach (var item in listDrone)
            {
                int index = dgvDrone.Rows.Add();
                dgvDrone.Rows[index].Cells[0].Value = item.DroneID;
                dgvDrone.Rows[index].Cells[1].Value = item.Name;
                dgvDrone.Rows[index].Cells[2].Value = item.TypeID;
                dgvDrone.Rows[index].Cells[3].Value = item.RentalPrice;
                dgvDrone.Rows[index].Cells[4].Value = item.PurchasePrice;
                dgvDrone.Rows[index].Cells[5].Value = item.Image;
            }
        }

        private void SaveDrone(bool isEdit = false)
        {
            try
            {
                if (isEdit && dgvDrone.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một drone!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtGiaThue.Text, out int rentalPrice) || !int.TryParse(txtGiaMua.Text, out int purchasePrice))
                {
                    MessageBox.Show("Giá thuê hoặc giá mua không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existingDrone = droneService.FindById(txtMaDrone.Text);
                if (!isEdit && existingDrone != null)
                {
                    MessageBox.Show("Mã Drone đã tồn tại. Vui lòng nhập một mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var imageName = SaveAvatar(avatarFilePath, txtMaDrone.Text);

                var newDrone = new Drone_()
                {
                    DroneID = txtMaDrone.Text,
                    Name = txtTenDrone.Text,
                    TypeID = cmbLoai.SelectedValue.ToString(),
                    RentalPrice = rentalPrice,
                    PurchasePrice = purchasePrice,
                    Image = imageName, 
                };

                droneService.InsertUpdate(newDrone);
                MessageBox.Show(isEdit ? "Cập nhật drone thành công!" : "Thêm drone thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGrid(droneService.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pcboxThem_Click(object sender, EventArgs e)
        {
            SaveDrone(isEdit: false);
            OnDroneDataChanged();
        }

        private void pcboxSua_Click(object sender, EventArgs e)
        {
            SaveDrone(isEdit: true);
            OnDroneDataChanged();
        }

        private void pcboxXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDrone.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một drone để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedDroneID = dgvDrone.SelectedRows[0].Cells[0].Value.ToString();

                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa drone này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    droneService.Delete(selectedDroneID);
                    BindGrid(droneService.GetAll());
                    MessageBox.Show("Xóa drone thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnDroneDataChanged();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDrone_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedDroneID = dgvDrone.Rows[e.RowIndex].Cells[0].Value.ToString();
                var selectedDrone = droneService.FindById(selectedDroneID);

                if (selectedDrone != null)
                {
                    txtMaDrone.Text = selectedDrone.DroneID;
                    txtTenDrone.Text = selectedDrone.Name;
                    txtGiaThue.Text = selectedDrone.RentalPrice.ToString();
                    txtGiaMua.Text = selectedDrone.PurchasePrice.ToString();
                    cmbLoai.SelectedValue = selectedDrone.TypeID;
                    LoadAvatar(selectedDroneID);
                }
            }
        }
        public void setGridViewStyle(DataGridView dgview)
        {
            //dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DeepPink;
            //dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.LavenderBlush;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void btThemHinh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avatarFilePath = openFileDialog.FileName;
                    pcBHinh.Image = Image.FromFile(avatarFilePath);
                }
            }
        }

        private void LoadAvatar(string droneID)
        {
            string folderPath = Path.Combine(Application.StartupPath, "Images");
            var drone = droneService.FindById(droneID);
            if (drone != null && !string.IsNullOrEmpty(drone.Image))
            {
                string avatarFilePath = Path.Combine(folderPath, drone.Image);

                if (File.Exists(avatarFilePath))
                {
                    pcBHinh.Image = Image.FromFile(avatarFilePath);
                }
                else
                {
                    pcBHinh.Image = null;
                }
            }
            else
            {
                pcBHinh.Image = null;
            }
        }

        private string SaveAvatar(string sourceFilePath, string droneID)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileExtension = Path.GetExtension(sourceFilePath);
                string targetFilePath = Path.Combine(folderPath, $"{droneID}{fileExtension}");

                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Không tìm thấy file: {sourceFilePath}");
                }

                File.Copy(sourceFilePath, targetFilePath, true);

                return $"{droneID}{fileExtension}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu avatar: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}