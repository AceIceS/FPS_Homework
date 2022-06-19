using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FPS_Homework_Framework;

namespace FPS_Homework_Enemy_AI
{

    public class EnemyChaseState : EnemyBaseState
    {
        public float Distance = 0;
        
        public override void OnInitState(FSM fsm)
        {
            base.OnInitState(fsm);
        }
        
        public override void OnEnterState()
        {
            //Debug.LogError("Enter Chase State");
            
            mNavMeshAgent.enabled = true;
            mNavMeshAgent.destination = mPlayer.transform.position;
            
            mEntity.transform.LookAt(mPlayer.transform);
        }
                
        public override void OnUpdateState()
        {
            mNavMeshAgent.destination = 
                GameWorld.TheGameWorld.PlayerGameObject.transform.position;
            
            Vector3 vel = mNavMeshAgent.velocity;
            float vert = vel.x;
            //float hor = vel.z;
            vert = Mathf.Clamp(vert, 0.5f, 1.0f);
            mAnimator.SetFloat("vertical", vert);
            //mAnimator.SetFloat("horizontal",hor);
            
            // face player
            mFsm.FSMEntity.transform.LookAt(mPlayer.transform);
            
            // check if player is in attack range
            if (IsNearPlayer())
            {
                ChangeState(EnemyStateNames.DecisionState);
            }
            
        }
      
        public override void OnLeaveState()
        {
            mNavMeshAgent.enabled = false;
            //mAnimator.SetFloat("vertical", 0);
        }
        
        // 
        private bool IsNearPlayer()
        {
            return DistanceBetweenPlayerOnXZPlane() <= Distance;
        }
    }

}
