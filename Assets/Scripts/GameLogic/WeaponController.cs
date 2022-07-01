using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Weapon
{
    public class WeaponController : MonoBehaviour
    {
        public List<WeaponBase> Weapons;
        
        public Transform WeaponSlot;

        public Transform WeaponDefaultPosition;
        
        public Transform WeaponAimPosition;
        protected WeaponBase mCurrentWeapon;
        protected int mCurrentWeaponIndex = 0; 
            
        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (Weapons != null && Weapons.Count > 0)
            {
                mCurrentWeapon = Weapons[mCurrentWeaponIndex];
                mCurrentWeapon.InstantiateWeapon(WeaponSlot, gameObject);
            }
        }

        // Fire,returns true when success
        public bool OpenFire()
        {
            if (mCurrentWeapon != null && mCurrentWeapon.CanOpenFire())
            {
                mCurrentWeapon.HandleWeaponFire();
                return true;
            }
            return false;
        }

        public void BindHitAction(ProjectileBaseController pc)
        {
            pc.OnHitTargetAction += OnHitTarget;
        }
        // what if weapon hit damageable target ?
        protected virtual void OnHitTarget(Vector3 point, Vector3 normal, Collider collider, float projectileDamage)
        {
        }

    }

}
