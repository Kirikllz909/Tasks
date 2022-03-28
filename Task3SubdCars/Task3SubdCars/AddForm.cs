using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3SubdCars
{
    public partial class AddForm : Form
    {
        DbActions dbActions;
        public delegate void updateForm();
        event updateForm update;
        public AddForm(DbActions db, updateForm method)
        {
            update += method;
            dbActions = db;
            InitializeComponent();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                if (name == "")
                    throw new Exception("Empty Name");
                decimal cost = Convert.ToDecimal(textBox2.Text);
                string productionCountry = textBox3.Text;
                if (productionCountry == "")
                    throw new Exception("Empty production country");
                int count = Convert.ToInt32(textBox4.Text);
                await dbActions.CreateCar(new Car(name, cost, productionCountry, count));
                update?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Adding error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
