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
    public partial class TicketCancle : Form
    {
        public TicketCancle()
        {
            InitializeComponent();
            LoadData();

        }

        ConnectionString cs = new ConnectionString();



        private void LoadData()
        {
            try
            {
                string connectionString = cs.DBConn;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "Select * from TicketBookings where TicketStatus='Booked'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dt.Clear();
                        adapter.Fill(dt);
                        ////dataGridView1.DataSource = dt;
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

        private void button5_Click(object sender, EventArgs e)
        {
            int ticketId = int.Parse(textBox1.Text);
            string visitorName = textBox2.Text;
            string ticketStatus = "Cancelled";
            DateTime purchaseDate = dateTimePicker1.Value;
            decimal refundAmount = decimal.Parse(textBox4.Text);
            string cancelReason = textBox5.Text;

            try
            {
                string connectionString = cs.DBConn;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(
                        "UPDATE TicketBookings SET TicketStatus = @TicketStatus, RefundAmount = @RefundAmount, CancelReason = @CancelReason " +
                        "WHERE TicketId = @TicketId", con);

                    command.Parameters.AddWithValue("@TicketId", ticketId);
                    command.Parameters.AddWithValue("@TicketStatus", ticketStatus);
                    command.Parameters.AddWithValue("@RefundAmount", refundAmount);
                    command.Parameters.AddWithValue("@CancelReason", cancelReason);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Ticket cancelled successfully", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while cancelling the ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string TicketId = textBox1.Text;
            SqlConnection conn = new SqlConnection(cs.DBConn);
           // SqlCommand command = new SqlCommand("DELETE FROM Ticket_Cancle WHERE TicketId =@TickeId", conn);
            SqlCommand command = new SqlCommand("DELETE FROM TicketBookings WHERE TicketId =@TickeId", conn);

            command.Parameters.AddWithValue("@TicketId", TicketId);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Record deleted successfully", "deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearFields();
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

        private void button6_Click(object sender, EventArgs e)
        {
            int TicketId = int.Parse(textBox1.Text);
            string VisitorName = textBox2.Text;
            string TicketStatus = textBox3.Text;
            DateTime Purchasedate = dateTimePicker1.Value;
            string RefundAmt = textBox4.Text;
            string CancleReason = textBox5.Text;
            string TicketType = comboBox1.Text;
            {
                try
                {
                    string connectionString = cs.DBConn;

                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();

                            SqlCommand command = new SqlCommand("Insert into TicketBookings values (@TicketId,@VisitorName,@TicketType,@TicketStatus,@PurchaseDate,@RefundAmount,@CancleReason)", con);
                            command.Parameters.AddWithValue("@TicketId", TicketId);
                            command.Parameters.AddWithValue("@VisitorName", VisitorName);
                            command.Parameters.AddWithValue("@TicketType", TicketType);
                            command.Parameters.AddWithValue("@TicketStatus", TicketStatus);
                            command.Parameters.AddWithValue("@PurchaseDate", Purchasedate);
                            command.Parameters.AddWithValue("RefundAmount", RefundAmt);
                            command.Parameters.AddWithValue("CancleReason", CancleReason);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int ticketId))
            {
                try
                {
                    string connectionString = cs.DBConn;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT * FROM TicketBookings WHERE TicketId = @TicketId AND TicketStatus='Booked'";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@TicketId", ticketId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    textBox2.Text = reader["VisitorName"].ToString();
                                    comboBox1.Text = reader["TicketType"].ToString();
                                    textBox3.Text = reader["TicketPrice"].ToString();
                                    dateTimePicker1.Value = Convert.ToDateTime(reader["BookingDate"]);
                                    if (reader["TicketStatus"].ToString() == "Cancelled")
                                    {
                                        textBox4.Text = reader["RefundAmount"].ToString();
                                        textBox5.Text = reader["CancelReason"].ToString();
                                    }
                                    else
                                    {
                                        textBox4.Text = (decimal.Parse(reader["TicketPrice"].ToString()) * 0.90m).ToString("F2"); // 10% refund calculation
                                        textBox5.Clear(); // Clear cancel reason if not cancelled
                                    }
                                }
                                else
                                {
                                    ClearFields();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while retrieving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ClearFields();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

