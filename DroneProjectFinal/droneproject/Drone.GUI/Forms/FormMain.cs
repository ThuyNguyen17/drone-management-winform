using Drone.BUS;
using Drone.DAL.Entities;
using Drone.GUI.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;
using Drone.DAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text.RegularExpressions;
namespace Drone.GUI.Forms
{
    public partial class FormMain : MaterialForm
    {
        private readonly TypeService typeService = new TypeService();
        private readonly DroneService droneService = new DroneService();
        private readonly CustomerService customerService = new CustomerService();
        private readonly ContractDetailService contractDetailService = new ContractDetailService();
        private readonly ContractService contractService = new ContractService();
        private readonly FeedBackService feedBackService = new FeedBackService();
        private readonly PromotionService promotionService = new PromotionService();
        private readonly TechicianService tech = new TechicianService();
        private readonly TechnicianTeam techteam = new TechnicianTeam();
        private readonly PenaltyTicketService penaltyTicketService = new PenaltyTicketService();
        public FormMain()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Pink200,
                Primary.Pink300,
                Primary.Pink400,
                Accent.Pink400,
                TextShade.WHITE
            );
        }
        public FormMain(string username)
        {
            InitializeComponent();
        }
        private void materialCardLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Đã đăng xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                var loginForm = new frmLogin();
                loginForm.Show();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString("HH:mm:ss tt");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                tabPageTrangChu.BackColor = Color.FromArgb(252, 209, 209);

                //label20.Text = DateTime.Now.ToString("dd/MM/yyyy");

                lbTongTien.Text = $"{contractService.GetTotalContractMoney()} VND";
                lbSoHopDong.Text = $"{contractService.GetTotalContractsCount()}";
                lbKetquaKH.Text = $"{customerService.GetAll().Count()}";
                LoadDrone();
                LoadHopDong();
                LoadCardFeedBack();
                LoadKithuatvien(); // Load danh sách kỹ thuật viên
                loadcombox_reuqest();
                var listCus = customerService.GetAll();
                BindGridCustomer(listCus);
                setGridViewStyle(dgvCustomer);
                // kiểu lúc đầu sẽ còn load cái thống kê lên 
                // kiểu đặt cái tổng doanh thu thống kê ở form load nên nếu xóa hợp đồng nó ko load lại lần nữa 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.reportViewer1.RefreshReport();
        }
        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.DefaultCellStyle.SelectionBackColor = Color.DeepPink;
            dgview.BackgroundColor = Color.LavenderBlush;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        //---------------------------------------------------------
        // tabpage Quản Lý Kỹ Thuật Viên 
        //---------------------------------------------------------

        private Panel CreateTechCard(Technician technician)
        {
            var card = new RoundedPanel
            {
                Width = 230,
                Height = 130,
                CornerRadius = 30,
                FillColor = Color.Pink,
                BorderColor = Color.Pink,
                BorderWidth = 2,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };
            card.Click += (s, e) => HighlightSelectedCard2(card, technician.TechnicianID); // Gọi HighlightSelectedCard2 khi nhấn vào card
            card.MouseEnter += (s, e) =>
            {
                card.FillColor = ColorTranslator.FromHtml("#667BC6");
                card.Invalidate();
            };

            card.MouseLeave += (s, e) =>
            {
                card.FillColor = Color.Pink;
                card.Invalidate();
            };

            Label lblKTVID = new Label
            {
                Name = "lblKTVID",
                Text = technician.TechnicianID,
                Font = new System.Drawing.Font("Arial", 14, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#402B3A"),
                BackColor = Color.Transparent,
                Location = new Point(15, 15),
                AutoSize = true
            };
            card.Controls.Add(lblKTVID);

            Label lblKTVName = new Label
            {
                Name = "lblKTVName",
                Text = $"Tên KTV: {tech.FindById(technician.TechnicianID)?.Name}",
                Font = new System.Drawing.Font("Arial", 12, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#89375F"),
                BackColor = Color.Transparent,
                Location = new Point(15, 45),
                AutoSize = true
            };
            card.Controls.Add(lblKTVName);

            Label lblRole = new Label
            {
                Name = "lblRole",
                Text = $"Role: {technician.Role}",
                Font = new System.Drawing.Font("Arial", 15, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#FDFFD2"),
                BackColor = Color.Transparent,
                Location = new Point(15, 70),
                AutoSize = true
            };
            card.Controls.Add(lblRole);

            Label lblStatus = new Label
            {
                Name = "lblStatus",
                Text = $"Status: {technician.Status}",
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#69247C"),
                BackColor = Color.Transparent,
                Location = new Point(18, 100),
                AutoSize = true
            };
            card.Controls.Add(lblStatus);
            return card;
        }

        private void HighlightSelectedCard2(Panel card, string techId)
        {
            try
            {
                foreach (Control control in fcontrol_ktv.Controls)
                {
                    if (control is RoundedPanel panel)
                    {
                        panel.FillColor = (panel == card) ? Color.LightBlue : Color.Pink;
                        panel.BorderColor = (panel == card) ? Color.Blue : Color.Pink;
                        panel.Invalidate();
                    }
                }

                var technician = tech.FindById(techId);
                if (technician == null)
                {
                    MessageBox.Show($"Không tìm thấy kỹ thuật viên với ID: {techId}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ShowDetailTechnician(techId); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm nổi bật thẻ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKithuatvien()
        {
            fcontrol_ktv.Controls.Clear(); 
            try
            {
                List<Technician> technicianList = tech.GetAll(); 

                if (technicianList.Count == 0)
                {
                    MessageBox.Show("Không có kỹ thuật viên nào để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Panel firstCard = null;
                foreach (var technician in technicianList)
                {
                    Panel technicianCard = CreateTechCard(technician); 
                    fcontrol_ktv.Controls.Add(technicianCard);

                    if (firstCard == null)
                    {
                        firstCard = technicianCard; 
                    }
                }

                if (firstCard != null)
                {
                    HighlightSelectedCard2(firstCard, technicianList[0].TechnicianID); 
                }

                Panel detailTechnician = new RoundedPanel
                {
                    Width = 320,
                    Height = 419,
                    CornerRadius = 30,
                    FillColor = Color.Pink,
                    BorderColor = Color.Pink,
                    BorderWidth = 2,
                    Margin = new Padding(10),
                    Location = new Point(540, 90)
                };

                tabPage1.Controls.Add(detailTechnician);

                lbTongKTV.Text = technicianList.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải danh sách kỹ thuật viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDetailTechnician(string techId)
        {
            try
            {
                var technician = tech.FindById(techId);
                if (technician == null)
                {
                    MessageBox.Show("Không tìm thấy kỹ thuật viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lbMaKTV.Text = technician.TechnicianID ?? "N/A";
                lbTenKTV.Text = technician.Name ?? "N/A";
                lbThuocTeam.Text = technician.TechnicianTeamID ?? "N/A";
                lbchucvu.Text = technician.Role ?? "N/A";
                LoadAvatarTech(techId); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị chi tiết kỹ thuật viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAvatarTech(string techid)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");
                var technician = tech.FindById(techid);

                if (technician != null && !string.IsNullOrEmpty(technician.Image))
                {
                    string avatarFilePath = Path.Combine(folderPath, technician.Image);

                    if (File.Exists(avatarFilePath))
                    {
                        using (FileStream fs = new FileStream(avatarFilePath, FileMode.Open, FileAccess.Read))
                        {
                            pcBKTV.Image = System.Drawing.Image.FromStream(fs);
                        }
                    }
                    else
                    {
                        MessageBox.Show("File ảnh không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        pcBKTV.Image = null;
                    }
                }
                else
                {
                    pcBKTV.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pcBKTV.Image = null;
            }
        }

        
        private void txtTkKTV_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTkKTV.Text.ToLower();
            FilterCards3(searchText);
        }
        private void FilterCards3(string searchText)
        {
            searchText = searchText?.ToLower() ?? string.Empty;
            int visibleDroneCount = 0;
            foreach (Control ctrl in fcontrol_ktv.Controls)
            {
                if (ctrl is Panel card)
                {
                    var lblKTVID = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblKTVID");
                    var lblKTVName = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblKTVName");
                    var lblRole = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblRole");
                    var lblStatus = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblStatus");
                    bool matchesSearch = (lblKTVID != null && lblKTVID.Text.ToLower().Contains(searchText)) ||
                                                    (lblKTVName != null && lblKTVName.Text.ToLower().Contains(searchText)) ||
                                                   (lblRole != null && lblRole.Text.ToLower().Contains(searchText)) || (lblStatus != null && lblStatus.Text.ToLower().Contains(searchText));

                    card.Visible = matchesSearch;
                    if (matchesSearch)
                    {
                        visibleDroneCount++;
                    }
                }
            }
            lbTongKTV.Text = $"{visibleDroneCount}";
        }
        private void pcBThemXoaKTV_Click(object sender, EventArgs e)
        {
            FormAddNhanVien frmAddNhanVien = new FormAddNhanVien();
            frmAddNhanVien.KTVDataChanged += FrmAdd_KTVDataChanged;
            frmAddNhanVien.ShowDialog();
        }
        private void FrmAdd_KTVDataChanged(object sender, EventArgs e)
        {
            LoadKithuatvien();
        }
        //---------------------------------------------------------
        // tabpage Quản Lý Khách Hàng
        //---------------------------------------------------------

        private void BindGridCustomer(List<Customer> listCustomer)
        {
            dgvCustomer.Rows.Clear();
            foreach (var customer in listCustomer)
            {
                int index = dgvCustomer.Rows.Add();
                dgvCustomer.Rows[index].Cells["CustomerID"].Value = customer.CustomerID;
                dgvCustomer.Rows[index].Cells["NameCustomer"].Value = customer.Name;
                dgvCustomer.Rows[index].Cells["Type"].Value = customer.Type;
                dgvCustomer.Rows[index].Cells["Phone"].Value = customer.Phone;
                dgvCustomer.Rows[index].Cells["Email"].Value = customer.Email;
                dgvCustomer.Rows[index].Cells["Address"].Value = customer.Address;
            }
            lbKetquaKH.Text = $"{listCustomer.Count}";
        }

        private void ResetKhachHang_Click(object sender, EventArgs e)
        {
            var allCustomers = customerService.GetAll();
            BindGridCustomer(allCustomers);
            lbKetquaKH.Text = $"{allCustomers.Count}";
        }

        private void btTimKiemKH_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtTimKiemKH.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchKeyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var filteredList = customerService.SearchCustomers(searchKeyword);

                if (filteredList.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                BindGridCustomer(filteredList);
                lbKetquaKH.Text = $"{filteredList.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvCustomer.Rows.Count)
            {
                var selectedRow = dgvCustomer.Rows[e.RowIndex];
                selectedCustomerID = selectedRow.Cells["CustomerID"].Value.ToString();
                selectedCustomerName = selectedRow.Cells["NameCustomer"].Value.ToString();
                selectedCustomerType = selectedRow.Cells["Type"].Value.ToString();
                selectedCustomerPhone = selectedRow.Cells["Phone"].Value.ToString();
                selectedCustomerEmail = selectedRow.Cells["Email"].Value.ToString();
                selectedCustomerAddress = selectedRow.Cells["Address"].Value.ToString();
            }
        }

        private void tabPage4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowContextMenu((Control)sender, e.Location);
            }
        }

        private void ShowContextMenu(Control control, Point location)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Clear();

            ToolStripMenuItem AddItem = new ToolStripMenuItem("Thêm hợp đồng");
            AddItem.Click += (s, e) => picBoxThem_Click(s, e);
            contextMenu.Items.Add(AddItem);

            ToolStripMenuItem delete = new ToolStripMenuItem("Xóa hợp đồng");
            delete.Click += (s, e) => picBoxXoa_Click(s, e);
            contextMenu.Items.Add(delete);

            ToolStripMenuItem SuaItem = new ToolStripMenuItem("Sửa hợp đồng");
            SuaItem.Click += (s, e) => picBoxSua_Click(s, e);
            contextMenu.Items.Add(SuaItem);

            contextMenu.Show(control, location);
        }


        private void dgvCustomer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvCustomer.HitTest(e.X, e.Y);
                if (hitTest.Type == DataGridViewHitTestType.Cell)
                {
                    dgvCustomer.ClearSelection();
                    dgvCustomer.Rows[hitTest.RowIndex].Selected = true;
                    ShowContextMenu(dgvCustomer, e.Location);
                }
            }
        }

        private void picBoxThem_Click(object sender, EventArgs e)
        {
            FormAddKhachHang formAdd = new FormAddKhachHang();
            formAdd.OnCustomerAdded += (customer) =>
            {
                customerService.InsertUpdate(customer);
                var allCustomers = customerService.GetAll();
                BindGridCustomer(allCustomers);
                HighlightRowInGrid(dgvCustomer.Rows.Count - 1);
            };
            formAdd.ShowDialog();
        }

        private void picBoxXoa_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var customerID = dgvCustomer.SelectedRows[0].Cells["CustomerID"].Value.ToString();

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                customerService.Delete(customerID);
                var allCustomers = customerService.GetAll();
                BindGridCustomer(allCustomers);
                MessageBox.Show("Khách hàng đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dgvCustomer.Rows.Count > 0)
                {
                    int nextRowIndex = Math.Min(dgvCustomer.SelectedRows[0]?.Index ?? 0, dgvCustomer.Rows.Count - 1);
                    HighlightRowInGrid(nextRowIndex);
                }
            }
        }
        string selectedCustomerID, selectedCustomerName, selectedCustomerType, selectedCustomerPhone, selectedCustomerEmail, selectedCustomerAddress;
        private void picBoxSua_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvCustomer.SelectedRows[0];
            selectedCustomerID = selectedRow.Cells["CustomerID"].Value?.ToString();
            selectedCustomerName = selectedRow.Cells["NameCustomer"].Value?.ToString();
            selectedCustomerType = selectedRow.Cells["Type"].Value?.ToString();
            selectedCustomerPhone = selectedRow.Cells["Phone"].Value?.ToString();
            selectedCustomerEmail = selectedRow.Cells["Email"].Value?.ToString();
            selectedCustomerAddress = selectedRow.Cells["Address"].Value?.ToString();

            FormAddKhachHang formAdd = new FormAddKhachHang();
            formAdd.SetCustomerData(selectedCustomerID, selectedCustomerName, selectedCustomerType, selectedCustomerPhone, selectedCustomerEmail, selectedCustomerAddress);
            formAdd.OnCustomerAdded += (customer) =>
            {
                customerService.InsertUpdate(customer);

                selectedRow.Cells["NameCustomer"].Value = customer.Name;
                selectedRow.Cells["Type"].Value = customer.Type;
                selectedRow.Cells["Phone"].Value = customer.Phone;
                selectedRow.Cells["Email"].Value = customer.Email;
                selectedRow.Cells["Address"].Value = customer.Address;
                HighlightRowInGrid(selectedRow.Index);
            };
            formAdd.ShowDialog();
        }
        private void HighlightRowInGrid(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dgvCustomer.Rows.Count)
            {
                dgvCustomer.Rows[rowIndex].Selected = true;
                dgvCustomer.CurrentCell = dgvCustomer.Rows[rowIndex].Cells[0];
            }
        }

        //---------------------------------------------------------
        // tabpage Quản Lý drone
        //---------------------------------------------------------

        private Panel CreateDroneCard(Drone_ drone)
        {
            var card = new RoundedPanel
            {
                Width = 210,
                Height = 110,
                CornerRadius = 30,
                FillColor = Color.Pink,
                BorderColor = Color.Pink,
                BorderWidth = 2,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };

            card.Click += (s, e) => HighlightSelectedCard1(card, drone.DroneID);
            card.MouseEnter += (s, e) =>
            {
                card.FillColor = ColorTranslator.FromHtml("#667BC6");
                card.Invalidate();
            };

            card.MouseLeave += (s, e) =>
            {
                card.FillColor = Color.Pink;
                card.Invalidate();
            };

            Label lblDroneName = new Label
            {
                Name = "lblDroneName",
                Text = $"Tên: {drone.Name}",
                Font = new System.Drawing.Font("Arial", 12, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#402B3A"),
                BackColor = Color.Transparent,
                Location = new Point(15, 15),
                AutoSize = true
            };
            card.Controls.Add(lblDroneName);

            Label lblPrice = new Label
            {
                Name = "lblPrice",
                Text = $"Giá: {drone.RentalPrice} VND",
                Font = new System.Drawing.Font("Arial", 16, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#F8F4EC"),
                BackColor = Color.Transparent,
                Location = new Point(13, 40),
                AutoSize = true
            };
            card.Controls.Add(lblPrice);

            Label lblType = new Label
            {
                Name = "lblType",
                Text = $"Mã Loại: {drone.TypeID}",
                Font = new System.Drawing.Font("Arial", 12, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#69247C"),
                BackColor = Color.Transparent,
                Location = new Point(17, 70),
                AutoSize = true
            };
            card.Controls.Add(lblType);

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(card, "Nhấn để xem chi tiết về drone này");

            return card;
        }

        private void HighlightSelectedCard1(Panel card, string droneid)
        {
            foreach (RoundedPanel panel in flpDrone.Controls)
            {
                if (panel == card)
                {
                    panel.FillColor = Color.LightBlue;
                    panel.BorderColor = Color.Blue;
                }
                else
                {
                    panel.FillColor = Color.Pink;
                    panel.BorderColor = Color.Pink;
                }

                panel.Invalidate();
            }
            ShowDetailDrone(droneid);
        }

        private void ShowDetailDrone(string droneid)
        {
            if (string.IsNullOrEmpty(droneid))
            {
                MessageBox.Show("DroneID không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var drone = droneService.FindById(droneid);
            if (drone == null)
            {
                MessageBox.Show("Không tìm thấy thông tin drone!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var type = typeService.GetById(drone.TypeID);
            if (type == null)
            {
                MessageBox.Show("Không tìm thấy thông tin loại drone!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lbMaDrone1.Text = drone.DroneID;
            lbTenDrone.Text = drone.Name;
            lbMaLoaiDrone.Text = drone.TypeID;
            lbTenLoai.Text = type.TypeName;
            lbGiaBan.Text = $"{drone.PurchasePrice} VND";
            lbGiaThue.Text = $"{drone.RentalPrice} VND";
            LoadAvatar(droneid);
        }
        private void LoadAvatar(string droneID)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");
                var drone = droneService.FindById(droneID);

                if (drone != null && !string.IsNullOrEmpty(drone.Image))
                {
                    string avatarFilePath = Path.Combine(folderPath, drone.Image);

                    if (File.Exists(avatarFilePath))
                    {
                        using (FileStream fs = new FileStream(avatarFilePath, FileMode.Open, FileAccess.Read))
                        {
                            pcBDrone.Image = System.Drawing.Image.FromStream(fs);
                        }
                    }
                    else
                    {
                        MessageBox.Show("File ảnh không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        pcBDrone.Image = null;
                    }
                }
                else
                {
                    pcBDrone.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pcBDrone.Image = null;
            }
        }
        private void LoadDrone()
        {
            flpDrone.Controls.Clear();
            try
            {
                List<Drone_> droneList = droneService.GetAll();

                if (droneList.Count == 0)
                {
                    MessageBox.Show("Không có drone nào để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Panel firstCard = null;

                foreach (var drone in droneList)
                {
                    Panel droneCard = CreateDroneCard(drone);
                    flpDrone.Controls.Add(droneCard);

                    if (firstCard == null)
                    {
                        firstCard = droneCard;
                    }
                }

                if (firstCard != null)
                {
                    HighlightSelectedCard1(firstCard, droneList[0].DroneID);
                }

                lbTongDrone.Text = droneList.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải drone: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pcBThemSuaXoaDrone_Click(object sender, EventArgs e)
        {
            FormAddDrone frmAddDrone = new FormAddDrone();
            // Đăng ký sự kiện
            frmAddDrone.DroneDataChanged += FrmAddDrone_DroneDataChanged;
            frmAddDrone.ShowDialog();

        }
        private void FrmAddDrone_DroneDataChanged(object sender, EventArgs e)
        {
            LoadDrone();
        }
        private void txtTKDrone_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTKDrone.Text.ToLower();
            FilterCards2(searchText);
        }
        private void FilterCards2(string searchText)
        {
            searchText = searchText?.ToLower() ?? string.Empty;
            int visibleDroneCount = 0;

            foreach (Control ctrl in flpDrone.Controls)
            {
                if (ctrl is Panel card)
                {
                    var lblDroneName = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblDroneName");
                    var lblPrice = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblPrice");
                    var lblType = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblType");
                    bool matchesSearch = (lblDroneName != null && lblDroneName.Text.ToLower().Contains(searchText)) ||
                                                    (lblType != null && lblType.Text.ToLower().Contains(searchText)) ||
                                                    (lblPrice != null && lblPrice.Text.ToLower().Contains(searchText));

                    card.Visible = matchesSearch;
                    if (matchesSearch)
                    {
                        visibleDroneCount++;
                    }
                }
            }
            lbTongDrone.Text = $"{visibleDroneCount}";
        }


        //---------------------------------------------------------
        // tabPage Quản Lý Hợp Đồng
        //---------------------------------------------------------

        private void panelThemHD_Click(object sender, EventArgs e)
        {
            FormAddHopDong formHopDong = new FormAddHopDong();
            formHopDong.OnContractUpdated += (newContract) =>
            {
                Panel contractCard = CreateContractCard(newContract);

                contractCard.Click += (s, args) =>
                {
                    currentSelectedContract = newContract;
                    HighlightSelectedCard(contractCard);
                };
                flpHopDong.Controls.Add(contractCard);
                lbTongContract.Text = flpHopDong.Controls.Count.ToString();
            };
            formHopDong.ShowDialog();
        }

        private void panelSuaHD_Click(object sender, EventArgs e)
        {
            if (currentSelectedContract == null)
            {
                MessageBox.Show("Chọn một hợp đồng để sửa.", "Chưa chọn hợp đồng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Contract_ updatedContract = contractService.GetAll().FirstOrDefault(c => c.ContractID == currentSelectedContract.ContractID);

            if (updatedContract == null)
            {
                MessageBox.Show("Hợp đồng không tìm thấy trong database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormAddHopDong formHopDong = new FormAddHopDong(updatedContract);

            formHopDong.OnContractUpdated += (contract) =>
            {
                currentSelectedContract = contract;
                LoadHopDong();
            };
            formHopDong.ShowDialog();
        }
        private void panelXoaHD_Click(object sender, EventArgs e)
        {
            if (currentSelectedContract == null)
            {
                MessageBox.Show("Chọn một hợp đồng cần xóa.", "Chưa chọn hợp đồng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show($"Bạn có chắc muốn xóa hợp đồng {currentSelectedContract.ContractID}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                contractService.Delete(currentSelectedContract.ContractID);
                var cardToRemove = flpHopDong.Controls
                    .OfType<Panel>()
                    .FirstOrDefault(card =>
                        card.Controls.OfType<Label>().Any(lbl => lbl.Name == "lblContractID" && lbl.Text == currentSelectedContract.ContractID)); // Sử dụng currentSelectedContract

                if (cardToRemove != null)
                {
                    flpHopDong.Controls.Remove(cardToRemove);
                    lbTongContract.Text = flpHopDong.Controls.Count.ToString();
                }
                currentSelectedContract = null;
                MessageBox.Show("Hợp đồng đã được xóa thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTimKiemHD_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimKiemHD.Text.ToLower();
            FilterCards(searchText);
        }
        private void FilterCards(string searchText)
        {
            searchText = searchText?.ToLower() ?? string.Empty;
            int visibleCardCount = 0;
            foreach (Control ctrl in flpHopDong.Controls)
            {
                if (ctrl is Panel card)
                {
                    var lblContractID = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblContractID");
                    var lblTotal = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblTotal");
                    var lblTime = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblTime");
                    var lblCustomerName = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblCustomerName");
                    bool matchesSearch = (lblContractID != null && lblContractID.Text.ToLower().Contains(searchText)) ||
                                                    (lblTotal != null && lblTotal.Text.ToLower().Contains(searchText)) ||
                                                    (lblTime != null && lblTime.Text.ToLower().Contains(searchText)) ||
                                                    (lblCustomerName != null && lblCustomerName.Text.ToLower().Contains(searchText));

                    card.Visible = matchesSearch;
                    if (matchesSearch)
                    {
                        visibleCardCount++;
                    }
                }
            }

            lbTongContract.Text = visibleCardCount.ToString();
        }
        private Contract_ currentSelectedContract = null;

        private void LoadHopDong()
        {
            flpHopDong.Controls.Clear();
            try
            {
                List<Contract_> contractList = contractService.GetAll();

                if (contractList.Count == 0)
                {
                    MessageBox.Show("Không có hợp đồng nào để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbTongContract.Text = "0";
                    return;
                }

                foreach (var contract in contractList)
                {
                    Panel contractCard = CreateContractCard(contract);
                    contractCard.Click += (s, e) =>
                    {
                        currentSelectedContract = contract;
                        HighlightSelectedCard(contractCard);
                    };
                    flpHopDong.Controls.Add(contractCard);
                }
                lbTongContract.Text = contractList.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải hợp đồng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HighlightSelectedCard(Panel card)
        {
            foreach (RoundedPanel panel in flpHopDong.Controls)
            {
                if (panel == card)
                {
                    panel.FillColor = Color.LightBlue;
                    panel.BorderColor = Color.Blue;
                }
                else
                {
                    panel.FillColor = Color.Pink;
                    panel.BorderColor = Color.Pink;
                }

                panel.Invalidate();
            }
        }

        private Panel CreateContractCard(Contract_ contract)
        {
            var card = new RoundedPanel
            {
                Width = 250,
                Height = 130,
                CornerRadius = 30,
                FillColor = Color.Pink,
                BorderColor = Color.Pink,
                BorderWidth = 2,
                Margin = new Padding(20),
                Cursor = Cursors.Hand
            };

            card.MouseEnter += (s, e) =>
            {
                card.FillColor = ColorTranslator.FromHtml("#667BC6");
                card.Invalidate();
            };

            card.MouseLeave += (s, e) =>
            {
                card.FillColor = Color.Pink;
                card.Invalidate();
            };

            Label lblContractID = new Label
            {
                Name = "lblContractID",
                Text = contract.ContractID,
                Font = new System.Drawing.Font("Arial", 14, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#402B3A"),
                BackColor = Color.Transparent,
                Location = new Point(15, 15),
                AutoSize = true
            };
            card.Controls.Add(lblContractID);

            Label lblCustomerName = new Label
            {
                Name = "lblCustomerName",
                Text = $"Tên KH: {customerService.FindById(contract.CustomerID)?.Name}",
                Font = new System.Drawing.Font("Arial", 12, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#89375F"),
                BackColor = Color.Transparent,
                Location = new Point(15, 45),
                AutoSize = true
            };
            card.Controls.Add(lblCustomerName);

            Label lblTotal = new Label
            {
                Name = "lblTotal",
                Text = $"Giá: {contract.TotalValue:N0} VND",
                Font = new System.Drawing.Font("Arial", 15, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#FDFFD2"),
                BackColor = Color.Transparent,
                Location = new Point(15, 70),
                AutoSize = true
            };
            card.Controls.Add(lblTotal);

            Label lblTime = new Label
            {
                Name = "lblTime",
                Text = $"Ngày: {contract.NgayLap:dd/MM/yyyy}",
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#69247C"),
                BackColor = Color.Transparent,
                Location = new Point(18, 100),
                AutoSize = true
            };
            card.Controls.Add(lblTime);

            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(card, "Nhấn đúp chuột để xem chi tiết về hợp đồng này");
            card.MouseDoubleClick += (s, e) =>
            {
                var detailedContract = contractService.FindById(contract.ContractID);

                if (detailedContract != null)
                {
                    FormCTHopDong formCTHopDong = new FormCTHopDong(detailedContract);
                    formCTHopDong.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin hợp đồng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            return card;
        }
        private void ExportToPdf()
        {
            if (currentSelectedContract == null)
            {
                MessageBox.Show("Chọn một hợp đồng để in hóa đơn.", "No Contract Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Document doc = new Document(PageSize.A4, 50, 50, 50, 50);

                string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Contracts");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, $"{currentSelectedContract.ContractID}_Contract.pdf");

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfWriter.GetInstance(doc, fs);

                    doc.Open();

                    // Load Vietnamese font
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");
                    BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 12);

                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);
                    Paragraph title = new Paragraph("Contract Detail", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(title);
                    doc.Add(new Paragraph(" ", font));
                    doc.Add(new Paragraph(" ", font));

                    Paragraph contractId = new Paragraph($"Contract ID: {currentSelectedContract.ContractID}", font);


                    doc.Add(contractId);

                    var customer = customerService.FindById(currentSelectedContract.CustomerID);
                    Paragraph customerName = new Paragraph($"Customer Name: {customer?.Name ?? "Unknown"}", font);


                    doc.Add(customerName);

                    Paragraph date = new Paragraph($"Date: {currentSelectedContract.NgayLap:dd/MM/yyyy}", font);


                    doc.Add(date);

                    Paragraph technicalTeam = new Paragraph($"Technical Team: {currentSelectedContract.TechnicianTeamID}", font);


                    doc.Add(technicalTeam);

                    Paragraph request = new Paragraph($"Request: {currentSelectedContract.Request}", font);


                    doc.Add(request);
                    doc.Add(new Paragraph(" ", font));

                    // Contract Items Table
                    PdfPTable table = new PdfPTable(3);
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Drone", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Thuê Ngày", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase("Price", font)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    var contractDetails = contractDetailService.GetByContractId(currentSelectedContract.ContractID);
                    foreach (var item in contractDetails)
                    {
                        table.AddCell(new PdfPCell(new Phrase(item.DroneID, font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase($"{item.NgayBatDau_:dd/MM/yyyy} - {item.NgayKetThuc:dd/MM/yyyy}", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                        table.AddCell(new PdfPCell(new Phrase($"{item.Price:N0} VND", font)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    }
                    doc.Add(table);
                    doc.Add(new Paragraph(" ", font));
                    doc.Add(new Paragraph(" ", font));
                    var promotion = promotionService.FindById(currentSelectedContract.PromotionID);
                    if (promotion != null)
                    {
                        Paragraph promotionDescription = new Paragraph($"Promotion: {promotion.Description}", font);


                        doc.Add(promotionDescription);

                        Paragraph discountRate = new Paragraph($"Discount Rate: {promotion.DiscountRate / 100:P0}", font);


                        doc.Add(discountRate);
                    }

                    doc.Add(new Paragraph(" ", font));
                    Paragraph totalValue = new Paragraph($"Total: {currentSelectedContract.TotalValue:N0} VND", font)
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    doc.Add(totalValue);

                    doc.Close();
                }

                MessageBox.Show($"Contract {currentSelectedContract.ContractID}_Contract.pdf generated successfully!");
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while generating the contract PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InHoaDon_Click(object sender, EventArgs e)
        {
            ExportToPdf();
        }
        private void panelTableHD_Click(object sender, EventArgs e)
        {
            FormdgvHopDong formContractList = new FormdgvHopDong();
            formContractList.Show();
        }


        //---------------------------------------------------------
        // tabPageThongKe
        //---------------------------------------------------------
        private void btThongKeHD_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;
            var monthlyContract = contractService.GetMonthlyData(startDate, endDate);

            if (chartTKDon.Series["Contract"] == null)
            {
                chartTKDon.Series.Add("Contract");
                chartTKDon.Series["Contract"].ChartType = SeriesChartType.Column;
            }
            else
            {
                chartTKDon.Series["Contract"].Points.Clear();
            }

            foreach (var result in monthlyContract)
            {
                string label = $"{result.Month}/{result.Year}";
                chartTKDon.Series["Contract"].Points.AddXY(label, result.ContractCount);
            }

            dgvHopDong.Rows.Clear();
            foreach (var result in monthlyContract)
            {
                dgvHopDong.Rows.Add($"{result.Month}/{result.Year}", result.ContractCount);
            }
        }

        private void btResetThongKeHD_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = new DateTime(DateTime.Today.Year, 12, 31);
            btThongKeHD_Click(sender, e);
        }

        private void btTKDoanhThu_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker3.Value;
            DateTime endDate = dateTimePicker4.Value;
            if (startDate > endDate)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var monthlyRevenues = contractService.GetMonthlyData(startDate, endDate);
            if (chartTkDoanhThu.Series["Số Tiền"] == null)
            {
                chartTkDoanhThu.Series.Add("Số Tiền");
                chartTkDoanhThu.Series["Số Tiền"].ChartType = SeriesChartType.Column;
            }
            else
            {
                chartTkDoanhThu.Series["Số Tiền"].Points.Clear();
            }

            foreach (var result in monthlyRevenues)
            {
                string label = $"{result.Month}/{result.Year}";
                chartTkDoanhThu.Series["Số Tiền"].Points.AddXY(label, result.TotalMoney);
            }
        }

        private void btResetDoanhThu_Click(object sender, EventArgs e)
        {
            dateTimePicker3.Value = DateTime.Today;
            dateTimePicker4.Value = new DateTime(DateTime.Today.Year, 12, 31);
            btTKDoanhThu_Click(sender, e);
        }

        private void btCTDoanhThu_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker3.Value;
            DateTime endDate = dateTimePicker4.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var monthlyMoney = contractService.GetMonthlyData(startDate, endDate);

            if (monthlyMoney == null || !monthlyMoney.Any())
            {
                MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormCTDoanhThu detailForm = new FormCTDoanhThu(monthlyMoney);
            detailForm.Show();
        }

        private void btPhanTich_Click(object sender, EventArgs e)
        {
            string request = cmbYeuCau.Text;
            string contentPattern = "chiếu (.*?) trong";
            string timePattern = "trong (\\d+) phút";
            string sizePattern = "kích thước.*?tối thiểu (\\d+)m";

            // Extract content
            Match contentMatch = Regex.Match(request, contentPattern);
            string content = contentMatch.Success ? contentMatch.Groups[1].Value : "";

            // Extract time
            Match timeMatch = Regex.Match(request, timePattern);
            string time = timeMatch.Success ? timeMatch.Groups[1].Value : "";

            // Extract size
            Match sizeMatch = Regex.Match(request, sizePattern);
            string size = sizeMatch.Success ? sizeMatch.Groups[1].Value : "";

            // Set extracted values to the textboxes
            txt_noidung.Text = content;
            txt_time.Text = time ;
            txt_size.Text = size ;
            double droneArea = 1;
            double timeFactor = 1;
            double shapeAere = double.Parse(txt_size.Text);
            if (int.Parse(txt_time.Text) > 20)
            {
                droneArea = int.Parse(txt_time.Text) / 20;
            }
            txt_soluongdrone.Text = Math.Ceiling((shapeAere / droneArea) * timeFactor).ToString();

        }
        private void loadcombox_reuqest()
        {
            List<String> contractList = contractService.GetRequest();

            cmbYeuCau.DataSource = contractList;



        }
        private Point createpoint(int x, int y)
        {
            return new Point(x,y);
        }

        private void btTao_Click(object sender, EventArgs e)
        {
            Form canvasForm = new Form
            {
                Text = "Canvas",
                Size = new Size(800, 600)
            };

            // Create a PictureBox to act as the canvas
            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            // Add paint event to draw on the PictureBox
            pictureBox.Paint += (s, paintEventArgs) =>
            {
                Graphics g = paintEventArgs.Graphics;
                Pen pen = new Pen(Color.Black, 2);

                // Dynamic list to store points
                List<Point> points = new List<Point>();

                if (txt_noidung != null && !string.IsNullOrEmpty(txt_noidung.Text))
                {
                    int j = 0;
                    foreach (char c in txt_noidung.Text)
                    {
                        j++;

                        // Get coordinates for the current character
                        List<(double x, double y)> coordinates = GetCoordinates(c);
                        if (coordinates.Count > 0)
                        {
                            // Update coordinates based on character position
                            coordinates = UpdatePositionLetter(coordinates, j);

                            // Add updated coordinates to the points list
                            foreach (var coord in coordinates)
                            {
                                int xPoint = (int)(coord.x * 10);
                                int yPoint = (int)(coord.y * 10);
                                points.Add(new Point(xPoint, yPoint));
                            }
                        }
                    }
                }

                // Draw points on the canvas
                foreach (Point point in points)
                {
                    g.DrawEllipse(pen, point.X - 3, point.Y - 3, 4, 4);
                }
            };

            canvasForm.Controls.Add(pictureBox);
            canvasForm.ShowDialog();
        }

        private List<(double x, double y)> UpdatePositionLetter(List<(double x, double y)> coordinates, double j)
        {
            int length = coordinates.Count;
            for (int i = 0; i < length; i++)
            {
                // Thêm giá trị j vào x và y để cách các chữ cái với nhau
                double x = coordinates[i].x + j * 10;  // Cộng thêm j vào tọa độ x để tạo khoảng cách ngang
                double y = coordinates[i].y;           // Không thay đổi y, giữ nguyên chiều dọc

                coordinates[i] = (x, y);
            }
          
            return coordinates;
        }

        private List<(double x, double y)> GetCoordinates(char letter)
        {
            List<(double x, double y)> coordinates = new List<(double x, double y)>();
            switch (letter)
            {
                case ' ':
                    for (int i = 0; i < 8; i++)
                    {
                        double x = 10 - i * 5.0 / 7; // Từ 10 đến 5
                        double y = i * 20.0 / 7; // Từ 0 đến 20
                        coordinates.Add((x, y));
                    }
                    break;

                case 'a':
                    // Diagonal left (8 points)
                    for (int i = 0; i < 8; i++)
                    {
                        double x = i * 5.0 / 7; // Từ 0 đến 5
                        double y = i * 20.0 / 7; // Từ 0 đến 20
                        coordinates.Add((x, y));
                    }

                    // Diagonal right (8 points)
                    for (int i = 0; i < 8; i++)
                    {
                        double x = 10 - i * 5.0 / 7; // Từ 10 đến 5
                        double y = i * 20.0 / 7; // Từ 0 đến 20
                        coordinates.Add((x, y));
                    }

                    // Horizontal middle (4 points)
                    for (int i = 0; i < 4; i++)
                    {
                        double x = 2 + i * 6.0 / 3; // Từ 2 đến 8
                        double y = 10; // Always at y = 10
                        coordinates.Add((x, y));
                    }
                    break;

                case 'b':
                    // Vertical body (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 0; // Always at x = 0
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Upper half (6 points)
                    for (int i = 0; i < 6; i++)
                    {
                        double x = 2 + i * 3.0 / 5; // From 2 to 5
                        double y = 15 - i * 5.0 / 5; // From 15 to 10
                        coordinates.Add((x, y));
                    }

                    // Lower half (6 points)
                    for (int i = 0; i < 6; i++)
                    {
                        double x = 2 + i * 3.0 / 5; // From 2 to 5
                        double y = 5 - i * 5.0 / 5; // From 5 to 0
                        coordinates.Add((x, y));
                    }
                    break;

                case 'c':
                    // 'C' shape curve (12 points)
                    for (int i = 0; i < 12; i++)
                    {
                        double angle = Math.PI * (i / 11.0); // From 0 to pi
                        double x = 5 * Math.Cos(angle); // Radius = 5
                        double y = 10 + 10 * Math.Sin(angle); // Shifted up by 10 units
                        coordinates.Add((x, y));
                    }
                    break;

                case 'd':
                    // Vertical line (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 0; // Always at x = 0
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Half circle (8 points)
                    for (int i = 0; i < 8; i++)
                    {
                        double angle = Math.PI * (i / 7.0); // Half circle from 0 to pi
                        double x = 10 + 5 * Math.Cos(angle); // Radius = 5
                        double y = 10 + 5 * Math.Sin(angle); // Half circle shape
                        coordinates.Add((x, y));
                    }
                    break;

                // Repeat similar pattern for other letters (e, f, g, ...)

                case 'e':
                    // Left side of E (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 0; // Always at x = 0
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Horizontal lines of E (3 points)
                    for (int i = 0; i < 3; i++)
                    {
                        double x = 0; // Always at x = 0
                        double y = 5 + i * 5.0; // 5, 10, 15
                        coordinates.Add((x, y));
                    }
                    break;
                case 'f':
                    // Vertical line of F (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 0; // Always at x = 0
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Upper horizontal line of F (4 points)
                    for (int i = 0; i < 4; i++)
                    {
                        double x = 0; // Always at x = 0
                        double y = 0 + i * 5.0; // 0 to 15
                        coordinates.Add((x, y));
                    }

                    // Middle horizontal line of F (4 points)
                    for (int i = 0; i < 4; i++)
                    {
                        double x = 0; // Always at x = 0
                        double y = 10 + i * 5.0; // 10 to 15
                        coordinates.Add((x, y));
                    }
                    break;

                case 'g':
                    // Vertical line of G (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 0; // Always at x = 0
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Half-circle for G (12 points)
                    for (int i = 0; i < 12; i++)
                    {
                        double angle = Math.PI * (i / 11.0); // From 0 to pi
                        double x = 10 + 5 * Math.Cos(angle); // Radius = 5
                        double y = 10 + 5 * Math.Sin(angle); // Half circle shape
                        coordinates.Add((x, y));
                    }
                    break;

                case 'h':
                    // Vertical lines of H (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 0; // Left vertical line
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        double x = 10; // Right vertical line
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Horizontal middle line of H (4 points)
                    for (int i = 0; i < 4; i++)
                    {
                        double x = 0 + i * 5.0; // From 0 to 15
                        double y = 10; // Always at y = 10
                        coordinates.Add((x, y));
                    }
                    break;

                case 'i':
                    // Vertical line of I (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 5; // Always at x = 5
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }
                    break;

                case 'j':
                    // Vertical line of J (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 5; // Always at x = 5
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Half-circle for J (7 points)
                    for (int i = 0; i < 7; i++)
                    {
                        double angle = Math.PI * (i / 6.0); // From 0 to pi
                        double x = 5 + 5 * Math.Cos(angle); // Radius = 5
                        double y = 20 + 5 * Math.Sin(angle); // Half circle
                        coordinates.Add((x, y));
                    }
                    break;

                case 'k':
                    // Vertical line of K (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 0; // Always at x = 0
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Upper diagonal line of K (8 points)
                    for (int i = 0; i < 8; i++)
                    {
                        double x = i * 5.0 / 7; // From 0 to 5
                        double y = 10 + i * 10.0 / 7; // From 10 to 20
                        coordinates.Add((x, y));
                    }

                    // Lower diagonal line of K (8 points)
                    for (int i = 0; i < 8; i++)
                    {
                        double x = i * 5.0 / 7; // From 0 to 5
                        double y = 10 - i * 10.0 / 7; // From 10 to 0
                        coordinates.Add((x, y));
                    }
                    break;

                case 'l':
                    // Vertical line of L (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 0; // Always at x = 0
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Horizontal bottom line of L (4 points)
                    for (int i = 0; i < 4; i++)
                    {
                        double x = i * 5.0; // From 0 to 15
                        double y = 20; // Always at y = 20
                        coordinates.Add((x, y));
                    }
                    break;

                case 'm':
                    // Vertical lines of M (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = 0; // Left vertical line
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        double x = 10; // Right vertical line
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Diagonal lines of M (10 points)
                    for (int i = 0; i < 10; i++)
                    {
                        double x = i * 5.0 / 9; // From 0 to 5
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        double x = 10 - i * 5.0 / 9; // From 10 to 5
                        double y = i * 20.0 / 9; // From 0 to 20
                        coordinates.Add((x, y));
                    }
                    break;

                // Continue with similar cases for other letters (n, o, p, ...)

                case 'z':
                    // Diagonal lines of Z (8 points)
                    for (int i = 0; i < 8; i++)
                    {
                        double x = i * 5.0 / 7; // From 0 to 5
                        double y = i * 20.0 / 7; // From 0 to 20
                        coordinates.Add((x, y));
                    }

                    // Bottom horizontal line of Z (8 points)
                    for (int i = 0; i < 8; i++)
                    {
                        double x = 0 + i * 5.0 / 7; // From 0 to 5
                        double y = 20; // Always at y = 20
                        coordinates.Add((x, y));
                    }
                    break;

                default:
                    // Not supporting this letter
                    break;
            }
            return coordinates;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            materialTab.SelectedIndex = 1;
        }

        private void materialTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (materialTab.SelectedTab == tabPageLogout)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có muốn thoát không?",
                    "Xác nhận thoát",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        //---------------------------------------------------------
        // tabPageBaoCao
        //---------------------------------------------------------
        private void btLapBaoCao_Click(object sender, EventArgs e)
        {
         
            DateTime startDate = dtpStart.Value.Date;
            DateTime endDate = dtpEnd.Value.Date.AddDays(1).AddTicks(-1);

            var filteredData = contractService.GetAll()
                .Where(item => item.NgayLap >= startDate && item.NgayLap <= endDate)
                .ToList();

            if (rbDoanhThu.Checked)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("NgayLap", typeof(DateTime));
                dataTable.Columns.Add("ContractID", typeof(string));
                dataTable.Columns.Add("CustomerID", typeof(string));
                dataTable.Columns.Add("TechnicianTeamID", typeof(string));
                dataTable.Columns.Add("TotalValue", typeof(decimal));
                dataTable.Columns.Add("PromotionID", typeof(string));

                decimal totalValue = filteredData.Sum(item => (decimal)(item.TotalValue ?? 0));

                foreach (var item in filteredData)
                {
                    dataTable.Rows.Add(item.NgayLap, item.ContractID, item.CustomerID, item.TechnicianTeamID, item.TotalValue, item.PromotionID);
                }

                ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dataTable);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.LocalReport.ReportPath = Path.Combine(Application.StartupPath, "Report1.rdlc");

                var parameters = new ReportParameter[]
                {
                    new ReportParameter("MaNVBC", txtNhanVienBC.Text),
                    new ReportParameter("TenNVBC", txtNameBC.Text),
                    new ReportParameter("NgayLapBC", DateTime.Now.ToString("dd/MM/yyyy")),
                    new ReportParameter("TongDoanhThu", totalValue.ToString("N2"))
                };

                reportViewer1.LocalReport.SetParameters(parameters);
            }
            else if (rbHopDong.Checked)
            {
                DataTable dataTable1 = new DataTable();
                dataTable1.Columns.Add("ContractID", typeof(string));
                dataTable1.Columns.Add("CustomerID", typeof(string));
                dataTable1.Columns.Add("TechnicianTeamID", typeof(string));
                dataTable1.Columns.Add("NgayLap", typeof(DateTime));

                int totalContracts = filteredData.Count;

                foreach (var item in filteredData)
                {
                    dataTable1.Rows.Add(item.ContractID, item.CustomerID, item.TechnicianTeamID, item.NgayLap);
                }

                ReportDataSource reportDataSource = new ReportDataSource("DataSet2", dataTable1);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.LocalReport.ReportPath = Path.Combine(Application.StartupPath, "Report2.rdlc");

                var parameters = new ReportParameter[]
                {
                    new ReportParameter("MaNVBC", txtNhanVienBC.Text),
                    new ReportParameter("TenNVBC", txtNameBC.Text),
                    new ReportParameter("NgayLapBC", DateTime.Now.ToString("dd/MM/yyyy")),
                    new ReportParameter("TongSoLuong", totalContracts.ToString())
                };

                reportViewer1.LocalReport.SetParameters(parameters);
            }
            else if (rbViPhamHopDong.Checked)
            {
                var penaltyTickets = penaltyTicketService.GetAll()
                    .Where(ticket => ticket.NgayLap >= startDate && ticket.NgayLap <= endDate)
                    .ToList();


                DataTable penaltyTable = new DataTable();
                penaltyTable.Columns.Add("TicketID", typeof(string));  
                penaltyTable.Columns.Add("ContractID", typeof(string));
                penaltyTable.Columns.Add("DroneID", typeof(string));
                penaltyTable.Columns.Add("NgayLap", typeof(DateTime));
                penaltyTable.Columns.Add("PenaltyAmount", typeof(decimal));
                penaltyTable.Columns.Add("Reason_", typeof(string));  

                foreach (var ticket in penaltyTickets)
                {
                    penaltyTable.Rows.Add(ticket.TicketID, ticket.ContractID, ticket.DroneID, ticket.NgayLap, ticket.PenaltyAmount, ticket.Reason_);
                }

                ReportDataSource reportDataSource = new ReportDataSource("DataSet3", penaltyTable);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.LocalReport.ReportPath = Path.Combine(Application.StartupPath, "Report3.rdlc");

                var parameters = new ReportParameter[]
                {
                    new ReportParameter("MaNVBC", txtNhanVienBC.Text),
                    new ReportParameter("TenNVBC", txtNameBC.Text),
                    new ReportParameter("NgayLapBC", DateTime.Now.ToString("dd/MM/yyyy")),
                };

                reportViewer1.LocalReport.SetParameters(parameters);
            }

            reportViewer1.RefreshReport();
        }

        private void btResetBaoCao_Click(object sender, EventArgs e)
        {
            rbDoanhThu.Checked = false;
            rbViPhamHopDong.Checked = false;
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
            txtNhanVienBC.Clear();
            txtNameBC.Clear();

        }


        private void LoadCardFeedBack()
        {

            flpFeedback.Controls.Clear();
            var feedbacks = feedBackService.GetAll();
            foreach (var feedback in feedbacks)
            {
                flpFeedback.Controls.Add(CreateContractCard2(feedback));
            }
        }

        private Panel CreateContractCard2(Feedback feedback)
        {

            var card = new RoundedPanel
            {
                Width = 500,
                Height = 180,
                CornerRadius = 15,
                FillColor = Color.White,
                BorderColor = Color.Pink,
                BorderWidth = 2,
                Margin = new Padding(35),
                Cursor = Cursors.Hand
            };

            card.MouseEnter += (s, e) =>
            {
                card.FillColor = ColorTranslator.FromHtml("#E7CCCC");
                card.Invalidate();
            };

            card.MouseLeave += (s, e) =>
            {
                card.FillColor = Color.White;
                card.Invalidate();
            };

            card.Click += (s, e) =>
            {
                card.FillColor = ColorTranslator.FromHtml("#D9ABAB");
                card.BorderColor = Color.Red;
                card.Invalidate();
            };

            var contract = contractService.FindById(feedback.ContractID);
            var customer = contract != null ? customerService.FindById(contract.CustomerID) : null;

            PictureBox picAvatar = new PictureBox
            {
                Name = "picAvatar",
                Size = new Size(50, 50),
                Location = new Point(15, 15),
                Image = Properties.Resources.icons8_user_50__3_,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            card.Controls.Add(picAvatar);

            Label lblCustomerName = new Label
            {
                Name = "lblCustomerName",
                Text = customer != null ? $"Tên Khách Hàng: {customer.Name}" : "Tên Khách Hàng: Không xác định",
                Font = new System.Drawing.Font("Arial", 12, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#333333"),
                BackColor = Color.Transparent,
                Location = new Point(90, 35),
                AutoSize = true
            };
            card.Controls.Add(lblCustomerName);

            Label lblComment = new Label
            {
                Name = "lblComment",
                Text = $"Nhận xét: {feedback.Comment}",
                Font = new System.Drawing.Font("Arial", 12, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#555555"),
                BackColor = Color.Transparent,
                Location = new Point(15, 75),
                AutoSize = true
            };
            card.Controls.Add(lblComment);

            CheckBox chkSelect = new CheckBox
            {
                Name = $"chkSelect_{feedback.FeedbackID}",
                Location = new Point(450, 15),
                Size = new Size(20, 20),
                BackColor = ColorTranslator.FromHtml("#B4B4B8"),
                CheckAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(chkSelect);
            card.Controls.SetChildIndex(chkSelect, 0);

            for (int i = 0; i < 5; i++)
            {
                PictureBox star = new PictureBox
                {
                    Name = $"star_{i}",
                    Size = new Size(20, 20),
                    Location = new Point(15 + i * 25, 110),
                    Image = i < feedback.Rating ? Properties.Resources.icons8_star_50__2_ : Properties.Resources.icons8_star_50,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Transparent
                };
                card.Controls.Add(star);
            }

            System.Windows.Forms.Button btnDelete = new System.Windows.Forms.Button
            {
                Name = "btnDelete",
                Text = "Xóa",
                Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 30),
                Location = new Point(400, 120),
                Cursor = Cursors.Hand
            };

            btnDelete.FlatAppearance.BorderSize = 0;

            btnDelete.Click += (s, e) =>
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhận xét này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    feedBackService.Delete(feedback.FeedbackID);
                    LoadCardFeedBack();
                }
            };

            card.Controls.Add(btnDelete);


            return card;
        }


        private void pcBXoaFeed_Click(object sender, EventArgs e)
        {
            var selectedFeedbacks = flpFeedback.Controls.OfType<Panel>().Where(panel => panel.Controls.OfType<CheckBox>().FirstOrDefault()?.Checked == true).ToList();

            if (selectedFeedbacks.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một feedback để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa {selectedFeedbacks.Count} feedback đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                foreach (var panel in selectedFeedbacks)
                {
                    var feedbackId = panel.Controls.OfType<CheckBox>().FirstOrDefault()?.Name.Split('_')[1] ?? "0";
                    feedBackService.Delete(feedbackId);
                }
                LoadCardFeedBack();
            }
        }
      
    }
}

