using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Pooling
{
    public delegate GameObject FactoryFunction();

    /// <summary>
    /// Gameobject Pool
    /// </summary>
    public class Pool
	{
        private FactoryFunction factory;
        private List<GameObject> pooledObjects;

        public Pool(FactoryFunction _objectToPool)
        {
            this.factory = _objectToPool;
            pooledObjects = new List<GameObject>();
        }

        public GameObject GetObject()
		{
			for (int i = 0; i < pooledObjects.Count; i++)
			{
                if (pooledObjects[i].Destroyed)
				{
                    pooledObjects[i].ResetInternalState();
                    return pooledObjects[i];
                }
            }
            pooledObjects.Add(factory.Invoke());
            return pooledObjects.Last();
        }
    }
}
