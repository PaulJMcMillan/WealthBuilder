using System;
using System.Windows.Forms;

namespace WealthBuilder
{
   

    public partial class CashAppTransForm : Form
    {
        private enum TransType
        {
            Deposit = 1,
            Withdrawal = 2
        }

        private int spendAccountId=0;
        private int standardAccountId=0;
        private WealthBuilderEntities1 db = new WealthBuilderEntities1();

        public CashAppTransForm()
        {
            InitializeComponent();
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
                var rs = db.Accounts;

                foreach(var r in rs)
                {
                    if (r.Name == null) continue;
                    if (r.Name.ToUpper().IndexOf("SPEND") > -1) spendAccountId = r.Id;
                    if (r.Name.ToUpper().IndexOf("STANDARD") > -1) standardAccountId = r.Id;
                }

                if (spendAccountId == 0 || standardAccountId == 0 || !float.TryParse(amountTextBox.Text, out float amount) || !DateTime.TryParse(transDateTimePicker.Value.ToString(), out DateTime transDate))
                {
                        MessageBox.Show("Invalid data: Somethings wrong with the spend or standard account(s), amount, or date.");
                        return;
                }

                CreateTransaction(spendAccountId, amount, transDate, TransType.Withdrawal);
                CreateTransaction(standardAccountId, amount, transDate, TransType.Deposit);
                CreateTransaction(standardAccountId, amount, transDate, TransType.Withdrawal);
                MessageBox.Show("Your transactions have been entered.",Text);
        }

        private void CreateTransaction(int accountId, float amount, DateTime transDate, TransType transType)
        {
            var trans = new Transaction();
            trans.AccountId = accountId;
            trans.Date = transDate;

            if(transType == TransType.Deposit)
            {
                trans.Deposit = (decimal)amount;
            }
            else
            {
                trans.Withdrawal = (decimal)amount;
            }

            string description;

            if (accountId==spendAccountId)
            {
                description = "xfer for cash app";
            }
            else
            {
                if (transType==TransType.Deposit)
                {
                    description = "xfer for cash app";
                }
                else
                {
                    description = "xfer to Cash app";
                }
            }

            trans.Description = description;
            trans.Cleared = true;
            db.Transactions.Add(trans);
            db.SaveChanges();
        }

        private void QuickCashAppTransForm_Load(object sender, EventArgs e)
        {
            transDateTimePicker.Value = DateTime.Today;
        }

        private void QuickCashAppTransForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Dispose();
            db = null;
        }
    }
}
