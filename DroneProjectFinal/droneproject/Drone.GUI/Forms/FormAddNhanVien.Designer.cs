namespace Drone.GUI.Forms
{
    partial class FormAddNhanVien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddNhanVien));
            this.pcboxXoa = new System.Windows.Forms.PictureBox();
            this.pcboxSua = new System.Windows.Forms.PictureBox();
            this.pcboxThem = new System.Windows.Forms.PictureBox();
            this.dgvKTV = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btThemHinh = new System.Windows.Forms.Button();
            this.pcBHinhKTV = new System.Windows.Forms.PictureBox();
            this.cmbThuocTeam = new System.Windows.Forms.ComboBox();
            this.txtTinhTrang = new System.Windows.Forms.TextBox();
            this.txtChucVu = new System.Windows.Forms.TextBox();
            this.txtTenKTV = new System.Windows.Forms.TextBox();
            this.txtMaKTV = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxXoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxSua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxThem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKTV)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBHinhKTV)).BeginInit();
            this.SuspendLayout();
            // 
            // pcboxXoa
            // 
            this.pcboxXoa.Image = ((System.Drawing.Image)(resources.GetObject("pcboxXoa.Image")));
            this.pcboxXoa.Location = new System.Drawing.Point(474, 12);
            this.pcboxXoa.Name = "pcboxXoa";
            this.pcboxXoa.Size = new System.Drawing.Size(28, 30);
            this.pcboxXoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcboxXoa.TabIndex = 13;
            this.pcboxXoa.TabStop = false;
            this.pcboxXoa.Click += new System.EventHandler(this.pcboxXoa_Click);
            // 
            // pcboxSua
            // 
            this.pcboxSua.BackColor = System.Drawing.Color.Transparent;
            this.pcboxSua.Image = ((System.Drawing.Image)(resources.GetObject("pcboxSua.Image")));
            this.pcboxSua.Location = new System.Drawing.Point(431, 12);
            this.pcboxSua.Name = "pcboxSua";
            this.pcboxSua.Size = new System.Drawing.Size(28, 30);
            this.pcboxSua.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcboxSua.TabIndex = 11;
            this.pcboxSua.TabStop = false;
            this.pcboxSua.Click += new System.EventHandler(this.pcboxSua_Click);
            // 
            // pcboxThem
            // 
            this.pcboxThem.Image = ((System.Drawing.Image)(resources.GetObject("pcboxThem.Image")));
            this.pcboxThem.Location = new System.Drawing.Point(388, 12);
            this.pcboxThem.Name = "pcboxThem";
            this.pcboxThem.Size = new System.Drawing.Size(28, 30);
            this.pcboxThem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcboxThem.TabIndex = 12;
            this.pcboxThem.TabStop = false;
            this.pcboxThem.Click += new System.EventHandler(this.pcboxThem_Click);
            // 
            // dgvKTV
            // 
            this.dgvKTV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKTV.BackgroundColor = System.Drawing.Color.LavenderBlush;
            this.dgvKTV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKTV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dgvKTV.Location = new System.Drawing.Point(383, 56);
            this.dgvKTV.Name = "dgvKTV";
            this.dgvKTV.Size = new System.Drawing.Size(568, 299);
            this.dgvKTV.TabIndex = 15;
            this.dgvKTV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKTV_CellClick_1);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Mã KTV";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Tên KTV";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Thuộc Team";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Chức Vụ";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Tình Trạng";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Image";
            this.Column6.Name = "Column6";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LavenderBlush;
            this.groupBox1.Controls.Add(this.btThemHinh);
            this.groupBox1.Controls.Add(this.pcBHinhKTV);
            this.groupBox1.Controls.Add(this.cmbThuocTeam);
            this.groupBox1.Controls.Add(this.txtTinhTrang);
            this.groupBox1.Controls.Add(this.txtChucVu);
            this.groupBox1.Controls.Add(this.txtTenKTV);
            this.groupBox1.Controls.Add(this.txtMaKTV);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label56);
            this.groupBox1.Controls.Add(this.label54);
            this.groupBox1.Controls.Add(this.label53);
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 372);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Tin Kỹ Thuật Viên:";
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
            // pcBHinhKTV
            // 
            this.pcBHinhKTV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcBHinhKTV.Location = new System.Drawing.Point(93, 230);
            this.pcBHinhKTV.Name = "pcBHinhKTV";
            this.pcBHinhKTV.Size = new System.Drawing.Size(104, 102);
            this.pcBHinhKTV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcBHinhKTV.TabIndex = 5;
            this.pcBHinhKTV.TabStop = false;
            // 
            // cmbThuocTeam
            // 
            this.cmbThuocTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbThuocTeam.FormattingEnabled = true;
            this.cmbThuocTeam.Location = new System.Drawing.Point(235, 42);
            this.cmbThuocTeam.Name = "cmbThuocTeam";
            this.cmbThuocTeam.Size = new System.Drawing.Size(94, 26);
            this.cmbThuocTeam.TabIndex = 4;
            // 
            // txtTinhTrang
            // 
            this.txtTinhTrang.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhTrang.Location = new System.Drawing.Point(93, 177);
            this.txtTinhTrang.Name = "txtTinhTrang";
            this.txtTinhTrang.Size = new System.Drawing.Size(136, 24);
            this.txtTinhTrang.TabIndex = 1;
            // 
            // txtChucVu
            // 
            this.txtChucVu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChucVu.Location = new System.Drawing.Point(93, 136);
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.Size = new System.Drawing.Size(136, 24);
            this.txtChucVu.TabIndex = 1;
            // 
            // txtTenKTV
            // 
            this.txtTenKTV.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenKTV.Location = new System.Drawing.Point(93, 90);
            this.txtTenKTV.Name = "txtTenKTV";
            this.txtTenKTV.Size = new System.Drawing.Size(136, 24);
            this.txtTenKTV.TabIndex = 1;
            // 
            // txtMaKTV
            // 
            this.txtMaKTV.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaKTV.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaKTV.Location = new System.Drawing.Point(93, 44);
            this.txtMaKTV.Name = "txtMaKTV";
            this.txtMaKTV.Size = new System.Drawing.Size(85, 24);
            this.txtMaKTV.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tình Trạng:";
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
            this.label56.Size = new System.Drawing.Size(42, 15);
            this.label56.TabIndex = 0;
            this.label56.Text = "Team:";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(18, 136);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(55, 15);
            this.label54.TabIndex = 0;
            this.label54.Text = "Chức Vụ:";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(18, 90);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(56, 15);
            this.label53.TabIndex = 0;
            this.label53.Text = "Tên KTV:";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(18, 47);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(53, 15);
            this.label52.TabIndex = 0;
            this.label52.Text = "Mã KTV:";
            // 
            // FormAddNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(972, 407);
            this.Controls.Add(this.pcboxXoa);
            this.Controls.Add(this.pcboxSua);
            this.Controls.Add(this.pcboxThem);
            this.Controls.Add(this.dgvKTV);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormAddNhanVien";
            this.Text = "Nhân Viên";
            this.Load += new System.EventHandler(this.FormAddNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcboxXoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxSua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcboxThem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKTV)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBHinhKTV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcboxXoa;
        private System.Windows.Forms.PictureBox pcboxSua;
        private System.Windows.Forms.PictureBox pcboxThem;
        private System.Windows.Forms.DataGridView dgvKTV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btThemHinh;
        private System.Windows.Forms.PictureBox pcBHinhKTV;
        private System.Windows.Forms.ComboBox cmbThuocTeam;
        private System.Windows.Forms.TextBox txtTinhTrang;
        private System.Windows.Forms.TextBox txtChucVu;
        private System.Windows.Forms.TextBox txtTenKTV;
        private System.Windows.Forms.TextBox txtMaKTV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label52;
    }
}