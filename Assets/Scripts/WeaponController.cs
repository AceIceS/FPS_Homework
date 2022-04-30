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
            if (Weapons != null)
            {
                mCurrentWeapon = Weapons[mCurrentWeaponIndex];
                mCurrentWeapon.InstantiateWeapon(WeaponSlot, gameObject);
            }
        }

        // Fire,returns true when success
        public bool OpenFire()
        {
            if (mCurrentWeapon.CanOpenFire())
            {
                mCurrentWeapon.HandleWeaponFire();
                return true;
            }
            return false;
        }
        
    }

}
