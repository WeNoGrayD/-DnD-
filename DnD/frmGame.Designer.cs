using DnD_ClassLibrary;

namespace DnD
{
    public partial class frmGame
    {

        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbChooseAbility = new System.Windows.Forms.ComboBox();
            this.lstvCharInfo = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbChooseTarget = new System.Windows.Forms.ComboBox();
            this.txtbChooseAbility = new System.Windows.Forms.TextBox();
            this.txtbChooseTarget = new System.Windows.Forms.TextBox();
            this.btnShowParty = new System.Windows.Forms.Button();
            this.btnShowEncounter = new System.Windows.Forms.Button();
            this.btnRollDie = new System.Windows.Forms.Button();
            this.lstbChat = new System.Windows.Forms.ListBox();
            this.lstvTargetInfo = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtbCharName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbChooseAbility
            // 
            this.cbChooseAbility.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbChooseAbility.FormattingEnabled = true;
            this.cbChooseAbility.Location = new System.Drawing.Point(503, 303);
            this.cbChooseAbility.Name = "cbChooseAbility";
            this.cbChooseAbility.Size = new System.Drawing.Size(285, 41);
            this.cbChooseAbility.TabIndex = 1;
            // 
            // lstvCharInfo
            // 
            this.lstvCharInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstvCharInfo.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstvCharInfo.HideSelection = false;
            this.lstvCharInfo.Location = new System.Drawing.Point(503, 59);
            this.lstvCharInfo.Name = "lstvCharInfo";
            this.lstvCharInfo.Size = new System.Drawing.Size(285, 191);
            this.lstvCharInfo.TabIndex = 3;
            this.lstvCharInfo.UseCompatibleStateImageBehavior = false;
            this.lstvCharInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Хар";
            this.columnHeader1.Width = 97;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Знач";
            this.columnHeader2.Width = 183;
            // 
            // cbChooseTarget
            // 
            this.cbChooseTarget.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbChooseTarget.FormattingEnabled = true;
            this.cbChooseTarget.Location = new System.Drawing.Point(503, 397);
            this.cbChooseTarget.Name = "cbChooseTarget";
            this.cbChooseTarget.Size = new System.Drawing.Size(285, 41);
            this.cbChooseTarget.TabIndex = 4;
            // 
            // txtbChooseAbility
            // 
            this.txtbChooseAbility.Enabled = false;
            this.txtbChooseAbility.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbChooseAbility.Location = new System.Drawing.Point(503, 256);
            this.txtbChooseAbility.Name = "txtbChooseAbility";
            this.txtbChooseAbility.Size = new System.Drawing.Size(285, 41);
            this.txtbChooseAbility.TabIndex = 5;
            this.txtbChooseAbility.Text = "Выберите способность:";
            // 
            // txtbChooseTarget
            // 
            this.txtbChooseTarget.Enabled = false;
            this.txtbChooseTarget.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbChooseTarget.Location = new System.Drawing.Point(503, 350);
            this.txtbChooseTarget.Name = "txtbChooseTarget";
            this.txtbChooseTarget.Size = new System.Drawing.Size(285, 41);
            this.txtbChooseTarget.TabIndex = 6;
            this.txtbChooseTarget.Text = "Выберите цель:";
            // 
            // btnShowParty
            // 
            this.btnShowParty.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnShowParty.Location = new System.Drawing.Point(800, 12);
            this.btnShowParty.Name = "btnShowParty";
            this.btnShowParty.Size = new System.Drawing.Size(144, 78);
            this.btnShowParty.TabIndex = 8;
            this.btnShowParty.Text = "Показать лист команды";
            this.btnShowParty.UseVisualStyleBackColor = true;
            this.btnShowParty.Click += new System.EventHandler(this.btnShowParty_Click);
            // 
            // btnShowEncounter
            // 
            this.btnShowEncounter.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnShowEncounter.Location = new System.Drawing.Point(800, 111);
            this.btnShowEncounter.Name = "btnShowEncounter";
            this.btnShowEncounter.Size = new System.Drawing.Size(144, 105);
            this.btnShowEncounter.TabIndex = 9;
            this.btnShowEncounter.Text = "Показать лист противников";
            this.btnShowEncounter.UseVisualStyleBackColor = true;
            this.btnShowEncounter.Click += new System.EventHandler(this.btnShowEncounter_Click);
            // 
            // btnRollDie
            // 
            this.btnRollDie.Enabled = false;
            this.btnRollDie.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRollDie.Location = new System.Drawing.Point(800, 238);
            this.btnRollDie.Name = "btnRollDie";
            this.btnRollDie.Size = new System.Drawing.Size(144, 78);
            this.btnRollDie.TabIndex = 10;
            this.btnRollDie.Text = "Бросить кубик d6";
            this.btnRollDie.UseVisualStyleBackColor = true;
            this.btnRollDie.Click += new System.EventHandler(this.btnRollDie_Click);
            // 
            // lstbChat
            // 
            this.lstbChat.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstbChat.FormattingEnabled = true;
            this.lstbChat.ItemHeight = 28;
            this.lstbChat.Location = new System.Drawing.Point(12, 12);
            this.lstbChat.Name = "lstbChat";
            this.lstbChat.Size = new System.Drawing.Size(480, 620);
            this.lstbChat.TabIndex = 11;
            // 
            // lstvTargetInfo
            // 
            this.lstvTargetInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lstvTargetInfo.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstvTargetInfo.HideSelection = false;
            this.lstvTargetInfo.Location = new System.Drawing.Point(503, 444);
            this.lstvTargetInfo.Name = "lstvTargetInfo";
            this.lstvTargetInfo.Size = new System.Drawing.Size(285, 191);
            this.lstvTargetInfo.TabIndex = 12;
            this.lstvTargetInfo.UseCompatibleStateImageBehavior = false;
            this.lstvTargetInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Хар";
            this.columnHeader3.Width = 97;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Знач";
            this.columnHeader4.Width = 183;
            // 
            // txtbCharName
            // 
            this.txtbCharName.Enabled = false;
            this.txtbCharName.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbCharName.Location = new System.Drawing.Point(503, 12);
            this.txtbCharName.Name = "txtbCharName";
            this.txtbCharName.Size = new System.Drawing.Size(285, 41);
            this.txtbCharName.TabIndex = 7;
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 642);
            this.Controls.Add(this.lstvTargetInfo);
            this.Controls.Add(this.lstbChat);
            this.Controls.Add(this.btnRollDie);
            this.Controls.Add(this.btnShowEncounter);
            this.Controls.Add(this.btnShowParty);
            this.Controls.Add(this.txtbCharName);
            this.Controls.Add(this.txtbChooseTarget);
            this.Controls.Add(this.txtbChooseAbility);
            this.Controls.Add(this.cbChooseTarget);
            this.Controls.Add(this.lstvCharInfo);
            this.Controls.Add(this.cbChooseAbility);
            this.Name = "frmGame";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbChooseAbility;
        private System.Windows.Forms.ListView lstvCharInfo;
        private System.Windows.Forms.ComboBox cbChooseTarget;
        private System.Windows.Forms.TextBox txtbChooseAbility;
        private System.Windows.Forms.TextBox txtbChooseTarget;
        private System.Windows.Forms.Button btnShowParty;
        private System.Windows.Forms.Button btnShowEncounter;
        private System.Windows.Forms.Button btnRollDie;
        private System.Windows.Forms.ListBox lstbChat;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lstvTargetInfo;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox txtbCharName;
    }
}

