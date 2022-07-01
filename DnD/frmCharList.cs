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
    public partial class frmCharList : Form
    {
        // Списки строк параметров и эффектов для каждого персонажа.

        List<List<string[]>> charsParamsStrs = new List<List<string[]>>(),
                         charsEffectsStrs =  new List<List<string[]>>(); 
        public frmCharList()
        {
            InitializeComponent();
        }

        public void SetCharList<T>(List<T> chars) where T : Character
        {
            cbSelectChar.Items.AddRange(chars.Select(ch => ch.CI.CharName).ToArray());

            bool charsAreNPCs = chars?[0] is EnemyNPC;

            cbSelectChar.Text = charsAreNPCs ? "Выберите монстра" : "Выберите персонажа";

            for (int i = 0; i < chars.Count; i++)
            {
                T chr = chars[i];
                charsParamsStrs.Add(new List<string[]>());
                charsEffectsStrs.Add(new List<string[]>());

                string[] charParamsNames, charParamsValues;
                chr.GetCharInfoForGUI(out charParamsNames, out charParamsValues);
                for (int j = 0; j < charParamsNames.Length; j++)
                    charsParamsStrs[i]
                        .Add(new string[] { charParamsNames[j], charParamsValues[j] });

                string[] charEffectsNames, charEffectsValues;
                chr.GetCharEffectsForGUI(out charEffectsNames, out charEffectsValues);
                for (int j = 0; j < charEffectsNames.Length; j++)
                    charsEffectsStrs[i]
                        .Add(new string[] { charEffectsNames[j], charEffectsValues[j] });
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbSelectChar_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstvCharParams.Items.Clear();
            lstvCharEffects.Items.Clear();

            int sI = cbSelectChar.SelectedIndex;

            foreach (string[] param in charsParamsStrs[sI])
                lstvCharParams.Items.Add(new ListViewItem(param));

            foreach (string[] effect in charsEffectsStrs[sI])
                lstvCharEffects.Items
                    .Add(new ListViewItem(string.Concat(effect[0], " ", effect[1])));
        }
    }
}
