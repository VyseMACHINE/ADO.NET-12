using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Common;
using Dapper;

namespace CleaningCompanyApp
{
    public partial class RegisterForm : Form
    {
        string cnStr;
        string providerName;

        public RegisterForm()
        {
            InitializeComponent();
            cnStr = ConfigurationManager.ConnectionStrings["CleaningCompanyApp.Properties.Settings." +
                "CleaningCompanyConnectionString"].ConnectionString;
            providerName = ConfigurationManager.ConnectionStrings["CleaningCompanyApp.Properties.Settings." +
                "CleaningCompanyConnectionString"].ProviderName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Заказчик")
                InsertIntoCustomer();
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Поставщик")
                InsertIntoSupplier();
            else MessageBox.Show("Заполнены не все поля!!!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Заказчик")
            {
                label5.Visible = true;
                textBox4.Visible = true;
            }
            else if (comboBox1.SelectedItem.ToString() == "Поставщик")
            {
                label5.Visible = false;
                textBox4.Visible = false;
            }
        }

        private void InsertIntoCustomer()
        {
            if(textBox1.Text!=""&& textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                var pFactory = DbProviderFactories.GetFactory(providerName);
                using (var connection = pFactory.CreateConnection())
                {
                    connection.ConnectionString = cnStr;

                    Customer customer = new Customer
                    {
                        FullName = textBox3.Text,
                        Login = textBox1.Text,
                        Password = textBox2.Text,
                        PhoneNumber = textBox4.Text
                    };

                    string selectSql = $"Select * from Customer where Login = '{customer.Login}'";
                    string insertSql = "Insert into Customer values(@FullName, @Login, @Password, @PhoneNumber)";

                    var res = connection.Query(selectSql);

                    if (res.Count() == 0)
                    {
                        connection.Execute(insertSql, customer);
                        MessageBox.Show("Регистрация прошла успешно!!!");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Введенный логин уже существует!!!");
                        this.DialogResult = DialogResult.None;
                    }       
                }
            }
            else
            {
                MessageBox.Show("Заполнены не все поля!!!");
            }
        }

        private void InsertIntoSupplier()
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                var pFactory = DbProviderFactories.GetFactory(providerName);
                using (var connection = pFactory.CreateConnection())
                {
                    connection.ConnectionString = cnStr;

                    Supplier supplier = new Supplier
                    {
                        FullName = textBox3.Text,
                        Login = textBox1.Text,
                        Password = textBox2.Text,
                    };

                    string selectSql = $"Select * from Supplier where Login = '{supplier.Login}'";
                    string insertSql = "Insert into Supplier values(@FullName, @Login, @Password)";

                    var res = connection.Query(selectSql);

                    if (res.Count() == 0)
                    {
                        connection.Execute(insertSql, supplier);
                        MessageBox.Show("Регистрация прошла успешно!!!");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Введенный логин уже существует!!!");
                        this.DialogResult = DialogResult.None;
                    }
                        
                }
            }
            else
            {
                MessageBox.Show("Заполнены не все поля!!!");
            }
        }
    }
}
