//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using Microsoft.VisualBasic.ApplicationServices;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WealthBuilder
{
    public class SplashScreenApplication : WindowsFormsApplicationBase
    {
        protected override void OnCreateSplashScreen()
        {
            this.SplashScreen = new SplashScreenForm();
            this.SplashScreen.ShowInTaskbar = false;
            this.SplashScreen.Cursor = Cursors.AppStarting;
        }

        protected override void OnCreateMainForm() 
        {
            PreApplicationStart.Perform();
            PreApplicationStart.EstablishDBConnection();

            if (!EntitiesExist())
            {
                string msg = "You must setup Entities to use this app.  An Entity is an organization that you want to track the "+
                    "cash flow and budget for, such as a family, company, project, property, etc.  The next form will allow you to do that.";
                MessageBox.Show(new Form { TopMost = true }, msg, App.Title);
                Code.Form.Open("EntitiesForm", true);
            }

            MainForm = new MainForm();
        }

        private bool EntitiesExist()
        {
            using (var db = new WBEntities())
            {
                var rs = db.Entities.ToList();
                return rs.Count > 0;
            }
        }
    }
}
