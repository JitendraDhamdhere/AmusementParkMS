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
    public partial class BillingForm : Form
    {
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
            string sql = "SELECT Max(ID+1) FROM BillTable";
            cmd = new SqlCommand(sql);
            cmd.Connection = con;
            if (Convert.IsDBNull(cmd.ExecuteScalar()))
            {
                Num = 1;
                lblId.Text = Convert.ToString(Num);
                txtBillId.Text = Convert.ToString("INV-" + Num);
            }
            else
            {
                Num = System.Convert.ToInt32((cmd.ExecuteScalar()));
                lblId.Text = Convert.ToString(Num);
                txtBillId.Text = Convert.ToString("INV-" + Num);
            }
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void rest()
        {
            txtItemName.Text = "";
            txtCustomerName.Text = "";
            txtContactNo.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            txtTotal.Text = "";
            txtDiscount.Text = "";
            txtDiscountPrice.Text = "";
            txtGTotal.Text = "";
            lblSubtotal.Text = "0.0";
            dtpDOB.Text = DateTime.Today.ToString();
            ListView1.Items.Clear();
            AutocompleteSuggestId();
        }

        private void AutocompleteSuggestId()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ItemName FROM food_beverages", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ItemName");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    col.Add(ds.Tables[0].Rows[i]["ItemName"].ToString());
                txtItemName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtItemName.AutoCompleteCustomSource = col;
                txtItemName.AutoCompleteMode = AutoCompleteMode.Suggest;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public BillingForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            AutoIdGeneration();
            btnSave.Visible = true;
            rest();
        }

        private void BillingForm_Load(object sender, EventArgs e)
        {
            AutoIdGeneration();
            btnSave.Visible = true;
            rest();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtItemName.Text == "")
                {
                    MessageBox.Show("Please select Product Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtItemName.Focus();
                    return;
                }
                if (txtPrice.Text == "")
                {
                    MessageBox.Show("Please enter price", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrice.Focus();
                    return;
                }
                if (txtQuantity.Text == "")
                {
                    MessageBox.Show("Please enter quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.Focus();
                    return;
                }

                if (txtTotal.Text == "")
                {
                    MessageBox.Show("Please enter total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotal.Focus();
                    return;
                }

                if (ListView1.Items.Count == 0)
                {

                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(txtItemName.Text);
                    lst.SubItems.Add(txtPrice.Text);
                    lst.SubItems.Add(txtQuantity.Text);
                    lst.SubItems.Add(txtTotal.Text);
                    ListView1.Items.Add(lst);
                    lblSubtotal.Text = subtot().ToString();
                    txtItemName.Text = "";
                    txtPrice.Text = "";
                    txtTotal.Text = "";
                    txtQuantity.Text = "";
                    return;
                }

                for (int j = 0; j <= ListView1.Items.Count - 1; j++)
                {
                    if (ListView1.Items[j].SubItems[1].Text == txtItemName.Text)
                    {
                        ListView1.Items[j].SubItems[1].Text = txtItemName.Text;
                        ListView1.Items[j].SubItems[2].Text = txtPrice.Text;
                        ListView1.Items[j].SubItems[3].Text = txtQuantity.Text;
                        ListView1.Items[j].SubItems[4].Text = txtTotal.Text;
                        lblSubtotal.Text = subtot().ToString();
                        txtItemName.Text = "";
                        txtPrice.Text = "";
                        txtTotal.Text = "";
                        txtQuantity.Text = "";
                        return;

                    }
                }

                ListViewItem lst1 = new ListViewItem();

                lst1.SubItems.Add(txtItemName.Text);
                lst1.SubItems.Add(txtPrice.Text);
                lst1.SubItems.Add(txtQuantity.Text);
                lst1.SubItems.Add(txtTotal.Text);
                ListView1.Items.Add(lst1);
                lblSubtotal.Text = subtot().ToString();
                txtItemName.Text = "";
                txtPrice.Text = "";
                txtTotal.Text = "";
                txtQuantity.Text = "";
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("No items to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int itmCnt = 0;
                    int i = 0;
                    int t = 0;

                    ListView1.FocusedItem.Remove();
                    itmCnt = ListView1.Items.Count;
                    t = 1;

                    for (i = 1; i <= itmCnt + 1; i++)
                    {
                        t = t + 1;
                    }
                }

                btnRemove.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerName.Text == "")
                {
                    MessageBox.Show("Please Enter Customer Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerName.Focus();
                    return;
                }
                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("sorry no item added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AutoIdGeneration();

                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "insert Into BillTable(BillId,CustomerName,ContactNo,Discount,DiscountPrice,Total,BillDate) VALUES ('" + txtBillId.Text + "','" + txtCustomerName.Text + "','" + txtContactNo.Text + "','" + txtDiscount.Text + "','" + txtDiscountPrice.Text + "','" + txtGTotal.Text + "','" + dtpDOB.Text + "')";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Close();


                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);

                    string cd = "insert Into BillInfoTable(BillId,ProductName,Price,Quantity,SubTotal) VALUES (@d1,@d2,@d3,@d4,@d5)";
                    cmd = new SqlCommand(cd);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", txtBillId.Text);
                    cmd.Parameters.AddWithValue("d2", ListView1.Items[i].SubItems[1].Text);
                    cmd.Parameters.AddWithValue("d3", ListView1.Items[i].SubItems[2].Text);
                    cmd.Parameters.AddWithValue("d4", ListView1.Items[i].SubItems[3].Text);
                    cmd.Parameters.AddWithValue("d5", ListView1.Items[i].SubItems[4].Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                MessageBox.Show("Successfully Saved", "Bill Treatment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rest();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT Quantity from food_beverages WHERE ItemName = '" + txtItemName.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtPrice.Text = rdr.GetValue(0).ToString().Trim();
                }
                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

            Calc();
        }


        private void Calc()
        {
            try
            {
                double val1 = 0;
                double val2 = 0;
                double.TryParse(txtPrice.Text, out val1);
                double.TryParse(txtQuantity.Text, out val2);
                val1 = Math.Round(val1, 2);
                val2 = Math.Round(val2, 2);
                double I = (val1 * val2);
                I = Math.Round(I, 2);
                txtTotal.Text = I.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public double subtot()
        {
            int i = 0;
            int j = 0;
            double k = 0;
            i = 0;
            j = 0;
            k = 0;
            try
            {
                j = ListView1.Items.Count;
                for (i = 0; i <= j - 1; i++)
                {
                    k = k + Convert.ToDouble(ListView1.Items[i].SubItems[4].Text);
                    k = Math.Round(k, 2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return k;
        }

        public void Calculate()
        {

            try
            {
                double val1 = 0;
                double val2 = 0;
                double val3 = 0;
                double val4 = 0;


                if (txtDiscount.Text != "")
                {
                    val3 = Convert.ToDouble(((Convert.ToDouble(lblSubtotal.Text)) * Convert.ToDouble(txtDiscount.Text) / 100));
                    val3 = Math.Round(val3, 2);
                    txtDiscountPrice.Text = val3.ToString();

                }

                double.TryParse(lblSubtotal.Text, out val2);
                double.TryParse(txtDiscountPrice.Text, out val3);
                double.TryParse(txtTotal.Text, out val4);
                val2 = Math.Round(val2, 2);
                val3 = Math.Round(val3, 2);
                val4 = val1 + val2 - val3;
                val4 = Math.Round(val4, 2);
                txtGTotal.Text = val4.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }
    }
}
