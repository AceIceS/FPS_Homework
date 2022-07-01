
using FPS_Homework_Framework;

namespace FPS_Homework_Enemy_AI
{

    // make decision: attack or chase player
    public class MeleeEnemyDecisionState : EnemyBaseState
    {

        public float DecisionDistance = 1.5f;
        
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
            return DistanceBetweenPlayerOnXZPlane() <= DecisionDistance;
        }

    }

}
