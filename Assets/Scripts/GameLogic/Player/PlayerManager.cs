
using System;
using UnityEngine;

namespace FPS_Homework_Player
{

    public class PlayerManager : MonoBehaviour
    {

        private PlayerLocomotionController mPlayerLocomotionController;
        private PlayerInputHandler mPlayerInputHandler;
        private PlayerWeaponController mPlayerWeaponController;

        private void Start()
        {
            mPlayerLocomotionController = GetComponent<PlayerLocomotionController>();
            mPlayerInputHandler = GetComponent<PlayerInputHandler>();
            mPlayerWeaponController = GetComponent<PlayerWeaponController>();
        }

        // update loop of player
        // todo:implement this in other place instead of update
        private void Update()
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
        }

        private void LateUpdate()
        {
            mPlayerInputHandler.ResetInputActionsInLateUpdate();
            mPlayerWeaponController.HandleWeaponsAnimationInLateUpdate();
        }
    }

}