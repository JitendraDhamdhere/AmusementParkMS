using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Amusement_Park_MS
{
    public partial class Login_Form : Form
    {

        ConnectionString cs=new ConnectionString();
        public Login_Form()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string role = comboBox1.SelectedItem.ToString()!=null? comboBox1.SelectedItem.ToString() : "Employee";
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            string connectionString = cs.DBConn;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "";
                   

                    if (role == "Admin")
                    {
                        query = "SELECT COUNT(1) FROM Admin WHERE Username = @Username AND Password = @Password";
                    }
                    else if (role == "Employee")
                    {
                        query = "SELECT COUNT(1) FROM Employee WHERE Username = @Username AND Password = @Password";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        MessageBox.Show("Login Successful");
                        if (role == "Admin")
                        {
                            query = "SELECT COUNT(1) FROM Admin WHERE Username = @Username AND Password = @Password";
                            Admin admin = new Admin();
                            admin.ShowDialog();
                            this.Hide();
                        }
                        else if (role == "Employee")
                        {
                            query = "SELECT COUNT(1) FROM Employee WHERE Username = @Username AND Password = @Password";
                            panelEmployee employee = new panelEmployee();
                            employee.ShowDialog();
                            this.Hide();
                           

                    }


                    else
                    {
                            MessageBox.Show("Enter valid username and password");
                            
                    }
                        return;
                    }
                    reader.Close();
                    conn.Close();
                }

                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }




            }
        }

    }
}