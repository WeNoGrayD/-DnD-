using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Абстрактный класс для способностей персонажей.
    /// </summary>

    public abstract class Ability : ICloneable
    {
        // Название способности.

        public string AbName;

        // Параметр, на который воздействует способность.

        public CharParams CharParam { get; private set; }

        // Требуемый для способности уровень персонажа.

        public int RequiredLVL;

        // Конструктор класса.

        public Ability(string abName, int requiredLVL, CharParams charParam)
        {
            AbName = abName;
            RequiredLVL = requiredLVL;
            CharParam = charParam;
        }

        // Метод выдачи списка персонажей, на которых действует способность.

        public abstract List<Character> GetCharactersOnWhichCanBeUsed();

        // Метод использования способности.

        public abstract void UseAbility();

        // Метод получения текста способности.

        public abstract string GetAbilityText();

        // Метод клонирования объекта.

        public object Clone()
        {
            return (Ability)MemberwiseClone();
        }
    }

    /// <summary>
    /// Атакующая способность.
    /// </summary>

    public class AbAttack : Ability
    {
        // Наносимый атакой урон.

        public int DMG { get; set; }

        // Конструктор класса.

        public AbAttack(string abName, int requiredLVL, CharParams charParam, int dmg)
            : base(abName, requiredLVL, charParam)
        {
            DMG = dmg;
        }

        // Метод выдачи списка персонажей, на которых действует способность.

        public override List<Character> GetCharactersOnWhichCanBeUsed()
        {

            return (List<Character>)GameInfo.EncounterLiving;
        }

        // Метод использования способности.

        public override void UseAbility()
        {
            throw new NotImplementedException();

        }

        // Метод получения текста способности.

        public override string GetAbilityText()
        {
            string atValue = this.DMG.ToString() + ParamInfo.paramNamesRus[this.CharParam];
            return atValue;
        }
    }

    /// <summary>
    /// Атака по площади. Не реализована.
    /// </summary>

    public class AbAOEAttack : AbAttack
    {
        // Конструктор класса.
        public AbAOEAttack(string abName, int requiredLVL, CharParams charParam, int dmg)
            : base(abName, requiredLVL, charParam, dmg)
        {
            DMG = dmg;
        }

        // Метод использования способности.


        // Метод использования способности.

        public override void UseAbility()
        {
            throw new NotImplementedException();

        }

        public override string GetAbilityText()
        {
            string atValue = this.DMG.ToString() + " " +
                ParamInfo.paramNamesRus[this.CharParam];
            return atValue;
        }
    }

    // Перечисление видов эффектов.

    public enum EffectTypes
    {
        BUFF,
        DEBUFF
    }

    /// <summary>
    /// Способность, накладывающая эффект на цель.
    /// </summary>

    public class AbEffect : Ability
    {
        /* Свойство указывает значение единоразовой 
           добавки/отнятия от значения параметра, 
           на который воздействует эффект. */

        public int EffectValue { get; set; }
        
        // Свойство указывает, повторяется ли действие эффекта каждый ход.

        public bool IsCumulative { get; private set; }

        // Свойство указывает, временный ли это эффект.

        public bool IsTemporary { get; private set; }

        // Счётчик ходов, которые действует эффект.

        public int CountOfMoves { get; set; }

        // Тип налагаемого эффекта (полезный/вредный).

        public EffectTypes EffectType { get; private set; }

        // Конструктор класса.

        public AbEffect(string abName, int requiredLVL, CharParams charParam,
                      int eValue, bool isCumulative, bool isTemporary,
                      int moves)
            : base(abName, requiredLVL, charParam)
        {
            EffectValue = eValue;
            IsCumulative = isCumulative;
            IsTemporary = isTemporary;
            CountOfMoves = moves;

            if (EffectValue > 0)
                EffectType = EffectTypes.BUFF;
            else
                EffectType = EffectTypes.DEBUFF;
        }

        // Метод выдачи списка персонажей, на которых действует способность.

        public override List<Character> GetCharactersOnWhichCanBeUsed()
        {

        }

        public override void UseAbility()
        {
            throw new NotImplementedException();

        }

        // Метод использования способности.

        public override void UseAbility()
        {
            throw new NotImplementedException();

        }

        // Метод получения текста способности.

        public override string GetAbilityText()
        {
            int efInt = this.EffectValue;
            string efValue = efInt > 0 ? "+" + efInt.ToString() : efInt.ToString();
            efValue += " " + ParamInfo.paramNamesRus[this.CharParam] + 
                " (" + CountOfMoves + " х.)";

            return efValue;
        }
    }

    /// <summary>
    /// Способность, призывающая неигрового персонажа.
    /// </summary>
    
    public class AbSummonery : Ability
    {
        public List<EnemyNPC> SummoningChars;
        public AbSummonery(string abName, int requiredLVL, CharParams charParam,
                      List<EnemyNPC> summoning)
            : base(abName, requiredLVL, charParam)
        {
            SummoningChars = summoning;
        }

        // Метод выдачи списка персонажей, на которых действует способность.

        public override List<Character> GetCharactersOnWhichCanBeUsed()
        {

        }

        // Метод использования способности.

        public override void UseAbility()
        {
            throw new NotImplementedException();

        }

        // Метод получения текста способности.

        public override string GetAbilityText()
        {
            string smnValue = "Призыв: ";
            smnValue += SummoningChars[0] + 
                "(" + SummoningChars[0].CI.CharLVL.ToString() + " ур.)";
            foreach (EnemyNPC npc in SummoningChars.Skip(1))
                smnValue += ", " + npc +
                "(" + npc.CI.CharLVL.ToString() + " ур.)";
            return smnValue;
        }
    }
}
