using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Enemy_AI
{


    public class EnemyHesitateState : EnemyBaseState
    {
        public float HesitateTime;
        
        private float mEnterStateTime;
        
        public override void OnEnterState()
        {
            mEnterStateTime = Time.time;
        }

        public override void OnUpdateState()
        {
            if (Time.time - mEnterStateTime >= HesitateTime)
            {
                ChangeState(EnemyStateNames.DecisionState);
            }
        }

    }
    
    
}
