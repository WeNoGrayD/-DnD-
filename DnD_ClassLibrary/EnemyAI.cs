using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Класс, содержащий поведение враждебного персонажа.
    /// </summary>
    public class EnemyAI : ICloneable
    {
        /* Название вида ИИ. */

        public string AINameRus;

        /* Метод выборки следующей способности. */

        public delegate void GetMoveResultHandler 
            ();

        public GetMoveResultHandler GetMoveResult;

        // Конструктор класса.

        public EnemyAI(string aiNameRus, 
                       GetMoveResultHandler getMoveResult)
        {
            AINameRus = aiNameRus;
            GetMoveResult = getMoveResult;
        }

        // Метод клонирования объекта.

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
