using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Enemy;
using FPS_Homework_Framework;
using FPS_Homework_Player;
using UnityEngine;

namespace FPS_Homework_Weapon
{

    public class WeaponMelee : MonoBehaviour
    {
        public EnemyWeaponController WeaponController;

        private BoxCollider mWeaponCollider;
        private PlayerEntity mPlayerEntity;
        
        private void OnEnable()
        {
            mWeaponCollider = GetComponent<BoxCollider>();
        }

        public void OnActiveWeapon()
        {
            mWeaponCollider.enabled = true;
        }

        public void OnInactiveWeapon()
        {
            mWeaponCollider.enabled = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                mPlayerEntity =
                    GameWorld.TheGameWorld.PlayerGameObject.GetComponent<PlayerEntity>();
                if (mPlayerEntity != null)
                {
                    mPlayerEntity.OnDamaged();
                }
                
            }
        }
    }

}
