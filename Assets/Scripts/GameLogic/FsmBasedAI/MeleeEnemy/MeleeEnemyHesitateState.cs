using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Enemy_AI
{


    public class MeleeEnemyHesitateState : EnemyBaseState
    {
        private float mHesitateTime = 1.5f;
        
        private float mEnterStateTime;
        
        public override void OnEnterState()
        {
            mEnterStateTime = Time.time;
        }

        public override void OnUpdateState()
        {
            if (Time.time - mEnterStateTime >= mHesitateTime)
            {
                ChangeState(EnemyStateNames.DecisionState);
            }
        }

    }
    
    
}
