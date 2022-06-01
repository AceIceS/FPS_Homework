
namespace FPS_Homework_Framework
{

    public abstract class FSMState
    {
        public string StateName
        {
            get
            {
                return string.IsNullOrEmpty(mStateName)
                    ? this.GetType().ToString()
                    : mStateName;
            }
            set
            {
                mStateName = value;
            }
        }
        
        
        private string mStateName;
        protected FSM mFsm;

        #region State funcs
        
        // calls when create
        public virtual void OnInitState(FSM fsm)
        {
            mFsm = fsm;
        }
        
        public virtual void OnEnterState()
        {
            
        }

        public virtual void OnUpdateState()
        {
            
        }

        public virtual void OnLeaveState()
        {
            
        }

        public void ChangeState(string stateName)
        {
            mFsm.ChangeState(stateName);
        }
        
        #endregion
        
    }


}