
using UnityEngine;

namespace FPS_Homework_GamePlay
{

    public class GameUtilites : MonoBehaviour
    {
        // 
        public static GameUtilites Instance = null;

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            OnGameInitialize();
        }

        private void OnGameInitialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // load runtime particle prefabs
            FPS_Homework_Utils.RuntimeParticlesManager.Instance.LoadParticlesPrefab();
        }
        
    }

}