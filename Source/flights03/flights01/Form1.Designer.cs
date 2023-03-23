
namespace flights01
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lbArrival = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbLastUpdate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDomestic = new System.Windows.Forms.CheckBox();
            this.cbSchengen = new System.Windows.Forms.CheckBox();
            this.cbInternational = new System.Windows.Forms.CheckBox();
            this.lblDebug = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(583, 231);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 22);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(495, 231);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(76, 22);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Arr/Dep";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lbArrival
            // 
            this.lbArrival.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbArrival.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbArrival.Font = new System.Drawing.Font("Lucida Console", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbArrival.FormattingEnabled = true;
            this.lbArrival.ItemHeight = 48;
            this.lbArrival.Location = new System.Drawing.Point(21, 8);
            this.lbArrival.Margin = new System.Windows.Forms.Padding(2);
            this.lbArrival.Name = "lbArrival";
            this.lbArrival.Size = new System.Drawing.Size(613, 196);
            this.lbArrival.TabIndex = 2;
            this.lbArrival.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbArrival_MouseClick);
            this.lbArrival.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbArrival_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 120000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tbLastUpdate
            // 
            this.tbLastUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbLastUpdate.Enabled = false;
            this.tbLastUpdate.Location = new System.Drawing.Point(96, 234);
            this.tbLastUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.tbLastUpdate.Name = "tbLastUpdate";
            this.tbLastUpdate.Size = new System.Drawing.Size(115, 20);
            this.tbLastUpdate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 236);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Last Updated:";
            // 
            // cbDomestic
            // 
            this.cbDomestic.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbDomestic.AutoSize = true;
            this.cbDomestic.Location = new System.Drawing.Point(240, 233);
            this.cbDomestic.Margin = new System.Windows.Forms.Padding(2);
            this.cbDomestic.Name = "cbDomestic";
            this.cbDomestic.Size = new System.Drawing.Size(70, 17);
            this.cbDomestic.TabIndex = 5;
            this.cbDomestic.Text = "Domestic";
            this.cbDomestic.UseVisualStyleBackColor = true;
            this.cbDomestic.CheckedChanged += new System.EventHandler(this.cbDomestic_CheckedChanged);
            // 
            // cbSchengen
            // 
            this.cbSchengen.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbSchengen.AutoSize = true;
            this.cbSchengen.Location = new System.Drawing.Point(327, 233);
            this.cbSchengen.Margin = new System.Windows.Forms.Padding(2);
            this.cbSchengen.Name = "cbSchengen";
            this.cbSchengen.Size = new System.Drawing.Size(75, 17);
            this.cbSchengen.TabIndex = 6;
            this.cbSchengen.Text = "Schengen";
            this.cbSchengen.UseVisualStyleBackColor = true;
            this.cbSchengen.CheckedChanged += new System.EventHandler(this.cbSchengen_CheckedChanged);
            // 
            // cbInternational
            // 
            this.cbInternational.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbInternational.AutoSize = true;
            this.cbInternational.Location = new System.Drawing.Point(407, 233);
            this.cbInternational.Margin = new System.Windows.Forms.Padding(2);
            this.cbInternational.Name = "cbInternational";
            this.cbInternational.Size = new System.Drawing.Size(84, 17);
            this.cbInternational.TabIndex = 7;
            this.cbInternational.Text = "International";
            this.cbInternational.UseVisualStyleBackColor = true;
            this.cbInternational.CheckedChanged += new System.EventHandler(this.cbInternational_CheckedChanged);
            // 
            // lblDebug
            // 
            this.lblDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDebug.AutoSize = true;
            this.lblDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebug.Location = new System.Drawing.Point(93, 206);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(141, 20);
            this.lblDebug.TabIndex = 8;
            this.lblDebug.Text = "Airport Information";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(82, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(551, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Hover on list to display information - click line to save coordinates to Clipboar" +
    "d";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 259);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDebug);
            this.Controls.Add(this.cbInternational);
            this.Controls.Add(this.cbSchengen);
            this.Controls.Add(this.cbDomestic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbLastUpdate);
            this.Controls.Add(this.lbArrival);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnExit);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Flights  OSL (v 1.3)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ListBox lbArrival;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox tbLastUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbDomestic;
        private System.Windows.Forms.CheckBox cbSchengen;
        private System.Windows.Forms.CheckBox cbInternational;
        private System.Windows.Forms.Label lblDebug;
        private System.Windows.Forms.Label label2;
    }
}

