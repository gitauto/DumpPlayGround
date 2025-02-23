namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exportHTMLToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            dumpTestToolStripMenuItem = new ToolStripMenuItem();
            testToolStripMenuItem = new ToolStripMenuItem();
            clearPageToolStripMenuItem = new ToolStripMenuItem();
            clearPageLightModeToolStripMenuItem = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            dumpAWindowsFormToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, dumpTestToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportHTMLToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "&File";
            // 
            // exportHTMLToolStripMenuItem
            // 
            exportHTMLToolStripMenuItem.Name = "exportHTMLToolStripMenuItem";
            exportHTMLToolStripMenuItem.Size = new Size(178, 26);
            exportHTMLToolStripMenuItem.Text = "Export HTML";
            exportHTMLToolStripMenuItem.Click += ExportHTMLToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(178, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // dumpTestToolStripMenuItem
            // 
            dumpTestToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { testToolStripMenuItem, dumpAWindowsFormToolStripMenuItem, clearPageToolStripMenuItem, clearPageLightModeToolStripMenuItem });
            dumpTestToolStripMenuItem.Name = "dumpTestToolStripMenuItem";
            dumpTestToolStripMenuItem.Size = new Size(64, 24);
            dumpTestToolStripMenuItem.Text = "&Dump";
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(255, 26);
            testToolStripMenuItem.Text = "Run Dump Test";
            testToolStripMenuItem.Click += TestToolStripMenuItem_Click;
            // 
            // clearPageToolStripMenuItem
            // 
            clearPageToolStripMenuItem.Name = "clearPageToolStripMenuItem";
            clearPageToolStripMenuItem.Size = new Size(255, 26);
            clearPageToolStripMenuItem.Text = "Clear Page (Light Mode)";
            clearPageToolStripMenuItem.Click += ClearPageToolStripMenuItem_Click;
            // 
            // clearPageLightModeToolStripMenuItem
            // 
            clearPageLightModeToolStripMenuItem.Name = "clearPageLightModeToolStripMenuItem";
            clearPageLightModeToolStripMenuItem.Size = new Size(255, 26);
            clearPageLightModeToolStripMenuItem.Text = "Clear Page (Dark Mode))";
            clearPageLightModeToolStripMenuItem.Click += ClearPageLightModeToolStripMenuItem_Click;
            // 
            // dumpAWindowsFormToolStripMenuItem
            // 
            dumpAWindowsFormToolStripMenuItem.Name = "dumpAWindowsFormToolStripMenuItem";
            dumpAWindowsFormToolStripMenuItem.Size = new Size(255, 26);
            dumpAWindowsFormToolStripMenuItem.Text = "Dump a Windows Form";
            dumpAWindowsFormToolStripMenuItem.Click += DumpAWindowsFormToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "WinForms - Dump Playground";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exportHTMLToolStripMenuItem;
        private ToolStripMenuItem dumpTestToolStripMenuItem;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripMenuItem clearPageToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem clearPageLightModeToolStripMenuItem;
        private ToolStripMenuItem dumpAWindowsFormToolStripMenuItem;
    }
}
