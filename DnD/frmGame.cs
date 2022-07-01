using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DnD_ClassLibrary;

namespace DnD
{
    public partial class frmGame : Form
    {
        frmCharList PlayersPartyList;

        frmCharList EncounterList;
        public frmGame()
        {
            InitializeComponent();

            GameInfo.lstbChat = lstbChat;
            GameInfo.txtbCharName = txtbCharName;
            GameInfo.lstvCharInfo = lstvCharInfo;
            GameInfo.cbChooseAbility = cbChooseAbility;
            GameInfo.cbchooseTarget = cbChooseTarget;
            GameInfo.btnRollDie = btnRollDie;
            GameInfo.btnShowParty = btnShowParty;
            GameInfo.btnShowEncounter = btnShowEncounter;

            cbChooseAbility.SelectedIndexChanged += cbChooseAbilitySelectedItem_Changed;
            cbChooseTarget.SelectedIndexChanged += cbChooseTargetSelectedItem_Changed;

            GameLibraries GLibs = new GameLibraries();

            CreatePlayersParty(3);

            CreateEncounter(1);

            GameInfo.GUIWritelnEvent += GUI_FillCharName;
            GameInfo.GUIWritelnEvent += GUI_FillCharInfo;
            GameInfo.GUIWritelnEvent += GUI_Clear;
            GameInfo.GUIWritelnEvent += GUI_FillCharAbilities;
            GameInfo.ChatWritelnEvent += ChatWriteln;
            GameInfo.BtnRollDieEnablingEvent += btnRollDie_Enabling;

            this.HandleCreated += OnCreated;
        }

        public void OnCreated(object sender, EventArgs e)
        {
            GameInfo.SemGameStart = new Semaphore(1, 1);

            Thread DungeonMasterThread = new Thread(DungeonMaster.StartGame);
            DungeonMasterThread.Name = "DungeonMasterThread";
            DungeonMasterThread.Start();
        }

        // Создание списка персонажей игроков.

        private void CreatePlayersParty(int playersNum)
        {
            GameInfo.PlayersParty = new List<PlayerChar>();

            for (int i = 0; i < playersNum; i++)
            {
                frmCreatePlayerChar CharList = new frmCreatePlayerChar(this);
                CharList.ShowDialog();
            }
        }

        // Создание группы монстров для сражения.

        private void CreateEncounter(int i)
        {
            ;
            GameInfo.Encounter = new List<EnemyNPC>();

            switch (i)
            {
                case 0:
                    {
                        GameInfo.Encounter = new List<EnemyNPC>
                        {
                            new EnemyNPC(
                                GameInfo.Bestiary.Find(
                                npc => npc.CharName.Equals("Лич-некромант")),
                                1
                                ),
                            new EnemyNPC(
                                GameInfo.Bestiary.Find(
                                npc => npc.CharName.Equals("Скелет-лучник")),
                                1
                                ),
                            new EnemyNPC(
                                GameInfo.Bestiary.Find(
                                npc => npc.CharName.Equals("Скелет-воин")),
                                1
                                )
                        };
                        break;
                    }

                case 1:
                    {
                        GameInfo.Encounter = new List<EnemyNPC>
                        {
                            new EnemyNPC(
                                GameInfo.Bestiary.Find(
                                npc => npc.CharName.Equals("Гигантский огр")),
                                3
                                ),
                            new EnemyNPC(
                                GameInfo.Bestiary.Find(
                                npc => npc.CharName.Equals("Гоблин-лучник")),
                                3
                                ),
                            new EnemyNPC(
                                GameInfo.Bestiary.Find(
                                npc => npc.CharName.Equals("Гоблин-лучник")),
                                2
                                ),
                            new EnemyNPC(
                                GameInfo.Bestiary.Find(
                                npc => npc.CharName.Equals("Шаман орков")),
                                3
                                )
                        };
                        break;
                    }
            }
        }

        public void btnRollDie_Enabling()
        {
            btnRollDie.BeginInvoke((MethodInvoker)delegate ()
            {
                btnRollDie.Enabled = true;
            }
           );
        }

        // Обработчик события нажатия кнопки "бросить кубик".

        private void btnRollDie_Click(object sender, EventArgs e)
        {
            /* Проверяется, был ли игроком сделан выбор
               способности и мишени. */

            cbChooseAbility.Focus();
            string AbName = cbChooseAbility.SelectedText;

            if (AbName.Equals(""))
                return;

            cbChooseTarget.Focus();
            string TargetName = cbChooseTarget.SelectedText;

            if (TargetName.Equals(""))
                return;

            Ability CTCAb = GameInfo.UsedAbility = GameInfo.CurrentTurnChar.CI.CharClassInfo
                .ClassAbilities.Find(ab => ab.AbName == AbName);

            GameInfo.CurrentTurnTarget = CTCAb
                .GetCharactersOnWhichCanBeUsed()[cbChooseTarget.SelectedIndex];

            // Выполняется бросок кубика.

            GameInfo.d6DieRollResult = Dice.d6DieRoll();

            GameInfo.GameState = GameStates.DieHasBeenRolled;
        }

        /* Вывод имени персонажа на текущем ходу
           на пользовательский интерфейс. */

        public void GUI_FillCharName()
        {
            txtbCharName.BeginInvoke((MethodInvoker)delegate ()
            {
                txtbCharName.Text = GameInfo.CurrentTurnChar.CI.CharName;
            }
            );
        }

        /* Вывод информации о персонаже на текущем ходу
           на пользовательский интерфейс. */

        public void GUI_FillCharInfo()
        {
            lstvCharInfo.BeginInvoke((MethodInvoker)delegate ()
            {
                lstvCharInfo.Items.Clear();

                string[] strParamsNames, strParamsValues;

                ((PlayerChar)GameInfo.CurrentTurnChar).
                GetCharInfoForGUI(out strParamsNames, out strParamsValues);

                for (int i = 0; i < strParamsNames.Length; i++)
                {
                    lstvCharInfo.Items.Add(new ListViewItem
                    (new string[] { strParamsNames[i], strParamsValues[i] }));
                }
            });
        }

        /* Очистка комбобоксов выбора способности и мишени
           и спискового элемента с информацией о мишени. */

        public void GUI_Clear()
        {
            cbChooseAbility.BeginInvoke((MethodInvoker)delegate ()
            {
                cbChooseAbility.Focus();
                cbChooseAbility.SelectedText = "";
            });

            cbChooseTarget.BeginInvoke((MethodInvoker)delegate ()
            {
                cbChooseTarget.Items.Clear();
                cbChooseTarget.Focus();
                cbChooseTarget.SelectedText = "";
            });

            lstvTargetInfo.BeginInvoke((MethodInvoker)delegate ()
            {
                lstvTargetInfo.Items.Clear();
            });
        }

            /* Вывод списка способностей персонажа на текущем ходу
               на пользовательский интерфейс. */

            public void GUI_FillCharAbilities()
        {
            cbChooseAbility.BeginInvoke((MethodInvoker)delegate ()
            {
                cbChooseAbility.Items.Clear();

                foreach (Ability Ab in
                    GameInfo.CurrentTurnChar.CI.CharClassInfo.ClassAbilities)
                {
                    cbChooseAbility.Items.Add("'" + Ab.AbName + "' : " + Ab.GetAbilityText());
                }
            });
        }

        /* Вывод доступных мишеней (в зависимости от типа способности)
           на пользовательский интерфейс. */

        public void GUI_FillAbilityTargets()
        {
            cbChooseTarget.BeginInvoke((MethodInvoker)delegate ()
            {
                cbChooseTarget.Items.Clear();
                cbChooseTarget.Focus();
                cbChooseTarget.SelectedText = "";

                Ability CTCAb = GameInfo.UsedAbility;

                cbChooseTarget.Items
                .AddRange(CTCAb.GetCharactersOnWhichCanBeUsed()
                          .Select(chr => chr.CI.CharName).ToArray()
                         );
            });
        }

        /* Вывод информации о текущей мишени
           на пользовательский интерфейс. */

        public void GUI_FillTargetInfo()
        {
            lstvTargetInfo.BeginInvoke((MethodInvoker)delegate ()
            {
                lstvTargetInfo.Items.Clear();

                string[] strParamsNames, strParamsValues;

                if (GameInfo.CurrentTurnTarget is PlayerChar)
                    ((PlayerChar)GameInfo.CurrentTurnTarget).
                    GetCharInfoForGUI(out strParamsNames, out strParamsValues);

                else
                    ((EnemyNPC)GameInfo.CurrentTurnTarget).
                    GetCharInfoForGUI(out strParamsNames, out strParamsValues);

                for (int i = 0; i < strParamsNames.Length; i++)
                {
                    lstvTargetInfo.Items.Add(new ListViewItem
                    (new string[] { strParamsNames[i], strParamsValues[i] }));
                }
            });
        }

        // Метод вывода в чат сообщения.

        public void ChatWriteln(string str)
        {
            lstbChat.BeginInvoke((MethodInvoker)delegate ()
            {
                Graphics g = lstbChat.CreateGraphics();
                SizeF sf = g.MeasureString(str, lstbChat.Font);
                float chatWidth = lstbChat.Width;

                if (sf.Width > lstbChat.Width)
                {
                    List<string> strsChat = str.Split(' ').ToList();
                    string strChat, strBuf, strStart = "";

                    while (strsChat.Count > 0)
                    {
                        strChat = strStart;
                        strBuf = strChat;
                        int i = 0;
                        do
                        {
                            if (i == strsChat.Count)
                                break;
                            strBuf = strChat;
                            strChat += strsChat[i] + " ";
                            sf = g.MeasureString(strChat, lstbChat.Font);
                            i++;
                        }
                        while (sf.Width < chatWidth);

                        if (sf.Width < chatWidth)
                            lstbChat.Items.Add(strChat);
                        else
                        {
                            // Ограничение по числу символов строки.

                            if (strChat.Length > 255)
                                try
                                { strChat = strChat.Remove(256, strChat.Length - 256); }
                                finally {; }

                            sf = g.MeasureString(strChat, lstbChat.Font);
                            string word = "";
                            float oneCharLen = g.MeasureString("-", lstbChat.Font).Width;

                            while (strChat.Length > 0)
                            {
                                if (sf.Width < chatWidth)
                                {
                                    if (i == strsChat.Count)
                                        lstbChat.Items.Add(strChat);
                                    else
                                        strStart = strChat;
                                    break;
                                }

                                int lastWordInd = strChat.LastIndexOf(" ") + 1;
                                float wLen = lastWordInd == 0 ?
                                      strChat.Length / 2 : lastWordInd,
                                      wLenInc = strChat.Length / 2;

                                word = strChat.Substring(0, (int)Math.Floor(wLen));
                                sf = g.MeasureString(word, lstbChat.Font);

                                bool wsfwNLE;
                                while ((wsfwNLE = sf.Width > chatWidth) ||
                                       chatWidth - sf.Width > oneCharLen)
                                {
                                    wLenInc /= 2;

                                    wLen += wsfwNLE ? -wLenInc : wLenInc;

                                    word = strChat.Substring(0, (int)Math.Floor(wLen));
                                    if (wLenInc < 1)
                                        break;
                                    sf = g.MeasureString(word, lstbChat.Font);
                                }

                                strChat = strChat.Remove(0, word.Length);
                                sf = g.MeasureString(strChat, lstbChat.Font);
                                bool wsp = word.Last() == ' ';
                                if (strChat.Length == 0)
                                    strStart = wsp ? word : word += ' ';
                                else if (!wsp)
                                    word += "-";
                                else
                                    strChat = strChat.Remove(strChat.Length - 1);
                                lstbChat.Items.Add(word);
                            }
                        }

                        strsChat.RemoveRange(0, i);
                    }
                }
                else
                    lstbChat.Items.Add(str);
            }
            );
        }

        public void cbChooseAbilitySelectedItem_Changed(object sender, EventArgs e)
        {
            // Получение применяемой персонажем способности.

            cbChooseAbility.Focus();
            string AbName = cbChooseAbility.SelectedText.Split("'".ToCharArray())
                .Skip(1).First();

            GameInfo.UsedAbility = GameInfo.CurrentTurnChar.CI.CharClassInfo
                .ClassAbilities.Find(ab => ab.AbName == AbName);

            /* Заполнение комбобокса с выбором мишени. */

            GUI_FillAbilityTargets();

            lstvTargetInfo.Items.Clear();
        }

        public void cbChooseTargetSelectedItem_Changed(object sender, EventArgs e)
        {
            // Получение выбранной игроком способности персонажа.

            Ability CTCAb = GameInfo.UsedAbility;

            // Получение цели персонажа.

            cbChooseTarget.Focus();
            int TargetIndex = cbChooseTarget.SelectedIndex;

            /* Если персонаж использовал способность поддержки, то
               его целью может быть и союзник, и противник. 
               Иначе персонаж использовал атакующую способность
               на противнике. */

            if (CTCAb is AbEffect)
            {
                if (((AbEffect)CTCAb).EffectType == EffectTypes.BUFF)
                    GameInfo.CurrentTurnTarget = GameInfo.PlayersPartyLiving[TargetIndex];
                else
                    GameInfo.CurrentTurnTarget = GameInfo.EncounterLiving[TargetIndex];
            }

            else
                GameInfo.CurrentTurnTarget = GameInfo.EncounterLiving[TargetIndex];

            /* Вывод информации о выбранной мишени 
               на пользовательский интерфейс. */

            GUI_FillTargetInfo();
        }

        private void btnShowParty_Click(object sender, EventArgs e)
        {
            PlayersPartyList = new frmCharList();
            PlayersPartyList.SetCharList<PlayerChar>(GameInfo.PlayersParty);

            PlayersPartyList.ShowDialog();
        }

        private void btnShowEncounter_Click(object sender, EventArgs e)
        {
            EncounterList = new frmCharList();
            EncounterList.SetCharList<EnemyNPC>(GameInfo.Encounter);

            EncounterList.ShowDialog();
        }
    }
}

