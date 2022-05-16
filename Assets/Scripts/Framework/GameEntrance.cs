
using UnityEditor;
using UnityEngine;

namespace FPS_Homework_Framework
{

    public class GameEntrance : MonoBehaviour
    {
        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
            
            EventManager.Instance.InitializeAtGameEntrance();
            ResourceManager.Instance.InitializeAtGameEntrance();
            
        }

        // when player click start button
        public void StartGame()
        {
            Debug.Log("Prepare Start...");
            
            StartGameInternal();
        }

        public void  QuitGame()
        {
            Debug.Log("Quit Game...");
            
            QuitGameInternal();
            // quit 
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
            
        }

        private void  StartGameInternal()
        {
            SceneManager.Instance.LoadSceneAsync(FrameworkConstants.TestSceneResPath,OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            // Init GameWorld Component
            GameWorld gw = gameObject.AddComponent<GameWorld>();
            gw.EnterGameWorld();
 
        }
        
        private void QuitGameInternal()
        {
            
        }
        
    }
    
}
