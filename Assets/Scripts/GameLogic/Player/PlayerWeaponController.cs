
using System.Collections.Generic;

using UnityEngine;


using FPS_Homework_Framework;
using FPS_Homework_Enemy;
using FPS_Homework_GamePlay;
using FPS_Homework_Weapon;

using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace FPS_Homework_Player
{
    
    public class PlayerWeaponController : WeaponController
    {
        //public GameObject PlayerMainCamera;
        public GameObject PlayerWeaponCamera;
        public Transform WeaponHideSlot;
        public float SwitchWeaponSpeed = 10.0f;
        public float SwitchAimSpeed = 10.0f;
        public float AimCameraScaleSpeed = 10.0f;
        public Vector3 WeaponRecoilOffsets;
        public Vector3 WeaponTotalRecoilOffset;
        // Player Components
        private PlayerInputHandler mPlayerInputHandler;
        private PlayerLocomotionController mPlayerLocomotionController;
        private PlayerHUD mPlayerHUD;
        
        private Camera mPlayerWeaponCamera;
        private Vector3 mWeaponAimPosition;
        private readonly float mCameraDefaultFOV = 60.0f;
        
        private bool mHasFireThisFrame;
        private float mFiredTime;
        
        private WeaponBase mLastWeapon;
        private float mSwitchWeaponLastTime = -1;
        // hide weapon : 1s
        // show weapon : 1s
        // 0.5s
        private readonly float mSwitchWeaponIntervalTime = 2f;
        private bool mUpdateSwitchWeaponAnim = false;
        private bool mIsSwitchDownWeapon = true;
        
        // Start is called before the first frame update
        protected override void Start()
        {
            //base.Start();

            mPlayerInputHandler = GetComponent<PlayerInputHandler>();
            mPlayerInputHandler.OnSwitchWeaponAction += SwitchWeapon;
            mPlayerInputHandler.OnAimAction += OnAimAction;
            
            mPlayerLocomotionController = GetComponent<PlayerLocomotionController>();

            mPlayerWeaponCamera = PlayerWeaponCamera.GetComponent<Camera>();

            mPlayerHUD = GetComponent<PlayerHUD>();
        }

        private void OnDisable()
        {
            if (mPlayerInputHandler != null)
            {
                mPlayerInputHandler.OnSwitchWeaponAction -= SwitchWeapon;
                mPlayerInputHandler.OnAimAction -= OnAimAction;
            }
        }

        public bool AddWeaponToPlayer(WeaponBase weapon)
        {
            if (Weapons == null)
            {
                Weapons = new List<WeaponBase>();
            }
            else
            {
                for (int i = 0; i < Weapons.Count; ++i)
                {
                    // already have
                    if (Weapons[i].WeaponType == weapon.WeaponType)
                    {
                        return false;
                    }
                }
            }
            
            Weapons.Add(weapon);
            weapon.InstantiateWeapon(WeaponSlot, gameObject);  
            // already has weapon
            if (mCurrentWeapon != null)
            {
                weapon.WeaponInstance.transform.localPosition = WeaponHideSlot.transform.localPosition;
                weapon.HideWeapon();
            }
            else
            {
                mCurrentWeapon = weapon;
                mPlayerHUD.OnChangeWeapon(weapon.InfoPrefabName, weapon.CrossHairPrefabName);
                mPlayerHUD.OnUpdateWeaponAmmoInfo(weapon.AmmoInfo);
            }

            return true;
            
        }

        public bool AddWeaponAmmo(int amount, WeaponType weaponType)
        {
            if (Weapons == null)
            {
                return false;
            }

            for (int i = 0; i < Weapons.Count; ++i)
            {
                if (Weapons[i].WeaponType == weaponType)
                {
                    Weapons[i].AddAmmo(amount);
                    mPlayerHUD.OnUpdateWeaponAmmoInfo(mCurrentWeapon.AmmoInfo);
                    return true;
                }
            }

            return false;
        }
        
        public void HandlePlayerWeapons()
        {
            // switching weapon
            if (mUpdateSwitchWeaponAnim)
            {
                return;
            }
            
            if (mPlayerInputHandler.Isfire == true)
            {
                if (OpenFire() == true)
                {
                    // do some records to generate recoil effect
                    CalculateWeaponRecoilOffsets();
                    mPlayerHUD.OnUpdateWeaponAmmoInfo(mCurrentWeapon.AmmoInfo);
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
            if (mCurrentWeapon != null && mCurrentWeapon.NeedReload())
            {
                if (mPlayerInputHandler.IsAim)
                {
                    mPlayerInputHandler.IsAim = false;
                    CancelAimWeaponAnimation();
                    mPlayerHUD.SetAimHudActive(true);
                    
                }
                mCurrentWeapon.Reload();
                mPlayerHUD.OnUpdateWeaponAmmoInfo(mCurrentWeapon.AmmoInfo);
            }
        }
        
        public void HandleWeaponsAnimationInLateUpdate()
        {
            if (mCurrentWeapon == null)
            {
                return;
            }
            
            HandleWeaponAimAnimation();
            HandleSwitchWeaponAnimation();
            
            WeaponSlot.localPosition = mWeaponAimPosition;
        }
        
        private void HandleWeaponAimAnimation()
        {
            // no weapon but player press mouse right button
            if (mCurrentWeapon == null)
            {
                mPlayerInputHandler.IsAim = false;
            }
            
            if (mPlayerInputHandler.IsAim)
            {
                AimWeaponAnimation();
            }
            else
            {
                CancelAimWeaponAnimation();
            }
        }

        private void AimWeaponAnimation()
        {
            // transform weapon position
            // todo:different weapons may have different aim offest
            mWeaponAimPosition = Vector3.Lerp(mWeaponAimPosition,
                WeaponAimPosition.localPosition + mCurrentWeapon.AimPositionOffset,
                SwitchAimSpeed * Time.deltaTime);
            // set fov
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,
                mCurrentWeapon.AimCameraFOV,
                AimCameraScaleSpeed * Time.deltaTime);
            mPlayerWeaponCamera.fieldOfView = Mathf.Lerp(
                mPlayerWeaponCamera.fieldOfView,
                mCurrentWeapon.AimCameraFOV,
                AimCameraScaleSpeed * Time.deltaTime); 
        }

        private void CancelAimWeaponAnimation()
        {
            // 
            mWeaponAimPosition = Vector3.Lerp(mWeaponAimPosition,
                WeaponDefaultPosition.localPosition,
                SwitchAimSpeed * Time.deltaTime);
            //
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,
                mCameraDefaultFOV,
                AimCameraScaleSpeed * Time.deltaTime);
            mPlayerWeaponCamera.fieldOfView = Mathf.Lerp(
                mPlayerWeaponCamera.fieldOfView,
                mCameraDefaultFOV,
                AimCameraScaleSpeed * Time.deltaTime); 
        }

        private void OnAimAction(bool flag)
        {
            // only works when have a weapon 
            if (mCurrentWeapon != null)
            {
                mPlayerHUD.SetAimHudActive(flag);
            }
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
            if (mCurrentWeapon == null)
            {
                return;;
            }
            
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

        // hide old weapon & show new weapon
        private void HandleSwitchWeaponAnimation()
        {
            if (mUpdateSwitchWeaponAnim)
            {
                if (mIsSwitchDownWeapon)
                {
                    // hide old weapon
                    mLastWeapon.WeaponInstance.transform.localPosition =
                        Vector3.Lerp(mLastWeapon.WeaponInstance.transform.localPosition,
                            WeaponHideSlot.transform.localPosition,
                            SwitchWeaponSpeed * Time.deltaTime);
                    if ((mLastWeapon.WeaponInstance.transform.localPosition - 
                        WeaponHideSlot.transform.localPosition).magnitude <= 0.1f)
                    {
                        mIsSwitchDownWeapon = false;
                        mLastWeapon.HideWeapon();
                    }

                }
                else
                {
                    // show new weapon
                    if (!mCurrentWeapon.WeaponInstance.activeSelf)
                    {
                        
                        mCurrentWeapon.ShowWeapon();
                    }
                    mCurrentWeapon.WeaponInstance.transform.localPosition =
                        Vector3.Lerp(mCurrentWeapon.WeaponInstance.transform.localPosition,
                            mCurrentWeapon.PositionInWeaponSlot,
                            SwitchWeaponSpeed * Time.deltaTime);
                    if ((mCurrentWeapon.WeaponInstance.transform.localPosition - 
                         mCurrentWeapon.PositionInWeaponSlot).magnitude <= 0.1f)
                    {
                        mCurrentWeapon.WeaponInstance.transform.localPosition =
                            mCurrentWeapon.PositionInWeaponSlot;
                        mCurrentWeapon.WeaponInstance.transform.localEulerAngles =
                            mCurrentWeapon.RotationInWeaponSlot;
                        mUpdateSwitchWeaponAnim = false;
                    }
                    
                }
            }
            
        }
        
        private void SwitchWeapon(bool isNext)
        {
            // check switch time
            if (CheckSwitchWeaponValid() == false)
            {
                return;
            }
            
            mSwitchWeaponLastTime = Time.time;
            
            // cal index
            if(isNext)
            {
                mCurrentWeaponIndex = (1 + mCurrentWeaponIndex) % Weapons.Count;
            }
            else
            {
                --mCurrentWeaponIndex;
                if (mCurrentWeaponIndex < 0)
                {
                    mCurrentWeaponIndex = Weapons.Count - 1;
                }
            }

            mLastWeapon = mCurrentWeapon;
            mCurrentWeapon = Weapons[mCurrentWeaponIndex];
            mUpdateSwitchWeaponAnim = true;
            mIsSwitchDownWeapon = true;
            mPlayerHUD.OnChangeWeapon(mCurrentWeapon.InfoPrefabName, mCurrentWeapon.CrossHairPrefabName);
            mPlayerHUD.OnUpdateWeaponAmmoInfo(mCurrentWeapon.AmmoInfo);
            // current weapon
            // switched weapon
        }

        private bool CheckSwitchWeaponValid()
        {
            float triggerTime = Time.time;
            if (mSwitchWeaponLastTime >= 0 && triggerTime - mSwitchWeaponLastTime < mSwitchWeaponIntervalTime)
            {
                return false;
            }
            // check weapon valid
            if (Weapons == null || Weapons.Count < 2)
            {
                return false;
            }
            // check aim
            if (mPlayerInputHandler.IsAim)
            {
                return false;
            }

            return true;
        }

        protected override void OnHitTarget(Vector3 point, Vector3 normal, Collider collider, float projectileDamage)
        {
            DamageableTarget dt = collider.GetComponent<DamageableTarget>();

            // blood splat impact effect
            ResourceManager.Instance.GenerateFxAt(dt.ImpactFXName, point,
                Quaternion.LookRotation(normal), 2.0f);


            // On Hit
            EnemyEntityBase eeb = dt.EntityGameObject.GetComponent<EnemyEntityBase>();
            eeb.OnHit(projectileDamage);
            //Debug.LogError("Hit Collider Name : " + collider.gameObject.name);
            //Debug.LogError("Hit Target Name : " + obj.name);
            
            mPlayerHUD.OnHitTarget();
        }
        
        
    }

}
