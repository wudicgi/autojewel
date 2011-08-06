/***********************************************************************
This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

The GNU General Public License can be found at
http://www.gnu.org/copyleft/gpl.html
***********************************************************************/

namespace WudiLabs.AutoJewel
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupMode = new System.Windows.Forms.GroupBox();
            this.radioModeBalance = new System.Windows.Forms.RadioButton();
            this.radioModeZen = new System.Windows.Forms.RadioButton();
            this.radioModeIceStorm = new System.Windows.Forms.RadioButton();
            this.radioModeLightning = new System.Windows.Forms.RadioButton();
            this.radioModeClassic = new System.Windows.Forms.RadioButton();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.labelTip = new System.Windows.Forms.Label();
            this.groupProcess = new System.Windows.Forms.GroupBox();
            this.radioProcessTrial = new System.Windows.Forms.RadioButton();
            this.radioProcessStandard = new System.Windows.Forms.RadioButton();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupMode.SuspendLayout();
            this.groupProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMode
            // 
            this.groupMode.Controls.Add(this.radioModeBalance);
            this.groupMode.Controls.Add(this.radioModeZen);
            this.groupMode.Controls.Add(this.radioModeIceStorm);
            this.groupMode.Controls.Add(this.radioModeLightning);
            this.groupMode.Controls.Add(this.radioModeClassic);
            this.groupMode.Location = new System.Drawing.Point(8, 8);
            this.groupMode.Name = "groupMode";
            this.groupMode.Size = new System.Drawing.Size(240, 64);
            this.groupMode.TabIndex = 0;
            this.groupMode.TabStop = false;
            this.groupMode.Text = "Mode";
            // 
            // radioModeBalance
            // 
            this.radioModeBalance.AutoSize = true;
            this.radioModeBalance.Location = new System.Drawing.Point(80, 40);
            this.radioModeBalance.Name = "radioModeBalance";
            this.radioModeBalance.Size = new System.Drawing.Size(64, 17);
            this.radioModeBalance.TabIndex = 4;
            this.radioModeBalance.Text = "Balance";
            this.radioModeBalance.UseVisualStyleBackColor = true;
            // 
            // radioModeZen
            // 
            this.radioModeZen.AutoSize = true;
            this.radioModeZen.Location = new System.Drawing.Point(10, 40);
            this.radioModeZen.Name = "radioModeZen";
            this.radioModeZen.Size = new System.Drawing.Size(44, 17);
            this.radioModeZen.TabIndex = 3;
            this.radioModeZen.Text = "Zen";
            this.radioModeZen.UseVisualStyleBackColor = true;
            // 
            // radioModeIceStorm
            // 
            this.radioModeIceStorm.AutoSize = true;
            this.radioModeIceStorm.Location = new System.Drawing.Point(160, 19);
            this.radioModeIceStorm.Name = "radioModeIceStorm";
            this.radioModeIceStorm.Size = new System.Drawing.Size(70, 17);
            this.radioModeIceStorm.TabIndex = 2;
            this.radioModeIceStorm.Text = "Ice Storm";
            this.radioModeIceStorm.UseVisualStyleBackColor = true;
            // 
            // radioModeLightning
            // 
            this.radioModeLightning.AutoSize = true;
            this.radioModeLightning.Checked = true;
            this.radioModeLightning.Location = new System.Drawing.Point(80, 19);
            this.radioModeLightning.Name = "radioModeLightning";
            this.radioModeLightning.Size = new System.Drawing.Size(68, 17);
            this.radioModeLightning.TabIndex = 1;
            this.radioModeLightning.TabStop = true;
            this.radioModeLightning.Text = "Lightning";
            this.radioModeLightning.UseVisualStyleBackColor = true;
            // 
            // radioModeClassic
            // 
            this.radioModeClassic.AutoSize = true;
            this.radioModeClassic.Location = new System.Drawing.Point(10, 19);
            this.radioModeClassic.Name = "radioModeClassic";
            this.radioModeClassic.Size = new System.Drawing.Size(58, 17);
            this.radioModeClassic.TabIndex = 0;
            this.radioModeClassic.Text = "Classic";
            this.radioModeClassic.UseVisualStyleBackColor = true;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 151);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(374, 22);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "Done.";
            // 
            // labelTip
            // 
            this.labelTip.Location = new System.Drawing.Point(262, 22);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new System.Drawing.Size(110, 50);
            this.labelTip.TabIndex = 4;
            this.labelTip.Text = "Press Ctrl + F8\r\n(if not modified)\r\nto start/stop";
            // 
            // groupProcess
            // 
            this.groupProcess.Controls.Add(this.radioProcessTrial);
            this.groupProcess.Controls.Add(this.radioProcessStandard);
            this.groupProcess.Location = new System.Drawing.Point(8, 78);
            this.groupProcess.Name = "groupProcess";
            this.groupProcess.Size = new System.Drawing.Size(240, 64);
            this.groupProcess.TabIndex = 1;
            this.groupProcess.TabStop = false;
            this.groupProcess.Text = "Process";
            // 
            // radioProcessTrial
            // 
            this.radioProcessTrial.AutoSize = true;
            this.radioProcessTrial.Location = new System.Drawing.Point(10, 40);
            this.radioProcessTrial.Name = "radioProcessTrial";
            this.radioProcessTrial.Size = new System.Drawing.Size(175, 17);
            this.radioProcessTrial.TabIndex = 1;
            this.radioProcessTrial.Text = "popcapgame1.exe (trial version)";
            this.radioProcessTrial.UseVisualStyleBackColor = true;
            // 
            // radioProcessStandard
            // 
            this.radioProcessStandard.AutoSize = true;
            this.radioProcessStandard.Checked = true;
            this.radioProcessStandard.Location = new System.Drawing.Point(10, 19);
            this.radioProcessStandard.Name = "radioProcessStandard";
            this.radioProcessStandard.Size = new System.Drawing.Size(159, 17);
            this.radioProcessStandard.TabIndex = 0;
            this.radioProcessStandard.TabStop = true;
            this.radioProcessStandard.Text = "Bejeweled3.exe (purchased)";
            this.radioProcessStandard.UseVisualStyleBackColor = true;
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(262, 88);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(100, 24);
            this.buttonAbout.TabIndex = 2;
            this.buttonAbout.Text = "&About...";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(262, 118);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(100, 24);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "&Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 173);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.groupProcess);
            this.Controls.Add(this.labelTip);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.groupMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "AutoJewel";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupMode.ResumeLayout(false);
            this.groupMode.PerformLayout();
            this.groupProcess.ResumeLayout(false);
            this.groupProcess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMode;
        private System.Windows.Forms.RadioButton radioModeIceStorm;
        private System.Windows.Forms.RadioButton radioModeLightning;
        private System.Windows.Forms.RadioButton radioModeClassic;
        private System.Windows.Forms.RadioButton radioModeBalance;
        private System.Windows.Forms.RadioButton radioModeZen;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.Label labelTip;
        private System.Windows.Forms.GroupBox groupProcess;
        private System.Windows.Forms.RadioButton radioProcessTrial;
        private System.Windows.Forms.RadioButton radioProcessStandard;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Button buttonExit;
    }
}

