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

namespace Amusement_Park_MS
{
    public partial class Food_and_Beverages : Form
    {
        public Food_and_Beverages()
        {
            InitializeComponent();
            LoadData();
            ClearFields();
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
            string sql = "SELECT Max(ItemId+1) FROM food_beverages";
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
                    string query = "Select ItemId,ItemName,ItemType,Category,Price,Quantity,Availability from food_beverages";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dt.Clear();
                        adapter.Fill(dt);
                        //dataGridView1.DataSource = dt;
                        //dataGridView1.Refresh();

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
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            AutoIdGeneration();

        }
        private void label10_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Minimized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Hide();
            panelEmployee panelEmployee = new panelEmployee();
            panelEmployee.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ItemId = int.Parse(textBox1.Text);
            string ItemName = textBox2.Text;
            string ItemType = comboBox1.Text;
            string Category = comboBox2.Text;
            string Price =textBox3.Text;
            string Quantity = textBox4.Text;
            string Availability = comboBox3.Text;
            {
                try
                {
                    string connectionString = cs.DBConn;

                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("Insert into food_beverages values(@ItemId,@ItemName,@ItemType,@Category,@Price,@Quantity,@Availability)", con);
                            command.Parameters.AddWithValue("@ItemId", ItemId);
                            command.Parameters.AddWithValue("@ItemName", ItemName);
                            command.Parameters.AddWithValue("@ItemType", ItemType);
                            command.Parameters.AddWithValue("@Category", Category);
                            command.Parameters.AddWithValue("@Price", Price);
                            command.Parameters.AddWithValue("@Quantity", Quantity);
                            command.Parameters.AddWithValue("@Availability", Availability);
                            command.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Record Added Successfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearFields();
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
            string ItemId = textBox1.Text;
            string ItemName = textBox2.Text;
            string ItemType = comboBox1.Text;
            string Category = comboBox2.Text;
            string Price = textBox3.Text;
            string Quantity = textBox4.Text;
            string Availability = comboBox3.Text;
            {
                try
                {
                    string connectionString = cs.DBConn;

                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("UPDATE food_beverages SET ItemId=@ItemId,ItemName=@ItemName,ItemType=@ItemType,Category=@Category,Price=@Price,Quantity=@Quantity,Availability=@Availability Where ItemId=@ItemId", con);
                            command.Parameters.AddWithValue("@ItemId", ItemId);
                            command.Parameters.AddWithValue("@ItemName", ItemName);
                            command.Parameters.AddWithValue("@ItemType", ItemType);
                            command.Parameters.AddWithValue("@Category", Category);
                            command.Parameters.AddWithValue("@Price", Price);
                            command.Parameters.AddWithValue("@Quantity", Quantity);
                            command.Parameters.AddWithValue("@Availability", Availability);
                            command.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Record Updated Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearFields();
                            con.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ItemId = textBox1.Text;
            SqlConnection conn = new SqlConnection(cs.DBConn);
            SqlCommand command = new SqlCommand("DELETE food_beverages WHERE ItemId =@ItemId", conn);
            command.Parameters.AddWithValue("@ItemId", ItemId);
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