using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;

namespace Amusement_Park_MS
{
    public partial class RideManagement : Form
    {
       
        public RideManagement()
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
            string sql = "SELECT Max(RideId+1) FROM Rides";
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
        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text="";
        }
        private void LoadData()
        {
            try
            {
                string connectionString= cs.DBConn;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Select RideId,RideName,RideType,Capacity,Duration,OpenTime,CloseTime, Status from Rides";
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
             catch(Exception ex)
            {
                MessageBox.Show($"An error occured while loading data:{ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string RideId = textBox1.Text;
            string RideType = comboBox1.SelectedItem.ToString();
            string RideName = comboBox2.SelectedItem.ToString();
            string Capacity = textBox2.Text;
            string Duration = textBox3.Text;
            string OpenTime = textBox4.Text;
            string CloseTime = textBox5.Text;
            string Status = comboBox3.SelectedItem.ToString();
            {
                try
                {
                    string connectionString = cs.DBConn;

                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();

                            SqlCommand command = new SqlCommand("Insert into Rides values(@RideId,@RideName,@RideType,@Capacity,@Duration,@OpenTime,@closeTime, @status)",con);
                            command.Parameters.AddWithValue("@RideID", RideId);
                            command.Parameters.AddWithValue("@RideName", RideName);
                            command.Parameters.AddWithValue("@RideType", RideType);
                            command.Parameters.AddWithValue("@Capacity", Capacity);
                            command.Parameters.AddWithValue("@Duration", Duration);
                            command.Parameters.AddWithValue("@OpenTime", OpenTime);
                            command.Parameters.AddWithValue("@closeTime", CloseTime);
                            command.Parameters.AddWithValue("@status", Status);
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
            string RideID = textBox1.Text;
            string RideType = comboBox1.Text;
            string RideName = comboBox2.Text;
            string Capacity = textBox2.Text;
            string Duration = textBox3.Text;
            string OpenTime = textBox4.Text;
            string CloseTime = textBox5.Text;
            string Status = comboBox3.Text;

            try
            {
               
                string connectionString = cs.DBConn;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cnn = new SqlCommand("UPDATE Rides SET RideID=@RideID, RideName = @RideName, RideType = @RideType, Capacity = @Capacity, Duration = @Duration,OpenTime=@OpenTime,CloseTime=@closeTime,Status = @status WHERE RideID = @RideID", con);
                    cnn.Parameters.AddWithValue("@RideID", RideID);
                    cnn.Parameters.AddWithValue("@RideName", RideName);
                    cnn.Parameters.AddWithValue("@RideType", RideType);
                    cnn.Parameters.AddWithValue("@Capacity", Capacity);
                    cnn.Parameters.AddWithValue("@Duration", Duration);
                    cnn.Parameters.AddWithValue("@OpenTime", OpenTime);
                    cnn.Parameters.AddWithValue("@closeTime", CloseTime);
                    cnn.Parameters.AddWithValue("@status", Status); 
                    cnn.ExecuteNonQuery();
                    MessageBox.Show("Record Updated Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    con.Close();                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string RideID = textBox1.Text;           
            SqlConnection conn = new SqlConnection(cs.DBConn);
            SqlCommand command = new SqlCommand("DELETE Rides WHERE RideID =@RideID", conn);
            command.Parameters.AddWithValue("@RideID",RideID);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Record deleted successfully", "deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                conn.Close();
            }
            catch(Exception ex)
            {

                MessageBox.Show("Error deleting record:"+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Record Saved Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Hide();
            panelEmployee panelEmployee = new panelEmployee();
            panelEmployee.ShowDialog();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Minimized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}





