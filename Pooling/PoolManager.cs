using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.Pooling
{
    public class PoolManager
    {
        private Dictionary<string, Pool> pools;
        private static PoolManager instance;

        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static PoolManager Instance
		{
			get
			{
                if (instance == null) { 
                    instance = new PoolManager();
                }
                return instance;
            }
			set
			{
                instance = value;
			}
          }

		public PoolManager()
		{
            pools = new Dictionary<string, Pool>();
        }

        /// <summary>
        /// Register a new pool
        /// </summary>
        /// <param name="poolID"></param>
        /// <param name="factory"></param>
        /// <returns>The registered pool</returns>
       
        public Pool RegisterPool(string poolID, FactoryFunction factory)
        {
            if (!pools.ContainsKey(poolID))
            {
                Pool p = new Pool(factory);
                pools.Add(poolID, p);
                return p;
            }
            else
                return pools[poolID];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poolID"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when a pool with specified poolID does not exsist</exception>
        public GameObject GetObject(string poolID)
        {
            if (pools.ContainsKey(poolID) )
                return pools[poolID].GetObject();
            throw new System.ArgumentOutOfRangeException("Pool does not exsist");
        }
        
        // Pool Utilities
        public Pool GetPool(string poolID)
        {
            if (pools.ContainsKey(poolID))
                return pools[poolID];
            return null;
        }
        public bool IsPoolRegistered(string poolID)
        {
            return pools.ContainsKey(poolID);
        }
    }
}
