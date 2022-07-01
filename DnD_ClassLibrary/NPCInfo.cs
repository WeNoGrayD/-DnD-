using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Класс, вмещающий информацию о классе неигрового персонажа-врага.
    /// </summary>

    public class NPCInfo : CharInfo
    {
        // ИИ врага.

        public EnemyAI AI;

        // Конструктор класса.

        public NPCInfo(string cName, ClassInfo charClassInfo, EnemyAI ai, int storedEXP)
                : base(cName, charClassInfo)
        {
            /* Каждый монстр - это не только ценный мех, но и также
               целый килограмм опыта. */

            CharParamsInfo.Add(new ParamInfo(CharParams.storedEXP, storedEXP));
            CharClassInfo.ClassParamsIncInfo.Add(new ParamInfo(CharParams.storedEXP, 5));

            // При повышении уровня проверяются новые доступные навыки.

            LVLUpEvent += CheckForNewClassAbilities;

            // Присваивание искусственного идиота вражескому ИИ.

            AI = (EnemyAI)ai.Clone();
        }

        // Метод клонирования объекта.

        public override object Clone()
        {
            NPCInfo ci = new NPCInfo(
                this.CharName, 
                (ClassInfo)this.CharClassInfo.Clone(), 
                this.AI, 
                this.CharParamsInfo
                .Find(p => p.CharParam == CharParams.storedEXP).ParamValue
                );
            return ci;
        }
    }
}
