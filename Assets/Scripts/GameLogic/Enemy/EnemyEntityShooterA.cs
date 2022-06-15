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
            var decisionState = new MeleeEnemyDecisionState();
            decisionState.StateName = "Decision";
            var chaseState = new MeleeEnemyChaseState();
            chaseState.StateName = "Chase";
            var attackState = new ShooterAAttackState();
            attackState.StateName = "Attack";
            var deadState = new EnemyDeadState();
            deadState.StateName = "Dead";
            var hesitateState = new MeleeEnemyHesitateState();
            hesitateState.StateName = "Hesitate";
            
            mFSM = FSM.CreateFSM(this,new List<FSMState>()
            {
                decisionState,
                chaseState,
                attackState,
                deadState,
                hesitateState
            });
            
        }
        
    }

}
