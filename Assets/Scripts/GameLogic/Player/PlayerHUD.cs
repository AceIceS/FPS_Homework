using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using FPS_Homework_Framework;

namespace FPS_Homework_Player
{
    
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField]
        private InteractableHUD mInteractableHUD;

        [SerializeField] 
        private GameObject mWeaponInfoParent;
        private GameObject mCurrentWeaponInfo;
        private Dictionary<string, GameObject> mName2WeaponInfo;
        [SerializeField] 
        private Text mWeaponAmmo;

        [SerializeField] 
        private GameObject mCrossHairParent;
        private GameObject mCurrentCrossHair;
        private Dictionary<string, GameObject> mName2CrossHairPrefab;

        [SerializeField] 
        private GameObject mHitTargetHUD;
        private float mActiveHitTargetHUDTime;
        
        private void OnEnable()
        {
            mName2WeaponInfo = new Dictionary<string, GameObject>();
            mName2CrossHairPrefab = new Dictionary<string, GameObject>();
            
            OnChangeWeaponCrossHair("DefaultAimUI");
        }

        public void OnUpdatePlayerHUD()
        {
            //
            CheckInactiveHitTargetHUD();

        }

        public void OnChangeWeapon(string weaponInfoName, string weaponCrossHairName)
        {
            OnChangeWeaponInfo(weaponInfoName);
            OnChangeWeaponCrossHair(weaponCrossHairName);
        }
        
        
        #region Interactable Object
        
        public void EnableShowInteractableInfo(string info)
        {
            mInteractableHUD.EnableShowInteractableInfo(info);
        }

        public void DisableShowInteractableInfo()
        {
            mInteractableHUD.DisableShowInteractableInfo();    
        }

        #endregion
        
        #region Weapon Ammo

        public void OnUpdateWeaponAmmoInfo(string ammoInfo)
        {
            mWeaponAmmo.text = ammoInfo;
        }
        
        
        #endregion

        private void OnChangeWeaponInfo(string weaponInfoName)
        {
            GameObject ch = null;
            mName2WeaponInfo.TryGetValue(weaponInfoName, out ch);
            if (ch == null)
            {
                ch = ResourceManager.Instance.GetGameObjectCopy(weaponInfoName);
                if (ch == null)
                {
                    return;
                }
                mName2WeaponInfo.Add(weaponInfoName, ch);
            }

            // hide former
            if (mCurrentWeaponInfo != null)
            {
                mCurrentWeaponInfo.SetActive(false);
            }

            mCurrentWeaponInfo = ch;
            mCurrentWeaponInfo.transform.SetParent(mWeaponInfoParent.transform);
            mCurrentWeaponInfo.transform.localPosition = Vector3.zero;
            mCurrentWeaponInfo.SetActive(true);
        }
        
        #region CrossHair
        
        public void SetAimHudActive(bool flag)
        {
            if (mCurrentCrossHair != null)
            {
                mCurrentCrossHair.SetActive(flag);
            }
        }
        
        private void OnChangeWeaponCrossHair(string crossHairName)
        {
            GameObject ch = null;
            mName2CrossHairPrefab.TryGetValue(crossHairName, out ch);
            if (ch == null)
            {
                ch = ResourceManager.Instance.GetGameObjectCopy(crossHairName);
                if (ch == null)
                {
                    return;
                }
                mName2CrossHairPrefab.Add(crossHairName, ch);
            }

            // hide former
            if (mCurrentCrossHair != null)
            {
                mCurrentCrossHair.SetActive(false);
            }

            mCurrentCrossHair = ch;
            mCurrentCrossHair.transform.SetParent(mCrossHairParent.transform);
            mCurrentCrossHair.transform.localPosition = Vector3.zero;
            mCurrentCrossHair.SetActive(true);
            
        }
        
        #endregion

        #region Hit Target
        
        public void OnHitTarget()
        {
            mHitTargetHUD.SetActive(true);
            mActiveHitTargetHUDTime = Time.time;
        }

        private void CheckInactiveHitTargetHUD()
        {
            if (Time.time - mActiveHitTargetHUDTime > 0.5f)
            {
                mHitTargetHUD.SetActive(false);
            }
        }
        

        #endregion
        
        
    }
    

    #region HUDs
    
    [Serializable]
    public class InteractableHUD
    {
        [SerializeField]
        private Text mShowText;

        public void EnableShowInteractableInfo(string info)
        {
            mShowText.text = info;
            mShowText.gameObject.SetActive(true);
        }

        public void DisableShowInteractableInfo()
        {
            mShowText.gameObject.SetActive(false);    
        }
        
    }

    #endregion
    
}
