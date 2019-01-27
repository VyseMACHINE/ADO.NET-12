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
    public partial class EnterForm : Form
    {
        string cnStr;
        string providerName;
        public EnterForm()
        {
            InitializeComponent();
            cnStr = ConfigurationManager.ConnectionStrings["CleaningCompanyApp.Properties.Settings." +
                "CleaningCompanyConnectionString"].ConnectionString;
            providerName = ConfigurationManager.ConnectionStrings["CleaningCompanyApp.Properties.Settings." +
                "CleaningCompanyConnectionString"].ProviderName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
            var pFactory = DbProviderFactories.GetFactory(providerName);

            using (var connection = pFactory.CreateConnection())
            {
                connection.ConnectionString = cnStr;
                if(textBox1.Text!=""&& textBox2.Text != ""&& comboBox1.SelectedItem != null)
                {
                    string login = textBox1.Text;
                    string password = textBox2.Text;
                    string category = comboBox1.SelectedItem.ToString() == "Поставщик" ? "Supplier":"Customer";

                    string selectSql = $"Select * from {category} where Login = '{login}' and Password = '{password}'";

                    var res = connection.Query<Object>(selectSql);

                    if(res.Count() == 0)
                    {
                        MessageBox.Show("Пользователь не зарегистрирован!!!");
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("Заполнены не все поля!!!");
                }
                
            }
        }
    }
}
