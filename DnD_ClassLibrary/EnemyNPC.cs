using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Класс, вмещающий информацию о персонаже врага.
    /// </summary>

    public class EnemyNPC : Character, ICloneable
    {
        // Конструктор класса.

        public EnemyNPC(NPCInfo enemyCharInfo, int lvl)
            : base(enemyCharInfo)
        {
            CI = (NPCInfo)enemyCharInfo.Clone();

            // Персонаж скорее жив, чем мёртв.

            CI.IsAlive = true;

            // Список наложенных эффектов изначально пуст. 

            CI.Effects = new List<AbEffect>();

            /* Получение значения максимального уровня
               на случай ошибки ввода уровня монстра. ECI.CharLVL = lvl;*/

            int maxLVL_value = 0;

            foreach (ParamInfo cpi in CI.CharClassInfo.ClassParamsInfo)
            {
                if (cpi.CharParam == CharParams.maxLVL)
                {
                    maxLVL_value = cpi.ParamValue;
                    break;
                }
            }

            if (lvl < maxLVL_value)
                CI.CharLVL = lvl;
            else
            {
                CI.CharLVL = maxLVL_value;
                lvl = maxLVL_value;
            }

            /* Наращивание параметров противника
               до соответствующих его уровню. */

            for (int i = 1; i <= lvl; i++)
                ((NPCInfo)CI).OnLVLUp(i, CI);
        }

        // Метод клонирования объекта.

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
