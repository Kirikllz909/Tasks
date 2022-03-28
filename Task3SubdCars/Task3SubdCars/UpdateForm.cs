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
    public partial class UpdateForm : Form
    {
        private DbActions dbActions;
        public delegate void updateForm();
        event updateForm update;
        public UpdateForm(DbActions db, updateForm method)
        {
            dbActions = db;
            update += method;
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int carId = Convert.ToInt32(textBox1.Text);
                string name = textBox2.Text;
                if (name == "")
                    throw new Exception("Empty name");
                decimal cost = Convert.ToDecimal(textBox3.Text);
                string productionCountry = textBox4.Text;
                if (productionCountry == "")
                    throw new Exception("Empty production country");
                int count = Convert.ToInt32(textBox5.Text);
                await dbActions.UpdateCar(carId, new Car(name, cost, productionCountry, count));
                update?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Updating error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
