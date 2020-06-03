//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;

namespace WealthBuilder
{
    internal class Reconciliation
    {
        internal static decimal CalculateUnclearedTransactions(int accountId, StringBuilder reconciliationReport)
        {
            using (var db = new WBEntities())
            {
                var rs = Code.Transactions.GetUnclearedTransactionsByAccountId(accountId);
                decimal totalUnclearedDeposits = CreateUnclearedTransactionReportEntries(reconciliationReport, "deposits", rs);
                reconciliationReport.Append(Environment.NewLine);
                decimal totalUnclearedWds= CreateUnclearedTransactionReportEntries(reconciliationReport, "withdrawals", rs);
               
                reconciliationReport.Append(Environment.NewLine);

                decimal unclearedAmount = 0;

                foreach (var r in rs)
                {
                    
                    decimal unclearedDepositAmount;
                    decimal unclearedWithdrawalAmount;

                    if (r.Deposit == null)
                    {
                        unclearedDepositAmount = 0;
                    }
                    else
                    {
                        unclearedDepositAmount = Convert.ToDecimal(r.Deposit);
                    }

                    if (r.Withdrawal == null)
                    {
                        unclearedWithdrawalAmount = 0;
                    }
                    else
                    {
                        unclearedWithdrawalAmount = Convert.ToDecimal(r.Withdrawal);
                    }

                    unclearedAmount += unclearedDepositAmount - unclearedWithdrawalAmount;
                }

                reconciliationReport.Append(Environment.NewLine);
                reconciliationReport.Append("Sum of Uncleared Amounts: " + unclearedAmount.ToString("C") + " (Sum of Uncleared Deposits and Uncleared Withdrawals)");
                reconciliationReport.Append(Environment.NewLine);
                return unclearedAmount;
            }
        }

        private static decimal CreateUnclearedTransactionReportEntries(StringBuilder reconciliationReport, string type, List<Transaction> unclearedTransactions)
        {
            if(type=="deposits")
            {
                reconciliationReport.Append(Environment.NewLine);
                reconciliationReport.Append("Uncleared Deposits" + Environment.NewLine);
                reconciliationReport.Append("==================" + Environment.NewLine + Environment.NewLine);
            }
            else
            {
                reconciliationReport.Append("Uncleared Withdrawals" + Environment.NewLine);
                reconciliationReport.Append("=====================" + Environment.NewLine + Environment.NewLine);
            }

            decimal total = 0;

            foreach(var unclearedTransaction in unclearedTransactions)
            {
                if (type == "deposits" && unclearedTransaction.Deposit==0) continue;
                if (type != "deposits" && unclearedTransaction.Withdrawal==0) continue;

                DateTime date = unclearedTransaction.Date;
                reconciliationReport.Append(date.ToShortDateString());
                reconciliationReport.Append(" " + unclearedTransaction.Description ?? string.Empty);

                if (type=="deposits")
                {
                        decimal deposit = unclearedTransaction.Deposit;
                        reconciliationReport.Append(" " + deposit.ToString("C") + Environment.NewLine + Environment.NewLine);
                        total += deposit;
                }
                else
                {
                        decimal wd = unclearedTransaction.Withdrawal;
                        reconciliationReport.Append(" " + wd.ToString("C") + Environment.NewLine + Environment.NewLine);
                        total += wd;
                }
            }

            reconciliationReport.Append(Environment.NewLine + Environment.NewLine);
            reconciliationReport.Append("Total: " + total.ToString("C"));
            reconciliationReport.Append(Environment.NewLine + Environment.NewLine);
            return total;
        }
    }
}