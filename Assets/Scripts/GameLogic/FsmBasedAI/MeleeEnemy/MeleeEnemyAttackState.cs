using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Enemy;
using UnityEngine;

using FPS_Homework_Framework;

namespace FPS_Homework_Enemy_AI
{

    public class MeleeEnemyAttackState : EnemyBaseState
    {
        private EnemyEntityMelee mMeleeEnemyEntity;

        private const string mAttackParamName = "attack";
        
        // control leave state
        // delay 0.5s
        private bool mAttackMotionHasEnd;
        private float mAttackEndTime;

        // lerp speed to 0 when attack
        private float mSpeed;
        
        public override void OnInitState(FSM fsm)
        {
            base.OnInitState(fsm);
            
            mMeleeEnemyEntity = fsm.FSMEntity as EnemyEntityMelee;
            mMeleeEnemyEntity.OnMeleeAttackWeaponActive += 
                OnMeleeAttackWeaponActive;
            mMeleeEnemyEntity.OnMeleeAttackWeaponInactive += 
                OnMeleeAttackWeaponInactive;
            mMeleeEnemyEntity.OnMeleeAttackEnd +=
                OnAttackEnd;

        }
        
        public override void OnEnterState()
        {
            //Debug.LogError("Enter Attack State");
            
            mAttackMotionHasEnd = false;
            int attackType = Random.Range(1, 4);
            mAnimator.SetInteger(mAttackParamName, attackType);

            mSpeed = mAnimator.GetFloat("vertical");
        }

        public override void OnUpdateState()
        {
            if (mAttackMotionHasEnd)
            {
                if (Time.time - mAttackEndTime > 0.1f)
                {
                    // Attack once, waiting for some time
                    ChangeState(EnemyStateNames.HesitateState);
                    //ChangeState(EnemyStateNames.DeadState);
                }
            }

            if (mSpeed != 0)
            {
                mSpeed = Mathf.Lerp(mSpeed, 0, Time.deltaTime * 10);
                mAnimator.SetFloat("vertical", mSpeed);
            }
            
        }

        public override void OnLeaveState()
        {
            mAnimator.SetInteger(mAttackParamName,-1);
        }

        private void OnMeleeAttackWeaponActive()
        {
            //Debug.LogError("Weapon Active : " + mEntity.name);
        }

        private void OnMeleeAttackWeaponInactive()
        {
            //Debug.LogError("Weapon Inactive : " + mEntity.name);
        }

        private void OnAttackEnd()
        {
            mAttackMotionHasEnd = true;
            mAttackEndTime = Time.time;
        }
        
    }
    
}
