using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FPS_Homework_Framework;
using FPS_Homework_Enemy;
 
namespace FPS_Homework_Enemy_AI
{


    public class DroneEnemyAttackState : EnemyBaseState
    {

        private EnemyWeaponController mWeaponController;
        
        private float mAttackTime = 0;
        private float mAttackTimeInterval = 2.0f;
        
        
        public override void OnInitState(FSM fsm)
        {
            base.OnInitState(fsm);
            mWeaponController = mEntity.GetComponent<EnemyWeaponController>();
            
        }
        
        public override void OnEnterState()
        {
            mAttackTime = Time.time;
        }

        public override void OnUpdateState()
        {
            if (DistanceBetweenPlayerOnXZPlane() >= 5.0f)
            {
                ChangeState(EnemyStateNames.DecisionState);    
            }

            mEntity.transform.LookAt(GameWorld.TheGameWorld.PlayerGameObject.transform);
            mWeaponController.AimTarget(GameWorld.TheGameWorld.PlayerGameObject);
            if (Time.time - mAttackTime < mAttackTimeInterval)
            {
                return;
            }

            mAttackTime = Time.time;
            //
            mWeaponController.OpenFire();
        }

        public override void OnLeaveState()
        {
            mWeaponController.LoseTarget();
        }
    }

}
