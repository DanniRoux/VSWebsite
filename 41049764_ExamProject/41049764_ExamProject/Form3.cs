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
    public partial class Form3 : Form
    {
        // Declaring global variables
        SqlConnection conn;
        SqlCommand command;
        SqlDataAdapter adapter;
        SqlDataReader dataReader;
        DataSet ds;

        // Declaring connection string
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user-pc\OneDrive\Desktop\C#\41049764_ExamProject\41049764_ExamProject\Database1.mdf;Integrated Security=True";

        public Form3()
        {
            InitializeComponent();

            // Setting the from to be in the center
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Displaying form 1 and hiding the current one
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void btnLogin1_Click(object sender, EventArgs e)
        {
            // Exception handeling
            try
            {
                // if email is empty
                if (txtEmailLog.Text == "")
                {
                    // Throw error
                    errorProvider1.SetError(txtEmailLog, "Please enter a Username");
                    txtEmailLog.Text = "";
                    txtPassLog.Text = "";  //show error and clear textboxes
                    txtEmailLog.Focus();
                } // if password is empty
                else if (txtPassLog.Text == "")
                {
                    // Throw error
                    errorProvider1.Clear();
                    errorProvider1.SetError(txtPassLog, "Please enter a Password");
                    txtPassLog.Text = "";  //show error and clear textboxes
                    txtPassLog.Focus();
                }
                else
                {
                    // Exception handeling
                    try
                    {
                        // new sql connection
                        conn = new SqlConnection(connectionString);

                        if (conn.State == ConnectionState.Closed)
                        {
                            // Testing if connnection is open
                            conn.Open();
                        }

                        // Selecting all info from table
                        string sql = "SELECT * FROM UserInfo";

                        command = new SqlCommand(sql, conn);
                        dataReader = command.ExecuteReader();

                        bool matchFound = false;  //check to see if details match the details in the table

                        while (dataReader.Read())
                        {
                            //get the value from the textboxes
                            if (txtEmailLog.Text == dataReader.GetValue(2).ToString() && txtPassLog.Text == dataReader.GetValue(3).ToString())  
                            {
                                matchFound = true;
                                break;
                            }
                        }
                   
                        // Closing connection
                        conn.Close();

                        if (matchFound)
                        {
                            // Displaying form 4 and hide the current one
                            Form4 frm4 = new Form4();
                            frm4.Show();
                            this.Hide();

                        }
                        else
                        {
                            // Error if username or password is incorrect
                            lblError.Text = "Username or Password is Incorrect!";
                            txtEmailLog.Text = "";
                            txtPassLog.Text = "";  //show error
                            txtEmailLog.Focus();
                        }
                    }
                    catch (SqlException se)
                    {
                        // Error message
                        MessageBox.Show("The following error occurred:\t" + se.ToString());  //show error message
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("The following error occurred:\t" + er.ToString());  //show error message
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Setting cursor on text box
            ActiveControl = txtEmailLog; 
        }
    }
}
