using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace demo5
{
    public partial class Form4 : Form
    {
        string connectionString = "Host=localhost;Username=postgres;Password=seki;Database=d5";
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string material = textBox1.Text;
            string type = comboBox1.SelectedItem?.ToString();
            string price = textBox2.Text;
            string kolvo = textBox3.Text;
            string min = textBox4.Text;
            string kolvoUp = textBox5.Text;
            string ed = textBox6.Text;

            // Проверка на заполненность всех полей
            if (string.IsNullOrEmpty(material) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(price) ||
                string.IsNullOrEmpty(kolvo) || string.IsNullOrEmpty(min) || string.IsNullOrEmpty(kolvoUp) || string.IsNullOrEmpty(ed))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // SQL-запрос для добавления нового материала
            string query = "INSERT INTO material (material, type, price, kolvo, min, kolvo_up, ed) " +
                           "VALUES (@material, @type, @price, @kolvo, @min, @kolvo_up, @ed)";

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        // Добавляем параметры в запрос
                        cmd.Parameters.AddWithValue("@material", material);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@kolvo", kolvo);
                        cmd.Parameters.AddWithValue("@min", min);
                        cmd.Parameters.AddWithValue("@kolvo_up", kolvoUp);
                        cmd.Parameters.AddWithValue("@ed", ed);

                        // Выполняем запрос
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Материал успешно добавлен.");
                ClearFields(); // Очищаем поля после добавления
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении материала: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void Form4_Load(object sender, EventArgs e)
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
    }
}
