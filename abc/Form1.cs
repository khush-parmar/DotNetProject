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
namespace abc
{
    public partial class Form1 : Form
    {
        string constr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\khushal\abc\abc\vikram.mdf;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bindgrid();
        }

        private void bindgrid()
        {
            SqlConnection cn = new SqlConnection(constr);
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from mytbl", cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cn.Close();
          
        }
        private void clearall()
        {
            this.txtid.Clear();
            this.txtnm.Clear();
            this.txtct.Clear();
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            savedata();
            clearall();
            bindgrid();
        }
        private void savedata()
        {
            SqlConnection cn = new SqlConnection(constr);
            cn.Open();
            SqlCommand cmd = new SqlCommand("insert into mytbl(id,name,city)values(@id,@nm,@ct)", cn);
            cmd.Parameters.AddWithValue("@id", txtid.Text);
            cmd.Parameters.AddWithValue("@nm", txtnm.Text);
            cmd.Parameters.AddWithValue("@ct", txtct.Text);
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("data inserted");
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            deletedata();
            bindgrid();
            clearall();
        }

        private void deletedata()
        {
            SqlConnection cn = new SqlConnection(constr);
            cn.Open();
            SqlCommand cmd = new SqlCommand("delete from mytbl where id=@id", cn);
            cmd.Parameters.AddWithValue("@id", txtid.Text);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Updatedata();
            bindgrid();
            clearall();
        }

        private void Updatedata()
        {
            SqlConnection cn = new SqlConnection(constr);
            cn.Open();
            SqlCommand cmd = new SqlCommand("update mytbl set name=@nm,city=@ct where id=@id", cn);
            cmd.Parameters.AddWithValue("@id", txtid.Text);
            cmd.Parameters.AddWithValue("@nm", txtnm.Text);
            cmd.Parameters.AddWithValue("@ct", txtct.Text);
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("data updated");
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            Selectdata();
        }

        private void Selectdata()
        {
            SqlConnection cn = new SqlConnection(constr);
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from mytbl where id=@id", cn);
            cmd.Parameters.AddWithValue("@id",txtid.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txtnm.Text = dt.Rows[0]["name"].ToString();
            txtct.Text = dt.Rows[0]["city"].ToString();
            cn.Close();
        }

       

        
    }
}
