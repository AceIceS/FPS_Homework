
using System;
using System.Collections;
using FPS_Homework_Framework;
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

        //private int mAmmoConsumption = 0;
        private int mCurrentMagazine;
        [SerializeField]
        private int mTotalAmmo;
        
        private float mLastFireTime;

        private bool mIsReloading = false;
        private Coroutine mRelaodCoroutine;

        public override float CurrentAmmo
        {
            get
            {
                return mCurrentMagazine;
            }
        }

        public override float CurrentTotalAmmo
        {
            get
            {
                return mTotalAmmo;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            mCurrentMagazine = AmmoPerMagazine;
            mLastFireTime = 0;
            mIsReloading = false;
        }

        public override void AddAmmo(int ammo)
        {
            mTotalAmmo += ammo;
            if (mTotalAmmo < 0)
            {
                mTotalAmmo = 0;
            }
        }
        
        #region Reload

        public override bool NeedReload()
        {
            if (mIsReloading == false &&
                mCurrentMagazine < AmmoPerMagazine)
            {
                return true;
            }

            return false;
        }

        public override void Reload()
        {
            if (mTotalAmmo <= 0)
            {
                return;
            }
            
            mIsReloading = true;
            mWeaponAnimator.SetTrigger(mReloadAnimName);
            // TODO: what if the player switch weapon?
            mRelaodCoroutine =
                GameUtilites.Instance.StartCoroutine(ReloadFinished());
        }

        private IEnumerator ReloadFinished()
        {
            // Fill magazine
            int ammoToBeFilled = AmmoPerMagazine - mCurrentMagazine;
            if (mTotalAmmo >= ammoToBeFilled)
            {
                mCurrentMagazine = AmmoPerMagazine;
                mTotalAmmo -= ammoToBeFilled;
            }
            else
            {
                mCurrentMagazine += mTotalAmmo;
                mTotalAmmo = 0;
            }
            
            // wait for reload animation
            yield return new WaitForSeconds(ReloadTime);
            //
            mIsReloading = false;
            mWeaponAnimator.ResetTrigger(mReloadAnimName);
            yield return new WaitForSeconds(0.5f);

        }

        #endregion

        #region Fire

        public override bool CanOpenFire()
        {
            // 1.Reload?
            // 2.Ammo?
            // 3.Fire Interval?
            if (mIsReloading == false &&
                mCurrentMagazine > 0  &&
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

            --mCurrentMagazine;
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
