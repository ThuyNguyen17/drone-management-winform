using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Drone.BUS;
using Drone.DAL.Entities;

namespace Drone.GUI.Forms
{
    public partial class FormAddNhanVien : Form
    {
        private TechicianService technicianService;
        private TechTeamService technicianTeamService;
        private string avatarFilePath = string.Empty;
        public event EventHandler KTVDataChanged;
        public FormAddNhanVien()
        {
            technicianService = new TechicianService();
            technicianTeamService = new TechTeamService();
            InitializeComponent();
        }
        private void OnKTVDataChanged()
        {
            KTVDataChanged?.Invoke(this, EventArgs.Empty);
        }
        private void FillTypeCombobox(List<TechnicianTeam> listTechnicianTeams)
        {
            cmbThuocTeam.DataSource = listTechnicianTeams;
            cmbThuocTeam.DisplayMember = "TechnicianTeamID";
            cmbThuocTeam.ValueMember = "TechnicianTeamID";
        }

        private void BindGrid(List<Technician> listTechnicians)
        {
            dgvKTV.Rows.Clear();
            foreach (var technician in listTechnicians)
            {
                int index = dgvKTV.Rows.Add();
                dgvKTV.Rows[index].Cells[0].Value = technician.TechnicianID;
                dgvKTV.Rows[index].Cells[1].Value = technician.Name;
                dgvKTV.Rows[index].Cells[2].Value = technician.TechnicianTeamID;
                dgvKTV.Rows[index].Cells[3].Value = technician.Role;
                dgvKTV.Rows[index].Cells[4].Value = technician.Status;
                dgvKTV.Rows[index].Cells[5].Value = technician.Image;
            }
        }

        private void SaveTechnician(bool isEdit = false)
        {
            try
            {
                if (isEdit && dgvKTV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một kỹ thuật viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenKTV.Text))
                {
                    MessageBox.Show("Tên kỹ thuật viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existingTechnician = technicianService.FindById(txtMaKTV.Text);
                if (!isEdit && existingTechnician != null)
                {
                    MessageBox.Show("Mã kỹ thuật viên đã tồn tại. Vui lòng nhập một mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var imageName = SaveAvatar(avatarFilePath, txtMaKTV.Text);

                var newTechnician = new Technician()
                {
                    TechnicianID = txtMaKTV.Text,
                    Name = txtTenKTV.Text,
                    TechnicianTeamID = cmbThuocTeam.SelectedValue.ToString(),
                    Role = txtChucVu.Text,
                    Status = txtTinhTrang.Text,
                    Image = imageName,
                };

                technicianService.InsertUpdate(newTechnician);
                MessageBox.Show(isEdit ? "Cập nhật kỹ thuật viên thành công!" : "Thêm kỹ thuật viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGrid(technicianService.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvKTV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedTechnicianID = dgvKTV.Rows[e.RowIndex].Cells[0].Value.ToString();
                var selectedTechnician = technicianService.FindById(selectedTechnicianID);

                if (selectedTechnician != null)
                {
                    txtMaKTV.Text = selectedTechnician.TechnicianID;
                    txtTenKTV.Text = selectedTechnician.Name;
                    txtChucVu.Text = selectedTechnician.Role;
                    txtTinhTrang.Text = selectedTechnician.Status;
                    cmbThuocTeam.SelectedValue = selectedTechnician.TechnicianTeamID;
                    LoadAvatar(selectedTechnicianID);
                }
            }
        }

        private void LoadAvatar(string technicianID)
        {
            string folderPath = Path.Combine(Application.StartupPath, "Images");
            var technician = technicianService.FindById(technicianID);
            if (technician != null && !string.IsNullOrEmpty(technician.Image))
            {
                string avatarFilePath = Path.Combine(folderPath, technician.Image);

                if (File.Exists(avatarFilePath))
                {
                    pcBHinhKTV.Image = Image.FromFile(avatarFilePath);
                }
                else
                {
                    pcBHinhKTV.Image = null;
                }
            }
            else
            {
                pcBHinhKTV.Image = null;
            }
        }

        private string SaveAvatar(string sourceFilePath, string technicianID)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileExtension = Path.GetExtension(sourceFilePath);
                string targetFilePath = Path.Combine(folderPath, $"{technicianID}{fileExtension}");

                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Không tìm thấy file: {sourceFilePath}");
                }

                File.Copy(sourceFilePath, targetFilePath, true);

                return $"{technicianID}{fileExtension}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu avatar: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void pcboxThem_Click(object sender, EventArgs e)
        {
            SaveTechnician(isEdit: false);
            OnKTVDataChanged();
        }

        private void pcboxSua_Click(object sender, EventArgs e)
        {
            SaveTechnician(isEdit: true);
            OnKTVDataChanged();
        }

        private void pcboxXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvKTV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một kỹ thuật viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedTechnicianID = dgvKTV.SelectedRows[0].Cells[0].Value.ToString();

                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa kỹ thuật viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    technicianService.Delete(selectedTechnicianID);
                    BindGrid(technicianService.GetAll());
                    MessageBox.Show("Xóa kỹ thuật viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnKTVDataChanged();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormAddNhanVien_Load(object sender, EventArgs e)
        {
            List<Technician> listTechnicians = technicianService.GetAll();
            List<TechnicianTeam> listTechnicianTeams = technicianTeamService.GetAll();
            FillTypeCombobox(listTechnicianTeams);
            BindGrid(listTechnicians);
            setGridViewStyle(dgvKTV);
        }
        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.DefaultCellStyle.SelectionBackColor = Color.DeepPink;
            dgview.BackgroundColor = Color.LavenderBlush;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void dgvKTV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedKTVID = dgvKTV.Rows[e.RowIndex].Cells[0].Value.ToString();
                var selectedKTV = technicianService.FindById(selectedKTVID);

                if (selectedKTV != null)
                {
                    txtMaKTV.Text = selectedKTV.TechnicianID;
                    txtTenKTV.Text = selectedKTV.Name;
                    cmbThuocTeam.Text = selectedKTV.TechnicianTeamID;
                    txtChucVu.Text = selectedKTV.Role;
                    txtTinhTrang.Text= selectedKTV.Status;
                    LoadAvatar(selectedKTVID);
                }
            }
    }

        private void btThemHinh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avatarFilePath = openFileDialog.FileName;
                    pcBHinhKTV.Image = Image.FromFile(avatarFilePath);
                }
            }
        }
    }
}
