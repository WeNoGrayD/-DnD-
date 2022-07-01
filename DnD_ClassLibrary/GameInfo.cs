using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DnD_ClassLibrary
{
    // Перечисление состояний игры.

    public enum GameStates
    {
        MoveStart,
        DieHasBeenRolled,
        EndGame
    }

    // Перечисление результатов хода.

    public enum MoveResults
    {
        Standart, // Стандартный результат: способность направлена на цель.
        Summon
    }


    /// <summary>
    /// Класс, вмещающий почти всю информацию об игре.
    /// </summary>

    public class GameInfo
    {
        // Окно чата.

        static public ListBox lstbChat;

        // Окно с информацией о персонаже.

        static public ListView lstvCharInfo;

        // Комбобоксы выбора способности и мишени.

        static public ComboBox cbChooseAbility, cbchooseTarget;

        // Надпись с именем персонажа.

        static public TextBox txtbCharName;

        /* Кнопки готовности сделать ход, показать лист игроков,
           показать лист врагов. */

        static public Button btnRollDie, btnShowParty, btnShowEncounter;

        // Семафор для запуска потока начала игры.

        static public Semaphore SemGameStart;

        // Список реализованных классов персонажей.

        public static List<ClassInfo> ClassesList;

        // Список персонажей игроков.

        static public List<PlayerChar> PlayersParty;

        // Список живых персонажей игроков.

        static public List<PlayerChar> PlayersPartyLiving;

        // Список видов вражеского ИИ.

        static public List<EnemyAI> EnemyAIList;

        // Список видов врагов.

        static public List<NPCInfo> Bestiary;

        // Список врагов в сражении.

        static public List<EnemyNPC> Encounter;

        // Список живых врагов в сражении.

        static public List<EnemyNPC> EncounterLiving;

        // Текущее состояние игры.

        public static GameStates GameState;

        // Персонаж на текущем ходу.

        public static Character CurrentTurnChar;

        // Используемая текущим персонажем способность.

        public static Ability UsedAbility;

        // Цель персонажа на текущем ходу.

        public static Character CurrentTurnTarget;

        // Результат броска кубика.

        public static int d6DieRollResult;

        // Делегат события заполнения пользовательского интерфейса данными.

        public delegate void GUIWritelnEventHandler();

        // Событие заполнения пользовательского интерфейса данными.

        static public event GUIWritelnEventHandler GUIWritelnEvent;

        // Метод вывода нового сообщения в чат.

        static public void GUIWriteln()
        {
            GUIWritelnEvent();
        }

        // Делегат события вывода нового сообщения в чат.

        public delegate void ChatWritelnEventHandler(string str);

        // Событие вывода нового сообщения в чат.

        static public event ChatWritelnEventHandler ChatWritelnEvent;

        // Метод вывода нового сообщения в чат.

        static public void ChatWriteln(string str)
        {
            ChatWritelnEvent?.Invoke(str);
        }

        // Делегат для включения кнопки броска кубика.

        public delegate void BtnRollDieEnablingEventHandler();

        static public event BtnRollDieEnablingEventHandler BtnRollDieEnablingEvent;

        static public void BtnRollDieEnabling()
        {
            BtnRollDieEnablingEvent?.Invoke();
        }
    }
}
