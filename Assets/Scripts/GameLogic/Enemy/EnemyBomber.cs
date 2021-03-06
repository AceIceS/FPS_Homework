using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using FPS_Homework_Framework;
using FPS_Homework_Enemy_AI;
using FPS_Homework_Player;

namespace FPS_Homework_Enemy
{


    public class EnemyBomber : EnemyEntityBase
    {
        protected override void Start()
        {
            base.Start();
            mCurrentHealth = mTotalHealth = 80.0f;
        }

        protected override void OnInitFSM()
        {
            
            var chaseState = new EnemyChaseState();
            chaseState.Distance = 2.5f;
            chaseState.StateName = "Chase";
            chaseState.NextStateName = EnemyStateNames.AttackState;
            var attackState = new BomberDroneEnemyAttack();
            attackState.StateName = "Attack";
            var deadState = new EnemyDeadState();
            deadState.StateName = "Dead";
            deadState.OnEnemyEntityEnterDeadState += OnEntityEnterDeadState;

            mFSM = FSM.CreateFSM(this,new List<FSMState>()
            {
                chaseState,
                attackState,
                deadState
            });
            
        }
        
        protected override void PutIntoBattleGround()
        {
            mFSM.StartFSM(EnemyStateNames.ChaseState);
        }

        private void OnEntityEnterDeadState(GameObject entity, Animator animator)
        {
            // fx
            gameObject.SetActive(false);
            ResourceManager.Instance.GenerateFxAt("BomberExplosion",
                this.transform.position + new Vector3(0,1.6f,0), 
                Quaternion.identity,2.0f);
            
            // do damage
            GameObject player = GameWorld.TheGameWorld.PlayerGameObject;
            if (player != null &&
                (player.transform.position - this.transform.position).magnitude <= 2.5f)
            {
                PlayerEntity p = player.GetComponent<PlayerEntity>();
                if (p != null)
                {
                    p.OnDamaged(30.0f);
                }
            }
            
            GameWorld.TheGameWorld.GameProcedure.OnEliminateEnemy();
            EntityManager.Instance.DestroyEntity(ID);
            
        }
        
    }

}
