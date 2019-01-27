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
    public partial class SupplierForm : Form
    {
        protected string cnStr;
        protected string providerName;
        protected string supplierLogin;
        protected DbProviderFactory pFactory;
        protected Supplier supplier;

        public SupplierForm(string supplierLogin)
        {
            InitializeComponent();

            cnStr = ConfigurationManager.ConnectionStrings["CleaningCompanyApp.Properties.Settings." +
                "CleaningCompanyConnectionString"].ConnectionString;
            providerName = ConfigurationManager.ConnectionStrings["CleaningCompanyApp.Properties.Settings." +
                "CleaningCompanyConnectionString"].ProviderName;
            pFactory = DbProviderFactories.GetFactory(providerName);

            this.supplierLogin = supplierLogin;
        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {
            this.Text = supplierLogin;

            using (var connection = pFactory.CreateConnection())
            {
                connection.ConnectionString = cnStr;

                string selectSql = "Select * from Request where Status = 0 or Status = null";

                List<Request> requestList = connection.Query<Request>(selectSql).ToList();

                dataGridView1.DataSource = requestList;

                string selectSupplierSql = $"Select * from Supplier where Login = '{supplierLogin}'";

                supplier = connection.Query<Supplier>(selectSupplierSql).ToList().First();

                string selectSupplierRequestSql = $"Select * from Request where SupplierId = {supplier.Id}";

                dataGridView2.DataSource = connection.Query<Request>(selectSupplierRequestSql).ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool convert = int.TryParse(dataGridView1[0,index].Value.ToString(), out id);

                using (var connection = pFactory.CreateConnection())
                {
                    connection.ConnectionString = cnStr;

                    string updateSql = $"Update Request set Status = 1, SupplierId = {supplier.Id} where Id = {id}";

                    connection.Execute(updateSql);

                    string selectSqlList = "Select * from Request where Status = 0 or Status = null";

                    dataGridView1.DataSource = connection.Query<Request>(selectSqlList).ToList();

                    string selectSupplierRequestSql = $"Select * from Request where SupplierId = {supplier.Id}";

                    dataGridView2.DataSource = connection.Query<Request>(selectSupplierRequestSql).ToList();

                }
                
            }
            else
            {
                MessageBox.Show("Вы не выбрали заказ!!!");
            }
        }
    }
}
