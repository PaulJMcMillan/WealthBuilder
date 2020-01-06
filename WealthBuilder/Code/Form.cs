//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Windows.Forms;

namespace WealthBuilder.Code
{
    public class Form
    {
        private static System.Windows.Forms.Form form;
        private static string formName;
        private static bool modal;

        public static void Open(string formName)
        {
            Form.formName = formName;

            if (FormIsOpen())
            {
                form.Activate();
                return;
            }

            System.Windows.Forms.Form f = (System.Windows.Forms.Form)Activator.CreateInstance(Type.GetType("WealthBuilder." + formName));

            if (modal)
            {
                f.ShowDialog();
                return;
            }

            f.Show();
        }

        private static bool FormIsOpen()
        {
            FormCollection fc = Application.OpenForms;

            foreach (System.Windows.Forms.Form frm in fc)
            {
                if (frm.Name == formName)
                {
                    form = frm;
                    return true;
                }
            }

            return false;
        }

        public static bool FormIsOpen(string formName)
        {
            Form.formName = formName;
            return FormIsOpen();
        }

        public static System.Windows.Forms.Form GetOpenForm(string formName)
        {
            FormCollection fc = Application.OpenForms;

            foreach (System.Windows.Forms.Form frm in fc)
            {
                if (frm.Name == formName) return frm;
            }

            return null;
        }

        internal static void Open(string formName, bool modal)
        {
            Form.modal = modal;
            Open(formName);
        }
    }
}