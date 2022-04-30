using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FPS_Homework_Framework
{

    public class GameWorld : MonoBehaviour
    {
        public static GameWorld TheGameWorld = null;
        
        
        private List<IGameFrameworkModule> mGameFrameworkModules;

        
        // Start is called before the first frame update
        private void Start()
        {
            Debug.LogError("Game World Start");
            TheGameWorld = this;
            foreach(IGameFrameworkModule module in mGameFrameworkModules)
            {
                module.OnGameStart();
            }
        }

        private void Update()
        {
            if (mGameFrameworkModules != null)
            {
                foreach(IGameFrameworkModule module in mGameFrameworkModules)
                {
                    module.UpdateModule();
                }
            }
        }

        private void LateUpdate()
        {
            if (mGameFrameworkModules != null)
            {
                foreach(IGameFrameworkModule module in mGameFrameworkModules)
                {
                    module.LateUpdateModule();
                }
            }
        }

        private void FixedUpdate()
        {
            if (mGameFrameworkModules != null)
            {
                foreach(IGameFrameworkModule module in mGameFrameworkModules)
                {
                    module.FixUpdateModule();
                }
            }
        }


        #region Utility Funcs

        public void EnterGameWorld()
        {
            Debug.Log("Enter Game World");
            
            // priority order
            mGameFrameworkModules = new List<IGameFrameworkModule>()
            {
                EntityManager.Instance
            };
            
            foreach(IGameFrameworkModule module in mGameFrameworkModules)
            {
                module.InitializeModuleBeforeOnStart();
            }

        }
        
        
        #endregion


    }

}
