
using System;
using FPS_Homework_Framework;
using FPS_Homework_Weapon;
using UnityEngine;

namespace FPS_Homework_Player
{

    public class PlayerEntity : Entity
    {
        
        private PlayerInputHandler mPlayerInputHandler;
        private PlayerLocomotionController mPlayerLocomotionController;
        private PlayerWeaponController mPlayerWeaponController;
        private PlayerHUD mPlayerHUD;

        protected override void Start()
        {
            mPlayerLocomotionController = GetComponent<PlayerLocomotionController>();
            mPlayerInputHandler = GetComponent<PlayerInputHandler>();
            mPlayerWeaponController = GetComponent<PlayerWeaponController>();
            mPlayerHUD = GetComponent<PlayerHUD>();
            
        }

        public override void UpdateEntity()
        {
            // read input 
            float deltaTime = Time.deltaTime;                                         
            // Handle Raw Input Values
            mPlayerInputHandler.HandleRawInputs(deltaTime);
            // weapon reactions:aim,fire
            // handle weapon first because weapon recoil may affect camera
            mPlayerWeaponController.HandlePlayerWeapons();
            // handle reactions:walk,run,jump
            mPlayerLocomotionController.HandlePlayerLocomotion();
            // handle UI update
            mPlayerHUD.OnUpdatePlayerHUD();
            
        }

        public override void FixUpdateEntity()
        {
            
        }


        public override void LateUpdateEntity()
        {
            mPlayerInputHandler.ResetInputActionsInLateUpdate();
            mPlayerWeaponController.HandleWeaponsAnimationInLateUpdate();
        }

        public bool AddWeapon(WeaponBase weapon)
        {
            //mPlayerHUD.OnChangeWeaponCrossHair(weapon.CrossHairPrefabName);
            return mPlayerWeaponController.AddWeaponToPlayer(weapon);    
        }

        public bool AddWeaponAmmo(int amount, WeaponType weaponType)
        {
            return mPlayerWeaponController.AddWeaponAmmo(amount, weaponType);
        }
        
    }
    

}
