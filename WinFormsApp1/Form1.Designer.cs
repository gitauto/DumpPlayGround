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
            dumpAWindowsFormToolStripMenuItem = new ToolStripMenuItem();
            clearPageToolStripMenuItem = new ToolStripMenuItem();
            clearPageLightModeToolStripMenuItem = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, dumpTestToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(700, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportHTMLToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // exportHTMLToolStripMenuItem
            // 
            exportHTMLToolStripMenuItem.Name = "exportHTMLToolStripMenuItem";
            exportHTMLToolStripMenuItem.Size = new Size(143, 22);
            exportHTMLToolStripMenuItem.Text = "Export HTML";
            exportHTMLToolStripMenuItem.Click += ExportHTMLToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(143, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // dumpTestToolStripMenuItem
            // 
            dumpTestToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { testToolStripMenuItem, dumpAWindowsFormToolStripMenuItem, clearPageToolStripMenuItem, clearPageLightModeToolStripMenuItem });
            dumpTestToolStripMenuItem.Name = "dumpTestToolStripMenuItem";
            dumpTestToolStripMenuItem.Size = new Size(52, 20);
            dumpTestToolStripMenuItem.Text = "&Dump";
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(203, 22);
            testToolStripMenuItem.Text = "Run Dump Test";
            testToolStripMenuItem.Click += TestToolStripMenuItem_Click;
            // 
            // dumpAWindowsFormToolStripMenuItem
            // 
            dumpAWindowsFormToolStripMenuItem.Name = "dumpAWindowsFormToolStripMenuItem";
            dumpAWindowsFormToolStripMenuItem.Size = new Size(203, 22);
            dumpAWindowsFormToolStripMenuItem.Text = "Dump a Windows Form";
            dumpAWindowsFormToolStripMenuItem.Click += DumpAWindowsFormToolStripMenuItem_Click;
            // 
            // clearPageToolStripMenuItem
            // 
            clearPageToolStripMenuItem.Name = "clearPageToolStripMenuItem";
            clearPageToolStripMenuItem.Size = new Size(203, 22);
            clearPageToolStripMenuItem.Text = "Clear Page (Light Mode)";
            clearPageToolStripMenuItem.Click += ClearPageToolStripMenuItem_Click;
            // 
            // clearPageLightModeToolStripMenuItem
            // 
            clearPageLightModeToolStripMenuItem.Name = "clearPageLightModeToolStripMenuItem";
            clearPageLightModeToolStripMenuItem.Size = new Size(203, 22);
            clearPageLightModeToolStripMenuItem.Text = "Clear Page (Dark Mode))";
            clearPageLightModeToolStripMenuItem.Click += ClearPageLightModeToolStripMenuItem_Click;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(0, 27);
            webView21.Name = "webView21";
            webView21.Size = new Size(700, 311);
            webView21.TabIndex = 1;
            webView21.ZoomFactor = 1D;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(webView21);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "WinForms - Dump Playground";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
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
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
    }
}
