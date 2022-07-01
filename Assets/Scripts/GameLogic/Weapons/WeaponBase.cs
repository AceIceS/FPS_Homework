using System;
using System.Collections.Generic;
using FPS_Homework_Item;
using FPS_Homework_Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace FPS_Homework_Weapon
{
    //
    public enum WeaponType
    {
        Pistol,
        Rifle,
        GrenadeLauncher
    }

    //
    public enum WeaponFireMode
    {
        // pistol, shotgun
        Manual,
        // rifle
        Automatic,
        // laser
        Charge,
        // flame ejector
        Hold
    }
    
    [Serializable]
    public class WeaponFireTypeInfo
    {
        public WeaponFireMode FireMode;
        public float FireInterval;
    }
    
    [Serializable]
    public abstract class WeaponBase : ScriptableObjectItemBase
    {
        // Weapon Info
        public WeaponType WeaponType;
        public WeaponFireTypeInfo CurrentFireTypeInfo
        {
            get
            {
                return mFireModes[mFireModeIndex];
            }
        }
        public GameObject WeaponPrefab;
        public string InfoPrefabName;
        public string CrossHairPrefabName;
        public string MuzzleFXName;
        
        public Vector3 PositionInWeaponSlot;
        public Vector3 RotationInWeaponSlot;
        public Vector3 CreateMuzzlePosition;
        public Vector3 CreateMuzzleRotation;
        public Vector3 AimPositionOffset;
        public float AimCameraFOV = 60;
        [Header("Ammo")] 
        public ProjectileBase WeaponProjectilePrefab;

        [Header("Recoil Settings")]
        public float RecoilX;
        public float RecoilY;
        public float RecoilZ;
        public float AimRecoilX;
        public float AimRecoilY;
        public float AimRecoilZ;
        public float Snappiness;
        public float ReturnSpeed;

        [HideInInspector]
        public GameObject Owner;
        public Vector3 MuzzleWorldVelocity { get; private set; }
        private Vector3 mLastMuzzlePosition;
        
        [SerializeField]
        private WeaponFireTypeInfo[] mFireModes = null;
        private int mFireModeIndex = 0;

        public virtual float CurrentAmmo
        {
            get
            {
                return 0;
            }
        }

        public virtual float CurrentTotalAmmo
        {
            get
            {
                return 0;
            }
        }

        public string AmmoInfo
        {
            get
            {
                return CurrentAmmo + "/" + CurrentTotalAmmo;
            }
        }
        
        public GameObject WeaponInstance
        {
            get
            {
                return mWeaponInstance;
            }
        }
        
        private GameObject mWeaponInstance;
        protected GameObject mWeaponMuzzle;

        protected Animator mWeaponAnimator;
        protected const string mReloadAnimName = "Reload";
        
        protected virtual void OnEnable()
        {
        }

        public override bool OnPlayerInteract(PlayerEntity playerEntity)
        {
            return playerEntity.AddWeapon(this);
        }
        
        // Create Weapon and Bind Owner
        public virtual void InstantiateWeapon(Transform weaponSlot, GameObject owner)
        {
            if (WeaponPrefab == null)
            {
                Debug.LogError("No Weapon Prefab Set !!!");
                return;
            }

            mWeaponInstance = Instantiate(WeaponPrefab,
                Vector3.zero, 
                Quaternion.identity,
                weaponSlot) as GameObject;
            mWeaponAnimator = mWeaponInstance.GetComponent<Animator>();
            // reset position and rotation
            mWeaponInstance.transform.localPosition = PositionInWeaponSlot;
            mWeaponInstance.transform.localEulerAngles = RotationInWeaponSlot;
            // weapon layer
            mWeaponInstance.layer = 6;
            // Create Muzzle
            mWeaponMuzzle = new GameObject("Muzzle(Run Time)");
            mWeaponMuzzle.transform.parent = mWeaponInstance.transform;
            mWeaponMuzzle.transform.localPosition = CreateMuzzlePosition;
            mWeaponMuzzle.transform.localEulerAngles = CreateMuzzleRotation;
            
            Owner = owner;
        }

        public virtual void AddAmmo(int ammo)
        {
        }
        
        public void HideWeapon()
        {
            mWeaponInstance.SetActive(false);
        }

        public void ShowWeapon()
        {
            mWeaponInstance.SetActive(true);
        }
        
        public virtual bool NeedReload()
        {
            return false;
        }

        public virtual void Reload()
        {
            
        }
        
        public virtual bool CanOpenFire()
        {
            return true;
        }
        
        public void HandleWeaponFire()
        {
            OnBeforeFire();
            OpenFire();
            OnEndFire();
        }

        protected virtual void OnBeforeFire()
        {
            //
            if (Time.deltaTime > 0)
            {
                MuzzleWorldVelocity = 
                    (mWeaponMuzzle.transform.position - mLastMuzzlePosition) / Time.deltaTime;
                mLastMuzzlePosition = mWeaponMuzzle.transform.position;
            }
            
        }

        protected virtual void OpenFire()
        {
        }
        
        protected virtual void OnEndFire()
        {
            
        }
        
        protected virtual Vector3 WeaponTrajectoryDirection()
        {
            return mWeaponMuzzle.transform.forward;
        }
        
    }

}
