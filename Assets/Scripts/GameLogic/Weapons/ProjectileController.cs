
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

    public class ProjectileController : MonoBehaviour
    {
        // 
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
        
        
        private Vector3 mVelocity;
        
        private List<Collider> mShooterColliders;
        
        private bool mNeedCorrectTragjectory = false;
        private Vector3 mTrajectoryCorrectionVector;
        private Vector3 mAccumulatedTrajectoryCorrectionVector = Vector3.zero;

        private Vector3 mLastPosition;
        
        public void OnProjectileShot()
        {
            // Take care of colliders of shooter
            mShooterColliders = new List<Collider>();
            Collider[] colliders = ProjectileCreater.GetComponentsInChildren<Collider>();
            mShooterColliders.AddRange(colliders);
            
            // 
            mLastPosition = ProjectileInitialPos;
            mVelocity = transform.forward * ProjectileSpeed;
            //transform.position += MuzzleVelocity * Time.deltaTime;

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
        void Update()
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

        private bool IsValidHit(RaycastHit hit)
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

        private void ProjectileRaycastCheck()
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
        
        // Hit ground : Show FX
        // Hit Target : Invoke OnHitAction
        protected virtual void OnHitTarget(Vector3 point, Vector3 normal, Collider collider)
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
