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
    public partial class Suppliercs : Form
    {
        public Suppliercs()
        {
            InitializeComponent();
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
            string sql = "SELECT Max(SupplierId+1) FROM Supplier";
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
                    string query = "Select SupplierId,Name,Address,Phone,ProductName,Price,Quantity, OrderDate from Supplier";
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
            comboBox2.Text="";
            textBox6.Clear();
            comboBox1.Text = "";
           
        }
        private void label12_Click(object sender, EventArgs e)
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
            int SupplierId = int.Parse(textBox1.Text);
            string Name = textBox2.Text;
            string Address = textBox3.Text;
            string Phone= textBox4.Text;
            string ProductName = comboBox2.Text;
            string Price = textBox6.Text;
            string Quantity = comboBox1.Text;
            DateTime OrderDate = dateTimePicker1.Value;
            {
                try
                {
                    string connectionString = cs.DBConn;

                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();

                            SqlCommand command = new SqlCommand("Insert into Supplier values(@SupplierId,@Name,@Address,@Phone,@ProductName,@Price,@Quantity, @OrderDate)", con);
                            command.Parameters.AddWithValue("@SupplierId", SupplierId);
                            command.Parameters.AddWithValue("@Name", Name);
                            command.Parameters.AddWithValue("@Address", Address);
                            command.Parameters.AddWithValue("@Phone", Phone);
                            command.Parameters.AddWithValue("@ProductName", ProductName);
                            command.Parameters.AddWithValue("@Price",Price);
                            command.Parameters.AddWithValue("@Quantity",Quantity);
                            command.Parameters.AddWithValue("@OrderDate",OrderDate) ;
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

        private void button3_Click(object sender, EventArgs e)
        {
            int SupplierId = int.Parse(textBox1.Text);
            string Name = textBox2.Text;
            string Address = textBox3.Text;
            string Phone = textBox4.Text;
            string ProdutecName = comboBox2.Text;
            string Price = textBox6.Text;
            string Quantity = comboBox1.Text;
            DateTime OrderDate = dateTimePicker1.Value;
            try
            {
                string connectionString = cs.DBConn;

                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand command = new SqlCommand("Update  Supplier set SupplierId=@SupplierId,Name=@Name,Address=@Address,Phone=@Phone,ProductName=@ProductName,Price=@Price,Quantity=@Quantity,OrderDate= @OrderDate", con);
                        command.Parameters.AddWithValue("@SupplierId", SupplierId);
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@ProductName", ProductName);
                        command.Parameters.AddWithValue("@Price", Price);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@OrderDate", OrderDate);
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
                MessageBox.Show($"An error occurred while Updating data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int SupplierId = int.Parse(textBox1.Text);
            SqlConnection conn = new SqlConnection(cs.DBConn);
            SqlCommand command = new SqlCommand("DELETE Supplier WHERE SupplierId =@SupplierId", conn);
            command.Parameters.AddWithValue("@SupplierId",SupplierId);
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
    

