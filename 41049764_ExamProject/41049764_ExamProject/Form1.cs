using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _41049764_ExamProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Setting the from to be in the center
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Displaying form 2 and hiding the current form
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Displaying form 3 and hiding the current form
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Exiting the application
            Application.Exit();
        }
    }
}
