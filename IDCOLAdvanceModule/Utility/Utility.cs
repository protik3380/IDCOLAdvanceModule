using System;
using System.Windows.Forms;
using MetroFramework.Controls;
using System.Configuration;
using System.Net;
using GenericSecurity.BLL;

namespace IDCOLAdvanceModule.Utility
{
    public static class Utility
    {
        public static int TimeDuration = 300;
        public static int StartingLevelNo = 1;
        public static string FontName = "Maiandra GD";
        public static string RequisitionFileUploadLocation = ConfigurationManager.AppSettings.Get("RequisitionFileUploadLocation");
        public static string ExpenseFileUploadLocation = ConfigurationManager.AppSettings.Get("ExpenseFileUploadLocation");
        public static string SupportedFileFormat = "Image, Word, Excel, PDF|*.jpg; *.jpeg; *.png; *.doc; *.docx; *.xls; *.xlsx; *.pdf|Image (.jpg, .jpeg, .png)|*.jpg; *.jpeg; *.png|Word (.doc, .docx)|*.doc; *.docx|Excel (.xls, .xlsx)|*.xls; *.xlsx|PDF (.pdf)|*.pdf";
        public static string EmailFrom = ConfigurationManager.AppSettings.Get("EmailFrom");
        public static string EmailPassword = GetPasswordFromConfig();

        private static string GetPasswordFromConfig()
        {
            UserAccountController userAccountController = new UserAccountController();
            string encryptedPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
            //string decryptedPassword = userAccountController.Decrypt(encryptedPassword, true);
            string decryptedPassword = Database.Utility.Utility.Decrypt(encryptedPassword, true, "GenericIDCOLSECURITy");
            return decryptedPassword;
        }

        public static void MakeTextBoxToTakeNumbersOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        public static void CalculateNoOfDays(DateTime fromDate, DateTime toDate, MetroTextBox noOfTextBox)
        {
            double noOfDays = (toDate.Date - fromDate.Date).TotalDays + 1;
            noOfTextBox.Text = noOfDays.ToString();
        }

        public static void SellectOrUnSelectAllListItem(ListView listView, bool isChecked)
        {
            foreach (ListViewItem item in listView.Items)
            {
                item.Checked = isChecked;
            }
        }

        
        public static void SelectOrUnSelectAllGridItem(DataGridView gridView, bool isChecked)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                row.Cells[4].Value = isChecked;
            }
        }

        public static string GenerateFileName(string userName, string originalFileName)
        {
            DateTime value = DateTime.Now;
            return userName + "_" + value.ToString("yyyyMMddHHmmss") + "_" + originalFileName;
        }

        public static void SortColumn(ColumnClickEventArgs e, ListView listView)
        {
            ListViewColumnSorter sorter = (ListViewColumnSorter)listView.ListViewItemSorter;
            if (e.Column == sorter.SortColumn)
            {
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else if (sorter.Order == SortOrder.None)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                sorter.SortColumn = e.Column;
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else if (sorter.Order == SortOrder.None)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                }
            }

            listView.Sort();
        }

        public static string GetFormatedAmount(Decimal amount)
        {
            if (amount >= 0)
                return amount.ToString("N");
           return "(" + Math.Abs(amount).ToString("N") + ")";
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static int DateDifference(DateTime startDate, DateTime endDate)
        {
            var totalDays = (endDate.Date - startDate.Date).TotalDays;
            return (int) totalDays;
        }
    }
}
