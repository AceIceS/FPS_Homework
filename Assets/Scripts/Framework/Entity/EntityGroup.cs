using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Framework
{

    // a set of same entity
    public class EntityGroup : IEnumerable
    {
        public string EntityGroupName
        {
            get
            {
                return mEntityGroupName;
            }
        }

        private string mEntityGroupName;
            
        public List<Entity> Entities
        {
            get
            {
                return mEntities;
            }
        }
        
        private List<Entity> mEntities;
        
        public EntityGroup(string entityGroupName)
        {
            mEntityGroupName = entityGroupName;
            mEntities = new List<Entity>();
        }

        public void AddNewEntity(Entity entity)
        {
            mEntities.Add(entity);
        }

        public void DeleteEntity(Entity e)
        {
            if (mEntities != null)
            {
                for(int i = 0;i < mEntities.Count;++i)
                {
                    if (ReferenceEquals(e, mEntities[i]))
                    {
                        mEntities.Remove(mEntities[i]);
                        break;
                    }
                }
            }
        }
        
        // iterator
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < mEntities.Count; ++i)
            {
                yield return mEntities[i];
            }
        }
        
        
    }

}
