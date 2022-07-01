using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    ///  Игровые библиотеки классов, ИИ противников и списка монстров.
    /// </summary>
    public class GameLibraries
    {
        public GameLibraries()
        {
            CreateClassesList();
            CreateNPCsAi();
            CreateBestiary();
        }

        private static void CreateClassesList()
        {
            GameInfo.ClassesList = new List<ClassInfo>
            {
                // Класс "лучник".

                new ClassInfo
                (
                "Лучник",
                new List<Ability>
                {
                    new AbAttack("Шквал стрел", 0, CharParams.physDMG, 4),
                    new AbAttack("Град стрел", 1, CharParams.physDMG, 10),
                    new AbEffect("Прикрытие", 0, CharParams.physDEF, 8, false, true, 2)
                },
                new List<ParamInfo>
                {
                    new ParamInfo(CharParams.maxLVL, 3),
                    new ParamInfo(CharParams.maxHP, 14),
                    new ParamInfo(CharParams.physDEF, 3),
                    new ParamInfo(CharParams.magDEF, 1),
                    new ParamInfo(CharParams.physDMG, 6)
                },
                new List<ParamInfo>
                {
                    new ParamInfo(CharParams.maxHP, 2),
                    new ParamInfo(CharParams.physDEF, 2),
                    new ParamInfo(CharParams.magDEF, 1),
                    new ParamInfo(CharParams.physDMG, 2)
                }
                ),

                // Класс "маг".

                new ClassInfo
                (
                "Маг",
                new List<Ability>
                {
                    new AbAttack("Тычок посохом", 0, CharParams.physDMG, 1),
                    new AbAttack("Файербол", 0, CharParams.magDMG, 5),
                    new AbAttack("Большой файербол", 1, CharParams.magDMG, 10),
                    new AbEffect("Магическая поддержка", 0, CharParams.HP, 3, true, false, 2),
                    new AbEffect("Наведённая порча", 2, CharParams.HP, -5, true, false, 2),
                    new AbEffect("Магический заслон", 2, CharParams.magDEF, 8, false, true, 3),
                    new AbEffect("Благословение небес", 3, CharParams.HP, 10, false, false, 1)
                },
                new List<ParamInfo>
                {
                    new ParamInfo(CharParams.maxLVL, 3),
                    new ParamInfo(CharParams.maxHP, 12),
                    new ParamInfo(CharParams.physDEF, 2),
                    new ParamInfo(CharParams.magDEF, 5),
                    new ParamInfo(CharParams.physDMG, 1),
                    new ParamInfo(CharParams.magDMG, 4)
                },
                new List<ParamInfo>
                {
                    new ParamInfo(CharParams.maxHP, 1),
                    new ParamInfo(CharParams.physDEF, 2),
                    new ParamInfo(CharParams.magDEF, 4),
                    new ParamInfo(CharParams.physDMG, 1),
                    new ParamInfo(CharParams.magDMG, 8)
                }
                ),

                // Класс "воин".

                new ClassInfo
                (
                "Воин",
                new List<Ability>
                {
                    new AbAttack("Удар с размаху", 0, CharParams.physDMG, 6),
                    new AbAttack("Крушащий черепа удар", 1, CharParams.physDMG, 14),
                    new AbEffect("Проникающий удар", 0, CharParams.physDEF, -7, false, true, 3)
                },
                new List<ParamInfo>
                {
                    new ParamInfo(CharParams.maxLVL, 3),
                    new ParamInfo(CharParams.maxHP, 20),
                    new ParamInfo(CharParams.physDEF, 7),
                    new ParamInfo(CharParams.magDEF, 1),
                    new ParamInfo(CharParams.physDMG, 10)
                },
                new List<ParamInfo>
                {
                    new ParamInfo(CharParams.maxHP, 2),
                    new ParamInfo(CharParams.physDEF, 2),
                    new ParamInfo(CharParams.magDEF, 1),
                    new ParamInfo(CharParams.physDMG, 2)
                }
                )
            };
        }

        private static void CreateNPCsAi()
        {
            GameInfo.EnemyAIList = new List<EnemyAI>
            {
                new EnemyAI("Лучник",
                            () =>
                            {
                                 /* Проход по противникам. */

                                 List<Ability> Confront = new List<Ability>
                                 (GameInfo.CurrentTurnChar.CI.CharClassInfo.ClassAbilities
                                 .Where((cAb) => cAb is AbAttack ||
                                 ((cAb is AbEffect) && (((AbEffect)cAb).EffectType == EffectTypes.DEBUFF))));

                                 /* Список приоритетов способностей, направленных
                                    на врагов:
                                    1 параметр - номер способности в списке Confront;
                                    2 параметр - приоритет (0 - малый, 1 - высокий);
                                    3 параметр - цель способности. */

                                 List<Tuple<Ability, int, PlayerChar>> confrontPriorities =
                                 new List<Tuple<Ability, int, PlayerChar>>();

                                 foreach (Ability cAb in Confront)
                                 {
                                     foreach (PlayerChar player in GameInfo.PlayersPartyLiving)
                                     {
                                         /* Проверка потребности в пробитии защиты. */

                                         if (cAb is AbEffect)
                                         {
                                             confrontPriorities
                                             .Add(Tuple.Create(cAb, 0, player));
                                         }

                                         /* Проверка потребности в тумаках. */

                                         else
                                         {
                                             int cPhysDEF = player.CI.CharParamsInfo
                                                 .Find((p) =>
                                                 p.CharParam == CharParams.physDEF).ParamValue;

                                             int DMG = ((AbAttack)cAb).DMG;
                                             int effectiveDMG = cPhysDEF - DMG;

                                             if (effectiveDMG >= DMG / 6)
                                             {
                                                  confrontPriorities
                                                  .Add(Tuple.Create(cAb, 1, player));
                                             }

                                             else if (effectiveDMG > 0)
                                             {
                                                  confrontPriorities
                                                  .Add(Tuple.Create(cAb, 0, player));
                                             }
                                         }
                                     }
                                 }

                                 /* Проверка приоритетов. */

                                 List<Tuple<Ability, int, PlayerChar>> cHighPR =
                                 new List<Tuple<Ability, int, PlayerChar>>
                                 (confrontPriorities.Where((PR) => PR.Item2 == 1));

                                 if (cHighPR.Count > 0)
                                 {
                                     Random prRand = new Random();
                                     int notSoLucky = prRand.Next(0, cHighPR.Count - 1);

                                     GameInfo.CurrentTurnTarget =
                                     cHighPR[notSoLucky].Item3;
                                     GameInfo.UsedAbility = cHighPR[notSoLucky].Item1;
                                     return;
                                 }

                                 List<Tuple<Ability, int, PlayerChar>> cLowPR =
                                 new List<Tuple<Ability, int, PlayerChar>>
                                 (confrontPriorities.Where((PR) => PR.Item2 == 0));

                                 if (cLowPR.Count > 0)
                                 {
                                     Random prRand = new Random();
                                     int notSoLucky = prRand.Next(0, cLowPR.Count - 1);

                                     GameInfo.CurrentTurnTarget =
                                     cLowPR[notSoLucky].Item3;
                                     GameInfo.UsedAbility = cLowPR[notSoLucky].Item1;
                                     return;
                                 }
                             }
                            ),
                new EnemyAI("Маг",
                            () =>
                             {
                                NPCInfo ECI = (NPCInfo)GameInfo.CurrentTurnChar.CI;

                                List<AbEffect> Buffs = new List<AbEffect>();

                                foreach (AbEffect AbE in ECI.CharClassInfo.ClassAbilities
                                        .Where((Ab) => (Ab is AbEffect) &&
                                        ((AbEffect)Ab).EffectType == EffectTypes.BUFF))
                                {
                                    // Сеанс самолечения.

                                    if (AbE.CharParam == CharParams.HP)
                                    {
                                        int maxHP = ECI.CharParamsInfo.Find((p) => p.CharParam == CharParams.maxHP).ParamValue;

                                        if ((ECI.CurrentHP < maxHP / 4 && !AbE.IsCumulative) ||
                                            (ECI.CurrentHP < maxHP / 2 && AbE.IsCumulative))
                                        {
                                            GameInfo.CurrentTurnTarget =
                                            GameInfo.CurrentTurnChar;
                                            GameInfo.UsedAbility = AbE;
                                            return;
                                        }
                                    }

                                    Buffs.Add(AbE);
                                }

                                /* Список приоритетов "хороших" эффектов:
                                   1 параметр - номер эффекта в списке Buffs;
                                   2 параметр - приоритет (0 - малый, 1 - высокий);
                                   3 параметр - цель эффекта. */

                                List<Tuple<AbEffect, int, EnemyNPC>> buffsPriorities =
                                new List<Tuple<AbEffect, int, EnemyNPC>>();

                                foreach (AbEffect AbE in Buffs)
                                {                   
                                     // Акт помощи соратникам.

                                     foreach(EnemyNPC npc in GameInfo.EncounterLiving
                                             .Where((c) => c != GameInfo.CurrentTurnChar))
                                     {
                                          // Проверка потребности в лечении.

                                          if (AbE.CharParam == CharParams.HP)
                                          {
                                             int maxHP = npc.CI.CharParamsInfo.Find((p) => p.CharParam == CharParams.maxHP).ParamValue;

                                             if (npc.CI.CurrentHP < maxHP / 4 && !AbE.IsCumulative)
                                             {
                                                 buffsPriorities.Add(Tuple.Create(AbE, 1, npc));
                                             }

                                             else if (npc.CI.CurrentHP < maxHP / 2 && AbE.IsCumulative)
                                             {
                                                 buffsPriorities.Add(Tuple.Create(AbE, 0, npc));
                                             }
                                          }

                                          /* Если лечение не требуется,
                                             то необходимость использования эффекта невысокая. */

                                          else
                                          {
                                              buffsPriorities.Add(Tuple.Create(AbE, 0, npc));
                                          }
                                     }
                                }
                                
                                /* Проверка приоритетов. */

                                List<Tuple<AbEffect, int, EnemyNPC>> fHighPR =
                                new List<Tuple<AbEffect, int, EnemyNPC>>
                                (buffsPriorities.Where((PR) => PR.Item2 == 1));

                                if (fHighPR.Count > 0)
                                {
                                    Random prRand = new Random();
                                    int lucky = prRand.Next(0, fHighPR.Count - 1);

                                    GameInfo.CurrentTurnTarget =
                                    fHighPR[lucky].Item3;
                                    GameInfo.UsedAbility = fHighPR[lucky].Item1;
                                    return;
                                }

                                List<Tuple<AbEffect, int, EnemyNPC>> fLowPR =
                                new List<Tuple<AbEffect, int, EnemyNPC>>
                                (buffsPriorities.Where((PR) => PR.Item2 == 0));

                                if (fLowPR.Count > 0)
                                {
                                    Random prRand = new Random();
                                    int lucky = prRand.Next(0, fLowPR.Count - 1);

                                    GameInfo.CurrentTurnTarget =
                                    fLowPR[lucky].Item3;
                                    GameInfo.UsedAbility = fLowPR[lucky].Item1;
                                    return;
                                }

                                 /* Проход по противникам. */

                                 List<Ability> Confront = new List<Ability>
                                 (GameInfo.CurrentTurnChar.CI.CharClassInfo.ClassAbilities
                                 .Where((cAb) => cAb is AbAttack ||
                                 ((cAb is AbEffect) && (((AbEffect)cAb).EffectType == EffectTypes.DEBUFF))));

                                 /* Список приоритетов способностей, направленных
                                    на врагов:
                                    1 параметр - номер способности в списке Confront;
                                    2 параметр - приоритет (0 - малый, 1 - высокий);
                                    3 параметр - цель способности. */

                                 List<Tuple<Ability, int, PlayerChar>> confrontPriorities =
                                 new List<Tuple<Ability, int, PlayerChar>>();

                                 foreach (Ability cAb in Confront)
                                 {
                                     foreach (PlayerChar player in GameInfo.PlayersPartyLiving)
                                     {
                                         /* Проверка потребности в наведении сглаза. */

                                         if (cAb is AbEffect)
                                         {
                                             if (cAb.CharParam == CharParams.HP)
                                             {    confrontPriorities
                                                  .Add(Tuple.Create(cAb, 1, player));
                                             }

                                             else
                                             {
                                                  confrontPriorities
                                                  .Add(Tuple.Create(cAb, 0, player));
                                             }
                                         }

                                         /* Проверка потребности в тумаках. */

                                         else
                                         {
                                             int DMG = 0;
                                             int effectiveDMG = 0;

                                             if (((AbAttack)cAb).CharParam
                                             == CharParams.magDMG)
                                             {
                                                 int cMagDEF = player.CI.CharParamsInfo
                                                 .Find((p) =>
                                                 p.CharParam == CharParams.magDEF).ParamValue;

                                                 DMG = ((AbAttack)cAb).DMG;
                                                 effectiveDMG = cMagDEF - DMG;
                                             }

                                             else if (((AbAttack)cAb).CharParam
                                             == CharParams.physDMG)
                                             {
                                                 int cPhysDEF = player.CI.CharParamsInfo
                                                 .Find((p) =>
                                                 p.CharParam == CharParams.physDEF).ParamValue;

                                                 DMG = ((AbAttack)cAb).DMG;
                                                 effectiveDMG = cPhysDEF - DMG;
                                             }

                                             if (effectiveDMG >= DMG / 6)
                                             {
                                                  confrontPriorities
                                                  .Add(Tuple.Create(cAb, 1, player));
                                             }

                                             else if (effectiveDMG > 0)
                                             {
                                                  confrontPriorities
                                                  .Add(Tuple.Create(cAb, 0, player));
                                             }
                                         }
                                     }
                                 }

                                 /* Проверка приоритетов. */

                                 List<Tuple<Ability, int, PlayerChar>> cHighPR =
                                 new List<Tuple<Ability, int, PlayerChar>>
                                 (confrontPriorities.Where((PR) => PR.Item2 == 1));

                                 if (cHighPR.Count > 0)
                                 {
                                     Random prRand = new Random();
                                     int notSoLucky = prRand.Next(0, cHighPR.Count - 1);

                                     GameInfo.CurrentTurnTarget =
                                     cHighPR[notSoLucky].Item3;
                                     GameInfo.UsedAbility = cHighPR[notSoLucky].Item1;
                                     return;
                                 }

                                 List<Tuple<Ability, int, PlayerChar>> cLowPR =
                                 new List<Tuple<Ability, int, PlayerChar>>
                                 (confrontPriorities.Where((PR) => PR.Item2 == 0));
 
                                 if (cLowPR.Count > 0)
                                 {
                                     Random prRand = new Random();
                                     int notSoLucky = prRand.Next(0, cLowPR.Count - 1);

                                     GameInfo.CurrentTurnTarget =
                                     cLowPR[notSoLucky].Item3;
                                     GameInfo.UsedAbility = cLowPR[notSoLucky].Item1;
                                     return;
                                 }
                             }
                            ),
                new EnemyAI("Воин",
                            () => 
                            {
                                 /* Проход по противникам. */

                                 List<Ability> Confront = new List<Ability>
                                 (GameInfo.CurrentTurnChar.CI.CharClassInfo.ClassAbilities
                                 .Where((cAb) => cAb is AbAttack ||
                                 ((cAb is AbEffect) && (((AbEffect)cAb).EffectType == EffectTypes.DEBUFF))));

                                 /* Список приоритетов способностей, направленных
                                    на врагов:
                                    1 параметр - номер способности в списке Confront;
                                    2 параметр - приоритет (0 - малый, 1 - высокий);
                                    3 параметр - цель способности. */

                                 List<Tuple<Ability, int, PlayerChar>> confrontPriorities =
                                 new List<Tuple<Ability, int, PlayerChar>>();

                                 foreach (Ability cAb in Confront)
                                 {
                                     foreach (PlayerChar player in GameInfo.PlayersPartyLiving)
                                     {
                                         /* Проверка потребности в пробитии защиты. */

                                         if (cAb is AbEffect)
                                         {
                                             confrontPriorities
                                             .Add(Tuple.Create(cAb, 0, player));
                                         }

                                         /* Проверка потребности в тумаках. */

                                         else
                                         {
                                             int cPhysDEF = player.CI.CharParamsInfo
                                                 .Find((p) =>
                                                 p.CharParam == CharParams.physDEF).ParamValue;

                                             int DMG = ((AbAttack)cAb).DMG;
                                             int effectiveDMG = cPhysDEF - DMG;

                                             if (effectiveDMG >= DMG / 6)
                                             {
                                                  confrontPriorities
                                                  .Add(Tuple.Create(cAb, 1, player));
                                             }

                                             else if (effectiveDMG > 0)
                                             {
                                                  confrontPriorities
                                                  .Add(Tuple.Create(cAb, 0, player));
                                             }
                                         }
                                     }
                                 }

                                 /* Проверка приоритетов. */

                                 List<Tuple<Ability, int, PlayerChar>> cHighPR =
                                 new List<Tuple<Ability, int, PlayerChar>>
                                 (confrontPriorities.Where((PR) => PR.Item2 == 1));

                                 if (cHighPR.Count > 0)
                                 {
                                     Random prRand = new Random();
                                     int notSoLucky = prRand.Next(0, cHighPR.Count - 1);

                                     GameInfo.CurrentTurnTarget =
                                     cHighPR[notSoLucky].Item3;
                                     GameInfo.UsedAbility = cHighPR[notSoLucky].Item1;
                                     return;
                                 }

                                 List<Tuple<Ability, int, PlayerChar>> cLowPR =
                                 new List<Tuple<Ability, int, PlayerChar>>
                                 (confrontPriorities.Where((PR) => PR.Item2 == 0));

                                 if (cLowPR.Count > 0)
                                 {
                                     Random prRand = new Random();
                                     int notSoLucky = prRand.Next(0, cLowPR.Count - 1);

                                     GameInfo.CurrentTurnTarget =
                                     cLowPR[notSoLucky].Item3;
                                     GameInfo.UsedAbility = cLowPR[notSoLucky].Item1;
                                     return;
                                 }
                             }
                            )
            }; 
        }

        private static void CreateBestiary()
        {
            ClassInfo clArcher = GameInfo.ClassesList.Find(
                cl => cl.ClassNameRus.Equals("Лучник"));
            ClassInfo clMage = GameInfo.ClassesList.Find(
                cl => cl.ClassNameRus.Equals("Маг"));
            ClassInfo clWarrior = GameInfo.ClassesList.Find(
                cl => cl.ClassNameRus.Equals("Воин"));

            EnemyAI aiArcher = GameInfo.EnemyAIList.Find(
                ai => ai.AINameRus.Equals("Лучник"));
            EnemyAI aiMage = GameInfo.EnemyAIList.Find(
                ai => ai.AINameRus.Equals("Маг"));
            EnemyAI aiWarrior = GameInfo.EnemyAIList.Find(
                ai => ai.AINameRus.Equals("Воин"));

            GameInfo.Bestiary = new List<NPCInfo>
            {
                new NPCInfo("Гигантский огр", clWarrior, aiWarrior, 20),
                new NPCInfo("Гоблин-лучник", clArcher, aiArcher, 15),
                new NPCInfo("Шаман орков", clMage, aiMage, 25),
                new NPCInfo("Скелет-лучник", clArcher, aiArcher, 5),
                new NPCInfo("Скелет-воин", clWarrior, aiWarrior, 10),
                new NPCInfo("Лич-некромант", clMage, aiMage, 15)
            };
        }
    }
}
