using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using FPS_Homework_Framework;
using FPS_Homework_Player;

namespace FPS_Homework_Weapon
{

    public class EnemyProjectileController : ProjectileBaseController
    {
        public override void OnProjectileShot()
        {
            base.OnProjectileShot();
            
            // raycast immediately before projectile fly
            if (Physics.Raycast(transform.position, 
                transform.forward,
                out RaycastHit hit, 0.1f,
                ImpactLayers, QueryTriggerInteraction.Collide))
            {
                if (IsValidHit(hit))
                {
                    OnHitTarget(hit.point, hit.normal, hit.collider);
                }
            }
            
            Destroy(gameObject, MaxAliveTime);
        }
        
        protected override void UpdateProjectile()
        {
            // move
            transform.position += mVelocity * Time.deltaTime;
            
            //
            transform.forward = mVelocity.normalized;
            // gravity
            mVelocity += Graviaty * Time.deltaTime * Vector3.down;
            
            // raycast
            ProjectileRaycastCheck();

            mLastPosition = transform.position;
            
        }

        protected override void OnHitTarget(Vector3 point, Vector3 normal, Collider collider)
        {
            //Debug.LogError(collider.name);
            PlayerEntity pe = collider.GetComponent<PlayerEntity>();
            if (pe != null)
            {
                pe.OnDamaged(20.0f);
            }
            Destroy(gameObject);
        }
        
        protected override bool IsValidHit(RaycastHit hit)
        {
            return hit.collider.tag == "Player";
        }
        
        
    }

}
