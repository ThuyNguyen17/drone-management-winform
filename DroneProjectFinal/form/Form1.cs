using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp1.QLDRONEDataSet;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLDRONEDataSet.Contract_' table. You can move, or remove it, as needed.
            this.contract_TableAdapter.Fill(this.qLDRONEDataSet.Contract_);

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the clicked row is valid
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Retrieve the current row

                // Example: Assuming QLDRONEDataSet has a table for DroneRequests
                using (QLDRONEDataSet context = new QLDRONEDataSet())
                {
                    // Get the ID or key from the clicked row
                    String id = Convert.ToString(row.Cells["contractIDDataGridViewTextBoxColumn"].Value); // Replace "IdColumnName" with your column name

                    MessageBox.Show("ID : " + id);

                    // Query the dataset for the specific row (LINQ query example)
                    var droneRequest = context.Contract_.ContractIDColumn.Equals(id);
                                               
                                               


                        MessageBox.Show("dasda");


                    foreach (var contract in context.Contract_)
                    {
                        MessageBox.Show($"ContractID: {contract.ContractID}");
                    }

                    if (droneRequest != null)
                    {
                        // Example: Display details in text boxes or other UI elements
                        txt_contractid.Text = droneRequest.ContractID;
                        txt_customerID.Text = droneRequest.CustomerID;
                        txt_enddate.Text = droneRequest.EndDate.ToString();
                        txt_startdate.Text = droneRequest.StartDate.ToString();
                        txt_status.Text = droneRequest.Status;  
                        txt_technicalTeamID.Text= droneRequest.TechnicianTeamID.ToString();
                        txt_totalValue.Text = droneRequest.TotalValue.ToString();
                        if (droneRequest.Image != null)
                        {
                            using (var ms = new MemoryStream(droneRequest.Image))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }

                        // Perform additional actions based on the retrieved data
                    }
                    else
                    {
                        MessageBox.Show("No record found for the selected row.");
                    }
                }
            }
        }


        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.contract_TableAdapter.FillBy(this.qLDRONEDataSet.Contract_);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
