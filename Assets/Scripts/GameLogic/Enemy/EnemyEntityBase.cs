
using FPS_Homework_Framework;
using FPS_Homework_Enemy_AI;

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

        protected virtual void InitStatus()
        {
            
        }
        
        protected virtual void PutIntoBattleGround()
        {
            InitStatus();
            mFSM.StartFSM(EnemyStateNames.DecisionState);
        }

        public override void UpdateEntity()
        {
            mFSM.UpdateFSM();
            
            if (mEnemyUI != null)
            {
                mEnemyUI.OnUpdateEnemyUI();
            }

        }

        public void OnHit(float damage)
        {
            mCurrentHealth -= damage;
            if (mCurrentHealth <= 0)
            {
                mFSM.ChangeState(EnemyStateNames.DeadState);
            }
        }
        
    }

}
