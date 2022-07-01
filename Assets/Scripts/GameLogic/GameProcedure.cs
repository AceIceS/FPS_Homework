using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Framework;
using FPS_Homework_Player;
using UnityEngine;

using FPS_Homework_Enemy;

namespace FPS_Homework_GamePlay
{


    public class GameProcedure : MonoBehaviour
    {
        private int mWave;
        private int mCurrentEnemyNumber;
        private int mTotalEnemyNumber;


        private PlayerEntity mPlayerEntity;
        private List<GameObject> mEnemyRespawnPoints;
        private Dictionary<int, string> mEnemyID2Name;
        
        public void OnInitGameProcedure()
        {
            mPlayerEntity = GameWorld.TheGameWorld.PlayerGameObject.GetComponent<PlayerEntity>();
            
            mEnemyRespawnPoints = new List<GameObject>();
            mEnemyRespawnPoints.AddRange(GameObject.FindGameObjectsWithTag(FrameworkConstants.EnemyRespawnPointName));

            mEnemyID2Name = new Dictionary<int, string>()
            {
                {1,"EnemyEntityMeleeSmall"},
                {2,"EnemyBomberDrone"},
                {3,"EnemyDrone"}
            };
            // first wave
            SpawnEnemies();
        }
        
        public void OnEliminateEnemy()
        {
            --mCurrentEnemyNumber;
            // add points
            mPlayerEntity.AddKillPoints(10);
            if (mCurrentEnemyNumber <= 0)
            {
                SpawnEnemies();
            }
            
        }

        private void SpawnEnemies()
        {
            // set wave
            mPlayerEntity.SetWave();
            // spawn enemies
            mCurrentEnemyNumber = 
                mTotalEnemyNumber = 
                    Mathf.Clamp(mWave++, 1, 10);
            
            for (int i = 0; i < mTotalEnemyNumber; ++i)
            {
                int enemyType = Random.Range(1, 4);
                switch (enemyType)
                {
                    case 1:
                        EntityManager.Instance.AddEntity<EnemyEntityMelee>(
                            "EnemyEntityMeleeSmall",
                            mEnemyRespawnPoints[i].transform.position,
                            Quaternion.identity);
                        break;
                    case 2:
                        var bomberDrone = EntityManager.Instance.AddEntity<EnemyBomber>(
                            "EnemyBomberDrone",
                            mEnemyRespawnPoints[i].transform.position,
                            Quaternion.identity);
                        break;
                    case 3:
                        var drone = EntityManager.Instance.AddEntity<EnemyDrone>(
                            "EnemyDrone",
                            mEnemyRespawnPoints[i].transform.position,
                            Quaternion.identity);
                        break;
                }
            }
        }
        
        
    }

}
