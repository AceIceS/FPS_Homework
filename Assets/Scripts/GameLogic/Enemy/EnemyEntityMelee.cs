using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Enemy_AI;
using FPS_Homework_Framework;
using UnityEngine;
using UnityEngine.Events;


namespace FPS_Homework_Enemy
{

    public class EnemyEntityMelee : EnemyEntityBase
    {
        public UnityAction OnMeleeAttackWeaponActive;
        public UnityAction OnMeleeAttackWeaponInactive;
        public UnityAction OnMeleeAttackEnd;
        public UnityAction OnEntityDead;

        protected override void Start()
        {
            base.Start();
            mCurrentHealth = mTotalHealth = 100.0f;
        }

        protected override void OnInitFSM()
        {
            var decisionState = new MeleeEnemyDecisionState();
            decisionState.StateName = "Decision";
            var chaseState = new MeleeEnemyChaseState();
            chaseState.StateName = "Chase";
            var attackState = new MeleeEnemyAttackState();
            attackState.StateName = "Attack";
            var deadState = new EnemyDeadState();
            deadState.StateName = "Dead";
            deadState.OnEnemyEntityEnterDeadState += OnEntityEnterDeadState;
            var hesitateState = new MeleeEnemyHesitateState();
            hesitateState.StateName = "Hesitate";
            
            mFSM = FSM.CreateFSM(this,new List<FSMState>()
            {
                decisionState,
                chaseState,
                attackState,
                deadState,
                hesitateState
            });
            
        }
        
        
        
        #region Animation Evnet
        
        // fire by Animation Event
        private void AnimationEventMeleeWeaponActive()
        {
            if (OnMeleeAttackWeaponActive != null)
            {
                OnMeleeAttackWeaponActive.Invoke();
            }
        }

        private void AnimationEventMeleeWeaponInactive()
        {
            if (OnMeleeAttackWeaponInactive != null)
            {
                OnMeleeAttackWeaponInactive.Invoke();
            }
        }

        private void AnimationEventMeleeAttackEnd()
        {
            if (OnMeleeAttackEnd != null)
            {
                OnMeleeAttackEnd.Invoke();
            }
            
        }

        private void AnimationEventDeadAnimationPlayEnd()
        {
            //Debug.LogError("Dead");
            Vector3 pos = gameObject.transform.position;
            
            ResourceManager.Instance.GenerateFxAt("ExplosionBody",
                pos + new Vector3(0,0.66f,0),Quaternion.identity,3.0f);
            ResourceManager.Instance.GenerateFxAt("ExplosionBodyBloody",
                pos + new Vector3(0,0.66f,0),Quaternion.identity,3.0f);
            gameObject.SetActive(false);
            
            if (OnEntityDead != null)
            {
                OnEntityDead.Invoke();
            }
        }
        
        private void OnEntityEnterDeadState(GameObject entity, Animator animator)
        {
            animator.SetBool("dead", true);
            // TODO:set ragdoll
            
        }
        
        
        #endregion
        
    }

}
