using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FPS_Homework_Framework;

namespace FPS_Homework_Enemy_AI
{

    public class ShooterADecisionState : EnemyBaseState
    {
        public override void OnInitState(FSM fsm)
        {
            base.OnInitState(fsm);
        }

        public override void OnEnterState()
        {
            //Debug.LogError("Enter Decision State");
        }

        public override void OnUpdateState()
        {
            // check if player is in attack range
            if (IsPlayerInActionRange())
            {
                ChangeState(EnemyStateNames.AttackState);
            }
            else
            {
                ChangeState(EnemyStateNames.ChaseState);
            }
            
        }
        
        // 
        private bool IsPlayerInActionRange()
        {
            return DistanceBetweenPlayerOnXZPlane() <= 6.0f;
        }
        
    }

}
