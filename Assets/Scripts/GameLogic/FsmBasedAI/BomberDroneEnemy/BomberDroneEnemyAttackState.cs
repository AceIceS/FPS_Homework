using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FPS_Homework_Framework;

namespace FPS_Homework_Enemy_AI
{


    public class BomberDroneEnemyAttack : EnemyBaseState
    {
        private float mEnterTime = 0;
        
        public override void OnEnterState()
        {
            mEnterTime = Time.time;
            //
            ResourceManager.Instance.GenerateFxAt("BomberDronePrepare",
                mFsm.FSMEntity.transform.position + new Vector3(0,1.6f,0), 
                Quaternion.identity,0.8f);
        }

        public override void OnUpdateState()
        {
            if (Time.time - mEnterTime > 0.8f)
            {
                ChangeState(EnemyStateNames.DeadState);
            }
            
        }

    }

}
