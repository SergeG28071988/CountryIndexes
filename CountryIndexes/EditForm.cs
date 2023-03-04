using System;
using System.Windows.Forms;

namespace CountryIndexes
{
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
        }

        public string Country
        {
            get { return textCountry.Text; }
            set { textCountry.Text = value; }
        }

        public int GDP
        {
            get { return int.Parse(textGDP.Text); }
            set { textGDP.Text = value.ToString(); }
        }

        public double HDI
        {
            get { return double.Parse(textHDI.Text); }
            set { textHDI.Text = value.ToString(); }
        }

        public double CO2
        {
            get { return double.Parse(textCO2.Text); }
            set { textCO2.Text = value.ToString(); }
        }


        private void BtnOK_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textCountry.Text))
            {
                MessageBox.Show("Необходимо заполнить поле Country!");
                return;
            }

            int gdp = 0;

            if (!int.TryParse(textGDP.Text, out gdp))
            {
                MessageBox.Show("Необходимо заполнить поле GDP!");
                return;
            }

            float hdi = 0;

            if (!float.TryParse(textHDI.Text, out hdi))
            {
                MessageBox.Show("Необходимо заполнить поле HDI!");
                return;
            }
            float co2 = 0;

            if (!float.TryParse(textCO2.Text, out co2))
            {
                MessageBox.Show("Необходимо заполнить поле CO2!");
                return;
            }


            DialogResult = DialogResult.OK;

            Close();

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
