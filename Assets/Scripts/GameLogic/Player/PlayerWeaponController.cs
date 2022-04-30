using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Weapon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FPS_Homework_Player
{
    
    public class PlayerWeaponController : WeaponController
    {
        //public GameObject PlayerMainCamera;
        public GameObject PlayerWeaponCamera;
        public float SwitchAimSpeed = 10.0f;
        public Vector3 WeaponRecoilOffsets;
        public Vector3 WeaponTotalRecoilOffset;
        // Player Components
        private PlayerInputHandler mPlayerInputHandler;
        private PlayerLocomotionController mPlayerLocomotionController;

        private Vector3 mWeaponAimPosition;

        private bool mHasFireThisFrame;
        private float mFiredTime;
        
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            mPlayerInputHandler = GetComponent<PlayerInputHandler>();
            mPlayerInputHandler.OnSwitchWeaponAction += SwitchWeapon;
            
            mPlayerLocomotionController = GetComponent<PlayerLocomotionController>();
        }

        private void OnDisable()
        {
            if (mPlayerInputHandler != null)
            {
                mPlayerInputHandler.OnSwitchWeaponAction -= SwitchWeapon;
            }
        }

        public void HandlePlayerWeapons()
        {
            if (mPlayerInputHandler.Isfire == true)
            {
                if (OpenFire() == true)
                {
                    // do some records to generate recoil effect
                    CalculateWeaponRecoilOffsets();
                }
            }
            else
            {
                if (mPlayerInputHandler.TryReload == true)
                {
                    TryReload();
                }
            }
            // 
            
        }

        private void TryReload()
        {
            if (mCurrentWeapon.NeedReload())
            {
                if (mPlayerInputHandler.IsAim)
                {
                    mPlayerInputHandler.IsAim = false;
                    CancelAimWeapon();
                }
                mCurrentWeapon.Reload();
            }
        }
        
        public void HandleWeaponsAnimationInLateUpdate()
        {
            HandleWeaponAimAnimation();
            
            WeaponSlot.localPosition = mWeaponAimPosition;
        }
        
        private void HandleWeaponAimAnimation()
        {
            if (mPlayerInputHandler.IsAim)
            {
                AimWeapon();
            }
            else
            {
                CancelAimWeapon();
            }
        }

        private void AimWeapon()
        {
            // transform weapon position
            // todo:different weapons may have different aim offest
            mWeaponAimPosition = Vector3.Lerp(mWeaponAimPosition,
                WeaponAimPosition.localPosition + mCurrentWeapon.AimPositionOffset,
                SwitchAimSpeed * Time.deltaTime);
            // set fov
        }

        private void CancelAimWeapon()
        {
            // 
            mWeaponAimPosition = Vector3.Lerp(mWeaponAimPosition,
                WeaponDefaultPosition.localPosition,
                SwitchAimSpeed * Time.deltaTime);
        }
        
        private void CalculateWeaponRecoilOffsets()
        {
            mFiredTime = mCurrentWeapon.CurrentFireTypeInfo.FireInterval;
            mHasFireThisFrame = true;
            if (mPlayerInputHandler.IsAim)
            {
                WeaponTotalRecoilOffset += new Vector3(mCurrentWeapon.AimRecoilX,
                    Random.Range(-mCurrentWeapon.AimRecoilY, mCurrentWeapon.AimRecoilY),
                    Random.Range(-mCurrentWeapon.AimRecoilZ, mCurrentWeapon.AimRecoilZ));
            }
            else
            {
                WeaponTotalRecoilOffset += new Vector3(mCurrentWeapon.RecoilX,
                    Random.Range(-mCurrentWeapon.RecoilY, mCurrentWeapon.RecoilY),
                    Random.Range(-mCurrentWeapon.RecoilZ, mCurrentWeapon.RecoilZ));
            }
            
        }

        public void RecoverWeaponRecoil()
        {
            if (mHasFireThisFrame)
            {
                WeaponRecoilOffsets = Vector3.Lerp(WeaponRecoilOffsets,
                    WeaponTotalRecoilOffset, Time.deltaTime * mCurrentWeapon.Snappiness);
                
                mFiredTime -= Time.deltaTime;
                if (mFiredTime < 0)
                {
                    mHasFireThisFrame = false;
                }
            }
            else
            {
                WeaponRecoilOffsets = Vector3.Lerp(WeaponTotalRecoilOffset,
                    Vector3.zero, Time.deltaTime * mCurrentWeapon.ReturnSpeed);
                WeaponTotalRecoilOffset = WeaponRecoilOffsets;
            }
        }

        private void SwitchWeapon(bool isNext)
        {
            if(isNext)
            {
                mCurrentWeaponIndex = (1 + mCurrentWeaponIndex) % Weapons.Count;
            }
            else
            {
                --mCurrentWeaponIndex;
                if (mCurrentWeaponIndex < 0)
                {
                    mCurrentWeaponIndex = 0;
                }
            }
            // current weapon
            
            // switched weapon
        }
        
    }

}
