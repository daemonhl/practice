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
        public void Add(NegativeWord entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public NegativeWord Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NegativeWord> GetAll()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(NegativeWord entity)
        {
            throw new NotImplementedException();
        }
    }
}
