using System.Data;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Npgsql;

namespace ResponsiAriq
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private NpgsqlConnection conn;
        string connstring = "Host=localhost;Port=2022;Username=postgres;Password=informatika;Database=responsiAriq";
        public DataTable dt;
        public static NpgsqlCommand cmd;
        public string sql=null;
        private DataGridViewRow r;
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
           { 
            int idDept = 0;
            if (cbDepartemen.Text == "HR") { idDept = 1; }
            else if (cbDepartemen.Text == "ENG") { idDept = 2; }
            else if (cbDepartemen.Text == "DEV") { idDept = 3; }
            else if (cbDepartemen.Text == "PM") { idDept = 4; }
            else if (cbDepartemen.Text == "FIN") { idDept = 5; }
            try
            {
                conn.Open();
                sql = @"select * from insert_karyawan(:_nama, :_id_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_name", txtNama.Text);
                cmd.Parameters.AddWithValue("_id_dep", idDept);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data users berhasil disimpan", "Well Done!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }

            }
            catch
            {

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //int idDept = 0;
            //if (r == null)
            //{
            //    MessageBox.Show("Mohon pilih baris data yang akan diupdate!", "Naissss", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if(cbDepartemen.Text == "HR") { idDept = 1; }
            //else if(cbDepartemen.Text == "ENG") { idDept = 2; }
            //else if(cbDepartemen.Text == "DEV") { idDept = 3; }
            //else if(cbDepartemen.Text == "PM") { idDept = 4; }
            //else if( cbDepartemen.Text == "FIN") { idDept = 5; }
            //try
            //{
            //    conn.Open();
            //    sql = @"select * from eidt_karyawan(:_nama, :_id_dep, :_id_karyawan)";
            //    cmd = new NpgsqlCommand(sql, conn);
            //    cmd.Parameters.AddWithValue("_id", r.Cells["_id"].Value.ToString());
            //    cmd.Parameters.AddWithValue("_name", txtNama.Text);
            //    cmd.Parameters.AddWithValue("_alamat", txtAlamat.Text);
            //    cmd.Parameters.AddWithValue("_no_handphone", txtNo_Handphone.Text);
            //    if ((int)cmd.ExecuteScalar() == 1)
            //    {
            //        MessageBox.Show("Data users berhasil diupdate", "Well Done!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        conn.Close();
            //        btnLoaddata.PerformClick();
            //        txtName.Text = txtAlamat.Text = txtNo_Handphone.Text = null;
            //        r = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message, "FAIL UPDATE!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                dgvData.DataSource = null;
                sql = "select id_karyawan, nama, tb_karyawan.id_dep, nama_dep from tb_karyawan left join tb_departemen ON tb_departemen.id_dep = tb_karyawan.id_dep";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                dgvData.DataSource = dt;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "FAIL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}