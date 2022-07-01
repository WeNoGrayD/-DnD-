using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_ClassLibrary
{
    /// <summary>
    /// Класс, являющий собой список персонажей какой-либо группы.
    /// Может использоваться для хранения дополнительной информации 
    /// о группе, например, об общем инвентаре или отношениях
    /// между членами группы.
    /// </summary>
    class CharsContainer<T> : IEnumerable<T>
        where T : Character
    {
        // Список персонажей группы.

        private List<T> characters;

        public int Count { get { return characters.Count; } }
        public CharsContainer(params T[] characters)
        {
            this.characters = characters.ToList();
        }

        public CharsContainer(List<T> characters)
        {
            this.characters = characters;

            CharsContainer<EnemyNPC> npcs = new CharsContainer<EnemyNPC>();
            CharsContainer<Character> chrs = new CharsContainer<Character>();
            chrs = npcs;
            CharsContainer<PlayerChar> plrs = new CharsContainer<PlayerChar>();
            plrs = (CharsContainer<PlayerChar>)chrs;
        }

        public T this[int i]
        {
            get
            {
                return this.characters[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < characters.Count(); i++)
                yield return characters[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /* 
           Явное преобразование группы абстрактных персонажей
           к группе конкретных.
        */

        static public explicit operator CharsContainer<T>(CharsContainer<Character> chars)
        {
            List<T> inheritorsChars = new List<T>();

            foreach (Character chr in chars.Where(c => c is T))
                inheritorsChars.Add((T)chr); 

            return new CharsContainer<T>(inheritorsChars);
        }

        /* 
           Неявное преобразование группы конкретных персонажей (например, игроков)
           к группе абстрактных.
        */

        static public implicit operator CharsContainer<Character>(CharsContainer<T> chars)
        {
            List<Character> ancestorsChars = new List<Character>();

            foreach (T chr in chars)
                ancestorsChars.Add(chr);

            return new CharsContainer<Character>(ancestorsChars);
        }
    }
}
