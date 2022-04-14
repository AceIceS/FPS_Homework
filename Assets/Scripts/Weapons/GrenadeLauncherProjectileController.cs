using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Weapon
{

    public class GrenadeLauncherProjectileController : ProjectileController
    {
        protected override void OnHitTarget(Vector3 point, Vector3 normal, Collider collider)
        {   
            base.OnHitTarget(point, normal, collider);
            // TODO:Range damage
        }
    }

}
