﻿//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WealthBuilder
{
    public partial class ToolsForm : Form
    {
        public ToolsForm()
        {
            InitializeComponent();
        }

        private void ToolsForm_Load(object sender, EventArgs e)
        {
            createSupportFileButton.Location = new Point(Constants.x2, 15);
            createSupportFileButton.TabIndex = 1;
        }

        private void createSupportFileButton_Click(object sender, EventArgs e)
        {
            if (!SupportFile.Create())
            {
                MessageBox.Show("The support file could not be created. Please try again or contact Customer Care.");
                return;
            }

            MessageBox.Show("The support file has been created.");
        }
    }
}
