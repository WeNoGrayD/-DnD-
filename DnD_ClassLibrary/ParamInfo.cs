using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    // Перечисление параметров персонажа.

    public enum CharParams
    {
        maxLVL,
        physDMG,
        magDMG,
        HP,
        maxHP,
        physDEF,
        magDEF,
        storedEXP
    }


    /// <summary>
    /// Информация о параметре персонажа.
    /// </summary>

    public class ParamInfo : ICloneable
    {
        // Названия параметров.

        static public Dictionary<string, CharParams> paramNamesEng { get; } =
            new Dictionary<string, CharParams>
        {
            { "HP", CharParams.HP },
            { "MagDEF", CharParams.magDEF },
            { "MagDMG", CharParams.magDMG },
            { "MaxHP", CharParams.maxHP },
            { "MaxLVL", CharParams.maxLVL },
            { "PhysDEF", CharParams.physDEF },
            { "PhysDMG", CharParams.physDMG },
            { "StoredEXP", CharParams.storedEXP }
        };

        static public Dictionary<CharParams, string> paramNamesRus { get; } =
            new Dictionary<CharParams, string>
        {
            { CharParams.HP, "ОЗ" },
            { CharParams.magDEF, "Маг. ЗЩТ" },
            { CharParams.magDMG, "Маг. АТК" },
            { CharParams.maxHP, "Макс. ОЗ" },
            { CharParams.maxLVL, "Макс. УР" },
            { CharParams.physDEF, "Физ. ЗЩТ" },
            { CharParams.physDMG, "Физ. АТК" },
            { CharParams.storedEXP, "ОО" }
        };

        // Свойство, содержащее текущее значение параметра.

        private int paramValue;

        public int ParamValue {
            get { return paramValue; }
            set
            {
                if (value <= 0)
                    paramValue = 0;
                else
                    paramValue = value;
            }
        }

        // Поле, содержащее обозначение параметра.

        public CharParams CharParam { get; private set; }

        // Конструктор класса.

        public ParamInfo(CharParams charParam, int paramValue)
        {
            CharParam = charParam;
            ParamValue = paramValue;
        }

        // Метод выдачи названия параметра.

        public string GetParamName()
        {
            return paramNamesRus[CharParam];
        }

        // Метод клонирования объекта.

        public object Clone()
        {
            return (ParamInfo)MemberwiseClone();
        }
    }
}
