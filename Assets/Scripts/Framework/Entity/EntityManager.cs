using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace FPS_Homework_Framework
{

    public class EntityManager : BaseManager<EntityManager>, IGameFrameworkModule
    {
        private Dictionary<int, Entity> mEntityId2Entity;
        private Dictionary<string, EntityGroup> mEntityGroupName2EntityGroup;
        
        private List<Entity> mNewEntities;
        private int mEntityValidID;

        public void InitializeModuleBeforeOnStart()
        {
            mEntityId2Entity = new Dictionary<int, Entity>();
            mEntityGroupName2EntityGroup = new Dictionary<string, EntityGroup>();
            
            mNewEntities = new List<Entity>();
            
            mEntityValidID = 0;
        }
        
        public void UpdateModule()
        {
            foreach (var entity in mEntityId2Entity.Values)
            {
                if (entity.EntityStatus == EntityStatus.Active)
                {

                    entity.UpdateEntity();
                }
            }
        }

        public void FixUpdateModule()
        {
            foreach (var entity in mEntityId2Entity.Values)
            {
                if (entity.EntityStatus == EntityStatus.Active)
                {
                    entity.FixUpdateEntity();
                }
            }
        }

        public void LateUpdateModule()
        {
            foreach (var entity in mEntityId2Entity.Values)
            {
                if (entity.EntityStatus == EntityStatus.Active)
                {
                    entity.LateUpdateEntity();
                }

            }

            // active entity in next frame
            if (mNewEntities.Count > 0)
            {
                for (int i = 0; i < mNewEntities.Count; ++i)
                {
                    mEntityId2Entity.Add(mEntityValidID++, mNewEntities[i]);
                }
                mNewEntities.Clear();
            }
            
        }

        public Entity AddEntity<T>(string entityGroupName, Vector3 pos, Quaternion quat)
        {
            GameObject entity = ResourceManager.Instance.SpawnEntityInstanceCopy(entityGroupName, pos, quat);
            Entity entityComponent = (Entity)entity.AddComponent(typeof(T));
            mNewEntities.Add(entityComponent);
            // add or create entitygroup
            EntityGroup eg = null;
            if (mEntityGroupName2EntityGroup.TryGetValue(entityGroupName, out eg) == false)
            {
                eg = new EntityGroup(entityGroupName);
                mEntityGroupName2EntityGroup.Add(entityGroupName, eg);
            }
            eg.AddNewEntity(entityComponent);
            // bind entity group
            entityComponent.Group = eg;
            
            return entityComponent;
        }
        
        
    }

}