using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DnD_ClassLibrary
{
    /* Класс, берущий на себя обязанности
       руководителя организационным процессом
       битвы между игроками и компьютером. */

    public static class DungeonMaster
    {
        // Делегат события выбора персонажа мишенью.

        public delegate void TargetHasBeenChosenEventHandler();

        /* Событие выбора другим игроком данного персонажа
           в качестве мишени. */

        public static event TargetHasBeenChosenEventHandler TargetHasBeenChosenEvent;

        // Делегат события проверки наложенных эффектов.

        public delegate void CheckEffectsEventHandler();

        // Событие проверки наложенных эффектов.

        public static event CheckEffectsEventHandler CheckEffectsEvent;

        // Метод запуска игры.

        public static void StartGame()
        {
            GameInfo.SemGameStart.WaitOne();

            /* Переменная, сообщающая о том, что в обеих командах 
               присутствуют живые участники. */

            bool IsThereAnyoneAlive = true;

            // Перепись живых участников побоища.

            GameInfo.PlayersPartyLiving = new List<PlayerChar>(GameInfo.PlayersParty);

            GameInfo.EncounterLiving = new List<EnemyNPC>(GameInfo.Encounter);

            // Список участвующих в битве лиц.

            List<Character> DramatisPersonae = new List<Character>(GameInfo.PlayersParty);
            DramatisPersonae.AddRange(GameInfo.Encounter);

            /* Игра продолжается, пока в каждой команде присутствует
               хотя бы по одному живому участнику. */

            while (IsThereAnyoneAlive)
            {
                bool EveryoneInPlayersPartyAreDead = false,
                     EveryoneInEncounterAreDead = false;

                /*
                bool twasSummonery = false;
                List<EnemyNPC> summoned = null;
                */

                // Проход по действущим лицам.

                foreach (Character c in DramatisPersonae.
                    Where(c => c.CI.IsAlive))
                {
                    // Состояние игры: начало хода.

                    GameInfo.GameState = GameStates.MoveStart;

                    // Персонаж на текущем ходу.

                    GameInfo.CurrentTurnChar = c;

                    // Вывод в чат строки с именем персонажа на текущем ходу.

                    string strStartMove;

                    // Если ход персонажа игрока.

                    if (GameInfo.CurrentTurnChar is PlayerChar)
                    {
                        strStartMove = "Отважный и благородный " +
                        GameInfo.CurrentTurnChar.CI.CharName + " оценивает позиции противника.";

                        GameInfo.ChatWriteln(strStartMove);

                        // Вывод информации на пользовательский интерфейс.

                        GameInfo.GUIWriteln();

                        /* Если персонаж на текущем ходу является
                           персонажем игрока, то требуется подождать его хода,
                           иначе свой ход совершает персонаж-враг,
                           управляемый компьютером. */

                        GameInfo.BtnRollDieEnabling();

                        // Ожидание совершения игроком выбора.

                        WaitingForADieRoll();

                        string TargetName = GameInfo.CurrentTurnTarget.CI.CharName;

                        // Вывод в чат сообщения о ходе персонажа игрока.

                        string str = "Исполненный героизма " + GameInfo.CurrentTurnChar.CI.CharName;

                        if (GameInfo.UsedAbility is AbEffect)
                        {
                            if (((AbEffect)GameInfo.UsedAbility).EffectType is EffectTypes.BUFF)
                            {
                                str += " оказывает дружескую поддержку " + TargetName +
                                    " своей способностью " + GameInfo.UsedAbility.AbName +
                                    "!";
                            }
                            else
                            {
                                str += " устремляет свой праведный гнев на " + TargetName +
                                    " и использует свой стенд " + GameInfo.UsedAbility.AbName +
                                    "!";
                            }
                        }

                        else
                        {
                            str += " заносит своё оружие над " + TargetName +
                                " и применяет " + GameInfo.UsedAbility.AbName +
                                "!";
                        }

                        GameInfo.ChatWriteln(str);
                    }

                    // Если ход противника.

                    else
                    {
                        strStartMove = "Коварный и мерзкий " +
                        GameInfo.CurrentTurnChar.CI.CharName + " решил учудить очередную пакость.";

                        GameInfo.ChatWriteln(strStartMove);

                        ((NPCInfo)GameInfo.CurrentTurnChar.CI).AI.GetMoveResult();

                        string TargetName = GameInfo.CurrentTurnTarget.CI.CharName;

                        // Вывод в чат сообщения о ходе персонажа игрока.

                        string str = "Источающий смрад " + GameInfo.CurrentTurnChar.CI.CharName;

                        if (GameInfo.UsedAbility is AbEffect)
                        {
                            if (((AbEffect)GameInfo.UsedAbility).EffectType is EffectTypes.BUFF)
                            {
                                str += " совершает кровавый ритуал во имя демонических дьяволов " +
                                       "под названием " + GameInfo.UsedAbility.AbName +
                                       " и направляет его эффект на " + TargetName + "!";
                            }
                            else
                            {
                                str += " тянет свои грязные лапы к " + TargetName +
                                    " и пытается использовать " + GameInfo.UsedAbility.AbName +
                                    "!";
                            }
                        }

                        else
                        {
                            str += " тычет своим грязным и ржавым оружием в мужественное лицо " + 
                                TargetName + " и применяет " + 
                                GameInfo.UsedAbility.AbName + "!";
                        }

                        GameInfo.ChatWriteln(str);

                        // Выполняется бросок кубика.

                        GameInfo.d6DieRollResult = Dice.d6DieRoll();
                    }

                    string strDieRoll = GameInfo.CurrentTurnChar.CI.CharName +
                           " бросает кубик d6. Выпало " +
                           GameInfo.d6DieRollResult.ToString() + ".";

                    GameInfo.ChatWriteln(strDieRoll);

                    // Если призыв, то способность работает по другим правилам.

                    /*
                    if (GameInfo.UsedAbility is AbSummonery)
                    {
                        bool isCurrentCharAPlayer = GameInfo.CurrentTurnChar is PlayerChar;


                        if (!isCurrentCharAPlayer)
                        {
                            twasSummonery = true;
                            summoned = ((AbSummonery)GameInfo.UsedAbility)
                                .SummoningChars;
                            GameInfo.Encounter
                                .AddRange(summoned);
                        }
                    }
                    else
                    {
                        
                    }
                    */

                    /* Вызов метода подписки мишени на событие
                               "указана мишень". */

                    GameInfo.CurrentTurnTarget.CI.OnTargetHasBeenChosen();

                    // Вызов события "указана мишень". 

                    TargetHasBeenChosenEvent?.Invoke();

                    /* Вызов метода отписки мишени от события
                       "указана мишень". */

                    GameInfo.CurrentTurnTarget.CI.OffTargetHasBeenChosen();

                    /* Если после совершённого персонажем на текущем ходу
                       действия его мишень умерла, то необходимо сделать
                       перепись выживших и надбавить опыта персонажу,
                       если это персонаж игрока. */

                    if (!GameInfo.CurrentTurnTarget.CI.IsAlive)
                    {
                        if (GameInfo.CurrentTurnChar is PlayerChar)
                        {
                            GameInfo.ChatWriteln("Сын собаки " +
                                GameInfo.CurrentTurnTarget.CI.CharName +
                                " жил как собака и умер как собака." +
                                " Да славится имя твоё, " 
                                + GameInfo.CurrentTurnChar.CI.CharName + "!");

                            /* Из списка живущих монстров изымается 
                               дохлая собака. */

                            GameInfo.EncounterLiving.
                                Remove((EnemyNPC)GameInfo.CurrentTurnTarget);

                            // Надбавка персонажу опыта за вредность.

                            int storedEXP = ((NPCInfo)GameInfo.CurrentTurnTarget.CI).
                                CharParamsInfo.Find(cpi => cpi.CharParam == CharParams.storedEXP).
                                ParamValue;

                            ((PlayerCharInfo)GameInfo.CurrentTurnChar.CI).CurrentEXP += storedEXP;

                            EveryoneInEncounterAreDead =
                                GameInfo.EncounterLiving.Count == 0;

                            if (EveryoneInEncounterAreDead)
                                break;
                        }

                        else
                        {
                            GameInfo.ChatWriteln("Доблестный и благородный " +
                                GameInfo.CurrentTurnTarget.CI.CharName +
                                " погиб в бесславном бою от грязной руки " +
                                GameInfo.CurrentTurnChar.CI.CharName + "...");

                            /* Из списка живущих игроков изымается погибший
                               в бесславном бою. */

                            GameInfo.PlayersPartyLiving.
                                Remove((PlayerChar)GameInfo.CurrentTurnTarget);

                            EveryoneInPlayersPartyAreDead =
                                GameInfo.PlayersPartyLiving.Count == 0;

                            if (EveryoneInPlayersPartyAreDead)
                                break;
                        }
                    }

                    /* Обнуление ссылок на текущих персонажа, 
                       использованную способность и мишень. */

                    GameInfo.CurrentTurnChar = null;
                    GameInfo.UsedAbility = null;
                    GameInfo.CurrentTurnTarget = null;
                }

                /*
                if (twasSummonery && summoned.Exists(s => s.CI.IsAlive))
                    DramatisPersonae.AddRange(summoned.Where(s => s.CI.IsAlive));
                    */

                // Вывод в чат сообщения о победе игроков.

                if (EveryoneInPlayersPartyAreDead)
                {
                    // Вывод в чат сообщения о проигрыше игроков.

                    GameInfo.ChatWriteln("Наши доблестные герои были разбиты" +
                                             " полчищами дикарей и разбойников...");
                }
                
                else
                {
                    // Вывод в чат сообщения о победе игроков.

                    if (EveryoneInEncounterAreDead)
                        GameInfo.ChatWriteln("Негодяи, мерзавцы и подонки были разгромлены" +
                                         " отрядом наших доблестных героев!");
                }

                IsThereAnyoneAlive = !EveryoneInPlayersPartyAreDead &&
                    !EveryoneInEncounterAreDead;

                if (IsThereAnyoneAlive)
                {
                    /* Из списка действующих лиц изымаются
                       погибшие на поле боя. */

                    DramatisPersonae.RemoveAll(c => !c.CI.IsAlive);

                    /* Подписка всех действующих лиц на событие 
                       обработки наложенных эффектов. */

                    foreach (Character c in DramatisPersonae)
                        c.CI.OnCheckEffects();

                    /* Вызов события обработки наложенных эффектов. */

                    CheckEffectsEvent();

                    /* Отписка всех действующих лиц от события 
                       обработки наложенных эффектов. */

                    foreach (Character c in DramatisPersonae)
                        c.CI.OffCheckEffects();
                }

                else
                {
                    GameInfo.GameState = GameStates.EndGame;
                    break;
                }
            }
        }

        /* Метод, выполняющий ожидание работы игры того момента,
           когда игрок выберет способность и цель и нажмёт на кнопку
           готовности сделать ход. */

        public static void WaitingForADieRoll()
        {
            while (GameInfo.GameState != GameStates.DieHasBeenRolled)
                ;
        }
    }
}
