using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _41049764_ExamProject
{
    public partial class Form5 : Form
    {
        // Declaring global sql variables
        SqlConnection conn;
        SqlCommand command;
        SqlDataAdapter adapter;
        SqlDataReader dataReader;
        DataSet ds;

        // declaring global connection string
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user-pc\OneDrive\Desktop\C#\41049764_ExamProject\41049764_ExamProject\Database1.mdf;Integrated Security=True";
        public Form5()
        {
            InitializeComponent();

            // Declaring connection string
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        private void cmbVenue_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Establish a new SqlConnection using the connection string
                SqlConnection conn = new SqlConnection(connectionString);

                if (conn.State == ConnectionState.Closed)
                {
                    // Open the connection if it's not already open
                    conn.Open();
                }

                // Parse and validate the date input
                string dateInput = txtDate.Text;
                string dateFormat = "yyyy/MM/dd"; // Modify the format according to your needs

                if (!DateTime.TryParseExact(dateInput, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime eventDate))
                {
                    // Show an error message if the date format is invalid
                    MessageBox.Show("Invalid date format. Please enter the date in the format: " + dateFormat);
                    return;
                }

                // Build the SQL query to insert the event details into the database
                string sql = $"INSERT INTO EventDetails(Event_Name, Event_Description, Event_Date, Event_Venue, Event_Capacity, Event_Category, Event_Fee, Event_Registered_Amount, Event_Aditional_Requirements) VALUES ('{txtEName.Text}', '{txtDescr.Text}','{eventDate.ToString("yyyy-MM-dd")}', '{cmbVenue.SelectedItem}','{cmbCapacity.SelectedItem}','{txtCategory.Text}','{txtFee.Text}','{txtManyReg.Text}','{txtAddReq.Text}')";

                // Create a new SqlCommand with the SQL query and the SqlConnection
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter();

                // Set the InsertCommand of the SqlDataAdapter to the SqlCommand
                adapter.InsertCommand = command;

                // Execute the insert command
                adapter.InsertCommand.ExecuteNonQuery();

                // Close the connection
                conn.Close();

                // Clear the text boxes
                txtAddReq.Text = "";
                txtCategory.Text = "";
                txtDate.Text = "";
                txtDescr.Text = "";
                txtEName.Text = "";
                txtFee.Text = "";
                txtManyReg.Text = "";

                // Reload the data in the DataGridView
                loadAll();
            }
            catch (Exception err)
            {
                // Show an error message if an exception occurs
                MessageBox.Show("The following error occurred:\t" + err.ToString());
            }

        }
        public void loadAll()
        {
            try
            {
                // Create a new SqlConnection using the connection string
                conn = new SqlConnection(connectionString);

                if (conn.State == ConnectionState.Closed)
                {
                    // Open the connection if it's not already open
                    conn.Open();
                }

                // Define the SQL query to select event details from the EventDetails table
                string sql = "SELECT Event_Name, Event_Description, Event_Date, Event_Venue, Event_Capacity, Event_Category, Event_Fee, Event_Registered_Amount, Event_Aditional_Requirements FROM EventDetails";

                // Create a new SqlCommand with the SQL query and the SqlConnection
                command = new SqlCommand(sql, conn);
                adapter = new SqlDataAdapter();

                // Set the SelectCommand of the SqlDataAdapter to the SqlCommand
                adapter.SelectCommand = command;
                ds = new DataSet();

                // Fill the DataSet with the data from the EventDetails table
                adapter.Fill(ds, "EventDetails");

                // Set the DataSource of the dataGridView to the DataSet
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "EventDetails";

                // Close the connection
                conn.Close();
            }
            catch (Exception err)
            {
                // Show an error message if an exception occurs
                MessageBox.Show("The following error occurred:\t" + err.ToString());
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // Add venue options to the cmbVenue ComboBox
            cmbVenue.Items.Add("Crystal Gardens Convention Center");
            cmbVenue.Items.Add("Starlight Ballroom");
            cmbVenue.Items.Add("Serenity Plaza");
            cmbVenue.Items.Add("Golden Pavilion");
            cmbVenue.Items.Add("Emerald Hall");
            cmbVenue.Items.Add("Moonbeam Theater");
            cmbVenue.Items.Add("Harmony Lounge");

            // Set the drop-down style of cmbVenue and cmbCapacity to DropDownList
            cmbVenue.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCapacity.DropDownStyle = ComboBoxStyle.DropDownList;

            // Load all data
            loadAll();

            try
            {
                // Create a new SqlConnection using the connection string
                conn = new SqlConnection(connectionString);

                if (conn.State == ConnectionState.Closed)
                {
                    // Open the connection if it's not already open
                    conn.Open();
                }

                // Define the SQL query to select from the EventRegistration table
                string sql = "SELECT User_Email, Event_Name, Registration_fee FROM EventRegistration";

                // Create a new SqlCommand with the SQL query and the SqlConnection
                command = new SqlCommand(sql, conn);
                adapter = new SqlDataAdapter();

                // Set the SelectCommand of the SqlDataAdapter to the SqlCommand
                adapter.SelectCommand = command;
                ds = new DataSet();

                // Fill the DataSet with the data from the EventRegistration table
                adapter.Fill(ds, "EventRegistration");

                // Set the DataSource of the dataGridView2 to the DataSet
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "EventRegistration";

                // Close the connection
                conn.Close();
            }
            catch (Exception er)
            {
                // Show an error message if an exception occurs
                MessageBox.Show("The following error occurred:\t" + er.ToString());
            }
        }

        private void cmbVenue_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Get the selected venue from the first combo box
            string selectedVenue = cmbVenue.SelectedItem.ToString();

            // Update the second combo box (cmbWord) based on the selected venue
            if (selectedVenue == "Crystal Gardens Convention Center")
            {
                // Clear the existing items in cmbCapacity
                cmbCapacity.Items.Clear();

                // Add the capacity option for Crystal Gardens Convention Center
                cmbCapacity.Items.Add("1000");
            }
            else if (selectedVenue == "Starlight Ballroom")
            {
                cmbCapacity.Items.Clear();
                cmbCapacity.Items.Add("500");
            }
            else if (selectedVenue == "Serenity Plaza")
            {
                cmbCapacity.Items.Clear();
                cmbCapacity.Items.Add("300");
            }
            else if (selectedVenue == "Golden Pavilion")
            {
                cmbCapacity.Items.Clear();
                cmbCapacity.Items.Add("250");
            }
            else if (selectedVenue == "Emerald Hall")
            {
                cmbCapacity.Items.Clear();
                cmbCapacity.Items.Add("200");
            }
            else if (selectedVenue == "Moonbeam Theater")
            {
                cmbCapacity.Items.Clear();
                cmbCapacity.Items.Add("150");
            }
            else if (selectedVenue == "Harmony Lounge")
            {
                cmbCapacity.Items.Clear();
                cmbCapacity.Items.Add("100");
            }

            // Set the initial selected index of the second combo box
            cmbCapacity.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Exception handeling
            try
            {
                // declaring new connection 
                conn = new SqlConnection(connectionString);

                if (conn.State == ConnectionState.Closed)
                {
                    // Testing if connection is open
                    conn.Open();
                }

                // Delete from the tabel
                string sql = "DELETE FROM EventDetails WHERE Event_Name ='" + txtCancel.Text + "'";

                command = new SqlCommand(sql, conn);
                adapter = new SqlDataAdapter();

                // Deleting the information
                adapter.DeleteCommand = command;
                adapter.DeleteCommand.ExecuteNonQuery();

                // Closing connection
                conn.Close();
                txtCancel.Text = "";
                loadAll();
            }
            catch (Exception err)
            {
                // Set error messasage
                MessageBox.Show("Error" + err.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Dispalying form 1
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void btnUpdateE_Click(object sender, EventArgs e)
        {
            // Exception handeling
            try
            {
                // testing if connection is open
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Updateing the information
                string sql = @"UPDATE EventDetails SET 
                    Event_Description = @EventDescription,
                    Event_Date = @EventDate,
                    Event_Venue = @EventVenue,
                    Event_Capacity = @EventCapacity,
                    Event_Category = @EventCategory,
                    Event_Fee = @EventFee,
                    Event_Registered_Amount = @EventRegisteredAmount,
                    Event_Aditional_Requirements = @EventRequirements
                  WHERE Event_Name = @EventName";

                // Adding the updated infromation into the table
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@EventDescription", txtDescr.Text);
                command.Parameters.AddWithValue("@EventDate", txtDate.Text);
                command.Parameters.AddWithValue("@EventVenue", cmbVenue.SelectedItem.ToString());
                command.Parameters.AddWithValue("@EventCapacity", cmbCapacity.SelectedItem.ToString());
                command.Parameters.AddWithValue("@EventCategory", txtCategory.Text);
                command.Parameters.AddWithValue("@EventFee", txtFee.Text);
                command.Parameters.AddWithValue("@EventRegisteredAmount", txtManyReg.Text);
                command.Parameters.AddWithValue("@EventRequirements", txtAddReq.Text);
                command.Parameters.AddWithValue("@EventName", txtEName.Text);

                command.ExecuteNonQuery();

                // Update successful
                MessageBox.Show("Update successful");

                // Closing connection
                conn.Close();

                // Clearing the text boxes
                txtAddReq.Text = "";
                txtCategory.Text = "";
                txtDate.Text = "";
                txtDescr.Text = "";
                txtEName.Text = "";
                txtFee.Text = "";
                txtManyReg.Text = "";

                loadAll();
            }
            catch (SqlException se)
            {
                MessageBox.Show("The following error occurred:\n" + se.ToString()); // Show error message
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

                // Selection all the information from the table
                string sql = "SELECT * FROM EventDetails WHERE Event_Name = @EventName";

                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@EventName", txtFill.Text);

                SqlDataReader dr = command.ExecuteReader();

                // reading to get the infromation from the table
                if (dr.Read())
                {
                    txtEName.Text = dr["Event_Name"].ToString();
                    txtDescr.Text = dr["Event_Description"].ToString();
                    txtDate.Text = dr["Event_Date"].ToString();
                    txtCategory.Text = dr["Event_Category"].ToString();
                    txtFee.Text = dr["Event_Fee"].ToString();
                    txtManyReg.Text = dr["Event_Registered_Amount"].ToString();
                    txtAddReq.Text = dr["Event_Aditional_Requirements"].ToString();

                    // reading from the combo boxes
                    string venue = dr["Event_Venue"].ToString();
                    int venueIndex = cmbVenue.FindStringExact(venue);
                    cmbVenue.SelectedIndex = venueIndex;

                    string capacity = dr["Event_Capacity"].ToString();
                    int capacityIndex = cmbCapacity.FindStringExact(capacity);
                    cmbCapacity.SelectedIndex = capacityIndex;
                }

                // Closing the connection
                conn.Close();
                loadAll();
                txtFill.Text = "";
            }
            catch (SqlException se)
            {
                MessageBox.Show("The following error occurred:\t" + se.ToString());  //show error message
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clearing all the text boxes
            txtAddReq.Text = "";
            txtCategory.Text = "";
            txtDate.Text = "";
            txtDescr.Text = "";
            txtEName.Text = "";
            txtFee.Text = "";
            txtManyReg.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout1_Click(object sender, EventArgs e)
        {
            // Displaying form 1
            Form1 Frm1 = new Form1();
            Frm1.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Displaying form 4
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();
        }
    }
}
