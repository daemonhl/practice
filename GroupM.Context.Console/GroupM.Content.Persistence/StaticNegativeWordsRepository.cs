using GroupM.Content.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupM.Content.Entities;

namespace GroupM.Content.Persistence
{
    public class StaticNegativeWordsRepository : INegativeWordsRepository
    {
        private static Dictionary<int, NegativeWord> words;
        private static int lastId = 0;

        public StaticNegativeWordsRepository()
        {
            var badWords = new List<string> { "horrible", "nasty", "bad", "swine" };
            words = badWords.ToDictionary(key => badWords.IndexOf(key), value => new NegativeWord { Id = badWords.IndexOf(value), Text = value });
            lastId = badWords.Count - 1;
        }

        public void Add(NegativeWord entity)
        {
            entity.Id = ++lastId;

            words.Add(entity.Id, entity);
        }

        public void Delete(int id)
        {
            if (words.ContainsKey(id))
            {
                words[id] = null;
            }
        }

        public NegativeWord Get(int id)
        {
            return words[id];
        }

        public IEnumerable<NegativeWord> GetAll()
        {
            return words.Values;
        }

        public void SaveChanges()
        {
            // nothing to do
        }

        public void Update(NegativeWord entity)
        {
            if (words.ContainsKey(entity.Id))
            {
                words[entity.Id] = entity;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
