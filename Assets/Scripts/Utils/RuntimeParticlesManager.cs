using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace FPS_Homework_Utils
{

    [Serializable]
    public class RuntimeParticlesResPathInfo
    {
        public List<string> FxPrefabPath = new List<string>(){};
        public List<string> FxName = new List<string>(){};
    }
    
    // A singleton class to manage particles
    public class RuntimeParticlesManager
    {
        public static RuntimeParticlesManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new RuntimeParticlesManager();
                }
                return mInstance;
            }
        }
        
        private static RuntimeParticlesManager mInstance = null;
        private RuntimeParticlesResPathInfo mRuntimeParticlesResPathInfo;
        private Dictionary<string, GameObject> mParticleName2GameObject;
        
        private RuntimeParticlesManager()
        {
            mParticleName2GameObject = new Dictionary<string, GameObject>();
            mRuntimeParticlesResPathInfo = new RuntimeParticlesResPathInfo();
            // Deserialize json 
            StreamReader sr = new StreamReader(Application.dataPath + 
                                               "/Scripts/Utils/RuntimeParticlesResPath.json");
            string str = sr.ReadToEnd();
            //Debug.LogError(str);
            JsonUtility.FromJsonOverwrite(str, mRuntimeParticlesResPathInfo);
        }

        public void LoadParticlesPrefab()
        {
            for (int i = 0; i < mRuntimeParticlesResPathInfo.FxPrefabPath.Count; ++i)
            {
                GameObject fxPrefab = Resources.Load<GameObject>(mRuntimeParticlesResPathInfo.FxPrefabPath[i]);
                if (fxPrefab != null)
                {
                    mParticleName2GameObject.Add(mRuntimeParticlesResPathInfo.FxName[i], fxPrefab);
                }
                else 
                    Debug.LogError("null");
            }
        }
        
        public bool GenerateFxAt(string fxName, Vector3 pos,
            Quaternion rot, float aliveTime, Transform parent = null)
        {
            GameObject fxPrefab = null;
            if (mParticleName2GameObject.TryGetValue(fxName, out fxPrefab))
            {
                GameObject fxInstance = parent == null
                    ? UnityEngine.Object.Instantiate(fxPrefab, pos, rot)
                    : UnityEngine.Object.Instantiate(fxPrefab, pos, rot, parent);

                if (aliveTime > 0)
                {
                    UnityEngine.Object.Destroy(fxInstance, aliveTime);
                }
            }
            
            return false;
        }   
    }


}