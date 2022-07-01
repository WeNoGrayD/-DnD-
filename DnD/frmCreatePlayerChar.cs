using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DnD_ClassLibrary;

namespace DnD
{
    public partial class frmCreatePlayerChar : Form
    {
        frmGame Game;

        public frmCreatePlayerChar(frmGame game)
        {
            InitializeComponent();
            Game = game;
            LoadAdapter();
            btnCreateChar.Click += btnCreateChar_Click;
        }

        void LoadAdapter()
        {
            cmbbCharClass.Items.AddRange(GameInfo.ClassesList.Select(Cl => Cl.ClassNameRus).ToArray());
        }

        void btnCreateChar_Click(object sender, EventArgs e)
        {
            PlayerCharInfo pCharInfo;

            string pCharName = txtbCharName.Text;
            if (string.IsNullOrWhiteSpace(pCharName))
                return;

            cmbbCharClass.Focus();
            string pCharClassName = cmbbCharClass.SelectedText;
            ClassInfo pCharClassInfo = GameInfo.ClassesList.Find(cli => cli.ClassNameRus == pCharClassName);
            if (pCharClassInfo == null)
                return;

            pCharInfo = new PlayerCharInfo(pCharName, pCharClassInfo);

            GameInfo.PlayersParty.Add(new PlayerChar(pCharInfo));
            
            this.Close();
        }
    }
}
