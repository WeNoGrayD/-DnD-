using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Класс, содержащий всю информацию о персонаже игрока.
    /// </summary>

    public class PlayerChar : Character
    {
        // Конструктор класса.

        public PlayerChar(PlayerCharInfo pCharInfo)
            : base (pCharInfo)
        {
            CI = (PlayerCharInfo)pCharInfo.Clone();

            // Персонаж скорее жив, чем мёртв.

            CI.IsAlive = true;

            // Список наложенных эффектов изначально пуст. 

            CI.Effects = new List<AbEffect>();
        }
    }
}
