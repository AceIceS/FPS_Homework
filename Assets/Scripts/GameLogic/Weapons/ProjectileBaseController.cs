using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace FPS_Homework_Weapon
{

    public class ProjectileBaseController : MonoBehaviour
    {
        public UnityAction<Vector3, Vector3, Collider, float> OnHitTargetAction;
        
        // Player or other ?
        public GameObject ProjectileCreater;
        public Transform ProjectileTip;
        public float ProjectileRadius;
        
        public float MaxAliveTime;
        public float ProjectileDamage;
        public float ProjectileSpeed;
        public float Graviaty;
        
        public Vector3 MuzzleVelocity;
        public Vector3 ProjectileInitialPos;

        public float TrajectoryDeltaCorrection;
        
        public LayerMask ImpactLayers = -1;

        public List<string> RifleImpactFxName;
        public string RifleProjectileHoleDecalName;
        
        
        protected Vector3 mVelocity;
        
        protected List<Collider> mShooterColliders;
        
        protected bool mNeedCorrectTragjectory = false;
        protected Vector3 mTrajectoryCorrectionVector;
        protected Vector3 mAccumulatedTrajectoryCorrectionVector = Vector3.zero;

        protected Vector3 mLastPosition;
        
        public virtual void OnProjectileShot()
        {
            // Take care of colliders of shooter
            mShooterColliders = new List<Collider>();
            Collider[] colliders = ProjectileCreater.GetComponentsInChildren<Collider>();
            mShooterColliders.AddRange(colliders);
            
            // 
            mLastPosition = ProjectileInitialPos;
            mVelocity = transform.forward * ProjectileSpeed;
            //transform.position += MuzzleVelocity * Time.deltaTime;
        }

        private void Update()
        {
            UpdateProjectile();
        }

        protected virtual void UpdateProjectile()
        {
            
        }

        protected virtual void OnHitTarget(Vector3 point, Vector3 normal, Collider collider)
        {
            
        }
        
        
        
        protected void ProjectileRaycastCheck()
        {
            
            RaycastHit closestHit = new RaycastHit();
            closestHit.distance = Mathf.Infinity;
            bool foundHit = false;

            // Sphere cast
            Vector3 displacementSinceLastFrame = ProjectileTip.position - mLastPosition;
            RaycastHit[] hits = Physics.SphereCastAll(mLastPosition, ProjectileRadius,
                displacementSinceLastFrame.normalized, 
                displacementSinceLastFrame.magnitude, ImpactLayers,
                QueryTriggerInteraction.Collide);
            foreach (var hit in hits)
            {
                if (IsValidHit(hit) && hit.distance < closestHit.distance)
                {
                    foundHit = true;
                    closestHit = hit;
                    // only need first checked object
                    break;
                }
            }

            if (foundHit)
            {
                // Handle case of casting while already inside a collider
                if (closestHit.distance <= 0f)
                {
                    closestHit.point = transform.position;
                    closestHit.normal = -transform.forward;
                }

                OnHitTarget(closestHit.point, closestHit.normal, closestHit.collider);
            }
        }


        protected virtual bool IsValidHit(RaycastHit hit)
        {
            return false;
        }

    }

}
