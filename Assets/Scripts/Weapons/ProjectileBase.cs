using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Homework_Weapon
{

    public class ProjectileBase : ScriptableObject
    {
        public GameObject ProjectilePrefab;
        public float Damage;
        public float MaxAliveTime = 5.0f;

        public GameObject Owner { get; private set; }
        public Vector3 InitialPosition { get; private set; }
        public Vector3 InitialDirection { get; private set; }
        public Vector3 InheritedMuzzleVelocity { get; private set; }
        
        protected GameObject mNewProjectileInstance;

        public GameObject InstantiateProjectice(WeaponBase weapon, Vector3 pos, Quaternion quat)
        {
            mNewProjectileInstance = Instantiate(ProjectilePrefab,pos,quat);

            Owner = weapon.Owner;
            InitialPosition = pos;
            InitialDirection = quat.eulerAngles;
            InheritedMuzzleVelocity = weapon.MuzzleWorldVelocity;
            
            OnProjectileGenerated(weapon);

            return mNewProjectileInstance;
        }
        
        protected virtual void OnProjectileGenerated(WeaponBase weapon)
        {
        }


    }

}
