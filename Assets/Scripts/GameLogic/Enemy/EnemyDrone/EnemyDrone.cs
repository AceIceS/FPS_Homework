using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FPS_Homework_Framework;
using FPS_Homework_Enemy_AI;

namespace FPS_Homework_Enemy
{

    public class EnemyDrone : EnemyEntityBase
    {
        
        protected override void Start()
        {
            base.Start();
            mCurrentHealth = mTotalHealth = 300.0f;
        }

        protected override void OnInitFSM()
        {
            
            var decisionState = new MeleeEnemyDecisionState();
            decisionState.DecisionDistance = 5.0f;
            decisionState.StateName = "Decision";
            var chaseState = new DroneEnemyChaseState();
            chaseState.Distance = 5f;
            chaseState.StateName = "Chase";
            chaseState.FreezeRotationXZ = false;
            chaseState.NextStateName = EnemyStateNames.DecisionState;
            var attackState = new DroneEnemyAttackState();
            attackState.StateName = "Attack";
            var deadState = new EnemyDeadState();
            deadState.StateName = "Dead";
            deadState.OnEnemyEntityEnterDeadState += OnEntityEnterDeadState;

            mFSM = FSM.CreateFSM(this,new List<FSMState>()
            {
                decisionState,
                chaseState,
                attackState,
                deadState,
            });
            
        }

        private void OnEntityEnterDeadState(GameObject entity, Animator animator)
        {
            gameObject.SetActive(false);
            ResourceManager.Instance.GenerateFxAt("BomberExplosion",
                this.transform.position + new Vector3(0,1.6f,0), 
                Quaternion.identity,2.0f);

            EntityStatus = EntityStatus.Inactive;
            GameWorld.TheGameWorld.GameProcedure.OnEliminateEnemy();
            EntityManager.Instance.DestroyEntity(ID);
        }

        
    }

}