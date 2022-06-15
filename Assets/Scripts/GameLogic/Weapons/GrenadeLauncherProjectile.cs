using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Weapon
{
    
    [CreateAssetMenu(menuName = "WeaponProjectiles/GrenadeLauncherProjectile")]
    public class GrenadeLauncherProjectile : RifleProjectile
    {
        protected override void OnProjectileGenerated(WeaponBase weapon)
        {
            base.OnProjectileGenerated(weapon);
            
            // add controller to move this projectile
            var controller =  
                mNewProjectileInstance.GetComponent<GrenadeLauncherProjectileController>();
            
            // pass parameters
            controller.ProjectileCreater = Owner;
            controller.MuzzleVelocity = InheritedMuzzleVelocity;
            controller.ProjectileDamage = Damage;
            controller.MaxAliveTime = MaxAliveTime;
            controller.ProjectileInitialPos = InitialPosition;
            controller.ProjectileSpeed = ProjectileFlySpeed;
            
            
            controller.OnProjectileShot();
        }
    }

}
