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
using Npgsql;

namespace demo5
{
    public partial class Form2 : Form
    {
        string connectionString = "Host=localhost;Username=postgres;Password=seki;Database=d5";
        public Form2()
        {
            InitializeComponent();
            LoadMaterials();
            LoadSuppliers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void LoadMaterials()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.AutoScroll = true;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
    SELECT m.material, m.type, m.price, m.kolvo, m.min, m.kolvo_up, m.ed
    FROM material m
    ORDER BY m.material;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["material"].ToString();
                        string type = reader["type"].ToString();
                        string price = reader["price"].ToString();
                        string kolvo = reader["kolvo"].ToString();
                        string min = reader["min"].ToString();
                        string kolvoUp = reader["kolvo_up"].ToString();
                        string ed = reader["ed"].ToString();

                        // Вычисляем стоимость минимальной партии
                        decimal batchCost = CalculateMinimumBatchCost(price, kolvo, min, kolvoUp);

                        // Создание панели материала
                        Panel panel = new Panel
                        {
                            Width = flowLayoutPanel1.ClientSize.Width - 30,
                            Height = 100,
                            BorderStyle = BorderStyle.FixedSingle,
                            BackColor = Color.WhiteSmoke,
                            Padding = new Padding(8),
                            Margin = new Padding(5),
                            Tag = name
                        };

                        // Название и тип
                        Label labelHeader = CreateLabel($"{type} | {name}", true);
                        Label labelMinStock = CreateLabel($"Минимальное количество: {min} {ed}");
                        Label labelStock = CreateLabel($"Количество на складе: {kolvo} {ed}");
                        Label labelPrice = CreateLabel($"Цена: {price} р / Единица измерения: {ed}");
                        Label labelTotalCost = CreateLabel($"Стоимость партии: {batchCost:F2} р", true);
                        labelTotalCost.TextAlign = ContentAlignment.TopRight;
                        labelTotalCost.Anchor = AnchorStyles.Right;

                        // Расположение элементов
                        labelHeader.Location = new Point(5, 5);
                        labelMinStock.Location = new Point(5, 25);
                        labelStock.Location = new Point(5, 45);
                        labelPrice.Location = new Point(5, 65);
                        labelTotalCost.Location = new Point(panel.Width - 280, 35);

                        // Добавление элементов в панель
                        panel.Controls.Add(labelHeader);
                        panel.Controls.Add(labelMinStock);
                        panel.Controls.Add(labelStock);
                        panel.Controls.Add(labelPrice);
                        panel.Controls.Add(labelTotalCost);

                        // Добавление клика для редактирования
                        panel.Click += (sender, e) => OpenEditForm(name);
                        labelHeader.Click += (sender, e) => OpenEditForm(name);
                        labelMinStock.Click += (sender, e) => OpenEditForm(name);
                        labelStock.Click += (sender, e) => OpenEditForm(name);
                        labelPrice.Click += (sender, e) => OpenEditForm(name);
                        labelTotalCost.Click += (sender, e) => OpenEditForm(name);

                        // Добавление панели в FlowLayoutPanel
                        flowLayoutPanel1.Controls.Add(panel);
                    }
                }
            }
        }

        private decimal CalculateMinimumBatchCost(string priceStr, string kolvoStr, string minStr, string kolvoUpStr)
        {
            try
            {
                // Парсим значения, обрабатывая возможные проблемы с форматированием
                decimal price = decimal.Parse(priceStr.Replace(",", "."), CultureInfo.InvariantCulture);
                decimal currentStock = decimal.Parse(kolvoStr.Replace(",", "."), CultureInfo.InvariantCulture);
                decimal minStock = decimal.Parse(minStr.Replace(",", "."), CultureInfo.InvariantCulture);
                decimal packageQty = decimal.Parse(kolvoUpStr.Replace(",", "."), CultureInfo.InvariantCulture);

                // Если текущий запас выше минимального, заказ не нужен
                if (currentStock >= minStock)
                {
                    return 0;
                }

                // Вычисляем нехватку
                decimal shortage = minStock - currentStock;

                // Вычисляем количество упаковок (округляем вверх до целых упаковок)
                decimal packagesNeeded = Math.Ceiling(shortage / packageQty);

                // Вычисляем общее количество к заказу
                decimal totalQuantityToOrder = packagesNeeded * packageQty;

                // Вычисляем общую стоимость
                decimal totalCost = totalQuantityToOrder * price;

                return totalCost;
            }
            catch (Exception)
            {
                // Возвращаем 0 если парсинг не удался
                return 0;
            }
        }

        // Метод для создания метки
        private Label CreateLabel(string text, bool bold = false)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Comic Sans MS", bold ? 12 : 10, bold ? FontStyle.Bold : FontStyle.Regular)
            };
        }

        // Открытие формы редактирования по клику на панель
        private void OpenEditForm(string materialName)
        {
            Form3 editForm = new Form3(materialName);
            editForm.ShowDialog();
        }


        private void LoadSuppliers()
        {
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel2.AutoScroll = true;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT post, tip, inn, reit, data 
            FROM post 
            ORDER BY reit DESC;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["post"].ToString();
                        string category = reader["tip"].ToString();
                        string inn = reader["inn"].ToString();
                        int rating = Convert.ToInt32(reader["reit"]);
                        string regDate = reader["data"].ToString();

                        // Создаем панель поставщика
                        Panel panel = new Panel
                        {
                            Width = flowLayoutPanel1.ClientSize.Width - 30,
                            Height = 115,
                            BorderStyle = BorderStyle.FixedSingle,
                            BackColor = Color.WhiteSmoke,
                            Padding = new Padding(8),
                            Margin = new Padding(5)
                        };

                        // Создаем метки с данными
                        Label labelName = CreateLabel1($"Название: {name}", true);
                        Label labelCategory = CreateLabel1($"Категория: {category}");
                        Label labelINN = CreateLabel1($"ИНН: {inn}");
                        Label labelRating = CreateLabel1($"Рейтинг: {rating}");
                        Label labelRegDate = CreateLabel1($"Дата регистрации: {regDate}");

                        // Располагаем метки внутри панели
                        labelName.Location = new Point(5, 5);
                        labelCategory.Location = new Point(5, 25);
                        labelINN.Location = new Point(5, 45);
                        labelRating.Location = new Point(5, 65);
                        labelRegDate.Location = new Point(5, 85);

                        // Добавляем элементы в панель
                        panel.Controls.Add(labelName);
                        panel.Controls.Add(labelCategory);
                        panel.Controls.Add(labelINN);
                        panel.Controls.Add(labelRating);
                        panel.Controls.Add(labelRegDate);

                        // Добавляем панель в FlowLayoutPanel
                        flowLayoutPanel2.Controls.Add(panel);
                    }
                }
            }
        }

        // Метод для создания метки
        private Label CreateLabel1(string text, bool bold = false)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Comic Sans MS", bold ? 12 : 10, bold ? FontStyle.Bold : FontStyle.Regular)
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadMaterials();
        }
    }
}
