using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SharpBullet.OAL
{
    public static class PersistenceStrategyProvider
    {
        private static PersistenceStrategy defaultStrategy = new BasicPersistence();
        private static Hashtable strategies = new Hashtable();

        public static void AddStrategy(string key, PersistenceStrategy strategy)
        {
            strategies[key] = strategy;
        }

        public static PersistenceStrategy FindStrategyFor(Type type)
        {
            foreach (PersistenceStrategy strategy in strategies.Values)
            {
                if (strategy.CanPersist(type)) return strategy;
            }

            return defaultStrategy;
        }
    }
}
