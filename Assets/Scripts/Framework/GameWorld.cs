using System;
using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Enemy;
using FPS_Homework_Item;
using FPS_Homework_Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using FPS_Homework_GamePlay;

namespace FPS_Homework_Framework
{

    public class GameWorld : MonoBehaviour
    {
        public static GameWorld TheGameWorld = null;

        public GameObject PlayerGameObject
        {
            get
            {
                return mPlayer;
            }
        }
        
        public LayerMask PickupItemLayer
        {
            get
            {
                return mPickupItemLayer;
            }
        }
        
        private LayerMask mPickupItemLayer;

        public GameProcedure GameProcedure
        {
            get
            {
                return mGameProcedure;
            }
        }
        
        private List<IGameFrameworkModule> mGameFrameworkModules;
        [SerializeField]
        private GameObject mPlayerRespawnPoint;
        [SerializeField]
        private List<GameObject> mItemRespawnPoints;
        //[SerializeField]
        //private List<GameObject> mEnemyRespawnPoints;
        private GameProcedure mGameProcedure;
        
        // 
        private GameObject mPlayer = null;
        
        // Start is called before the first frame update
        private void Start()
        {
            Debug.Log("Game World Start");
            // init
            mPickupItemLayer = LayerMask.GetMask(FrameworkConstants.PickUpItemsLayerName);
            
            // Respawn Player
            mPlayerRespawnPoint = GameObject.FindWithTag(FrameworkConstants.PlayerRespawnPointName);
            if (mPlayerRespawnPoint != null)
            {
                var playerEntity = EntityManager.Instance.AddEntity<PlayerEntity>(FrameworkConstants.PlayerEntityName,
                    mPlayerRespawnPoint.transform.position,Quaternion.identity);
                mPlayer = playerEntity.gameObject;
            }
            else
            {
                Debug.LogError("PlayerRespawnPoint not found !");
            }
            
            // Item respawn points
            mItemRespawnPoints = new List<GameObject>();
            mItemRespawnPoints.AddRange(GameObject.FindGameObjectsWithTag(FrameworkConstants.ItemRespawnPointName));
            
            // Enemy respawn points
            //mEnemyRespawnPoints = new List<GameObject>();
            //mEnemyRespawnPoints.AddRange(GameObject.FindGameObjectsWithTag(FrameworkConstants.EnemyRespawnPointName));
            
            // test
            TestSpawnSomething();

            mGameProcedure = GetComponent<GameProcedure>();
            //mGameProcedure.OnInitGameProcedure();
        }

        private void Update()
        {
            if (mGameFrameworkModules != null)
            {
                foreach(IGameFrameworkModule module in mGameFrameworkModules)
                {
                    module.UpdateModule();
                }
            }
        }

        private void LateUpdate()
        {
            if (mGameFrameworkModules != null)
            {
                foreach(IGameFrameworkModule module in mGameFrameworkModules)
                {
                    module.LateUpdateModule();
                }
            }
        }

        private void FixedUpdate()
        {
            if (mGameFrameworkModules != null)
            {
                foreach(IGameFrameworkModule module in mGameFrameworkModules)
                {
                    module.FixUpdateModule();
                }
            }
        }

        #region Utility Funcs

        public void EnterGameWorld()
        {
            TheGameWorld = this;
            Debug.Log("Enter Game World");
            
            // priority order
            mGameFrameworkModules = new List<IGameFrameworkModule>()
            {
                EventManager.Instance,
                EntityManager.Instance
            };
            
            foreach(IGameFrameworkModule module in mGameFrameworkModules)
            {
                module.InitializeModuleBeforeOnStart();
            }

        }

        void TestSpawnSomething()
        {
            EntityManager.Instance.AddEntity<AmmoItemEntity>(
                "GrenadeLauncherAmmoItem",
                mItemRespawnPoints[0].transform.position + Vector3.up / 2,
                Quaternion.Euler(45,0,0));
            EntityManager.Instance.AddEntity<AmmoItemEntity>(
                "RifleAmmoItem",
                mItemRespawnPoints[1].transform.position + Vector3.up / 2,
                Quaternion.Euler(45,0,0));
            EntityManager.Instance.AddEntity<AmmoItemEntity>(
                "PistolAmmoItem",
                mItemRespawnPoints[2].transform.position + Vector3.up / 2,
                Quaternion.Euler(45,0,0));

            EntityManager.Instance.AddEntity<WeaponItemEntity>(
                "WeaponGrenadeLauncherItem",
                mItemRespawnPoints[3].transform.position + Vector3.up / 2,
                Quaternion.Euler(-45,0,0));
            EntityManager.Instance.AddEntity<WeaponItemEntity>(
                "WeaponPistolItem",
                mItemRespawnPoints[4].transform.position + Vector3.up / 2,
                Quaternion.Euler(-45,0,0));
            EntityManager.Instance.AddEntity<WeaponItemEntity>(
                "WeaponRifleItem",
                mItemRespawnPoints[5].transform.position + Vector3.up / 2,
                Quaternion.Euler(-45,0,0));

            // test enemy
            //EntityManager.Instance.AddEntity<EnemyEntityMelee>(
            //    "EnemyEntityMeleeSmall",
            //    mEnemyRespawnPoints[0].transform.position,
            //    Quaternion.identity);
            
            //var testEnemy =  EntityManager.Instance.AddEntity<EnemyEntityShooterA>(
            //    "EnemyEntityShooterA",
            //    mEnemyRespawnPoints[1].transform.position,
            //    Quaternion.identity);
            //var bomberDrone = EntityManager.Instance.AddEntity<EnemyBomber>(
            //    "EnemyBomberDrone",
            //    mEnemyRespawnPoints[1].transform.position,
            //    Quaternion.identity);
            //var drone = EntityManager.Instance.AddEntity<EnemyDrone>(
            //    "EnemyDrone",
            //    mEnemyRespawnPoints[6].transform.position,
            //    Quaternion.identity);

        }
        

        #endregion


    }

}
