using System;
using System.Windows.Forms;
using GenericSecurity.BLL;
using GenericSecurity.Type;

//using GenericSecurity.BLL;
//using GenericSecurity.Type;
using MetroFramework;

namespace IDCOLAdvanceModule.UI
{
    public partial class LoginUI : Form
    {
        public bool IsAuthenticate;
        public LoginUI()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUser())
                {
                    this.Hide();
                    MainUI mainUi = new MainUI();
                    //this.Dispose();
                    mainUi.Show();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateUser()
        {
            UserAccountController userAccount = new UserAccountController();

            try
            {
                if (userAccount.ValidateUser(usernameTextBox.Text, passwordTextBox.Text))
                {
                    IsAuthenticate = true;
                    Session.LoginUserName = usernameTextBox.Text;
                    UserInfo user = new UserInfo();
                    user = UserInfoController.GetUser(Session.LoginUserName);
                    if (user != null)
                    {
                        Session.LoginUserID = user.UserID;
                    }
                    else
                    {
                        WarningLabel.Text = @"User Id or Password is incorrect. Please Try again.";
                    }
                    return IsAuthenticate;
                }
                else
                {
                    WarningLabel.Text = @"User Id or Password is incorrect. Please Try again.";
                    IsAuthenticate = false;
                }
            }

            catch (Exception excp)
            {
                MessageBox.Show(@"Error Occurred: \n" + excp.Message, @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return IsAuthenticate;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
