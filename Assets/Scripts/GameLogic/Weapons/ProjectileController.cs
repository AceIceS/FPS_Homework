
using System.Collections.Generic;
using FPS_Homework_Enemy;
using FPS_Homework_GamePlay;
using UnityEngine;

using FPS_Homework_Framework;
using FPS_Homework_Player;
using FPS_Homework_Utils;
using UnityEngine.Events;


namespace FPS_Homework_Weapon
{

    public class ProjectileController : ProjectileBaseController
    {
        
        
        public override void OnProjectileShot()
        {
            base.OnProjectileShot();

            PlayerWeaponController playerWeaponController =
                ProjectileCreater.GetComponent<PlayerWeaponController>();
            // calculate trajectory correction vector if necessary
            if (playerWeaponController != null)
            {
                mNeedCorrectTragjectory = true;
                
                Vector3 cameraToMuzzle = (ProjectileInitialPos -
                                          playerWeaponController.PlayerWeaponCamera.transform.position);
                mTrajectoryCorrectionVector = 
                    Vector3.ProjectOnPlane(-cameraToMuzzle,
                    playerWeaponController.PlayerWeaponCamera.transform.forward);
                
                // raycast immediately before projectile fly
                if (Physics.Raycast(playerWeaponController.PlayerWeaponCamera.transform.position, 
                    cameraToMuzzle.normalized,
                    out RaycastHit hit, cameraToMuzzle.magnitude,
                    ImpactLayers, QueryTriggerInteraction.Collide))
                {
                    if (IsValidHit(hit))
                    {
                        OnHitTarget(hit.point, hit.normal, hit.collider);
                    }
                }
            }
            
            Destroy(gameObject, MaxAliveTime);
            
        }
        

        // Update is called once per frame
        protected override void UpdateProjectile()
        {
            // move
            transform.position += mVelocity * Time.deltaTime;

            // still need correct trajectory
            if (mNeedCorrectTragjectory == true &&
                mAccumulatedTrajectoryCorrectionVector.sqrMagnitude <
                mTrajectoryCorrectionVector.sqrMagnitude)
            {
                CorrectProjectileTragjectory();
            }
            
            //
            transform.forward = mVelocity.normalized;
            // gravity
            mVelocity += Graviaty * Time.deltaTime * Vector3.down;
            
            // raycast
            ProjectileRaycastCheck();

            mLastPosition = transform.position;

        }

        protected override bool IsValidHit(RaycastHit hit)
        {
            if (hit.collider.tag == "Entity")
            {
                return false;
            }
            
            if (hit.collider.isTrigger && 
                hit.collider.GetComponent<DamageableTarget>() == null)
            {
                return false;
            }

            // ignore shooter himself
            if (mShooterColliders != null && 
                mShooterColliders.Contains(hit.collider))
            {
                return false;
            }

            return true;
        }

        private void CorrectProjectileTragjectory()
        {
            Vector3 correctionLeft = 
                mTrajectoryCorrectionVector - mAccumulatedTrajectoryCorrectionVector;
            float distanceThisStep = (transform.position - mLastPosition).magnitude;
            Vector3 correctionThisFrame =
                (distanceThisStep / TrajectoryDeltaCorrection) * mTrajectoryCorrectionVector;
            correctionThisFrame = Vector3.ClampMagnitude(correctionThisFrame, correctionLeft.magnitude);
            mAccumulatedTrajectoryCorrectionVector += correctionThisFrame;

            // Detect end of correction
            if (mAccumulatedTrajectoryCorrectionVector.sqrMagnitude == 
                mTrajectoryCorrectionVector.sqrMagnitude)
            {
                mNeedCorrectTragjectory = false;
            }

            transform.position += correctionThisFrame;
        }

        // Hit ground : Show FX
        // Hit Target : Invoke OnHitAction
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
