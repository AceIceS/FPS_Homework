using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Weapon;
using UnityEngine;

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
            mPlayerLocomotionController = GetComponent<PlayerLocomotionController>();
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
            if (Weapon.NeedReload())
            {
                if (mPlayerInputHandler.IsAim)
                {
                    mPlayerInputHandler.IsAim = false;
                    CancelAimWeapon();
                }
                Weapon.Reload();
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
                WeaponAimPosition.localPosition + Weapon.AimPositionOffset,
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
            mFiredTime = Weapon.CurrentFireTypeInfo.FireInterval;
            mHasFireThisFrame = true;
            if (mPlayerInputHandler.IsAim)
            {
                WeaponTotalRecoilOffset += new Vector3(Weapon.AimRecoilX,
                    Random.Range(-Weapon.AimRecoilY, Weapon.AimRecoilY),
                    Random.Range(-Weapon.AimRecoilZ, Weapon.AimRecoilZ));
            }
            else
            {
                WeaponTotalRecoilOffset += new Vector3(Weapon.RecoilX,
                    Random.Range(-Weapon.RecoilY, Weapon.RecoilY),
                    Random.Range(-Weapon.RecoilZ, Weapon.RecoilZ));
            }
            
        }

        public void RecoverWeaponRecoil()
        {
            if (mHasFireThisFrame)
            {
                WeaponRecoilOffsets = Vector3.Lerp(WeaponRecoilOffsets,
                    WeaponTotalRecoilOffset, Time.deltaTime * Weapon.Snappiness);
                
                mFiredTime -= Time.deltaTime;
                if (mFiredTime < 0)
                {
                    mHasFireThisFrame = false;
                }
            }
            else
            {
                WeaponRecoilOffsets = Vector3.Lerp(WeaponTotalRecoilOffset,
                    Vector3.zero, Time.deltaTime * Weapon.ReturnSpeed);
                WeaponTotalRecoilOffset = WeaponRecoilOffsets;
            }
        }
        
        
    }

}
