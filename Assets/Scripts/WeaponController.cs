using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Weapon
{
    public class WeaponController : MonoBehaviour
    {
        public WeaponBase Weapon;

        public Transform WeaponSlot;

        public Transform WeaponDefaultPosition;
        
        public Transform WeaponAimPosition;
        
        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (Weapon != null)
            {
                Weapon.InstantiateWeapon(WeaponSlot, gameObject);
            }
        }

        // Fire,returns true when success
        public bool OpenFire()
        {
            if (Weapon.CanOpenFire())
            {
                Weapon.HandleWeaponFire();
                return true;
            }
            return false;
        }
        
    }

}
