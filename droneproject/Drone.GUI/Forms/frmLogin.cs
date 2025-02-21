using System;
using System.Windows.Forms;
using Drone.BUS;

namespace Drone.GUI.Forms
{
    public partial class frmLogin : Form
    {
        private readonly UserService _userService = new UserService();
        public frmLogin()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += btLogin_KeyDown;
            bool showPassword = checkBox1.Checked;
            txbPass.UseSystemPasswordChar = !showPassword;
        }
    
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txbUserName;
        }
        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }
        private void btLogin_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string password = txbPass.Text;

            if (_userService.IsLoginValid(username, password))
            {
                MessageBox.Show("Đăng nhập thành công!");
                FormMain mainForm = new FormMain();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {      
            Application.Exit(); 
        }

        private void btLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btLogin.PerformClick();
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool showPassword = checkBox1.Checked;
            txbPass.UseSystemPasswordChar = !showPassword;
        }

    
    }
    }
