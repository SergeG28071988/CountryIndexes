using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CountryIndexes
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CountryIndexes;Integrated Security=True;";
        private void LoadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch
            {
                MessageBox.Show("Ошибка работы с БД");
            }
        }

        private void LoadData()
        {
            using (var cn = new SqlConnection(connectionString))
            {
                cn.Open();

                string sql = "SELECT Country, GDP, HDI, CO2 FROM Countries;";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView.DataSource = ds.Tables[0];
            }
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            var frm = new EditForm();

            if (frm.ShowDialog(this) == DialogResult.OK)
            {

                try
                {
                    // Реализовать самостоятельно

                    LoadData();
                }

                catch
                {
                    MessageBox.Show("Ошибка работы с БД");
                }
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
                return;
            try
            {
                var country = dataGridView.SelectedRows[0].Cells["Country"].Value.ToString();

                var sql = "DELETE FROM Countries WHERE Country=@country";

                using (var cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
                LoadData();
            }
            catch
            {
                MessageBox.Show("Ошибка работы с БД");
            }

        }

        private void ChangeBtn_Click(object sender, EventArgs e)
        {
            var frm = new EditForm();
            if (dataGridView.SelectedRows.Count == 0)
                return;

            var country = dataGridView.SelectedRows[0].Cells["Country"].Value.ToString();

            frm.Country = country;
            frm.GDP = (int)dataGridView.SelectedRows[0].Cells["GDP"].Value;
            frm.HDI = (double)dataGridView.SelectedRows[0].Cells["HDI"].Value;
            frm.CO2 = (double)dataGridView.SelectedRows[0].Cells["CO2"].Value;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var sql = @"UPDATE Countries SET
                        Country=@country, GDP=@GDP, HDI=@HDI, CO2=@CO2 WHERE Country=@country1;";

                    using (var cn = new SqlConnection(connectionString))
                    {
                        cn.Open();

                        SqlCommand cmd = new SqlCommand(sql, cn);
                        cmd.Parameters.AddWithValue("@country1", country);
                        cmd.Parameters.AddWithValue("@country", frm.Country);
                        cmd.Parameters.AddWithValue("@GDP", frm.GDP);
                        cmd.Parameters.AddWithValue("@HDI", frm.HDI);
                        cmd.Parameters.AddWithValue("@CO2", frm.CO2);

                        cmd.ExecuteNonQuery();
                    }
                    LoadData();
                }
                catch
                {
                    MessageBox.Show("Ошибка работы с БД");
                }
            }
        }
    }
}

