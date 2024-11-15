using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amusement_Park_MS
{
    public partial class panelEmployee : Form
    {
        public panelEmployee()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Form login_Form = new Login_Form();
            login_Form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Form login_Form = new Login_Form();
            login_Form.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Minimized;
            else
                WindowState = FormWindowState.Normal;
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
            panelChildForm.Controls.Add(ChildForm);
            panelChildForm.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new RideManagement());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new TicketBooking());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            openChildForm(new TicketCancle());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new Food_and_Beverages());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openChildForm(new Order_Food());
        }

        /*private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new Package());
        }*/

        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new Customer_Feedback());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Form login_Form = new Login_Form();
            login_Form.ShowDialog();

        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new VisitorManagement());
        }

        private void BillBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new BillingForm());
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            openChildForm(new Report());
        }
    }

    
}

    

    



