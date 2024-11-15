using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Amusement_Park_MS
{
    public partial class EmployeeA : Form
    {
        public EmployeeA()
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
            string sql = "SELECT Max(EmployeeId+1) FROM Employess";
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
                textBox1.Text = Convert.ToString( Num);
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
                    string query = "Select EmployeeId,EmployeeName,DOB,Contact,Address,JobTitle,HireDate,Department from Employess";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dt.Clear();
                        adapter.Fill(dt);
                        //dataGridView2.DataSource = dt;
                       // dataGridView2.Refresh();

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
            textBox6.Clear();
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
            int EmployeeId = int.Parse(textBox1.Text);
            string EmployeeName = textBox2.Text;
            DateTime DOB = dateTimePicker1.Value;
            string Contact = textBox3.Text;
            string Address = textBox4.Text;
            string JobTitle = textBox5.Text;
            DateTime HireDate = dateTimePicker2.Value;
            string Department = textBox6.Text;
            {
                try
                {
                    string connectionString = cs.DBConn;

                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();

                            SqlCommand command = new SqlCommand("Insert into Employess values(@EmployeeId,@EmployeeName,@DOB,@Contact,@Address,@JobTitle,@HireDate, @Department)", con);
                            command.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                            command.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                            command.Parameters.AddWithValue("@DOB", DOB);
                            command.Parameters.AddWithValue("@Contact", Contact);
                            command.Parameters.AddWithValue("@Address", Address);
                            command.Parameters.AddWithValue("@JobTitle", JobTitle);
                            command.Parameters.AddWithValue("@HireDate", HireDate);
                            command.Parameters.AddWithValue("@Department", Department);
                            command.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Record Added Successfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            con.Close();
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
            int EmployeeId = int.Parse(textBox1.Text);
            string EmployeeName = textBox2.Text;
            DateTime DOB = dateTimePicker1.Value;
            string Contact = textBox3.Text;
            string Address = textBox4.Text;
            string JobTitle = textBox5.Text;
            DateTime HireDate = dateTimePicker2.Value;
            string Department = textBox6.Text;

            try
            {

                string connectionString = cs.DBConn;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand("UPDATE Employess SET EmployeeID=@EmployeeID, EmployeeName = @EmployeeName, DOB = @DOB,Contact = @Contact, Address = @Address,JobTitle=@JobTitle,HireDate=@HireDate,Department = @Department WHERE EmployeeID = @EmployeeID", con);
                    command.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                    command.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                    command.Parameters.AddWithValue("@DOB", DOB);
                    command.Parameters.AddWithValue("@Contact", Contact);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@JobTitle", JobTitle);
                    command.Parameters.AddWithValue("@HireDate", HireDate);
                    command.Parameters.AddWithValue("@Department", Department);
                    command.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Update Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while Update data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int EmployeeId = int.Parse(textBox1.Text);
            SqlConnection conn = new SqlConnection(cs.DBConn);
            SqlCommand command = new SqlCommand("DELETE Employess WHERE EmployeeID =@EmployeeID", conn);
            command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Record deleted successfully", "deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error deleting record:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void EmployeeA_Load(object sender, EventArgs e)
        {

        }
    }
}

        
    
