using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Player;
using UnityEngine;

using FPS_Homework_Utils;
using FPS_Homework_Weapon;

namespace FPS_Homework_Enemy
{

    public class EnemyWeaponController : WeaponController
    {
        public Transform AimLineStartPos;
        public LineRenderer mAimLine;

        [Header("Melee")]
        public WeaponMelee Melee;

        private PlayerEntity mPlayerEntity;
        
        public void AimTarget(GameObject target)
        {
            if (mAimLine == null)
            {
                mAimLine = GetComponent<LineRenderer>();
                mAimLine.startColor = Color.red;
                mAimLine.endColor = Color.red;
                mAimLine.startWidth = 0.03f;
                mAimLine.endWidth = 0.03f;
            }
            if(mAimLine.enabled == false)
            {
                mAimLine.enabled = true;
            }
            
            // aim target
            mAimLine.SetPosition(0, AimLineStartPos.position);
            mAimLine.SetPosition(1, target.transform.position +
                                    new Vector3(0,1.2f,0));
            
        }

        public void LoseTarget()
        {
            mAimLine.enabled = false;
        }
        

        protected override void OnHitTarget(Vector3 point, Vector3 normal, Collider collider, float projectileDamage)
        {
            if (collider.tag == "Player")
            {
                if (mPlayerEntity == null)
                {
                    mPlayerEntity = collider.GetComponent<PlayerEntity>();
                }
                mPlayerEntity.OnDamaged(15.0f);
            }
        }

        public void ActiveMeleeWeapon()
        {
            if (Melee != null)
            {
                Melee.OnActiveWeapon();
            }
        }

        public void InactiveMeleeWeapon()
        {
            if (Melee != null)
            {
                Melee.OnInactiveWeapon();
            }
        }
        
    }

}
