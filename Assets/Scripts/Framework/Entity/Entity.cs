using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Framework
{
    public enum EntityStatus
    {
        Active,
        Inactive,
        Destroy
    }
    
    
    public class Entity : MonoBehaviour
    {
        public EntityStatus EntityStatus = EntityStatus.Active;
        public EntityGroup Group;
        public int ID;
        #region override Funcs
        
        protected virtual void Start()
        {
            
        }
        
        public virtual void UpdateEntity()
        {
            
        }

        public virtual void FixUpdateEntity()
        {
            
        }


        public virtual void LateUpdateEntity()
        {
            
        }

        public void  DestroySelf()
        {
            Destroy(gameObject,3.0f);
        }

        #endregion
        
    }
    
}
