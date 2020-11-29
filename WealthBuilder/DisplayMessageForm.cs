using System.Windows.Forms;
//todo: change all messagebox.shows to displaymessageform to avoid the user getting stuck with no way of 
//closing the messagebox if it's behind another window.
namespace WealthBuilder
{
    public partial class DisplayMessageForm : Form
    {
        public DisplayMessageForm(string message)
        {
            InitializeComponent();
            messageLabel.Text = message;
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
