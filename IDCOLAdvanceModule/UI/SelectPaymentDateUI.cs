using MetroFramework;
using System;
using System.Windows.Forms;

namespace IDCOLAdvanceModule.UI
{
    public partial class SelectPaymentDateUI : Form
    {
        public bool IsPayButtonClicked { get; set; }

        public SelectPaymentDateUI()
        {
            InitializeComponent();
        }

        private void payMetroButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!paymentDateMetroDateTime.Checked)
                {
                    MessageBox.Show(@"Please select a payment date.", @"Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                IsPayButtonClicked = true;
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SelectPaymentDateUI_Load(object sender, EventArgs e)
        {
            try
            {
                paymentDateMetroDateTime.Value = DateTime.Now;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
