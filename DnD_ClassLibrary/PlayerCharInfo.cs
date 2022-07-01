using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Класс, вмещающий информацию о классе персонажа игрока.
    /// </summary>

    public class PlayerCharInfo : CharInfo
    {
        // Накопленный персонажем опыт на данном уровне.

        private int currentEXP;

        public int CurrentEXP
        {
            get { return currentEXP; }
            set
            {
                int maxLVL_value = 0;

                foreach (ParamInfo cpi in CharClassInfo.ClassParamsInfo)
                {
                    if (cpi.CharParam == CharParams.maxLVL)
                    {
                        maxLVL_value = cpi.ParamValue;
                        break;
                    }
                }

                /* Если персонаж не достиг максимального уровня и
                   не накопил достаточно опыта, то текущий опыт увеличивается.
                   Если персонаж не достиг максимального уровня и
                   накопил достаточно опыта, то его уровень повышается, а 
                   накопленный опыт сбрасывается, а также вызывается 
                   событие повышения уровня. */

                if (CharLVL < maxLVL_value)
                {
                    if (currentEXP + value < EXPForNewLVL[CharLVL + 1])
                        currentEXP += value;
                    else
                    {
                        /* Остаток полученного опыта. */

                        int EXPWithNewLVL = value -
                            (EXPForNewLVL[CharLVL + 1] - currentEXP);

                        CharLVL++;

                        currentEXP = 0;

                        // Вызывается событие повышения уровня.

                        OnLVLUp(CharLVL, this);

                        /* Рекурсивный вызов функции сеттера.
                           Опыт, даваемый монстром, может превышать
                           необходимое для достижения нового уровня
                           количество опыта. */

                        CurrentEXP = EXPWithNewLVL;
                    }
                }
            }
        }

        /* Массив значений опыта, необходимых персонажу 
           для перехода на следующий уровень. */

        public static int[] EXPForNewLVL = { 0, 10, 15, 20 };

        // Конструктор класса.

        public PlayerCharInfo(string cName, ClassInfo pCharClassInfo)
                : base(cName, pCharClassInfo)
        {
            CurrentEXP = 0;
            CharLVL = 0;

            /* При повышении уровня в чат выводится сообщении о получении
               персонажем нового уровня. */

            LVLUpEvent += (newLVL, ci) =>
            {
                string str = "Персонаж " + CharName + " получил уровень "
                             + newLVL.ToString() + "!";
                GameInfo.ChatWriteln(str);
            };

            // При повышении уровня проверяются новые доступные навыки.

            LVLUpEvent += CheckForNewClassAbilities;
        }

        // Метод клонирования объекта.

        public override object Clone()
        {
            PlayerCharInfo ci = new PlayerCharInfo(this.CharName, (ClassInfo)this.CharClassInfo.Clone());
            return ci;
        }
    }
}
