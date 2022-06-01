using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Framework
{

    public class FrameworkRuntimeUtilities : MonoBehaviour
    {
        private static FrameworkRuntimeUtilities mFrameworkRuntimeUtilities = null;

        private void OnEnable()
        {
            mFrameworkRuntimeUtilities = this;
        }

        private void OnDisable()
        {
            mFrameworkRuntimeUtilities = null;
        }
        
        public static Coroutine StartACoroutine(IEnumerator routine)
        {
            Coroutine co = null;
            if (mFrameworkRuntimeUtilities != null)
            {
                co = ((MonoBehaviour) mFrameworkRuntimeUtilities).StartCoroutine(routine);
            }
            return co;
        }
        public static Coroutine StartACoroutine(string methodName, object value)
        {
            Coroutine co = null;
            if (mFrameworkRuntimeUtilities != null)
            {
                co = ((MonoBehaviour) mFrameworkRuntimeUtilities).StartCoroutine(methodName,value);
            }
            return co;
        }
        
        public static Coroutine StartACoroutine(string methodName)
        {
            Coroutine co = null;
            if (mFrameworkRuntimeUtilities != null)
            {
                co = ((MonoBehaviour) mFrameworkRuntimeUtilities).StartCoroutine(methodName);
            }
            return co;
        }

    }


}