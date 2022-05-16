using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Player;
using UnityEngine;

using FPS_Homework_Weapon;

namespace FPS_Homework_Item
{

    [CreateAssetMenu(menuName = "PickupItem/Ammo")]
    public class ItemAmmo : ScriptableObjectItemBase
    {
        public WeaponType WeaponType;
        public int AmmoAmount;


        public override bool OnPlayerInteract(PlayerEntity playerEntity)
        {
            //Debug.LogError("Collect Ammo");
            return playerEntity.AddWeaponAmmo(AmmoAmount, WeaponType);
        }
    }
}


