using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDCOLAdvanceModule.Model.CustomException;

namespace IDCOLAdvanceModule.UI.Settings
{
    public partial class JustificationEntryUI : Form
    {
        public JustificationEntryUI()
        {
            InitializeComponent();
        }

        public JustificationEntryUI(string justification, decimal entilementAmount) : this( entilementAmount)
        {
            Info = justification;
            justificationTextBox.Text = Info;
        }

        public string Info { get; set; }
        public JustificationEntryUI(decimal entilementAmount)
            : this()
        {
            entitleValueLabel.Text = entilementAmount.ToString("N") + @"tk";
           
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                Validation();
                string justificationInfo = justificationTextBox.Text;
                Info = justificationInfo;
                this.Close();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool Validation()
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(justificationTextBox.Text))
            {
                errorMessage += "Please provide justification." + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new UiException(errorMessage);
            }
            else
            {
                return true;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void JustificationEntryUI_Load(object sender, EventArgs e)
        {
            try
            {
                SystemSounds.Asterisk.Play();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
