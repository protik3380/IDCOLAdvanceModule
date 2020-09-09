using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDCOLAdvanceModule.UI
{
    public partial class SelectReceiveDateUI : Form
    {
        public bool IsReceiveButtonClicked { get; set; }
        public SelectReceiveDateUI()
        {
            InitializeComponent();
        }

        private void receiveMetroButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!receiveDateMetroDateTime.Checked)
                {
                    MessageBox.Show(@"Please select a receive date.", @"Warning!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                IsReceiveButtonClicked = true;
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SelectReceivedDateUI_Load(object sender, EventArgs e)
        {
            try
            {
                receiveDateMetroDateTime.Value = DateTime.Now;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
