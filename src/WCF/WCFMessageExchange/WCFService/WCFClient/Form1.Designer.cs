
namespace WCFClient
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
            this.btnInit = new System.Windows.Forms.Button();
            this.pg = new System.Windows.Forms.ProgressBar();
            this.btnGetResponse = new System.Windows.Forms.Button();
            this.lblResponse = new System.Windows.Forms.Label();
            this.lblDt = new System.Windows.Forms.Label();
            this.btnStartProcessing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInit.Location = new System.Drawing.Point(12, 12);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(420, 23);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "Init chanel";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // pg
            // 
            this.pg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pg.Location = new System.Drawing.Point(167, 93);
            this.pg.Name = "pg";
            this.pg.Size = new System.Drawing.Size(265, 23);
            this.pg.TabIndex = 1;
            // 
            // btnGetResponse
            // 
            this.btnGetResponse.Enabled = false;
            this.btnGetResponse.Location = new System.Drawing.Point(12, 64);
            this.btnGetResponse.Name = "btnGetResponse";
            this.btnGetResponse.Size = new System.Drawing.Size(146, 23);
            this.btnGetResponse.TabIndex = 2;
            this.btnGetResponse.Text = "Get response";
            this.btnGetResponse.UseVisualStyleBackColor = true;
            this.btnGetResponse.Click += new System.EventHandler(this.btnGetResponse_Click);
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Location = new System.Drawing.Point(164, 69);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(0, 13);
            this.lblResponse.TabIndex = 3;
            // 
            // lblDt
            // 
            this.lblDt.AutoSize = true;
            this.lblDt.Location = new System.Drawing.Point(12, 141);
            this.lblDt.Name = "lblDt";
            this.lblDt.Size = new System.Drawing.Size(0, 13);
            this.lblDt.TabIndex = 4;
            // 
            // btnStartProcessing
            // 
            this.btnStartProcessing.Enabled = false;
            this.btnStartProcessing.Location = new System.Drawing.Point(12, 93);
            this.btnStartProcessing.Name = "btnStartProcessing";
            this.btnStartProcessing.Size = new System.Drawing.Size(146, 23);
            this.btnStartProcessing.TabIndex = 5;
            this.btnStartProcessing.Text = "Start processing";
            this.btnStartProcessing.UseVisualStyleBackColor = true;
            this.btnStartProcessing.Click += new System.EventHandler(this.btnStartProcessing_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 180);
            this.Controls.Add(this.btnStartProcessing);
            this.Controls.Add(this.lblDt);
            this.Controls.Add(this.lblResponse);
            this.Controls.Add(this.btnGetResponse);
            this.Controls.Add(this.pg);
            this.Controls.Add(this.btnInit);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.ProgressBar pg;
        private System.Windows.Forms.Button btnGetResponse;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.Label lblDt;
        private System.Windows.Forms.Button btnStartProcessing;
    }
}

