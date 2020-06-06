using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WealthBuilder
{
    public class Affordability
    {
        private static decimal amountToCheck;

        public static void Determine()
        {
            DateTime endDate = DateTime.Today.AddYears(1).AddDays(-1);
            int interval = 6;
            var cashFlowForecast = new CashFlowForecast(endDate, interval);
            cashFlowForecast.Calculate();

            if (cashFlowForecast.BalanceBelowThresholdDate != DateTime.MinValue)
            {
                string s = "You cannot add anything to the " + CurrentEntity.Name + " budget over the next 12 months because your cash balance of {0} will drop below {1} on {2}.";
                string cb = cashFlowForecast.BalanceBelowThreshold.ToString("C");
                string d = cashFlowForecast.BalanceBelowThresholdDate.ToShortDateString();
                long lowestBalanceThreshold;

                using (var db = new WBEntities())
                {
                    var entity = db.Entities.Where(x => x.Active == true && CurrentEntity.Id == x.Id).FirstOrDefault();
                    if (entity == null) return;
                    lowestBalanceThreshold = (long)entity.LowestBalance;
                }

                string t = lowestBalanceThreshold.ToString("C");
                string msg = string.Format(s, cb, t, d);
                MessageBox.Show(msg, "Affordability");
                return;
            }

            if (!GetTheAmountFromTheUsers()) return;
            decimal cushion = Code.Entity.GetLowestBalance();
            var record = cashFlowForecast.DailyBalances.Where(x => x.Value < amountToCheck + cushion).FirstOrDefault();

            if (record.Key != DateTime.MinValue)
            {
                string cannotMsg = "You cannot fit this amount into your budget over the next 12 months.";
                MessageBox.Show(cannotMsg, "Affordability");
                return;
            }

            record = cashFlowForecast.DailyBalances.Where(x => x.Value >= amountToCheck + cushion).FirstOrDefault();
            string dateString = record.Key.ToShortDateString();
            string approvalMessage = string.Format("You can fit this amount into your budget on {0}.", dateString);
            MessageBox.Show(approvalMessage, "Affordability");
        }

        private static bool GetTheAmountFromTheUsers()
        {
            string enterAmountMessage = "Enter the amount you wish to fit into your budet:";
            string response = Interaction.InputBox(enterAmountMessage, "Affordability", string.Empty);
            if (string.IsNullOrWhiteSpace(response)) return false;
            decimal amount;

            if (!decimal.TryParse(response, out amount))
            {
                MessageBox.Show("Please enter a valid amount.", "Invalid Amount");
                return false;
            }

            amountToCheck = amount;
            return true;
        }
    }
}
