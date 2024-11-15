using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Amusement_Park_MS
{
    public partial class Maintenance2 : Form
    {
        public Maintenance2()
        {
            InitializeComponent();
            LoadData();
            ClearFields();
            AutoIdGeneration();
        }

        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();

        private void AutoIdGeneration()
        {
            int Num = 0;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string sql = "SELECT Max(MaintenanceId+1) FROM Maintenance";
            cmd = new SqlCommand(sql);
            cmd.Connection = con;
            if (Convert.IsDBNull(cmd.ExecuteScalar()))
            {
                Num = 1;
                textBox1.Text = Convert.ToString(Num);
            }
            else
            {
                Num = System.Convert.ToInt32((cmd.ExecuteScalar()));
                textBox1.Text = Convert.ToString(Num);
            }
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        private void LoadData()
        {
            try
            {
                string connectionString = cs.DBConn;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Select MaintenanceId,Ridename,Maintenancedate,Type,Status,Assignedto,completedby,completationdate from Maintenance";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dt.Clear();
                        adapter.Fill(dt);
                        //dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while loading data:{ex.Message}");
            }
        }
        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox2.Text = "";

        }
        private void Maintenance2_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin admin = new Admin();
            admin.ShowDialog();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Minimized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string MaintenanceId = textBox1.Text;
            string RideName = comboBox2.SelectedItem.ToString();
            DateTime Maintenancedate = dateTimePicker1.Value;
            string Type = textBox2.Text;
            string Status = textBox3.Text;
            string Assignedto = textBox4.Text;
            string Completedby = textBox5.Text;
            DateTime completationdate = dateTimePicker2.Value;
            {
                try
                {
                    string connectionString = cs.DBConn;

                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();

                            SqlCommand command = new SqlCommand("Insert into Maintenance values (@MaintenanceId,@Ridename,@Maintenancedate,@Type,@Status,@Assignedto,@completedby,@completationdate)", con);
                            command.Parameters.AddWithValue("@MaintenanceId", MaintenanceId);
                            command.Parameters.AddWithValue("@Ridename", RideName);
                            command.Parameters.AddWithValue("@Maintenancedate", Maintenancedate);
                            command.Parameters.AddWithValue("@Type", Type);
                            command.Parameters.AddWithValue("@Status", Status);
                            command.Parameters.AddWithValue("@Assignedto", Assignedto);
                            command.Parameters.AddWithValue("@completedby", Completedby);
                            command.Parameters.AddWithValue("@complitationdate", completationdate);
                            con.Close();
                            MessageBox.Show("Maintenance added Successfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while adding data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string MaintenanceId = textBox1.Text;
            string RideName = comboBox2.SelectedItem.ToString();
            DateTime Maintenancedate = dateTimePicker1.Value;
            string Type = textBox2.Text;
            string Status = textBox3.Text;
            string Assignedto = textBox4.Text;
            string Completedby = textBox5.Text;
            DateTime completationdate = dateTimePicker2.Value;
            {
                try
                {
                    string connectionString = cs.DBConn;

                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();

                            SqlCommand command = new SqlCommand(" UPDATE Maintenance SET MaintenanceId=@MaintenanceId,RideName=@Ridename,Maintenancedate=@Maintenancedate,Type=@Type,Status=@Status,Assignedto=@Assignedto,Completedby=@completedby,completationdate=@completationdate", con);
                            command.Parameters.AddWithValue("@MaintenanceId", MaintenanceId);
                            command.Parameters.AddWithValue("@Ridename", RideName);
                            command.Parameters.AddWithValue("@Maintenancedate", Maintenancedate);
                            command.Parameters.AddWithValue("@Type", Type);
                            command.Parameters.AddWithValue("@Status", Status);
                            command.Parameters.AddWithValue("@Assignedto", Assignedto);
                            command.Parameters.AddWithValue("@completedby", Completedby);
                            command.Parameters.AddWithValue("@complitationdate", completationdate);
                            con.Close();
                            MessageBox.Show("Maintenance updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}


       