using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FPS_Homework_Framework;

namespace FPS_Homework_Item
{

    public class AmmoItemEntity : ItemEntity
    {
        protected override ScriptableObjectItemBase ScriptableObjectItem
        {
            get
            {
                string entityGroupName = Group.EntityGroupName;
                return ResourceManager.Instance.GetScriptableObjectRef(
                   entityGroupName.Substring(0,entityGroupName.Length - 4)) 
                   as ScriptableObjectItemBase;
            }
        }

        protected override void OnAfterPlayerInteract()
        {
            //base.OnAfterPlayerInteract();
        }
    }

}