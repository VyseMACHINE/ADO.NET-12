using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleaningCompanyApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SupplierForm supplierForm;
            CustomerForm customerForm;

            EnterForm enterForm = new EnterForm();
            DialogResult res = enterForm.ShowDialog(this);

            if (res == DialogResult.OK)
            {
                if (enterForm.comboBox1.SelectedItem.ToString() == "Поставщик")
                {
                    supplierForm = new SupplierForm(enterForm.textBox1.Text);
                    supplierForm.Show();
                }
                else if (enterForm.comboBox1.SelectedItem.ToString() == "Заказчик")
                {
                    customerForm = new CustomerForm(enterForm.textBox1.Text);
                    customerForm.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SupplierForm supplierForm;
            CustomerForm customerForm;

            RegisterForm registerForm = new RegisterForm();
            DialogResult res = registerForm.ShowDialog(this);

            if(res == DialogResult.OK)
            {
                if (registerForm.comboBox1.SelectedItem.ToString() == "Поставщик")
                {
                    supplierForm = new SupplierForm(registerForm.textBox1.Text);
                    supplierForm.Show();
                }                    
                else if (registerForm.comboBox1.SelectedItem.ToString() == "Заказчик")
                {
                    customerForm = new CustomerForm(registerForm.textBox1.Text);
                    customerForm.Show();
                }
            }
            
        }
    }
}
