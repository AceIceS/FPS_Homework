using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FPS_Homework_Framework
{
 
    public class SceneManager : BaseManager<SceneManager>
    {
        private Slider mLoadingSlider;
        
        public void LoadSceneAsync(string scenePath,UnityAction callback)
        {
            // firstly go to loading scene
            FrameworkRuntimeUtilities.StartACoroutine(LoadSceneAsyncInternal(scenePath,callback));
        }

        private IEnumerator LoadSceneAsyncInternal(string scenePath,UnityAction callback)
        {
            // the loading scene 
            UnityEngine.SceneManagement.SceneManager.LoadScene(FrameworkConstants.LoadingSceneResPath);
            yield return null;
            // register loading event for slider
            EventManager.Instance.RegisterEvent(new Event(EventID.LOADING,this,
                new EventArgsOneFloat(0)),OnLoadingSceneAsync);
            
            // pretend loading something because current scene is too small
            yield return new WaitForSeconds(1.0f);
            // load target scene
            AsyncOperation op =
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Single);
            yield return null;
            
            while (op.isDone == false)
            {
                // trigger event for loading scene UI
                EventManager.Instance.TriggerEventWithEventIdAndArgs(
                    EventID.LOADING,new EventArgsOneFloat(op.progress));
                
                yield return new WaitForSeconds(1.0f);
            }

            yield return new WaitForSeconds(1.0f);
            callback?.Invoke();
        }

        private void OnLoadingSceneAsync(object sender, EventArgs args)
        {
            if (mLoadingSlider == null)
            {
                GameObject go = GameObject.Find("LoadGameSlider");
                mLoadingSlider = go.GetComponent<Slider>();
            }

            mLoadingSlider.value = ((EventArgsOneFloat) args).floatArg;
            
        }
        
    }

}
