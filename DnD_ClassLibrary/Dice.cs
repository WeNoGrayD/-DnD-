using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Класс, определяющий броски кубиков.
    /// </summary>
    public class Dice
    {
        static public int d6DieRoll()
        {
            Random d6DieRoll = new Random();

            return d6DieRoll.Next(1, 6);
        }
    }
}
