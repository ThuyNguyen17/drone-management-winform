using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Drone.BUS;
using Drone.DAL.Entities;

namespace Drone.GUI.Forms
{
        public partial class FormAddKhachHang : Form
        {
            public event Action<Customer> OnCustomerAdded;
            private readonly DroneContextDB db;

            public FormAddKhachHang()
            {
                InitializeComponent();
                db = new DroneContextDB();
            }

            public void SetCustomerData(string customerID, string customerName, string customerType, string customerPhone, string customerEmail, string customerAddress)
            {
                txtMaKH.Text = customerID;
                txtTenKH.Text = customerName;

                // Set radio button selection based on customer type
                switch (customerType?.Trim())
                {
                    case "Cá nhân":
                        rdoCaNhan.Checked = true;
                        break;
                    case "Tổ chức":
                        rdoToChuc.Checked = true;
                        break;
                    default:
                        rdoCaNhan.Checked = false;
                        rdoToChuc.Checked = false;
                        break;
                }

                txtSDT.Text = customerPhone;
                txtEmail.Text = customerEmail;
                txtDiaChi.Text = customerAddress;
            }

            private void btDongY_Click(object sender, EventArgs e)
            {
                // Validate input fields
                if (!ValidateInput()) return;

                var newCustomer = new Customer
                {
                    CustomerID = txtMaKH.Text.Trim(),
                    Name = txtTenKH.Text.Trim(),
                    Type = rdoCaNhan.Checked ? "Cá nhân" : "Tổ chức",
                    Address = txtDiaChi.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtSDT.Text.Trim(),
                };

                try
                {
                    var customerService = new CustomerService();
                    customerService.InsertUpdate(newCustomer);

                    MessageBox.Show("Khách hàng đã được thêm/cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    OnCustomerAdded?.Invoke(newCustomer);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm khách hàng: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private bool ValidateInput()
            {
                //if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"))
                //{
                //    MessageBox.Show("Email không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}

                if (!Regex.IsMatch(txtSDT.Text, @"^(\\+84|0)[3|5|7|8|9][0-9]{8}$"))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtMaKH.Text) || string.IsNullOrWhiteSpace(txtTenKH.Text) ||
                    string.IsNullOrWhiteSpace(txtDiaChi.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtSDT.Text) || (!rdoCaNhan.Checked && !rdoToChuc.Checked))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin và chọn các giá trị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }

            private void btReset_Click(object sender, EventArgs e)
            {
                ResetFormFields();
            }

            private void ResetFormFields()
            {
                txtMaKH.Clear();
                txtTenKH.Clear();
                txtDiaChi.Clear();
                txtEmail.Clear();
                txtSDT.Clear();
                rdoCaNhan.Checked = false;
                rdoToChuc.Checked = false;
            }

            private void btThoat_Click(object sender, EventArgs e)
            {
                this.Close();
            }
        }
}
