using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Класс, вмещающий всю информацию об абстрактном персонаже.
    /// </summary>

    abstract public class Character
    {
        // Свойство хранит информацию о персонаже.

        public CharInfo CI { get; protected set;  }

        // Конструктор класса.

        public Character(CharInfo cI) { }

        /* Метод для сбора информации по параметрам
           персонажа для пользовательского интерфейса. */

        public void GetCharInfoForGUI(out string[] strParamsNames,
                                      out string[] strParamsValues)
        {
            Dictionary<string, string>  StrParams = new Dictionary<string, string>();

            int paramsCount = CI.CharParamsInfo.Count;

            StrParams.Add("Статус", CI.IsAlive ? "Жив" : "Мёртв" );
            StrParams.Add("Класс", CI.CharClassInfo.ClassNameRus);

            int j = 0;
            for (int i = 0; i < paramsCount; i++, j++)
            {
                ParamInfo par = CI.CharParamsInfo[j];

                string parName = "", parValue = "";

                // Получение информации об уровне персонажа.

                if (par.CharParam == CharParams.maxLVL)
                {
                    parName = "УР"; 
                    parValue = CI.CharLVL.ToString() + @"/" + par.ParamValue.ToString();

                    /* Если выводятся данные персонажа,
                       то добавляется параметр "ОО" (очки опыта). */

                    if (CI is PlayerCharInfo)
                    {
                        paramsCount++;
                        i++;

                        string exp;
                        if (CI.CharLVL != par.ParamValue)
                        {
                            exp = ((PlayerCharInfo)CI).CurrentEXP.ToString() + @"/" + 
                                PlayerCharInfo.EXPForNewLVL[CI.CharLVL + 1].ToString();
                        }
                        else
                        {
                            exp = "макс. УР";
                        }

                        StrParams.Add("ОО", exp);
                    }
                }
                else
                {
                    // Получение информации о здоровье персонажа.

                    if (par.CharParam == CharParams.maxHP)
                    {
                        parName = "ОЗ";
                        parValue = CI.CurrentHP.ToString() + @"/" + par.ParamValue.ToString();
                    }

                    // Получение информации об остальных параметрах персонажа.

                    else if (par.CharParam != CharParams.storedEXP)
                    {
                        parName = par.GetParamName();
                        parValue = par.ParamValue.ToString();
                    }
                    else continue;
                }

                StrParams.Add(parName, parValue);
            }

            strParamsNames = StrParams.Keys.ToArray();
            strParamsValues = StrParams.Values.ToArray();
        }

        /* Метод для сбора информации о наложенных
           на персонажа эффектах. */

        public void GetCharEffectsForGUI(out string[] strEffectsNames, 
                                         out string[] strEffectsValues)
        {
            Dictionary<string, string> StrEffects = new Dictionary<string, string>();

            foreach(AbEffect abEffect in CI.Effects)
            {
                string efName = abEffect.AbName,
                       efValue = abEffect.GetAbilityText();

                StrEffects.Add(efName, efValue);
            }

            strEffectsNames = StrEffects.Keys.ToArray();
            strEffectsValues = StrEffects.Values.ToArray();
        }
    }
}
