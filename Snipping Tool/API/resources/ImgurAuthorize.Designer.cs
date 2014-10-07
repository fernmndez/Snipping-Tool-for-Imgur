namespace Snipping_Tool.API.resources
{
    partial class ImgurAuthorize
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
            this.buttonVerify = new System.Windows.Forms.Button();
            this.pinTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(254, 13);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(92, 44);
            this.buttonVerify.TabIndex = 0;
            this.buttonVerify.Text = "Verify Pin";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            // 
            // pinTextBox
            // 
            this.pinTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pinTextBox.Location = new System.Drawing.Point(13, 13);
            this.pinTextBox.Name = "pinTextBox";
            this.pinTextBox.Size = new System.Drawing.Size(235, 44);
            this.pinTextBox.TabIndex = 1;
            this.pinTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pinTextBox_KeyDown);
            // 
            // ImgurAuthorize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(358, 68);
            this.Controls.Add(this.pinTextBox);
            this.Controls.Add(this.buttonVerify);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImgurAuthorize";
            this.ShowIcon = false;
            this.Text = "Enter Pin";
            this.Activated += new System.EventHandler(this.ImgurAuthorize_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonVerify;
        private System.Windows.Forms.TextBox pinTextBox;

    }
}