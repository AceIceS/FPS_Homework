using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FPS_Homework_Framework;

using FPS_Homework_Enemy_AI;

namespace FPS_Homework_Enemy
{

    public class EnemyEntityShooterA : EnemyEntityBase
    {

        protected override void Start()
        {
            base.Start();
            mCurrentHealth = mTotalHealth = 200.0f;
        }

        protected override void OnInitFSM()
        {
            var decisionState = new ShooterADecisionState();
            decisionState.StateName = "Decision";
            var chaseState = new EnemyChaseState();
            chaseState.Distance = 6.0f;
            chaseState.StateName = "Chase";
            var attackState = new ShooterAAttackState();
            attackState.StateName = "Attack";
            //var throwGrenadeState = new ShooterAThrowGrenadeState();
            //throwGrenadeState.StateName = "ShooterAThrowGrenade";
            var deadState = new EnemyDeadState();
            deadState.StateName = "Dead";
            deadState.OnEnemyEntityEnterDeadState += OnEntityEnterDeadState;
            var hesitateState = new EnemyHesitateState();
            hesitateState.HesitateTime = 1.5f;
            hesitateState.StateName = "Hesitate";
            
            mFSM = FSM.CreateFSM(this,new List<FSMState>()
            {
                decisionState,
                chaseState,
                attackState,
                //throwGrenadeState,
                deadState,
                hesitateState
            });
            
        }


        private void OnEntityEnterDeadState(GameObject entity, Animator an)
        {
            
        }
        
        
    }

}
