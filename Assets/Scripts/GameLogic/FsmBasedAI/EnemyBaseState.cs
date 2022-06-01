using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

using FPS_Homework_Framework;


namespace FPS_Homework_Enemy_AI
{

    public class EnemyBaseState : FSMState
    {
        protected GameObject mPlayer;
        protected GameObject mEntity;
        protected Animator mAnimator;
        protected NavMeshAgent mNavMeshAgent;

        public override void OnInitState(FSM fsm)
        {
            base.OnInitState(fsm);
            
            mPlayer = GameWorld.TheGameWorld.PlayerGameObject;
            mEntity = mFsm.FSMEntity.gameObject;
            
            mAnimator = fsm.FSMEntity.GetComponent<Animator>();
            mNavMeshAgent = fsm.FSMEntity.GetComponent<NavMeshAgent>();
            
        }

        protected float DistanceBetweenPlayerOnXZPlane()
        {
            Vector3 dir = mPlayer.transform.position - mEntity.transform.position;
            dir.y = 0;
            return dir.magnitude;    
        }
        
    }

}