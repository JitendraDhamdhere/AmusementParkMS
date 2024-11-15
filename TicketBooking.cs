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
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Amusement_Park_MS
{
    public partial class TicketBooking : Form
    {
        public TicketBooking()
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
            string sql = "SELECT Max(TicketId+1) FROM TicketBookings";
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
                    string query = "Select TicketId,Ride_Name,VisitorName,TicketType,TicketPrice,BookingDate from TicketBookings";
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
            comboBox1.Text = "";
            comboBox2.Text = "";
            AutoIdGeneration();

        }
       /* private void AutocompleteSuggestId()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ProductName FROM StockTable", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ProductName");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    col.Add(ds.Tables[0].Rows[i]["ProductName"].ToString());
                txtProductName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtProductName.AutoCompleteCustomSource = col;
                txtProductName.AutoCompleteMode = AutoCompleteMode.Suggest;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AutoIdGeneration()
        {
            int Num = 0;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            string sql = "SELECT Max(ID+1) FROM BillInfoTable";
            cmd = new SqlCommand(sql);
            cmd.Connection = con;
            if (Convert.IsDBNull(cmd.ExecuteScalar()))
            {
                Num = 1;
                lblId.Text = Convert.ToString(Num);
                txtBillId.Text = Convert.ToString("Bill-" + Num);
            }
            else
            {
                Num = System.Convert.ToInt32((cmd.ExecuteScalar()));
                lblId.Text = Convert.ToString(Num);
                txtBillId.Text = Convert.ToString("Bill-" + Num);
            }
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
       */


        //Book TIcket
        private void button1_Click(object sender, EventArgs e)
        {
            int ticketId = int.Parse(textBox1.Text);
            string rideName = comboBox2.SelectedItem.ToString();
            string visitorName = textBox2.Text;
            string ticketType = comboBox1.SelectedItem.ToString();
            decimal ticketPrice = decimal.Parse(textBox3.Text);
            DateTime bookingDate = dateTimePicker1.Value;

            try
            {
                string connectionString = cs.DBConn;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO TicketBookings (TicketId, Ride_Name, VisitorName, TicketType, TicketPrice, BookingDate, TicketStatus) " +
                        "VALUES (@TicketId, @Ride_Name, @VisitorName, @TicketType, @TicketPrice, @BookingDate, 'Booked')", con);

                    command.Parameters.AddWithValue("@TicketId", ticketId);
                    command.Parameters.AddWithValue("@Ride_Name", rideName);
                    command.Parameters.AddWithValue("@VisitorName", visitorName);
                    command.Parameters.AddWithValue("@TicketType", ticketType);
                    command.Parameters.AddWithValue("@TicketPrice", ticketPrice);
                    command.Parameters.AddWithValue("@BookingDate", bookingDate);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Ticket booked successfully", "Booked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while booking the ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string TicketId = textBox1.Text;
            SqlConnection conn = new SqlConnection(cs.DBConn);
            SqlCommand command = new SqlCommand("DELETE FROM TicketBookings WHERE TicketId =@TickeId", conn);
            command.Parameters.AddWithValue("@TicketId", TicketId);
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
            string TicketId = textBox1.Text;
            string RideName = comboBox2.SelectedItem.ToString();
            string VisitorName = textBox2.Text;
            string TicketType = comboBox1.SelectedItem.ToString();
            string TicketPrice = textBox3.Text;
            DateTime bookingdate = dateTimePicker1.Value;
            try
            {
                string connectionString = cs.DBConn;

                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand command = new SqlCommand("UPDATE TicketBookings SET Ride_Name=@Ride_Name,VisitorName=@Visitor_Name,TicketType=@TicketType,TicketPrice=@TicketPrice,BookingDate=@Booking_Date WHERE TicketId=@TicketId", con);
                        command.Parameters.AddWithValue("@TicketId", TicketId);
                        command.Parameters.AddWithValue("@Ride_Name", RideName);
                        command.Parameters.AddWithValue("@Visitior_Name", VisitorName);
                        command.Parameters.AddWithValue("@TicketType", TicketType);
                        command.Parameters.AddWithValue("@TicketPrice", TicketPrice);
                        command.Parameters.AddWithValue("@Booking_Date", bookingdate);
                        command.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Ticket Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
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
