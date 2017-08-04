using GroupM.Content.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupM.Content.Entities;

namespace GroupM.Content.Persistence
{
    public class StaticUserTextsRepository : IUserTextsRepository
    {
        private static Dictionary<int, UserText> texts;
        private static int lastId = 1;

        public StaticUserTextsRepository()
        {
            texts = new Dictionary<int, UserText>() {
                { 1, new UserText() { Id = 1, Text = "The weather in London in August is bad. Is like winter, horrible" } }
            };
        }

        public void Add(UserText entity)
        {
            entity.Id = ++lastId;

            texts.Add(entity.Id, entity);
        }

        public void Delete(int id)
        {
            if (texts.ContainsKey(id))
            {
                texts[id] = null;
            }
        }

        public UserText Get(int id)
        {
            return texts.ContainsKey(id) ? texts[id] : null;
        }

        public IEnumerable<UserText> GetAll()
        {
            return texts.Values;
        }

        public void SaveChanges()
        {
            // nothing to do
        }

        public void Update(UserText entity)
        {
            if (texts.ContainsKey(entity.Id))
            {
                texts[entity.Id] = entity;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
