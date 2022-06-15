using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using FPS_Homework_Item;
using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace FPS_Homework_Framework
{


    public class ResourceManager : BaseManager<ResourceManager>
    {
        [Serializable]
        public class ResPathInfo
        {
            public List<string> ResourcePath = new List<string>(){};
            public List<string> ResourceName = new List<string>(){};
        }
        
        
        private ResPathInfo mResPathInfo;
        private Dictionary<string, UnityObject> mResName2GameObject;

        public void InitializeAtGameEntrance()
        {
            mResName2GameObject = new Dictionary<string, UnityObject>();
            mResPathInfo = new ResPathInfo();
            
            // Deserialize json 
            using (StreamReader sr = new StreamReader(Application.dataPath +
                                 FrameworkConstants.ResInfoJsonPath))
            {
                string str = sr.ReadToEnd();
                //Debug.LogError(str);
                JsonUtility.FromJsonOverwrite(str, mResPathInfo);
            }
            
            // preload resources
            for (int i = 0; i < mResPathInfo.ResourceName.Count; ++i)
            {
                UnityObject fxPrefab = Resources.Load<UnityObject>(mResPathInfo.ResourcePath[i]);
                if (fxPrefab != null)
                {
                    mResName2GameObject.Add(mResPathInfo.ResourceName[i], fxPrefab);
                }
                else
                {
                    Debug.LogError("resource not found : " + mResPathInfo.ResourceName[i]);
                }
                
            }
            
        }
        
        #region Create Resource Instances
        
        // entity prefab
        public GameObject SpawnEntityInstanceCopy(string name, Vector3 pos, Quaternion quat)
        {
            GameObject newInstance = null;

            if (mResName2GameObject.TryGetValue(name, out UnityObject go))
            {
                newInstance = GameObject.Instantiate(go as GameObject, pos, quat);
                newInstance.name = name + "(clone)";
            }
            
            return newInstance;
        }
        
        // particles
        public bool GenerateFxAt(string fxName, Vector3 pos,
            Quaternion rot, float aliveTime, Transform parent = null)
        {

            if (mResName2GameObject.TryGetValue(fxName, out UnityObject fxPrefab))
            {
                GameObject fxInstance = parent == null
                    ? UnityEngine.Object.Instantiate(fxPrefab as GameObject, pos, rot)
                    : UnityEngine.Object.Instantiate(fxPrefab as GameObject, pos, rot, parent);
                fxInstance.name = fxName + "clone";
                if (parent != null)
                {
                    fxInstance.transform.localPosition = pos;
                    fxInstance.transform.localRotation = rot;
                }
                
                if (aliveTime > 0)
                {
                    UnityEngine.Object.Destroy(fxInstance, aliveTime);
                }
            }
            
            return false;
            
        }

        
        public ScriptableObject GetScriptableObjectCopy(string name)
        {
            return ScriptableObject.Instantiate(GetScriptableObjectRef(name));
        }

        public ScriptableObject GetScriptableObjectRef(string name)
        {
            UnityObject obj = null;
            mResName2GameObject.TryGetValue(name, out obj);
            return obj as ScriptableObject;
        }

        public Texture2D GetTexture2DRef(string name)
        {
            UnityObject texture = null;
            mResName2GameObject.TryGetValue(name, out texture);
            return  texture as Texture2D;
        }

        public GameObject GetGameObjectCopy(string name)
        {
            UnityObject obj = null;
            mResName2GameObject.TryGetValue(name, out obj);
            return GameObject.Instantiate(obj) as GameObject;
        }
        

        #endregion


    }
    
    
}
