using System;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    public static class DataGridViewHelper
    {
        public static bool DeleteRows(DataGridView dgv)
        {
            bool rowIsEmpty = true;

            foreach (DataGridViewCell cell in dgv.CurrentRow.Cells)
            {
                if (cell.Value != null && dgv.Columns[cell.ColumnIndex].Name != "Date")
                {
                    rowIsEmpty = false;
                    break;
                }
            }

            if (rowIsEmpty) return true;

            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        dgv.Rows.RemoveAt(row.Index);
                    }

                    return true;
                }

                int ri = dgv.CurrentCell.RowIndex;
                if (ri != -1) dgv.Rows.RemoveAt(ri);
                return true;
            }
            catch (Exception ex)
            {
                Error.Log(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }

        }

        public static void InsertCalendarColumn(DataGridView dgv, string headerText, string dataPropertyName, string columnName, int columnPosition)
        {
            CalendarColumn col = new CalendarColumn();
            col.HeaderText = headerText;
            col.DataPropertyName = dataPropertyName;
            col.Name = columnName;
            col.Width = 90;
            dgv.Columns.Insert(columnPosition, col);
        }
    }
}
