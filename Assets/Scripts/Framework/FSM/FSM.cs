using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Framework
{


    public class FSM
    {
        public Entity FSMEntity
        {
            get
            {
                return mEntity;
            }
        }
        
        private Entity mEntity;
        private FSMState mCurrentState;
        private Dictionary<string, FSMState> mStateName2State;

        // create new FSM for an entity.
        public static FSM CreateFSM(Entity entity,List<FSMState> states)
        {
            FSM fsm = new FSM(entity);
            foreach (var state in states)
            {
                fsm.mStateName2State.Add(state.StateName, state);
                state.OnInitState(fsm);
            }

            return fsm;
        }
        
        // private
        private FSM(Entity entity)
        {
            mEntity = entity;
            mStateName2State = new Dictionary<string, FSMState>();
        }
        
        // start this FSM
        public void StartFSM(string startStateName)
        {
            FSMState startState = GetState(startStateName);
            if (startState != null)
            {
                mCurrentState = startState;
                mCurrentState.OnEnterState();
            }
            
        }
        
        // update each frame
        public void UpdateFSM()
        {
            if (mCurrentState != null)
            {
                mCurrentState.OnUpdateState();
            }
        }

        public FSMState GetState(string stateName)
        {
            FSMState state = null;
            mStateName2State.TryGetValue(stateName, out state);
            return state;
        }

        public void ChangeState(string targetStateName)
        {
            if (mCurrentState == null)
            {
                Debug.LogError("current state is null");
                return;
            }

            FSMState newState = null;
            if (!mStateName2State.TryGetValue(targetStateName, out newState))
            {
                Debug.LogError("target state is null");
                return;
            }
            
            mCurrentState.OnLeaveState();
            mCurrentState = newState;
            mCurrentState.OnEnterState();
            
        }
        
    }

}
