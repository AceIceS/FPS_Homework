using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Enemy;
using UnityEngine;

using FPS_Homework_Framework;

namespace FPS_Homework_Enemy_AI
{

    public class DroneEnemyChaseState : EnemyChaseState
    {

        private EnemyWeaponController mWeaponController;
        
        public override void OnEnterState()
        {
            base.OnEnterState();

            if (mWeaponController == null)
            {
                mWeaponController = mEntity.GetComponent<EnemyWeaponController>();
            }
        }
                
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            mWeaponController.AimTarget(GameWorld.TheGameWorld.PlayerGameObject);
        }
      
        public override void OnLeaveState()
        {
            base.OnLeaveState();
            mWeaponController.LoseTarget();
        }
        
    }

}
