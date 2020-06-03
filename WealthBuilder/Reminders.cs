//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    internal class Reminders
    {
        public static void LoadInflows()
        {
            var entities = Code.Entities.GetActive();
            foreach (var entity in entities) LoadInflows(entity.Id);
        }

        internal static void Load()
        {
            using (var db = new WBEntities())
            {
                var rs = db.Entities.Where(x => x.Active == true);

                foreach(var entity in rs)
                {
                    LoadInflows(entity.Id);
                    LoadBudget(entity.Id);
                }
                
            }
        }

        private static void LoadInflows(long entityId)
        {

            using (var db = new WBEntities())
            {
                string entityName = db.Entities.Where(x => x.Id == entityId).FirstOrDefault().Name;
                var inflows = db.Inflows.Where(x => x.EntityId == entityId && x.InflowDate != null && x.FrequencyId != null);
                if (inflows.Count() == 0) return;
                var reminders = db.Reminders;

                foreach (var inflow in inflows)
                {
                    DateTime inflowDate = ((DateTime)inflow.InflowDate).Date;
                    if (inflowDate > DateTime.Today) continue;
                    DateTime startDate = inflow.StartDate == null ? DateTime.MinValue : ((DateTime)inflow.StartDate).Date;
                    DateTime endDate = inflow.EndDate == null ? DateTime.MaxValue : ((DateTime)inflow.EndDate).Date;
                    if (inflowDate < startDate || inflowDate > endDate) continue;
                    var reminder = new Reminder();
                    reminder.Date = inflowDate;
                    string inflowAmountString = inflow.Amount.ToString("C");
                    string inflowAmountPhrase = " in the amount of " + inflowAmountString;
                    string dateString = reminder.Date == null ?  string.Empty : ((DateTime)reminder.Date).Date.ToShortDateString();
                    reminder.Description = "Inflow for " + entityName + ": " + inflow.Name + inflowAmountPhrase + " to be received on " + dateString + ".";

                    if (!string.IsNullOrEmpty(inflow.Notes))
                    {
                        reminder.Description += "  NOTES: " + inflow.Notes;
                    }

                    reminder.InflowAmount = inflow.Amount;
                    reminder.EntityId = (int)entityId;
                    reminders.Add(reminder);

                    if (FrequencyCode.GetById((int)inflow.FrequencyId) == FrequencyCode.Once)
                    {
                        db.Inflows.Remove(inflow);
                        string s = reminder.Description + " is going to be moved from Inflows to Reminders because its Frequency is 'Once.'";
                        MessageBox.Show(s, "An Inflow is being moved to Reminders...");
                    }
                    else
                    {
                        DateTime newInflowDate = Date.IncrementDate(inflowDate, (int)inflow.FrequencyId);
                        inflow.InflowDate = newInflowDate.Date;
                    }
                }

                try
                {
                    db.SaveChanges();
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while trying to load reminders.  Please try again or contact Customer Care.");
                    Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                    return;
                } 
            }
        }

        private static void LoadBudget( long entityId)
        {
            using (var db = new WBEntities())
            {
                var budgets = db.Budgets.Where(x => x.EntityId == entityId && x.PayDate != null && x.FrequencyId != null);
                if (budgets.Count() == 0) return;
                var reminders = db.Reminders;
                string entityName = db.Entities.Where(x => x.Id == entityId).FirstOrDefault().Name;

                foreach (var budget in budgets)
                {
                    DateTime nextPayDate = DateTime.MaxValue;
                    if (budget.PayDate != null) nextPayDate = ((DateTime)budget.PayDate).Date;
                    if (nextPayDate.Date > DateTime.Today) continue;
                    DateTime startDate = DateTime.MinValue;
                    if (budget.StartDate != null) startDate = ((DateTime)budget.StartDate).Date;
                    DateTime endDate = budget.EndDate == null ? DateTime.MaxValue : ((DateTime)budget.EndDate).Date;
                    if (nextPayDate < startDate || nextPayDate > endDate) continue;
                    var reminder = new Reminder();
                    reminder.Date = nextPayDate;
                    string payDateString = nextPayDate.ToShortDateString();
                    string budgetAmountString = budget.Amount.ToString("C");
                    string budgetAmountMsg = " in the amount of " + budgetAmountString;
                    DateTime dueDate = DateTime.MinValue;

                    if (budget.DueDate != null)
                    {
                        dueDate = (DateTime)budget.DueDate;
                    }

                    string dueDateString = dueDate.ToShortDateString();
                    reminder.Description = "Budget for " + entityName + ": " + budget.Name + budgetAmountMsg + " is due on " + dueDateString + ", to be paid on " + payDateString + ".";

                    if (!string.IsNullOrEmpty(budget.Notes))
                    {
                        reminder.Description += "  NOTES: " + budget.Notes;
                    }

                    reminder.PayAmount = budget.Amount;
                    DateTime nextDueDate = budget.DueDate == null ? DateTime.MaxValue : ((DateTime)budget.DueDate).Date;
                    reminder.DueDate = nextDueDate;
                    reminder.EntityId = (int)entityId;
                    reminders.Add(reminder);

                    if (FrequencyCode.GetById((int)budget.FrequencyId) == FrequencyCode.Once)
                    {
                        string s = reminder.Description + "  This Budget Item will be moved to Reminders because it's Frequency is ONCE";
                        MessageBox.Show(s, "The Budget Item below is being MOVED...");
                        db.Budgets.Remove(budget);
                    }
                    else
                    {
                        DateTime newPayDate = Date.IncrementDate(nextPayDate, (int)budget.FrequencyId);
                        budget.PayDate = newPayDate;

                        if (!(nextDueDate == DateTime.MaxValue.Date))
                        {
                            DateTime newDueDate = Date.IncrementDate(nextDueDate, (int)budget.FrequencyId);
                            budget.DueDate = newDueDate;
                        }
                    }
                }

                try
                {
                    db.SaveChanges();
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while trying to load reminders.  Please try again or contact Customer Care.");
                    Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                    return;
                } 
            }
        }

        public static bool Exist()
        {
            try
            {
                using (var db = new WBEntities())
                {
                    var reminders = db.Reminders;
                    return reminders.Count() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to load reminders.  Please try again or contact Customer Care.");
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
    }
}