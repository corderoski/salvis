using Salvis.DataLayer.Repositories;
using Salvis.Entities;
using System;
using System.Collections.Generic;

namespace Salvis.Framework.Services
{
    public class TipService : ITipService
    {

        private readonly ITipRepository _tipRepository;

        public TipService(ITipRepository tipRepository)
        {
            _tipRepository = tipRepository;
        }

        public void Dispose()
        {
            _tipRepository.Dispose();
        }

        public Tip Add(Tip item)
        {
            return _tipRepository.Add(item);
        }

        public IEnumerable<Tip> GetRandom(int maxItem)
        {
            return _tipRepository.GetRandomItemsByQuantity(maxItem);
        }

        public Tip Get(int id)
        {
            return Get((long)id);
        }

        public Tip Get(long id)
        {
            return _tipRepository.Get(id);
        }

        public IEnumerable<Tip> Get()
        {
            return _tipRepository.Get();
        }
    }
}
