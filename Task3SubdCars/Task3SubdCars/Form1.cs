namespace Task3SubdCars
{
    public partial class Form1 : Form
    {
        private DbActions db = new DbActions();
        public Form1()
        {
            InitializeComponent();
            refreshGrid();
        }
        async public void refreshGrid()
        {
            Car[] cars = await db.GetAllCars();
            for(int i = 0; i < cars.Length; i++)
            {
                if(dataGridView1.Rows.Count <= i)
                    dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = cars[i].Id.ToString();
                dataGridView1[1, i].Value = cars[i].Name.ToString();
                dataGridView1[2, i].Value = cars[i].Cost.ToString();
                dataGridView1[3, i].Value = cars[i].ProductionCountry.ToString();
                dataGridView1[4, i].Value = cars[i].Count.ToString();
            }
            if(dataGridView1.Rows.Count > cars.Length)
            {
                for(int i = cars.Length; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows.RemoveAt(i);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm(db, refreshGrid);
            addForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateForm updateForm = new UpdateForm(db, refreshGrid);
            updateForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteForm deleteForm = new DeleteForm(db, refreshGrid);
            deleteForm.Show();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            Car[] cars = await db.GetAllCars();
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel file | *.xlsx";
            if(saveFile.ShowDialog()== DialogResult.OK)
            {
                try
                {
                    byte[] file = Excel.GenerateExcel(cars);
                    File.WriteAllBytes(saveFile.FileName, file);
                    MessageBox.Show("Report was successfully created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error while generating excel file",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }
    }
}