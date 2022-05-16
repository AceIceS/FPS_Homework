using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Player;
using UnityEngine;

namespace FPS_Homework_Item
{

    public class ScriptableObjectItemBase : ScriptableObject, IPlayerInteractable
    {
        public virtual bool OnPlayerInteract(PlayerEntity playerEntity)
        {
            return true;
        }
        
    }

}
