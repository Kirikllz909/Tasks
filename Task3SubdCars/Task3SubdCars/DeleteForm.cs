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
    public partial class DeleteForm : Form
    {
        private DbActions dbActions;
        public delegate void updateForm();
        event updateForm update;
        public DeleteForm(DbActions db, updateForm method)
        {
            this.dbActions = db;
            update += method;
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int carId = Convert.ToInt32(textBox1.Text);
                await dbActions.DeleteCar(carId);
                update?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deleting error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
