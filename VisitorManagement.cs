using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Amusement_Park_MS
{
    public partial class VisitorManagement : Form
    {
        public VisitorManagement()
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
            string sql = "SELECT Max(VisitorId+1) FROM Visitor";
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
                    string query = "Select VisitorId,VisitorName,Age,ContactNumber,Address,VisitDate,TicketType from Visitor";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dt.Clear();
                        adapter.Fill(dt);
                        //dataGridView1.DataSource = dt;
                       // dataGridView1.Refresh();

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
            comboBox1.Text = "";           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int  VisitorID = int.Parse(textBox1.Text);
            string VisitorName = textBox2.Text;
            string Age = textBox3.Text;
            string ContactNumber = textBox4.Text;
            string Address = textBox5.Text;
            DateTime VisitDate = dateTimePicker2.Value;
            string TicketType= comboBox1.Text;
            {
                try
                {
                    string connectionString = cs.DBConn;

                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();

                            SqlCommand command = new SqlCommand("Insert into Visitor values(@VisitorId,@VisitorName,@Age,@ContactNumber,@Address,@VisitDate,@TicketType)", con);
                            command.Parameters.AddWithValue("@VisitorId", VisitorID);
                            command.Parameters.AddWithValue("@VisitorName",VisitorName);
                            command.Parameters.AddWithValue("@Age", Age);
                            command.Parameters.AddWithValue("@ContactNumber", ContactNumber);
                            command.Parameters.AddWithValue("@Address", Address);
                            command.Parameters.AddWithValue("@Visitdate", VisitDate);
                            command.Parameters.AddWithValue("@TicketType", TicketType);                          
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

        private void label10_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Minimized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.Hide();
            panelEmployee panelEmployee = new panelEmployee();
            panelEmployee.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int VisitorID = int.Parse(textBox1.Text);
            string VisitorName = textBox2.Text;
            string Age = textBox3.Text;
            string ContactNumber = textBox4.Text;
            string Address = textBox5.Text;
            DateTime VisitDate = dateTimePicker2.Value;
            string TicketType = comboBox1.Text;
            {
                try
                {
                    string connectionString = cs.DBConn;

                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();

                            SqlCommand command = new SqlCommand("Update Visitor set VisitorId=@VisitorId,VisitorName= @VisitorName,Age= @Age,ContactNumber= @ContactNumber,Address= @Address,VisitDate= @VisitDate,TicketType= @TicketType", con);
                            command.Parameters.AddWithValue("@VisitorId", VisitorID);
                            command.Parameters.AddWithValue("@VisitorName", VisitorName);
                            command.Parameters.AddWithValue("@Age", Age);
                            command.Parameters.AddWithValue("@ContactNumber", ContactNumber);
                            command.Parameters.AddWithValue("@Address", Address);
                            command.Parameters.AddWithValue("@Visitdate", VisitDate);
                            command.Parameters.AddWithValue("@TicketType", TicketType);
                            command.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Record Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            con.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while update data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int VisitorId=int.Parse(textBox1.Text);
            SqlConnection conn = new SqlConnection(cs.DBConn);
            SqlCommand command = new SqlCommand("DELETE Visitor WHERE VisitorID =@VisitorID", conn);
            command.Parameters.AddWithValue("@VisitorId",VisitorId);
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
    }
}
