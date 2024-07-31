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
    public partial class Form4 : Form
    {
        public string eventName { get; set; }
        public string userEmail { get; set; }

        // Declaring global variables
        SqlConnection conn;
        SqlCommand command;
        SqlDataAdapter adapter;
        SqlDataReader dataReader;
        DataSet ds;

        // Declaring connection string
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user-pc\OneDrive\Desktop\C#\41049764_ExamProject\41049764_ExamProject\Database1.mdf;Integrated Security=True";
        string username;

        public Form4()
        {
            InitializeComponent();

            // Setting the from to be in the center
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void btnPremium_Click(object sender, EventArgs e)
        {
            // Exception handeling
            try
            {
                // new sql connection
                conn = new SqlConnection(connectionString);

                // Testing if connnection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Assuming you have an input field for the username
                string username = txtUsername.Text; 

                // Retrieve the user's membership information from the database
                string sql = $"SELECT User_Membership FROM UserInfo WHERE User_Name = @username";
                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@username", username);

                string membership = command.ExecuteScalar()?.ToString();

                if (membership == "Standard")
                {
                    // Update the users membership to premium in the database
                    string updateSql = $"UPDATE UserInfo SET User_Membership = 'Premium' WHERE User_Name = @username";
                    SqlCommand updateCommand = new SqlCommand(updateSql, conn);
                    updateCommand.Parameters.AddWithValue("@username", username);
                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Membership upgraded to premium successfully.");

                    // Update the membership variable
                    membership = "Premium"; 
                }
                else if (membership == "Premium")
                {
                    // Error message
                    MessageBox.Show("User already has a premium membership.");
                }
                else
                {
                    // Handle the case accordingly
                    MessageBox.Show("Invalid membership or membership information not found.");
                }

                // Closing connection
                conn.Close();

                if (membership == "Premium")
                {
                    // User is already a premium member, show the host button
                    btnHost.Visible = true;
                }
                else
                {
                    btnHost.Visible = false;
                }
            }
            catch (SqlException se)
            {
                // Show error message
                MessageBox.Show("The following error occurred:\t" + se.ToString());  
            }
        
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Exception handeling
            try
            {
                // new sql connection
                conn = new SqlConnection(connectionString);

                // Testing if connnection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // selection spesific information from the table
                string sql = $"SELECT Event_Name, Event_Description, Event_Date, Event_Venue, Event_Capacity, Event_Category, Event_Fee, Event_Registered_Amount, Event_Aditional_Requirements  FROM EventDetails WHERE Event_Name LIKE '%{txtSearch.Text}%'";
                command = new SqlCommand(sql, conn);
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                // Filling the table
                ds = new DataSet();
                adapter.Fill(ds, "EventDetails");

                // Displaying in the grid view
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "EventDetails";

                // Closing connection
                conn.Close();
                txtSearch.Text = "";   // Clearing textbox

            }
            catch (Exception err)
            {
                // Show error
                MessageBox.Show("Error: " + err.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Exception handeling
            try
            {
                // checking if connection is opened
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Updating the information the user selected
                string sql = @"UPDATE UserInfo SET User_Name = @User_Name, User_Email = @User_Email, User_Password = @User_Password WHERE User_Name = @Old_User_Name";

                // Filling the table with updated information
                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@User_Name", txtName.Text);
                command.Parameters.AddWithValue("@User_Email", txtEmail.Text);
                command.Parameters.AddWithValue("@User_Password", txtPassword.Text);
                command.Parameters.AddWithValue("@Old_User_Name", txtFill.Text);

                command.ExecuteNonQuery();

                // Update successful
                MessageBox.Show("Update successful");

                // Closing the connection
                conn.Close();

                // Clearing the textboxes
                txtFill.Text = "";
                txtName.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtFill.Focus();
            }
            catch (SqlException se)
            {
                MessageBox.Show("The following error occurred:\t" + se.ToString());  //show error message
            }
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            // Exception handeling
            try
            {
                // checking if the connection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Selecting all information from the table
                string sql = "SELECT * FROM UserInfo WHERE User_Name = @UserName";

                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@UserName", txtFill.Text);

                SqlDataReader dr = command.ExecuteReader();

                // Reading to get infromation
                if (dr.Read())
                {
                    txtName.Text = dr["User_Name"].ToString();
                    txtEmail.Text = dr["User_Email"].ToString();
                    txtPassword.Text = dr["User_Password"].ToString();
                }

                // Closing connection
                conn.Close();
            }
            catch (SqlException se)
            {
                MessageBox.Show("The following error occurred:\t" + se.ToString());  //show error message
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Making the button not visable
            btnHost.Visible = false;

            // Exception handeling
            try
            {
                // declaring the new sql connection
                conn = new SqlConnection(connectionString);

                // checking if the connection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Selecting all from the table
                string sql = "SELECT Event_Name, Event_Description, Event_Date, Event_Venue, Event_Capacity, Event_Category, Event_Fee, Event_Registered_Amount, Event_Aditional_Requirements FROM EventDetails";

                command = new SqlCommand(sql, conn);
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                // Filling the adapter
                ds = new DataSet();
                adapter.Fill(ds, "EventDetails");

                // Displying in the grid
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "EventDetails";

                // Closing connection
                conn.Close();

            }
            catch (Exception er)
            {
                MessageBox.Show("The following error occurred:\t" + er.ToString());  //show error message
            }

            // Testing if the member is premium or not
            if (!string.IsNullOrEmpty(username))
            {
                string membership = GetMembershipStatus(username);
                if (membership == "Premium")
                {
                    // User is logged in and a premium member, show the host button
                    btnHost.Visible = true;
                }
            }
        }
        private string GetMembershipStatus(string username)
        {
            // Exception handeling
            try
            {
                // declaring the new sql connection
                conn = new SqlConnection(connectionString);

                // checking if the connection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Selecting all from the table
                string sql = $"SELECT User_Membership FROM UserInfo WHERE User_Name = @username";
                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@username", username);

                // Testing if the member is premium or not
                string membership = command.ExecuteScalar()?.ToString();

                // Closing connection
                conn.Close();

                return membership;
            }
            catch (SqlException se)
            {
                MessageBox.Show("The following error occurred:\t" + se.ToString());  // Show error message
                return null;
            }
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnHost_Click(object sender, EventArgs e)
        {
            // Displaying form 5
            Form5 frm5 = new Form5();
            frm5.Show();
            this.Hide();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLogO_Click(object sender, EventArgs e)
        {
            // Displaying form 1
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void btnRegisterEvent_Click(object sender, EventArgs e)
        {
            // Exception handeling
            try
            {
                // declaring the new sql connection
                conn = new SqlConnection(connectionString);

                // checking if the connection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // assigning variabes
                string eventName = txtEReg.Text;
                string userEmail = txtNameReg.Text;

                // Retrieve the registration fee and current registered amount for the selected event
                string eventDetailsSql = "SELECT Event_Fee, Event_Registered_Amount, Event_Capacity FROM EventDetails WHERE Event_Name = @eventName";
                SqlCommand eventDetailsCommand = new SqlCommand(eventDetailsSql, conn);
                eventDetailsCommand.Parameters.AddWithValue("@eventName", eventName);
                SqlDataReader eventDetailsReader = eventDetailsCommand.ExecuteReader();

                // getting the inforrmation from the table
                if (eventDetailsReader.Read())
                {
                    decimal registrationFee = eventDetailsReader.GetDecimal(0);
                    int registeredAmount = eventDetailsReader.GetInt32(1);
                    int eventCapacity = eventDetailsReader.GetInt32(2);

                    eventDetailsReader.Close();

                    // Check if the event is already full
                    if (registeredAmount >= eventCapacity)
                    {
                        MessageBox.Show($"Sorry, the event {eventName} is already full. Registration is closed.");
                        txtEReg.Text = "";
                        return;
                    }

                    // Display the registration fee
                    DialogResult result = MessageBox.Show($"The registration fee for {eventName} is {registrationFee.ToString("C")}. Do you accept the fee?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Insert the event registration into the EventRegistration table
                        string insertSql = "INSERT INTO EventRegistration (User_Email, Event_Name, Registration_Fee) VALUES (@userEmail, @eventName, @registrationFee)";
                        SqlCommand insertCommand = new SqlCommand(insertSql, conn);

                        // Make sure this matches the textbox name
                        insertCommand.Parameters.AddWithValue("@userEmail", userEmail); 
                        insertCommand.Parameters.AddWithValue("@eventName", eventName);
                        insertCommand.Parameters.AddWithValue("@registrationFee", registrationFee);
                        insertCommand.ExecuteNonQuery();

                        // Message to show it was successful
                        MessageBox.Show("Event registration added successfully!");

                        // Clearing text boxs
                        txtEReg.Text = "";
                        txtNameReg.Text = "";
                    }
                    else
                    {
                        // User clicked "No" or closed the message box
                        // Clear the textboxes
                        txtEReg.Text = "";
                        txtNameReg.Text = "";
                        return;
                    }
                }
                else
                {
                    // error message
                    eventDetailsReader.Close();
                    MessageBox.Show($"The registration fee for {eventName} is not available.");
                }

                // closing connection
                conn.Close();
            }
            catch (SqlException se)
            {
                MessageBox.Show("The following error occurred:\n" + se.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred:\n" + ex.Message);
            }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            // Displaying form 1
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            // Displaying form 6
            Form6 frm6 = new Form6();
            frm6.Show();
            this.Hide();
        }

        private void btnLog1_Click(object sender, EventArgs e)
        {
            // Displaying form 1
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            // Exception handeling
            try
            {
                // declaring the new sql connection
                conn = new SqlConnection(connectionString);

                // checking if connection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Selecting all from the table
                string sql = "SELECT Event_Name, Event_Description, Event_Date, Event_Venue, Event_Capacity, Event_Category, Event_Fee, Event_Registered_Amount, Event_Aditional_Requirements FROM EventDetails";

                command = new SqlCommand(sql, conn);
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                // Filling the table
                ds = new DataSet();
                adapter.Fill(ds, "EventDetails");

                // displaying in the grid view
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "EventDetails";

                // Closing connection
                conn.Close();

            }
            catch (Exception er)
            {
                MessageBox.Show("The following error occurred:\t" + er.ToString());  //show error message
            }
        }
    }
}
