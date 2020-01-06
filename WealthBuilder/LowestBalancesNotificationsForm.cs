using System.Collections.Generic;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class LowestBalancesNotificationsForm : Form
    {
        public LowestBalancesNotificationsForm(List<string> lowestBalances)
        {
            InitializeComponent();
            lowestBalancesListBox.DataSource = lowestBalances;
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
