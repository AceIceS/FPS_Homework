using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Framework;
using FPS_Homework_Utils;
using UnityEngine;

namespace FPS_Homework_Weapon
{

    [CreateAssetMenu(menuName = "Weapons/WeaponLazerRifle")]
    public class WeaponLazerRifle : WeaponBase
    {
        [SerializeField]
        private ParticleSystem mFireFX;
        protected override void OnEnable()
        {
            // Muzzle FX
           //RuntimeParticlesManager.Instance.GenerateFxAt(
           //    MuzzleFXName,
           //    mWeaponMuzzle.transform.position,
           //    mWeaponMuzzle.transform.rotation, 1.0f,
           //    mWeaponMuzzle.transform);
        }
        
        protected override void OpenFire()
        {
            Debug.LogError("1");
            
        }
        
    }

}
