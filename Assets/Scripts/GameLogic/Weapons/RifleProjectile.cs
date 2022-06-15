
using UnityEngine;

namespace FPS_Homework_Weapon
{

    [CreateAssetMenu(menuName = "WeaponProjectiles/RifleProjectile")]
    public class RifleProjectile : ProjectileBase
    {
        public float ProjectileFlySpeed;

        protected override void OnProjectileGenerated(WeaponBase weapon)
        {
            // add controller to move this projectile
            var controller =  mNewProjectileInstance.GetComponent<ProjectileController>();
            // pass parameters
            controller.ProjectileCreater = Owner;
            controller.MuzzleVelocity = InheritedMuzzleVelocity;
            controller.ProjectileDamage = Damage;
            controller.MaxAliveTime = MaxAliveTime;
            controller.ProjectileInitialPos = InitialPosition;
            controller.ProjectileSpeed = ProjectileFlySpeed;

            WeaponController wc = weapon.Owner.GetComponent<WeaponController>();
            wc.BindHitAction(controller);
            
            
            controller.OnProjectileShot();

        }
        
    }

}
