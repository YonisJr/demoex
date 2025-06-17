using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo5
{
    public partial class Form3 : Form
    {
        string connectionString = "Host=localhost;Username=postgres;Password=seki;Database=d5";
        private string materialName;
        public Form3(string material)
        {
            InitializeComponent();
            materialName = material;
            LoadMaterialData();
            LoadSuppliersData();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT type FROM type";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["type"].ToString());
                    }
                }
            }
        }

        private void LoadMaterialData()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT type, price, kolvo, min, kolvo_up, ed
                FROM material
                WHERE material = @material";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@material", materialName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = materialName;
                            comboBox1.Text = reader["type"].ToString();
                            textBox2.Text = reader["price"].ToString();
                            textBox3.Text = reader["kolvo"].ToString();
                            textBox4.Text = reader["min"].ToString();
                            textBox5.Text = reader["kolvo_up"].ToString();
                            textBox6.Text = reader["ed"].ToString();
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                UPDATE material
                SET type = @type, price = @price, kolvo = @kolvo, min = @min, kolvo_up = @kolvo_up, ed = @ed
                WHERE material = @material";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@type", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@price", textBox2.Text);
                    cmd.Parameters.AddWithValue("@kolvo", textBox3.Text);
                    cmd.Parameters.AddWithValue("@min", textBox4.Text);
                    cmd.Parameters.AddWithValue("@kolvo_up", textBox5.Text);
                    cmd.Parameters.AddWithValue("@ed", textBox6.Text);
                    cmd.Parameters.AddWithValue("@material", materialName);

                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        private void LoadSuppliersData()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT p.post AS ""Поставщик"", p.inn AS ""ИНН"", 
                       p.reit AS ""Рейтинг"", p.data AS ""Дата регистрации""
                FROM postavki po
                JOIN post p ON po.post = p.post
                WHERE po.material = @material";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@material", materialName);
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }
            }
        }
    }
}
