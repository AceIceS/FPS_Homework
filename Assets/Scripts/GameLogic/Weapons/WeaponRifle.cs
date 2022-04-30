
using System;
using System.Collections;
using UnityEngine;
using FPS_Homework_Utils;
using FPS_Homework_GamePlay;

namespace FPS_Homework_Weapon
{

    [CreateAssetMenu(menuName = "Weapons/WeaponRifle")]
    public class WeaponRifle : WeaponBase
    {
        public int AmmoPerMagazine;
        public float ReloadTime;

        public float BulletSpreadAngle = 5.0f;

        private int mAmmoConsumption = 0;
        private float mLastFireTime;

        private bool mIsReloading = false;
        private Coroutine mRelaodCoroutine;

        protected override void OnEnable()
        {
            base.OnEnable();
            mAmmoConsumption = 0;
            mLastFireTime = 0;
            mIsReloading = false;
        }

        #region Reload

        public override bool NeedReload()
        {
            if (mIsReloading == false &&
                mAmmoConsumption > 0)
            {
                return true;
            }

            return false;
        }

        public override void Reload()
        {
            mIsReloading = true;
            mWeaponAnimator.SetTrigger(mReloadAnimName);
            // TODO: what if the player switch weapon?
            mRelaodCoroutine =
                GameUtilites.Instance.StartCoroutine(ReloadFinished());
        }

        private IEnumerator ReloadFinished()
        {
            yield return new WaitForSeconds(ReloadTime);
            //
            mIsReloading = false;
            mWeaponAnimator.ResetTrigger(mReloadAnimName);
            mAmmoConsumption = 0;
        }

        #endregion

        #region Fire

        public override bool CanOpenFire()
        {
            // 1.Reload?
            // 2.Ammo?
            // 3.Fire Interval?
            if (mIsReloading == false &&
                mAmmoConsumption < AmmoPerMagazine &&
                (mLastFireTime == 0 ||
                 Time.time >= mLastFireTime + CurrentFireTypeInfo.FireInterval))
            {
                return true;
            }

            return false;

        }

        protected override void OpenFire()
        {
            // projectile generate direction
            Vector3 projectileDir = WeaponTrajectoryDirection();

            // generate projectile
            // TODO:Generate by object pool
            GameObject newProjectile =
                WeaponProjectilePrefab.InstantiateProjectice(
                    this,
                    mWeaponMuzzle.transform.position,
                    Quaternion.LookRotation(projectileDir));

            // Muzzle FX
            RuntimeParticlesManager.Instance.GenerateFxAt(
                MuzzleFXName,
                mWeaponMuzzle.transform.position,
                mWeaponMuzzle.transform.rotation, 1.0f,
                mWeaponMuzzle.transform);

            // TODO:Sound

            ++mAmmoConsumption;
            mLastFireTime = Time.time;
        }

        // random trajectory direction
        protected override Vector3 WeaponTrajectoryDirection()
        {
            float spreadAngleRatio = BulletSpreadAngle / 180f;

            return Vector3.Slerp(mWeaponMuzzle.transform.forward,
                UnityEngine.Random.insideUnitSphere,
                spreadAngleRatio);
        }

        #endregion
        
        
        
    }

}
