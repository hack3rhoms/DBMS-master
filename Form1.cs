using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;


namespace DBMS
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection conn = new NpgsqlConnection
            ("Server=localhost;Port=5432;Database=DBMS;User Id=postgres;Password=1234;");
        private NpgsqlCommand cmd = new NpgsqlCommand();
        private string sql = null;

        private void dataGridView()
        {
            conn.Open();
            
            sql = @"select * from public.customer_list";
            cmd = new NpgsqlCommand(sql, conn);

            
            DataTable dt1 = new DataTable();
            NpgsqlDataAdapter dr1 = new NpgsqlDataAdapter(cmd);
            dr1.Fill(dt1);
            customerView.DataSource = dt1;

            
            sql = "select * from public.film_list";
            cmd = new NpgsqlCommand(sql, conn);

            DataTable dt2 = new DataTable();
            NpgsqlDataAdapter dr2 = new NpgsqlDataAdapter(cmd);
            dr2.Fill(dt2);
            filmView.DataSource = dt2;

            conn.Close();

        }
        private void deleteCustomer(int cid)
        {
            conn.Open();
            sql = @"call public.delete_customer(" +cid + ")";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }
        private void deleteFilm(int fid)
        {
            conn.Open();
            sql = @"call public.delete_film("+ fid + ")";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public Form1()
        {
            InitializeComponent();
            dataGridView();
        }


        

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void fInsertBtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            sql = @"call public.film_insert('" + iTitleTxtBox.Text + "','" + iCategoryTxtBox.Text + "','" + iActorFnameBox.Text + "','" + iActorLnameBox.Text + "'," + iYearTxtBox.Text + ",'" + iRatingCombo.Text + "'," + iLengthTxtBox.Text + ",'" + iDescTxtBox.Text + "')";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteReaderAsync();
            MessageBox.Show("Data Inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            
            conn.Close();
            dataGridView();

        }

        private void cInsertBtn_Click(object sender, EventArgs e)
        { 
            conn.Open();
            sql = @"call public.customer_insert('" + iFnameTxtBox.Text + "','" + iLnameTxtBox.Text + "','" + iEmailTxtBox.Text + "','" + iPhoneTxtBox.Text + "','" + iCityTxtBox.Text + "','" + iCountryTxtBox.Text + "','" + iZipcodTxtBox.Text + "','" +iAddressTxtBox.Text + "')";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteReaderAsync();
            MessageBox.Show("Data Inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            conn.Close();
            dataGridView();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void fDeleteBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in filmView.SelectedRows)
            {
                int fid = Convert.ToInt32(drow.Cells[0].Value);
                deleteFilm(fid);
                dataGridView();
            }
        }

        private void cDeleteBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in customerView.SelectedRows)
            {
                int cid = Convert.ToInt32(drow.Cells[0].Value);
                deleteCustomer(cid);
                dataGridView();
            }
        }

        int i = 0;
      
        private void filmView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            uTitleTxtBox.Text = filmView.Rows[i].Cells[1].Value.ToString();
            uDescBox.Text = filmView.Rows[i].Cells[2].Value.ToString();
            uCategoryTxtBox.Text = filmView.Rows[i].Cells[3].Value.ToString();
            uRYearTxtBox.Text = filmView.Rows[i].Cells[4].Value.ToString();
            uLengthBox.Text = filmView.Rows[i].Cells[6].Value.ToString();
            uRatingBox.SelectedItem = filmView.Rows[i].Cells[7].Value.ToString();
            uActorFnameBox.Text = filmView.Rows[i].Cells[8].Value.ToString();
            uActorLnameBox.Text = filmView.Rows[i].Cells[9].Value.ToString();
        }
        private void fUpdateBtn_Click(object sender, EventArgs e)
        {
            
            conn.Open();
            sql = @"call public.film_update('" + uTitleTxtBox.Text + "','" + uCategoryTxtBox.Text + "','" + uActorFnameBox.Text + "','" + uActorLnameBox.Text + "'," + uRYearTxtBox.Text + ",'" + uRatingBox.Text + "'," + uLengthBox.Text + ",'" + uDescBox.Text + "',"+filmView.Rows[i].Cells[0].Value+")";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteReaderAsync();
            MessageBox.Show("Data Updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            conn.Close();
            dataGridView();
        }
        private void customerView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            uFNameBox.Text = customerView.Rows[i].Cells[1].Value.ToString();
            ULNameBox.Text = customerView.Rows[i].Cells[2].Value.ToString();
            uEmailBox.Text = customerView.Rows[i].Cells[3].Value.ToString();
            uAddressBox.Text = customerView.Rows[i].Cells[4].Value.ToString();
            uZCodeBox.Text = customerView.Rows[i].Cells[5].Value.ToString();
            uPhoneBox.Text = customerView.Rows[i].Cells[6].Value.ToString();
            uCityBox.Text = customerView.Rows[i].Cells[7].Value.ToString();
            uCountryBox.Text = customerView.Rows[i].Cells[8].Value.ToString();

        }
        private void cUpdateBtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            sql = @"call public.customer_update('" + uFNameBox.Text + "','" + ULNameBox.Text + "','" + uEmailBox.Text + "','" + uPhoneBox.Text + "','" + uCityBox.Text + "','" + uCountryBox.Text + "','" + uZCodeBox.Text + "','" + uAddressBox.Text + "'," + customerView.Rows[i].Cells[0].Value + ")";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteReaderAsync();
            MessageBox.Show("Data Updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            conn.Close();
            dataGridView();
        }

        

        private void fSearcgBtn_Click(object sender, EventArgs e)
        {
            sql = "select * from public.film_list";
            cmd = new NpgsqlCommand(sql, conn);

            

            if (filmSearchCombo.Text == "Title")
            {
                sql = "select * from public.film_list where title like '%" + filmSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (filmSearchCombo.Text == "Category")
            {
                sql = "select * from public.film_list where category like '%" + filmSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (filmSearchCombo.Text == "Release Year")
            {
                sql = "select * from public.film_list where \"Release Year\" = " + filmSearchTxtBox.Text + "";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (filmSearchCombo.Text == "Rating")
            {
                sql = "select * from public.film_list where rating like '%" + filmSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (filmSearchCombo.Text == "Actor First Name")
            {
                sql = "select * from film_list where \"Actor First Name\" like '%" + filmSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (filmSearchCombo.Text == "Actor Last Name")
            {
                sql = "select * from public.film_list where \"Actor Last Name\" like '%" + filmSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
            }
            DataTable dt3 = new DataTable();
            NpgsqlDataAdapter dr3 = new NpgsqlDataAdapter(cmd);
            dr3.Fill(dt3);
            filmView.DataSource = dt3;
        }

        private void cSearchBtn_Click(object sender, EventArgs e)
        {
            sql = "select * from public.customer_list";
            cmd = new NpgsqlCommand(sql, conn);
            


            if (customerSearchCombo.Text == "First Name")
            {
                sql = "select * from public.customer_list where \"First Name\" like '%" + customerSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (customerSearchCombo.Text == "Last Name")
            {
                sql = "select * from public.customer_list where \"Last Name\" like '%" + customerSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (customerSearchCombo.Text == "Email")
            {
                sql = "select * from public.customer_list where email like '%" + customerSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (customerSearchCombo.Text == "Address")
            {
                sql = "select * from public.customer_list where address like '%" + customerSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (customerSearchCombo.Text == "Zip Code")
            {
                sql = "select * from public.customer_list where \"zip code\" like '%" + customerSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (customerSearchCombo.Text == "Phone")
            {
                sql = "select * from public.customer_list where phone like '%" + customerSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (customerSearchCombo.Text == "City")
            {
                sql = "select * from public.customer_list where city like '%" + customerSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            if (customerSearchCombo.Text == "Country")
            {
                sql = "select * from public.customer_list where country like '%" + customerSearchTxtBox.Text + "%'";
                cmd = new NpgsqlCommand(sql, conn);
                
            }
            DataTable dt4 = new DataTable();
            NpgsqlDataAdapter dr4 = new NpgsqlDataAdapter(cmd);
            dr4.Fill(dt4);
            customerView.DataSource = dt4;
            
        }
    }
}
