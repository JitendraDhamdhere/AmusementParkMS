using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Amusement_Park_MS
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        private Form activeForm = null;
        private void openChildForm(Form ChildForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.FormBorderStyle = FormBorderStyle.None;
            ChildForm.Dock = DockStyle.Fill;
            panelchildForm.Controls.Add(ChildForm);
            panelchildForm.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //openChildForm(new Dashboard());
            //..
            //Your Codes
            //..
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new EmployeeA());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new Maintenance2());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new Suppliercs());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Form login_Form = new Login_Form();
            login_Form.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Form login_Form = new Login_Form();
            login_Form.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Minimized;
            else
                WindowState = FormWindowState.Normal;   
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void panelchildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new VisitorManagement());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openChildForm(new Report());

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BillBtn1_Click(object sender, EventArgs e)
        {
            openChildForm(new BillingForm());

        }
    }
}

