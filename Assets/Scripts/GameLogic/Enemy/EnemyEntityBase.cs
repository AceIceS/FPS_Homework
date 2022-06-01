using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

using FPS_Homework_Framework;

namespace FPS_Homework_Enemy
{


    public class EnemyEntityBase : Entity
    {
        public float EntityTotalTotalHealth
        {
            get
            {
                return mTotalHealth;
            }
        }
        
        protected float mTotalHealth = 0;

        public float EntityCurrentHealth
        {
            get
            {
                return mCurrentHealth;
            }
            
        }
        protected float mCurrentHealth;
        // simple FSM based AI
        // EnemyEntityBase only holds data, and FSM controls it's behaviour
        protected FSM mFSM;
        
        protected EnemyUI mEnemyUI;
        protected override void Start()
        {
            OnInitFSM();
            PutIntoBattleGround();
            mEnemyUI = GetComponentInChildren<EnemyUI>();
            mEnemyUI.InitUI();
        }


        protected virtual void OnInitFSM()
        {
        }

        protected virtual void PutIntoBattleGround()
        {
            
        }

        public virtual void OnHit(float damage)
        {
            
        }
        
    }

}
