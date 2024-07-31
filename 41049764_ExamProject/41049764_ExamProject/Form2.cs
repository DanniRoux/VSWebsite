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
    public partial class Form2 : Form
    {
        // Declaring global variables
        SqlConnection conn;
        SqlCommand command;
        SqlDataAdapter adapter;
        SqlDataReader dataReader;
        DataSet ds;

        // Declaring connection string
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user-pc\OneDrive\Desktop\C#\41049764_ExamProject\41049764_ExamProject\Database1.mdf;Integrated Security=True";
        public Form2()
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Exception handeling
            try
            {
                // If Name is empty
                if (txtName.Text == "")
                {
                    // Error thrown
                    errorProviderReg.SetError(txtName, "Please enter a Username");

                    // Clearing text box
                    txtName.Text = "";
                    txtPassword.Text = "";  //show error and clear textboxes
                    txtName.Focus();
                    return;
                } // if email is empty
                else if (txtEmail.Text == "")
                {
                    // Error thrown
                    errorProviderReg.Clear();
                    errorProviderReg.SetError(txtPassword, "Please enter a Password");

                    // Clearing text box
                    txtEmail.Text = "";  //show error and clear textboxes
                    txtEmail.Focus();
                    return;
                } // if password is empty
                else if (txtPassword.Text == "")
                {
                    errorProviderReg.Clear();
                    errorProviderReg.SetError(txtPassword, "Please enter a Password");

                    // Clearing text box
                    txtPassword.Text = "";
                    txtPassword.Focus();
                    return;
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

                        // Asigning variables
                        string membership = "Standard";

                        // Insert user information
                        string sql = $"INSERT INTO UserInfo(User_Name, User_Email, User_Password, User_Membership) VALUES ('{txtName.Text}', '{txtEmail.Text}','{txtPassword.Text}', '{membership}')";

                        // new sql command with sql an coonnectionString paramaters
                        command = new SqlCommand(sql, conn);
                        adapter = new SqlDataAdapter();

                        // Insert user info into table
                        adapter.InsertCommand = command;
                        adapter.InsertCommand.ExecuteNonQuery();

                        // Closing connection
                        conn.Close();

                        // Displaying form 4 and hide the current one
                        Form4 frm4 = new Form4();
                        frm4.Show();
                        this.Hide();
                    }
                    catch (SqlException se)
                    {
                        // Error showing
                        MessageBox.Show("The following error occurred:\t" + se.ToString());  //show error message
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("The following error occurred:\t" + er.ToString());  //show error message
            }

            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Setting the cursor at the text box
            txtName.Focus();
        }
    }
}
