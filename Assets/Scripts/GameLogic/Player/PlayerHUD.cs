using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace FPS_Homework_Player
{
    
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField]
        private InteractableHUD mInteractableHUD;

        [SerializeField] 
        private GameObject mWeaponCrossHairParent;
        private GameObject mCurrentWeaponCrossHair;
        private Dictionary<string, GameObject> mCrossHairName2Object ;

        private void OnEnable()
        {
            mCrossHairName2Object = new Dictionary<string, GameObject>();
        }

        public void EnableShowInteractableInfo(string info)
        {
            mInteractableHUD.EnableShowInteractableInfo(info);
        }

        public void DisableShowInteractableInfo()
        {
            mInteractableHUD.DisableShowInteractableInfo();    
        }

        public void OnChangeWeaponCrossHair(string weaponName)
        {
            GameObject crossHair = null;
            if (mCrossHairName2Object.TryGetValue(weaponName, out crossHair))
            {
                    
            }
            
        }
        
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
