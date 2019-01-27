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
    public partial class CustomerForm : Form
    {
        protected string cnStr;
        protected string providerName;
        protected string customerLogin;

        public CustomerForm(string customerLogin)
        {
            InitializeComponent();

            cnStr = ConfigurationManager.ConnectionStrings["CleaningCompanyApp.Properties.Settings." +
                "CleaningCompanyConnectionString"].ConnectionString;
            providerName = ConfigurationManager.ConnectionStrings["CleaningCompanyApp.Properties.Settings." +
                "CleaningCompanyConnectionString"].ProviderName;
            this.customerLogin = customerLogin;
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            var pFactory = DbProviderFactories.GetFactory(providerName);

            using (var connection = pFactory.CreateConnection())
            {
                connection.ConnectionString = cnStr;

                string selectSql = $"Select * from Request where CustId = ( Select Id from Customer where Login ='{customerLogin}')";

                dataGridView1.DataSource = connection.Query<Request>(selectSql).ToList();

                this.Text = customerLogin;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var pFactory = DbProviderFactories.GetFactory(providerName);

                using (var connection = pFactory.CreateConnection())
                {
                    connection.ConnectionString = cnStr;

                    string insertSql = $"Insert into Request values (@CustId, @SupplierId,@Date, @ServiceName, @Status)";
                    string selectSql = $"Select * from Customer where Login ='{customerLogin}'";
                    string selectListSql = $"Select * from Request where CustId = ( Select Id from Customer where Login ='{customerLogin}')";

                    Customer customer = connection.Query<Customer>(selectSql).First();

                    Request request = new Request
                    {
                        CustId = customer.Id,
                        SupplierId = null,
                        Date = DateTime.Now,
                        ServiceName = comboBox1.SelectedItem.ToString(),
                        Status = false
                    };

                    connection.Execute(insertSql, request);                   

                    dataGridView1.DataSource = connection.Query<Request>(selectListSql).ToList();

                    MessageBox.Show("Заявка отправлена!!!");
                }
            }
        }
    }
}
