namespace Drone.GUI.Forms
{
    partial class FormAddDrone
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddDrone));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btThemHinh = new System.Windows.Forms.Button();
            this.pcBHinh = new System.Windows.Forms.PictureBox();
            this.cmbLoai = new System.Windows.Forms.ComboBox();
            this.txtGiaMua = new System.Windows.Forms.TextBox();
            this.txtGiaThue = new System.Windows.Forms.TextBox();
            this.txtTenDrone = new System.Windows.Forms.TextBox();
            this.txtMaDrone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pcboxSua = new System.Windows.Forms.PictureBox();
            this.pcboxXoa = new System.Windows.Forms.PictureBox();
            this.pcboxThem = new System.Windows.Forms.PictureBox();
            this.dgvDrone = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBHinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxSua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxXoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxThem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrone)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LavenderBlush;
            this.groupBox1.Controls.Add(this.btThemHinh);
            this.groupBox1.Controls.Add(this.pcBHinh);
            this.groupBox1.Controls.Add(this.cmbLoai);
            this.groupBox1.Controls.Add(this.txtGiaMua);
            this.groupBox1.Controls.Add(this.txtGiaThue);
            this.groupBox1.Controls.Add(this.txtTenDrone);
            this.groupBox1.Controls.Add(this.txtMaDrone);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label56);
            this.groupBox1.Controls.Add(this.label54);
            this.groupBox1.Controls.Add(this.label53);
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 372);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin drone:";
            // 
            // btThemHinh
            // 
            this.btThemHinh.BackColor = System.Drawing.Color.Thistle;
            this.btThemHinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btThemHinh.Location = new System.Drawing.Point(207, 262);
            this.btThemHinh.Name = "btThemHinh";
            this.btThemHinh.Size = new System.Drawing.Size(38, 24);
            this.btThemHinh.TabIndex = 6;
            this.btThemHinh.Text = "...";
            this.btThemHinh.UseVisualStyleBackColor = false;
            this.btThemHinh.Click += new System.EventHandler(this.btThemHinh_Click);
            // 
            // pcBHinh
            // 
            this.pcBHinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcBHinh.Location = new System.Drawing.Point(93, 230);
            this.pcBHinh.Name = "pcBHinh";
            this.pcBHinh.Size = new System.Drawing.Size(104, 102);
            this.pcBHinh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcBHinh.TabIndex = 5;
            this.pcBHinh.TabStop = false;
            // 
            // cmbLoai
            // 
            this.cmbLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLoai.FormattingEnabled = true;
            this.cmbLoai.Location = new System.Drawing.Point(235, 42);
            this.cmbLoai.Name = "cmbLoai";
            this.cmbLoai.Size = new System.Drawing.Size(94, 26);
            this.cmbLoai.TabIndex = 4;
            // 
            // txtGiaMua
            // 
            this.txtGiaMua.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiaMua.Location = new System.Drawing.Point(93, 173);
            this.txtGiaMua.Name = "txtGiaMua";
            this.txtGiaMua.Size = new System.Drawing.Size(136, 24);
            this.txtGiaMua.TabIndex = 1;
            // 
            // txtGiaThue
            // 
            this.txtGiaThue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiaThue.Location = new System.Drawing.Point(93, 136);
            this.txtGiaThue.Name = "txtGiaThue";
            this.txtGiaThue.Size = new System.Drawing.Size(136, 24);
            this.txtGiaThue.TabIndex = 1;
            // 
            // txtTenDrone
            // 
            this.txtTenDrone.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDrone.Location = new System.Drawing.Point(93, 90);
            this.txtTenDrone.Name = "txtTenDrone";
            this.txtTenDrone.Size = new System.Drawing.Size(136, 24);
            this.txtTenDrone.TabIndex = 1;
            // 
            // txtMaDrone
            // 
            this.txtMaDrone.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaDrone.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaDrone.Location = new System.Drawing.Point(93, 44);
            this.txtMaDrone.Name = "txtMaDrone";
            this.txtMaDrone.Size = new System.Drawing.Size(85, 24);
            this.txtMaDrone.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Giá Mua:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hình ảnh:";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(195, 47);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(34, 15);
            this.label56.TabIndex = 0;
            this.label56.Text = "Loại:";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(18, 136);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(60, 15);
            this.label54.TabIndex = 0;
            this.label54.Text = "Giá Thuê:";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(18, 90);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(31, 15);
            this.label53.TabIndex = 0;
            this.label53.Text = "Tên:";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(18, 47);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(65, 15);
            this.label52.TabIndex = 0;
            this.label52.Text = "Mã Drone:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1188, -149);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1130, -143);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // pcboxSua
            // 
            this.pcboxSua.BackColor = System.Drawing.Color.Transparent;
            this.pcboxSua.Image = ((System.Drawing.Image)(resources.GetObject("pcboxSua.Image")));
            this.pcboxSua.Location = new System.Drawing.Point(427, 26);
            this.pcboxSua.Name = "pcboxSua";
            this.pcboxSua.Size = new System.Drawing.Size(28, 30);
            this.pcboxSua.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcboxSua.TabIndex = 0;
            this.pcboxSua.TabStop = false;
            this.toolTip1.SetToolTip(this.pcboxSua, "Nhấn để Sửa");
            this.pcboxSua.Click += new System.EventHandler(this.pcboxSua_Click);
            // 
            // pcboxXoa
            // 
            this.pcboxXoa.Image = ((System.Drawing.Image)(resources.GetObject("pcboxXoa.Image")));
            this.pcboxXoa.Location = new System.Drawing.Point(470, 26);
            this.pcboxXoa.Name = "pcboxXoa";
            this.pcboxXoa.Size = new System.Drawing.Size(28, 30);
            this.pcboxXoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcboxXoa.TabIndex = 1;
            this.pcboxXoa.TabStop = false;
            this.toolTip1.SetToolTip(this.pcboxXoa, "Nhấn để Xóa");
            this.pcboxXoa.Click += new System.EventHandler(this.pcboxXoa_Click);
            // 
            // pcboxThem
            // 
            this.pcboxThem.Image = ((System.Drawing.Image)(resources.GetObject("pcboxThem.Image")));
            this.pcboxThem.Location = new System.Drawing.Point(384, 26);
            this.pcboxThem.Name = "pcboxThem";
            this.pcboxThem.Size = new System.Drawing.Size(28, 30);
            this.pcboxThem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcboxThem.TabIndex = 0;
            this.pcboxThem.TabStop = false;
            this.toolTip1.SetToolTip(this.pcboxThem, "Nhấn để thêm ");
            this.pcboxThem.Click += new System.EventHandler(this.pcboxThem_Click);
            // 
            // dgvDrone
            // 
            this.dgvDrone.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDrone.BackgroundColor = System.Drawing.Color.LavenderBlush;
            this.dgvDrone.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrone.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dgvDrone.Location = new System.Drawing.Point(361, 68);
            this.dgvDrone.Name = "dgvDrone";
            this.dgvDrone.Size = new System.Drawing.Size(568, 299);
            this.dgvDrone.TabIndex = 10;
            this.dgvDrone.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDrone_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Mã Drone";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Tên Drone";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Mã Loại";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Giá Thuê";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Giá Mua";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Image";
            this.Column6.Name = "Column6";
            // 
            // FormAddDrone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(941, 420);
            this.Controls.Add(this.pcboxXoa);
            this.Controls.Add(this.pcboxSua);
            this.Controls.Add(this.pcboxThem);
            this.Controls.Add(this.dgvDrone);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormAddDrone";
            this.Text = "Drone";
            this.Load += new System.EventHandler(this.FormAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBHinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxSua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxXoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxThem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrone)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbLoai;
        private System.Windows.Forms.TextBox txtMaDrone;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtGiaMua;
        private System.Windows.Forms.TextBox txtGiaThue;
        private System.Windows.Forms.TextBox txtTenDrone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btThemHinh;
        private System.Windows.Forms.PictureBox pcBHinh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pcboxSua;
        private System.Windows.Forms.PictureBox pcboxXoa;
        private System.Windows.Forms.PictureBox pcboxThem;
        private System.Windows.Forms.DataGridView dgvDrone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}