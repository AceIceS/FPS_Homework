using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FPS_Homework_Utils;

namespace FPS_Homework_Weapon
{


    [CreateAssetMenu(menuName = "Weapons/EnemyWeapon")]
    public class EnemyWeapon : WeaponBase
    {
        public float BulletSpreadAngle = 5.0f;
        private float mLastFireTime;

        public override void InstantiateWeapon(Transform weaponSlot, GameObject owner)
        {
            mWeaponMuzzle = new GameObject("Muzzle(Run Time)");
            mWeaponMuzzle.transform.parent = weaponSlot.transform;
            mWeaponMuzzle.transform.localPosition = Vector3.zero;
            mWeaponMuzzle.transform.localEulerAngles = Vector3.zero;
            
            Owner = owner;
            
        }


        // control fire interval in FSM
        public override bool CanOpenFire()
        {
            return true;

        }
        
        protected override void OpenFire()
        {
            // projectile generate direction
            Vector3 projectileDir = WeaponTrajectoryDirection();

            // generate projectile
            GameObject newProjectile =
                WeaponProjectilePrefab.InstantiateProjectice(
                    this,
                    mWeaponMuzzle.transform.position,
                    Quaternion.LookRotation(projectileDir));
            
            // Muzzle FX
            RuntimeParticlesManager.Instance.GenerateFxAt(
                MuzzleFXName,
                mWeaponMuzzle.transform.position,
                mWeaponMuzzle.transform.rotation, 1.0f,
                mWeaponMuzzle.transform);

            mLastFireTime = Time.time;
        }
        
    }

}
