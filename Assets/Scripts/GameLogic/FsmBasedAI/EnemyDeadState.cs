using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace FPS_Homework_Enemy_AI
{

    public class EnemyDeadState : EnemyBaseState
    {
        public UnityAction<GameObject,Animator> OnEnemyEntityEnterDeadState;
        
        public override void OnEnterState()
        {
            if (OnEnemyEntityEnterDeadState != null)
            {
                OnEnemyEntityEnterDeadState.Invoke(mEntity, mAnimator);
            }
        }

        public override void OnUpdateState()
        {
            
        }

        public override void OnLeaveState()
        {
            // reset ?
            
        }
        
    }


}