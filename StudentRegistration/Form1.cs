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

namespace StudentRegistration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentReg;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        bool Mode = true;
        string sql;


        public void Load()
        {
            try
            {
                sql = "select * from Student";
                cmd = new SqlCommand(sql, con);
                con.Open();
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }


                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void getID(String id)
        {
            sql = "SELECT * FROM Student WHERE id = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while (read.Read())
            {
                textName.Text = read[1].ToString();
                textCourse.Text = read[2].ToString();
                textFee.Text = read[3].ToString();

            }
            con.Close();
        }

       

        /*private void Form1_Load(object sender, EventArgs e)
        {

        }*/

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //if the mode is true add the records otherwise update the record
        private void button1_Click_1(object sender, EventArgs e)
        {
            string name = textName.Text;
            string course = textCourse.Text;
            string fee = textFee.Text;

            if (Mode == true)
            {
                sql = "INSERT INTO STUDENT(StudentName, Course, Fee) values(@StudentName, @Course, @Fee)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@StudentName", name);
                cmd.Parameters.AddWithValue("@Course", course);
                cmd.Parameters.AddWithValue("@Fee", fee);

                MessageBox.Show("Record Added");
                cmd.ExecuteNonQuery();

                textName.Clear();
                textCourse.Clear();
                textFee.Clear();
                textName.Focus();

            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "UPDATE Student SET StudentName = @StudentName, Course= @Course, Fee = @Fee WHERE id = @ID";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@StudentName", name);
                cmd.Parameters.AddWithValue("@Course", course);
                cmd.Parameters.AddWithValue("@Fee", fee);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record Updateddddd");
                cmd.ExecuteNonQuery();

                textName.Clear();
                textCourse.Clear();
                textFee.Clear();
                textName.Focus();
                button1.Text = "Save";
                Mode = true;
            }
            con.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textName.Clear();
            textCourse.Clear();
            textFee.Clear();
            textName.Focus();
            button1.Text = "Save";
            Mode = true;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "DELETE FROM Student WHERE id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id ", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
            }
        }
    }
}
