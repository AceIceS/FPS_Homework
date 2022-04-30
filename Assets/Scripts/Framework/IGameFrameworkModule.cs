using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Framework
{
    
    public interface IGameFrameworkModule
    {
        void OnGameStart();
        // 
        void InitializeModuleBeforeOnStart();
        void UpdateModule();
        void FixUpdateModule();
        void LateUpdateModule();
        
        
    }

}
