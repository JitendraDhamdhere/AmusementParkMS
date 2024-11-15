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
    public partial class Customer_Feedback : Form
    {
        public Customer_Feedback()
        {
            InitializeComponent();
            ClearFields();
            LoadData();
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
            string sql = "SELECT Max(FeedbackID+1) FROM Feedback";
            cmd = new SqlCommand(sql);
            cmd.Connection = con;
            if (Convert.IsDBNull(cmd.ExecuteScalar()))
            {
                Num = 1;
                textBox1.Text = Convert.ToString( Num);
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
        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox3.Text = "";
        }
        private void LoadData()
        {
            try
            {
                string connectionString = cs.DBConn;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Select FeedbackID,CustomerName,Email,Rating,Comments,Date from FeedBAck";
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
        private void button4_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string FeedbackID = textBox1.Text;
            SqlConnection conn = new SqlConnection(cs.DBConn);
            SqlCommand command = new SqlCommand("DELETE Feedback WHERE FeedbackID =@FeedbackID", conn);
            command.Parameters.AddWithValue("@FeedbackID", FeedbackID);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Feedback deleted successfully", "deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Feedback" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string FeedbackID = textBox1.Text;
            string CustomerName = textBox2.Text;
            string Email = textBox3.Text;
            string Rating = comboBox3.Text;
            string Comments = textBox4.Text;
            DateTime date = dateTimePicker1.Value;
            try
            {
                string connectionString = cs.DBConn;

                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand command = new SqlCommand("UPDATE Feedback SET FeedbackID=@FeedbackID,CustomerName=@CustomerName,Email=@Email,Rating=@Rating,Comments=@Comments,Date=@Date",con);
                        command.Parameters.AddWithValue("@FeedbackId", FeedbackID);
                        command.Parameters.AddWithValue("@CustomerName", CustomerName);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Rating", Rating);
                        command.Parameters.AddWithValue("@Comments", Comments);
                        command.Parameters.AddWithValue("@Date", date);
                        command.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Record Updates Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  
        private void button1_Click(object sender, EventArgs e)
        {
            string FeedbackID = textBox1.Text;
            string CustomerName = textBox2.Text;
            string Email = textBox3.Text;
            string Rating = comboBox3.Text;
            string Comments = textBox4.Text;
            DateTime date = dateTimePicker1.Value;
            try
            {
                string connectionString = cs.DBConn;

                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand command = new SqlCommand("Insert into Feedback values(@FeedbackID,@CustomerName,@Email,@Rating,@Comments,@Date)", con);
                        command.Parameters.AddWithValue("@FeedbackId", FeedbackID);
                        command.Parameters.AddWithValue("@CustomerName", CustomerName);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Rating", Rating);
                        command.Parameters.AddWithValue("@Comments", Comments);
                        command.Parameters.AddWithValue("@Date", date);
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
}
