namespace DnD
{
    partial class frmCharList
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
            this.lstvCharEffects = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClose = new System.Windows.Forms.Button();
            this.cbSelectChar = new System.Windows.Forms.ComboBox();
            this.lstvCharParams = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstvCharEffects
            // 
            this.lstvCharEffects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstvCharEffects.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstvCharEffects.HideSelection = false;
            this.lstvCharEffects.Location = new System.Drawing.Point(369, 12);
            this.lstvCharEffects.Name = "lstvCharEffects";
            this.lstvCharEffects.Size = new System.Drawing.Size(382, 332);
            this.lstvCharEffects.TabIndex = 0;
            this.lstvCharEffects.UseCompatibleStateImageBehavior = false;
            this.lstvCharEffects.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Эффекты";
            this.columnHeader1.Width = 377;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClose.Location = new System.Drawing.Point(757, 21);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 105);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Закрыть окно";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbSelectChar
            // 
            this.cbSelectChar.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbSelectChar.FormattingEnabled = true;
            this.cbSelectChar.Location = new System.Drawing.Point(12, 12);
            this.cbSelectChar.Name = "cbSelectChar";
            this.cbSelectChar.Size = new System.Drawing.Size(340, 41);
            this.cbSelectChar.TabIndex = 11;
            this.cbSelectChar.SelectedIndexChanged += new System.EventHandler(this.cbSelectChar_SelectedIndexChanged);
            // 
            // lstvCharParams
            // 
            this.lstvCharParams.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.lstvCharParams.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstvCharParams.HideSelection = false;
            this.lstvCharParams.Location = new System.Drawing.Point(12, 68);
            this.lstvCharParams.Name = "lstvCharParams";
            this.lstvCharParams.Size = new System.Drawing.Size(232, 276);
            this.lstvCharParams.TabIndex = 12;
            this.lstvCharParams.UseCompatibleStateImageBehavior = false;
            this.lstvCharParams.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Хар";
            this.columnHeader2.Width = 111;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Знач";
            this.columnHeader3.Width = 116;
            // 
            // frmCharList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 356);
            this.Controls.Add(this.lstvCharParams);
            this.Controls.Add(this.cbSelectChar);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lstvCharEffects);
            this.Name = "frmCharList";
            this.Text = "Лист противников";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstvCharEffects;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbSelectChar;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView lstvCharParams;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}