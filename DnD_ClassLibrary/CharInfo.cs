using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Класс, вмещающий информацию о персонаже.
    /// </summary>

    public class CharInfo : ICloneable
    {
        // Имя персонажа.

        public string CharName { get; private set; }

        // Уровень персонажа.

        public int CharLVL { get; set; }

        // Текущее здоровье персонажа.

        private int currentHP;

        public int CurrentHP
        {
            get { return currentHP; }
            set
            {
                {
                    if (value > 0)
                    {
                        int maxHP = CharParamsInfo.Find
                            (cpi => cpi.CharParam == CharParams.maxHP).ParamValue;
                        if (value < maxHP)
                            currentHP = value;
                        else
                            currentHP = maxHP;
                    }
                    else
                    {
                        currentHP = 0;

                        // Установка флага "персонаж жив" в false.

                        IsAlive = false;
                    }
                }
            }
        }

        /* Флаг, сообщающий состояние персонажа 
           (жив == true/мёртв == false). */

        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
            set
            {
                isAlive = value;

                // При смерти с персонажа снимаются эффекты.

                if (!value)
                    Effects?.Clear();
            }
        }

        // Информация о классе персонажа.

        public ClassInfo CharClassInfo { get; private set; }

        // Информация о воздействующих на персонажа эффектах.

        public List<AbEffect> Effects;

        // Информация о параметрах персонажа.

        public List<ParamInfo> CharParamsInfo;

        /* Метод подписки данного персонажа на событие
           класса DungeonMaster "указана мишень". */

        public void OnTargetHasBeenChosen()
        {
            /* Если персонаж был выбран мишенью другого игрока,
               то вычисляется нанесённый персонажа урон или
               на персонажа налагается эффект. */

            DungeonMaster.TargetHasBeenChosenEvent += GettingACuff;
        }

        /* Метод отписки данного персонажа от события
           класса DungeonMaster "указана мишень". */

        public void OffTargetHasBeenChosen()
        {
            /* Если персонаж был выбран мишенью другого игрока,
               то вычисляется нанесённый персонажа урон или
               на персонажа налагается эффект. */

            DungeonMaster.TargetHasBeenChosenEvent -= GettingACuff;
        }

        // Делегат события повышения уровня.

        public delegate void LVLUpEventHandler(int newLVL, CharInfo ci);

        // Событие повышения уровня.

        public event LVLUpEventHandler LVLUpEvent;

        // Метод вызова события повышения уровня.

        public void OnLVLUp(int newLVL, CharInfo ci)
        {
            LVLUpEvent?.Invoke(newLVL, ci);
        }

        // Метод подписки персонажа на событие проверки наложенных эффектов.

        public void OnCheckEffects()
        {
            DungeonMaster.CheckEffectsEvent += CheckEffects;
        }

        // Метод отписки персонажа от события проверки наложенных эффектов.

        public void OffCheckEffects()
        {
            DungeonMaster.CheckEffectsEvent -= CheckEffects;
        }

        // Конструктор класса.

        public CharInfo(string cName, ClassInfo charClassInfo)
        {
            CharName = cName;
            CharClassInfo = (ClassInfo)charClassInfo.Clone();

            /* Инициализация параметров персонажа 
               (стартовые значения - параметры, определённые классом). */

            CharParamsInfo = new List<ParamInfo>();

            foreach (ParamInfo par in CharClassInfo.ClassParamsInfo)
            {
                CharParamsInfo.Add((ParamInfo)par.Clone());
            }

            CurrentHP = CharParamsInfo.Find(cpi => cpi.CharParam == CharParams.maxHP).ParamValue;

            // При повышении уровня увеличиваются параметры персонажа.

            LVLUpEvent += IncreaseCharParams;
        }

        // Проверка на наличие новых доступных по уровную классовых навыков.

        public void CheckForNewClassAbilities(int newLVL, CharInfo ci)
        {
            /* В список способностей добавляются классовые способности,
               доступные на новом уровне. */

            ClassInfo ClI = GameInfo.ClassesList
                .Find(cl => cl.ClassNameRus.Equals(CharClassInfo.ClassNameRus));

            foreach (Ability a in ClI.ClassAbilities.Where(a => a.RequiredLVL == newLVL))
            {
                /* Добавление к параметрам способности 
                   параметров персонажа. */

                Ability Ab = (Ability)a.Clone();

                if (Ab is AbAttack)
                {
                    foreach (ParamInfo par in ci.CharParamsInfo.Where
                    (cpi =>
                    cpi.CharParam == CharParams.physDMG ||
                    cpi.CharParam == CharParams.magDMG))
                    {
                        if (par.CharParam == Ab.CharParam)
                        {
                            ((AbAttack)Ab).DMG += par.ParamValue;
                            break;
                        }
                    }
                }

                ci.CharClassInfo.ClassAbilities.Add(Ab);

                /* В окно чата выводится сообщение о получении персонажем
                   новой способности. */

                string str = "Персонаж ";
                str += CharName + " получил способность " + Ab.AbName + " !";
                GameInfo.ChatWriteln(str);
            }
        }

        // Увеличение параметров персонажа при получении нового уровня.

        public void IncreaseCharParams(int newLVL, CharInfo ci)
        {
            /* Поиск по увеличивающимся параметрам персонажа. 
                   Значения прироста находятся в экземпляре класса ClassInfo. */

            foreach (ParamInfo CPII in ci.CharClassInfo.ClassParamsIncInfo)
            {
                ParamInfo PCP = ci.CharParamsInfo.Find(p => p.CharParam == CPII.CharParam);
                PCP.ParamValue += CPII.ParamValue;
            }

            // Восстановление здоровья до максимального при повышении уровня.

            ci.CurrentHP = ci.CharParamsInfo.Find
                (cpi => cpi.CharParam == CharParams.maxHP).ParamValue;
        }

        /* Обработчик события выбора другим игроком данного персонажа
           в качестве мишени. */

        public void GettingACuff()
        {
            Ability Ab = GameInfo.UsedAbility;

            // Если персонаж был атакован, то вычисляется урон от удара. 

            if (Ab is AbAttack)
            {
                /* Выбор значения защиты соответствующего типа атакуемого персонажа
                   и урона от атакующей способности. */

                int contraDEF = 0;

                if (Ab.CharParam == CharParams.magDMG)
                    contraDEF = CharParamsInfo.Find((c) => c.CharParam == CharParams.magDEF).ParamValue;
                else
                    contraDEF = CharParamsInfo.Find((c) => c.CharParam == CharParams.physDEF).ParamValue;

                // К начальному значению урона добавляется результат броска кубика.

                int AbDMG = ((AbAttack)Ab).DMG + GameInfo.d6DieRollResult;

                // Вычисление действительного значения полученного урона.

                int EffectiveDMG = AbDMG - contraDEF;

                if (EffectiveDMG < 0)
                    EffectiveDMG = 0;

                /* Вычисление оставшегося числа ОЗ у персонажа 
                   и проверка на смертельность удара 
                   (проверка внутри свойства). */

                CurrentHP -= EffectiveDMG;
            }

            // Иначе на персонажа был наложен эффект.

            else
            {
                AbEffect Effect = (AbEffect)Ab.Clone();

                // К начальному значению параметра добавляется результат броска кубика.

                if (Effect.EffectType == EffectTypes.BUFF)
                    Effect.EffectValue += GameInfo.d6DieRollResult;
                else
                    Effect.EffectValue -= GameInfo.d6DieRollResult;

                /* В список эффектов, действующих на персонажа,
                   заносится наложенный эффект. */

                bool F = false;

                if (Effect.CountOfMoves > 1)
                {
                    string EffectName = Effect.AbName;

                    foreach (AbEffect ef in Effects)
                    {
                        /* Если таковой эффект уже был наложен,
                           то восстанавливается изначальное кол-во
                           ходов, которые он будет дейстовать. */ 

                        if (ef.AbName.Equals(EffectName))
                        {
                            F = true;
                            ef.CountOfMoves = Effect.CountOfMoves;
                            break;
                        }
                    }
                    
                    // Иначе эффект добавляется в список действующих эффектов.

                    if (!F)
                    {
                        Effects.Add(Effect);
                    }
                }

                // Срабатывание эффекта.

                if (!F)
                {
                    TriggeringEffect(Effect);
                }
            }
        }

        /* Метод, обслуживающий список действующих
           на персонажа эффектов: декремент
           оставшихся ходов у каждой способности,
           очистка списка от способностей с нулевых количеством
           ходов, выполнение действий повторяемых эффектов. */

        public void CheckEffects()
        {
            List<AbEffect> Wasted = new List<AbEffect>();

            foreach (AbEffect effect in Effects)
            {
                effect.CountOfMoves--;

                if (effect.CountOfMoves == 0)
                {
                    /* Восстановление изначального значения параметра. */

                    if (!effect.IsCumulative)
                    {
                        ParamInfo par = CharParamsInfo.Find(cpi => cpi.CharParam == effect.CharParam);
                        par.ParamValue -= effect.EffectValue;
                    }

                    Wasted.Add(effect);
                }

                else
                {
                    if (effect.IsCumulative)
                        TriggeringEffect(effect);
                }
            }

            Effects.RemoveAll(e => Wasted.Contains(e));
        }

        // Метод срабатывания эффекта.

        public void TriggeringEffect(AbEffect effect)
        {
            if (effect.CharParam == CharParams.HP)
                CurrentHP += effect.EffectValue;
            else
            {
                foreach (ParamInfo par in CharParamsInfo)
                {
                    if (par.CharParam == effect.CharParam)
                    {
                        par.ParamValue += effect.EffectValue;
                        break;
                    }
                }
            }
        }

        // Метод клонирования объекта.

        public virtual object Clone()
        {
            CharInfo ci = new CharInfo(this.CharName, (ClassInfo)this.CharClassInfo.Clone());
            return ci;
        }
    }
}
