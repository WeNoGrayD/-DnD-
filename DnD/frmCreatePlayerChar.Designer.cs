namespace DnD
{
    partial class frmCreatePlayerChar
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
            this.txtbCharName = new System.Windows.Forms.TextBox();
            this.cmbbCharClass = new System.Windows.Forms.ComboBox();
            this.btnCreateChar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtbCharName
            // 
            this.txtbCharName.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbCharName.Location = new System.Drawing.Point(12, 12);
            this.txtbCharName.Name = "txtbCharName";
            this.txtbCharName.Size = new System.Drawing.Size(248, 41);
            this.txtbCharName.TabIndex = 0;
            // 
            // cmbbCharClass
            // 
            this.cmbbCharClass.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbbCharClass.FormattingEnabled = true;
            this.cmbbCharClass.Location = new System.Drawing.Point(12, 68);
            this.cmbbCharClass.Name = "cmbbCharClass";
            this.cmbbCharClass.Size = new System.Drawing.Size(248, 41);
            this.cmbbCharClass.TabIndex = 1;
            // 
            // btnCreateChar
            // 
            this.btnCreateChar.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCreateChar.Location = new System.Drawing.Point(12, 126);
            this.btnCreateChar.Name = "btnCreateChar";
            this.btnCreateChar.Size = new System.Drawing.Size(248, 41);
            this.btnCreateChar.TabIndex = 2;
            this.btnCreateChar.Text = "Создать персонажа";
            this.btnCreateChar.UseVisualStyleBackColor = true;
            // 
            // frmCreatePlayerChar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 179);
            this.Controls.Add(this.btnCreateChar);
            this.Controls.Add(this.cmbbCharClass);
            this.Controls.Add(this.txtbCharName);
            this.Name = "frmCreatePlayerChar";
            this.Text = "Лист персонажа";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbCharName;
        private System.Windows.Forms.ComboBox cmbbCharClass;
        private System.Windows.Forms.Button btnCreateChar;
    }
}