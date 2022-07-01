using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Информация о классе персонажа.
    /// </summary>    

    public class ClassInfo : ICloneable
    {
        // Имя класса на русском языке.

        public string ClassNameRus { get; private set;  }

        // Список классовых способностей.

        public List<Ability> ClassAbilities;

        // Массив параметров персонажа (в зависимости от класса).

        public List<ParamInfo> ClassParamsInfo;

        // Массив прироста параметров персонажа (в зависиомсти от класса).

        public List<ParamInfo> ClassParamsIncInfo;

        /* Конструктор класса для инициализации
           списка классов персонажей. */

        public ClassInfo(string classNameRus, List<Ability> cAbilities,
                         List<ParamInfo> CPI, List<ParamInfo> CPII)
        {
            ClassNameRus = classNameRus;

            ClassAbilities = new List<Ability>();
            ClassAbilities.AddRange(cAbilities);

            ClassParamsInfo = new List<ParamInfo>();
            ClassParamsInfo.AddRange(CPI);

            ClassParamsIncInfo = new List<ParamInfo>();
            ClassParamsIncInfo.AddRange(CPII);
        }

        /* Просто конструктор. */

        public ClassInfo() { }

        // Метод клонирования объекта.

        public object Clone()
        {
            ClassInfo cli = new ClassInfo();

            cli.ClassNameRus = this.ClassNameRus;

            cli.ClassAbilities = new List<Ability>();
            foreach (Ability Ab in this.ClassAbilities.Where(ab => ab.RequiredLVL == 0))
            {
                cli.ClassAbilities.Add((Ability)Ab.Clone());
            }

            cli.ClassParamsInfo = new List<ParamInfo>();
            foreach (ParamInfo par in this.ClassParamsInfo)
            {
                cli.ClassParamsInfo.Add((ParamInfo)par.Clone());
            }

            cli.ClassParamsIncInfo = new List<ParamInfo>();
            foreach (ParamInfo par in this.ClassParamsIncInfo)
            {
                cli.ClassParamsIncInfo.Add((ParamInfo)par.Clone());
            }
            return cli;
        }
    }
}
