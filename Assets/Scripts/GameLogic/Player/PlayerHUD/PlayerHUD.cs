using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
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

        [SerializeField] private Slider mHealth;
        
        [SerializeField] 
        private GameObject mPauseGameHUD;

        [SerializeField] private GameObject mGameOverHUD;
        [SerializeField] private Text mWaveRecord;
        [SerializeField] private Text mScoreRecord;
        private int mScore = 0;
        private int mWave = 0;
        
        
        #region Compass Fields

        private bool mIsInitCompass;
        public RectTransform CompasRect;
        public float VisibilityAngle = 180f;
        public float HeightDifferenceMultiplier = 2f;
        public float MinScale = 0.5f;
        public float DistanceMinScale = 50f;
        public float CompasMarginRatio = 0.8f;

        public GameObject MarkerDirectionPrefab;
        
        private Transform mPlayerTransform;
        
        private Dictionary<Transform, PlayerCompassMarker> mEntity2CompassMarker = 
            new Dictionary<Transform, PlayerCompassMarker>();

        private float mWidthMultiplier;
        private float mHeightOffset;
        
        
        #endregion
        
        
        
        private void OnEnable()
        {
            mName2WeaponInfo = new Dictionary<string, GameObject>();
            mName2CrossHairPrefab = new Dictionary<string, GameObject>();
            
            OnChangeWeaponCrossHair("DefaultAimUI");
            
            mIsInitCompass = false;
            
        }

        public void OnUpdatePlayerHUD()
        {
            if (!mIsInitCompass)
            {
                mPlayerTransform = GameWorld.TheGameWorld.PlayerGameObject.transform;
                mWidthMultiplier = CompasRect.rect.width / VisibilityAngle;
                mHeightOffset = -CompasRect.rect.height / 2;
                mIsInitCompass = true;
                
            }
            //
            CheckInactiveHitTargetHUD();

            UpdateCompassHUD();

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
        
        #region Pause Game

        public void OnPauseGame(bool pause)
        {
            if (GameWorld.TheGameWorld.PlayerGameObject.
                GetComponent<PlayerEntity>().PlayerIsDead)
            {
                return;
            }
            
            if (pause)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mCrossHairParent.SetActive(false);
                mPauseGameHUD.SetActive(true);
            }
            else
            {
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mCrossHairParent.SetActive(true);
                mPauseGameHUD.SetActive(false);
            }
        }
        
        public void OnClickMainMenuButton()
        {
            // reset
            EntityManager.Instance.Clear();
            Debug.LogError("Load Start Scene");
            GameObject go = GameObject.Find("The Game World");
            Destroy(go);
            Time.timeScale = 1.0f;
            //SceneManager.Instance.LoadSceneAsync(FrameworkConstants.StartSceneResPath, OnStartSceneLoad);
            UnityEngine.SceneManagement.SceneManager.LoadScene(FrameworkConstants.StartSceneResPath);
            
        }

        private void OnStartSceneLoad()
        {
            Time.timeScale = 1.0f;
        }
        
        public void OnClickQuitGameButton()
        {
            Debug.LogError(11);
            // quit 
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
            
        }

        
        #endregion

        #region Score Board

        public void AddScore(int score)
        {
            mScore += score;
            mScoreRecord.text = string.Format("Score: {0}", mScore);
        }

        public void AddWave()
        {
            ++mWave;
            mWaveRecord.text = string.Format("Wave: {0}", mWave);
        }

        #endregion
        
        #region Health

        public void OnChangeHealthBar(float percent)
        {
            mHealth.value = percent;
        }
        
        #endregion
        
        #region GameOver

        public void OnShowGameOverUI()
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mCrossHairParent.SetActive(false);
            
            mGameOverHUD.SetActive(true);
        }
        
        public void OnHideGameOverUI()
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mCrossHairParent.SetActive(true);
            
            mGameOverHUD.SetActive(false);
        }
        
        public void Respawn()
        {
            OnChangeHealthBar(1.0f);
            GameWorld.TheGameWorld.PlayerGameObject.
                GetComponent<PlayerEntity>().Respawn();
            OnHideGameOverUI();
        }
        
        #endregion
        
        
        #region Compass

        private void UpdateCompassHUD()
        {

            foreach (var element in mEntity2CompassMarker)
            {
                float distanceRatio = 1;
                float heightDifference = 0;
                float angle;

                if (element.Value.IsDirection)
                {
                    angle = Vector3.SignedAngle(mPlayerTransform.forward,
                        element.Key.transform.localPosition.normalized, Vector3.up);
                }
                else
                {
                    Vector3 targetDir = (element.Key.transform.position - mPlayerTransform.position).normalized;
                    targetDir = Vector3.ProjectOnPlane(targetDir, Vector3.up);
                    Vector3 playerForward = Vector3.ProjectOnPlane(mPlayerTransform.forward, Vector3.up);
                    angle = Vector3.SignedAngle(playerForward, targetDir, Vector3.up);

                    Vector3 directionVector = element.Key.transform.position - mPlayerTransform.position;

                    heightDifference = (directionVector.y) * HeightDifferenceMultiplier;
                    heightDifference = Mathf.Clamp(heightDifference, -CompasRect.rect.height / 2 * CompasMarginRatio,
                        CompasRect.rect.height / 2 * CompasMarginRatio);

                    distanceRatio = directionVector.magnitude / DistanceMinScale;
                    distanceRatio = Mathf.Clamp01(distanceRatio);
                }

                if (angle > -VisibilityAngle / 2 && angle < VisibilityAngle / 2)
                {
                    element.Value.CanvasGroup.alpha = 1;
                    element.Value.CanvasGroup.transform.localPosition = new Vector2(mWidthMultiplier * angle,
                        heightDifference + mHeightOffset);
                    element.Value.CanvasGroup.transform.localScale =
                        Vector3.one * Mathf.Lerp(1, MinScale, distanceRatio);
                }
                else
                {
                    element.Value.CanvasGroup.alpha = 0;
                }
            }
        }
        
        public void RegisterCompassElement(Transform element, PlayerCompassMarker marker)
        {
            marker.transform.SetParent(CompasRect);

            mEntity2CompassMarker.Add(element, marker);
        }

        public void UnregisterCompassElement(Transform element)
        {
            if (mEntity2CompassMarker.TryGetValue(element, out PlayerCompassMarker marker) 
                && marker.CanvasGroup != null)
                Destroy(marker.CanvasGroup.gameObject);
            mEntity2CompassMarker.Remove(element);
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
