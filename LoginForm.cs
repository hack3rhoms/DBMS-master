using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace DBMS
{
    public partial class loginFrom : Form
    {
        public loginFrom()
        {
            InitializeComponent();
        }


        private NpgsqlConnection conn;
        string connString = String.Format("Server={0};Port={1};" +
            "User Id={2};Password={3};Database={4};", "localhost", "5432", "postgres", "1234", "DBMS");
        private NpgsqlCommand cmd;
        private string sql = null;

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            

            try
            {
                conn.Open();

                sql = @"select * from public.u_login(:_username,:_password)";
                cmd = new NpgsqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("_username", usernameTxtBox.Text);
                cmd.Parameters.AddWithValue("_password", passTxtBox.Text);

                int result = (int)cmd.ExecuteScalar();
                
                if (result == 1)
                {
                    this.Hide();
                    new Form1().Show();
                }
                else
                {
                    MessageBox.Show("Please check your username and password", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmd.Dispose();
                conn.Close();
            }
        }

        private void loginFrom_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connString);
        }
    }
}
