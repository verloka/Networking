
namespace WCFServiceClient
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbReverse = new System.Windows.Forms.TextBox();
            this.btnReverse = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.cbGet = new System.Windows.Forms.ComboBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.rtbGetHumanResult = new System.Windows.Forms.RichTextBox();
            this.cbRequest = new System.Windows.Forms.ComboBox();
            this.btnRequest = new System.Windows.Forms.Button();
            this.tbSecret = new System.Windows.Forms.TextBox();
            this.rtbRequestResult = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(542, 358);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblResult);
            this.tabPage1.Controls.Add(this.btnReverse);
            this.tabPage1.Controls.Add(this.tbReverse);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(534, 332);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Reverse string";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtbGetHumanResult);
            this.tabPage2.Controls.Add(this.btnGet);
            this.tabPage2.Controls.Add(this.cbGet);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(534, 332);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Get human";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rtbRequestResult);
            this.tabPage3.Controls.Add(this.tbSecret);
            this.tabPage3.Controls.Add(this.btnRequest);
            this.tabPage3.Controls.Add(this.cbRequest);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(534, 332);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Request human";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbReverse
            // 
            this.tbReverse.Location = new System.Drawing.Point(6, 21);
            this.tbReverse.Name = "tbReverse";
            this.tbReverse.Size = new System.Drawing.Size(390, 20);
            this.tbReverse.TabIndex = 0;
            // 
            // btnReverse
            // 
            this.btnReverse.Location = new System.Drawing.Point(402, 19);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(126, 23);
            this.btnReverse.TabIndex = 1;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(7, 64);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 13);
            this.lblResult.TabIndex = 2;
            // 
            // cbGet
            // 
            this.cbGet.FormattingEnabled = true;
            this.cbGet.Items.AddRange(new object[] {
            "Male",
            "Shemale"});
            this.cbGet.Location = new System.Drawing.Point(6, 20);
            this.cbGet.Name = "cbGet";
            this.cbGet.Size = new System.Drawing.Size(390, 21);
            this.cbGet.TabIndex = 0;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(402, 19);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(126, 23);
            this.btnGet.TabIndex = 2;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // rtbGetHumanResult
            // 
            this.rtbGetHumanResult.Location = new System.Drawing.Point(6, 47);
            this.rtbGetHumanResult.Name = "rtbGetHumanResult";
            this.rtbGetHumanResult.ReadOnly = true;
            this.rtbGetHumanResult.Size = new System.Drawing.Size(522, 279);
            this.rtbGetHumanResult.TabIndex = 4;
            this.rtbGetHumanResult.Text = "";
            // 
            // cbRequest
            // 
            this.cbRequest.FormattingEnabled = true;
            this.cbRequest.Items.AddRange(new object[] {
            "Male",
            "Shemale"});
            this.cbRequest.Location = new System.Drawing.Point(6, 22);
            this.cbRequest.Name = "cbRequest";
            this.cbRequest.Size = new System.Drawing.Size(202, 21);
            this.cbRequest.TabIndex = 1;
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(402, 22);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(126, 23);
            this.btnRequest.TabIndex = 3;
            this.btnRequest.Text = "Request";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // tbSecret
            // 
            this.tbSecret.Location = new System.Drawing.Point(214, 23);
            this.tbSecret.Name = "tbSecret";
            this.tbSecret.Size = new System.Drawing.Size(182, 20);
            this.tbSecret.TabIndex = 4;
            // 
            // rtbRequestResult
            // 
            this.rtbRequestResult.Location = new System.Drawing.Point(6, 49);
            this.rtbRequestResult.Name = "rtbRequestResult";
            this.rtbRequestResult.ReadOnly = true;
            this.rtbRequestResult.Size = new System.Drawing.Size(522, 277);
            this.rtbRequestResult.TabIndex = 5;
            this.rtbRequestResult.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 382);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.TextBox tbReverse;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.ComboBox cbGet;
        private System.Windows.Forms.RichTextBox rtbGetHumanResult;
        private System.Windows.Forms.ComboBox cbRequest;
        private System.Windows.Forms.TextBox tbSecret;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.RichTextBox rtbRequestResult;
    }
}

