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
    public partial class Order_Food : Form
    {
        public Order_Food()
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
            string sql = "SELECT Max(OrderId+1) FROM FoodOrder";
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
                    string query = "Select OrderId,VisitorName,OrderDate,ItemName,Quantity,TotalCost,Status from FoodOrder";
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
            comboBox3.Text = "";
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
        //DateTime Purchasedate = dateTimePicker1.Value;
        private void button1_Click(object sender, EventArgs e)
        {
            int OrderId = int.Parse(textBox1.Text);
            string VisitorName = textBox2.Text;
            DateTime OrderDate = dateTimePicker1.Value;
            string ItemName = textBox3.Text;
            string Quantity = textBox4.Text;
            string TotalCost = textBox5.Text;
            string Status = comboBox3.Text;

            try
            {
                string connectionString = cs.DBConn;

                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand command = new SqlCommand("Insert into FoodOrder values(@OrderId,@VisitorName,@OrderDate,@ItemName,@Quantity,@TotalCost,@Status)", con);
                        command.Parameters.AddWithValue("@OrderId", OrderId);
                        command.Parameters.AddWithValue("@VisitorName", VisitorName);
                        command.Parameters.AddWithValue("@OrderDate", OrderDate);
                        command.Parameters.AddWithValue("@ItemName", ItemName);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@TotalCost", TotalCost);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Order Confirmed successfully", "Comfirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button2_Click(object sender, EventArgs e)
        {
            string OrderID = textBox1.Text;
            SqlConnection conn = new SqlConnection(cs.DBConn);
            SqlCommand command = new SqlCommand("DELETE FoodOrder WHERE OrderId =@OrderId", conn);
            command.Parameters.AddWithValue("@OrderId",OrderID);
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

        private void button3_Click(object sender, EventArgs e)
        {
            int OrderId = int.Parse(textBox1.Text);
            string VisitorName = textBox2.Text;
            DateTime OrderDate = dateTimePicker1.Value;
            string ItemName = textBox3.Text;
            string Quantity = textBox4.Text;
            string TotalCost = textBox5.Text;
            string Status = comboBox3.Text;
            try
            {

                string connectionString = cs.DBConn;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cnn = new SqlCommand("UPDATE FoodOrder SET  OrderId =@OrderId,VisitorName=@VisitorName,OrderDate=@OrderDate,ItemName=@ItemName,Quantity=@Quantity,TotalCost=@TotalCost,Status=@Status", con);
                    cnn.Parameters.AddWithValue("@OrderId", OrderId);
                    cnn.Parameters.AddWithValue("@VisitorName", VisitorName);
                    cnn.Parameters.AddWithValue("@OrderDate", OrderDate);
                    cnn.Parameters.AddWithValue("@ItemName", ItemName);
                    cnn.Parameters.AddWithValue("@Quantity", Quantity);
                    cnn.Parameters.AddWithValue("@TotalCost", TotalCost);
                    cnn.Parameters.AddWithValue("@Status", Status);
                    cnn.ExecuteNonQuery();
                    con.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
