using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _41049764_ExamProject
{
    public partial class Form6 : Form
    {
        // Declaring global sql variables
        SqlConnection conn;
        SqlCommand command;
        SqlDataAdapter adapter;
        SqlDataReader dataReader;
        DataSet ds;

        // declaring global connection string
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user-pc\OneDrive\Desktop\C#\41049764_ExamProject\41049764_ExamProject\Database1.mdf;Integrated Security=True";
        public Form6()
        {
            InitializeComponent();

            // Declaring connection string
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            // Displaying form 4
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // Adding heading to the list box
            listBox1.Items.Add("YOUR EVENT DETAILS:");
            listBox1.Items.Add("---------------------------------------------------------------------------------------------");
            listBox1.Items.Add("Event Name \t\t     Registration fee Paid:");
            listBox1.Items.Add("---------------------------------------------------------------------------------------------");

            // Exception handeling
            try
            {
                // declaring new sql connection 
                conn = new SqlConnection(connectionString);

                // tetsing if the connection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                
                // Selecting all from the table
                string sql = "SELECT Event_Name, Registration_Fee FROM EventRegistration";

                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();

                // reading from the tabel
                while (dataReader.Read())
                {
                    // Getting the values from the table
                    string eventName = dataReader.GetString(0);
                    decimal registrationFee = dataReader.GetDecimal(1);

                    // Adding and displaying to the lisbox
                    listBox1.Items.Add($"{eventName} \t\t\t {registrationFee.ToString("C")}");
                }

                // Closing the connection
                conn.Close();
            }
            catch (Exception er)
            {
                // Set error message
                MessageBox.Show("An error occurred: " + er.ToString());
            }
        }
    }
}
