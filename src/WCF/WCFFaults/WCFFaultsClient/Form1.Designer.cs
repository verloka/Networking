
namespace WCFFaultsClient
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
            this.cbType = new System.Windows.Forms.ComboBox();
            this.btnDivide = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.tbLeft = new System.Windows.Forms.TextBox();
            this.tbRight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnResession = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbType
            // 
            this.cbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Unhandled",
            "Unhandled (with session)",
            ".Net Exception",
            "FaultException",
            "FaultException<T>",
            "Unhandled (+ global hundler)",
            "FaultException (+ global hundler)"});
            this.cbType.Location = new System.Drawing.Point(15, 25);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(773, 21);
            this.cbType.TabIndex = 0;
            // 
            // btnDivide
            // 
            this.btnDivide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDivide.Location = new System.Drawing.Point(713, 82);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(75, 23);
            this.btnDivide.TabIndex = 1;
            this.btnDivide.Text = "Divide";
            this.btnDivide.UseVisualStyleBackColor = true;
            this.btnDivide.Click += new System.EventHandler(this.btnDivide_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoEllipsis = true;
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(12, 126);
            this.lblResult.MaximumSize = new System.Drawing.Size(600, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lblResult.Size = new System.Drawing.Size(40, 33);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "Result:";
            // 
            // tbLeft
            // 
            this.tbLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLeft.Location = new System.Drawing.Point(15, 84);
            this.tbLeft.Name = "tbLeft";
            this.tbLeft.Size = new System.Drawing.Size(340, 20);
            this.tbLeft.TabIndex = 3;
            // 
            // tbRight
            // 
            this.tbRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRight.Location = new System.Drawing.Point(367, 84);
            this.tbRight.Name = "tbRight";
            this.tbRight.Size = new System.Drawing.Size(340, 20);
            this.tbRight.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(355, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "/";
            // 
            // btnResession
            // 
            this.btnResession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResession.Location = new System.Drawing.Point(713, 111);
            this.btnResession.Name = "btnResession";
            this.btnResession.Size = new System.Drawing.Size(75, 23);
            this.btnResession.TabIndex = 7;
            this.btnResession.Text = "Resession";
            this.btnResession.UseVisualStyleBackColor = true;
            this.btnResession.Click += new System.EventHandler(this.btnResession_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 162);
            this.Controls.Add(this.btnResession);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbRight);
            this.Controls.Add(this.tbLeft);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnDivide);
            this.Controls.Add(this.cbType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Button btnDivide;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox tbLeft;
        private System.Windows.Forms.TextBox tbRight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnResession;
    }
}

