using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FPS_Homework_Framework;

using FPS_Homework_GamePlay;

namespace FPS_Homework_Weapon
{

    public class GrenadeLauncherProjectileController : ProjectileController
    {
        protected override void OnHitTarget(Vector3 point, Vector3 normal, Collider collider)
        {   

            
            DamageableTarget dt = collider.GetComponent<DamageableTarget>();
            
            // hit ground
            if (dt == null)
            {
                // impact effect
                ResourceManager.Instance.GenerateFxAt(RifleImpactFxName[0],
                    point, Quaternion.LookRotation(normal), 1.0f);
                // decal
                ResourceManager.Instance.GenerateFxAt(RifleProjectileHoleDecalName,
                    point + normal * 0.001f, Quaternion.LookRotation(normal), 5.0f);
            }
            else
            {
                if (OnHitTargetAction != null)
                {
                    OnHitTargetAction.Invoke(point,normal,collider, ProjectileDamage);    
                }
            }
            // destroy projectile
            Destroy(gameObject);
        }
    }

}
