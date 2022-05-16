
using System;
using UnityEngine;

using FPS_Homework_Framework;
using FPS_Homework_Player;

namespace FPS_Homework_Item
{

    public class ItemEntity : Entity
    {
        protected virtual ScriptableObjectItemBase ScriptableObjectItem
        {
            get
            {
                return null;
            }
        }
        // rotate in the scene
        public override void UpdateEntity()
        {
            gameObject.transform.Rotate(Vector3.up,1.0f,Space.World);
        }

        public void OnPlayerInteract(PlayerEntity player)
        {
            //Debug.LogError("Interact With: " + gameObject.name);
            
            // call manager to inactive this entity
            //string entityGroupName = Group.EntityGroupName;
            //ScriptableObjectItemBase scriptableObjectItem = 
            //    ResourceManager.Instance.GetScriptableObjectCopy(
            //        entityGroupName.Substring(0,entityGroupName.Length - 4)) as ScriptableObjectItemBase;

            ScriptableObjectItemBase scriptableObjectItem = ScriptableObjectItem;
            if (scriptableObjectItem != null)
            {
                scriptableObjectItem.OnPlayerInteract(player);
            }
            else
            {
                Debug.LogError("Invalid Interact Item!");
            }
            
            OnAfterPlayerInteract();
        }

        // what if after interact
        // eg. hide this entity ?
        protected virtual void OnAfterPlayerInteract()
        {
            
        }
        
    }

}
