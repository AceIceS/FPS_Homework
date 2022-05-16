
using System;
using UnityEngine;

using FPS_Homework_Framework;
using FPS_Homework_Item;

using FPS_Homework_Utils;


namespace FPS_Homework_Player
{

    public class PlayerLocomotionController : MonoBehaviour
    {
        
        #region Fields

        public Camera PlayerMainCamera;
        // Input Control Parameters
        [Header("Properties")]
        public float Gravity = 15f;
        public float VerticalRotateSpeed = 1.2f;
        public float HorizontalRotateSpeed = 1.2f;
        public bool IsInvertY = true;
        public float MoveSpeed = 10.0f;
        public float MoveAccelerate = 5.0f;
        public float SpeedUpMultiplier = 1.5f;
        public float JumpImpulse = 6.0f;
        public float MoveSpeedInAir = 10.0f;
        public float MoveAccelerateInAir = 4;
        [Header("Sideways Settings")] 
        public float HeadTranslateOffset;
        public float HeadRotationOffset;
        public float PlayerSidewaysRotateVelocity = 6.0f;
        public float PlayerSidewaysTranslateVelocity = 6.0f;
        
        public Vector3 PlayerVelocity;
        [Header("Shake Camera as Player Run Settins")]
        public float BobFrequency = 5f;
        public float BobHorizontalAmplitude = 0.2f;
        public float BobVerticalAmplitude = 0.1f;
        public float HeadBobSmoothing = 0.1f;
        public float RunningBobMultiper = 1.5f;
        
        // components
        private PlayerEntity mPlayerEntity;
        private PlayerInputHandler mPlayerInputHandler;
        private CharacterController mCharacterController;
        private PlayerWeaponController mPlayerWeaponController;
        private PlayerHUD mPlayerHUD;
        
        [SerializeField]
        private Transform mHead;
        
        // input
        private float mCameraVerticalAngle = 0;
        private float mCameraHorizontalAngle = 0;
        // player status
        private Vector3 mGroundNormal;
        [SerializeField]
        private bool mIsOnGround = true;
        private float mGroundCheckDis = 0.05f;
        private float mGroundCheckDisInAir = 0.07f;
        private float mJumpGroundPreventionTime = 0.2f;
        private Vector3 mLastImpactSpeed;
        
        private float mLastJumpTime;

        private float mCurrentSidewaysRotateOffset;
        private float mCurrentSidewaysRotateTargetOffset;
        private Vector3 mHeadOriginPostition;
        private float mCurrentSidewaysTranslateOffset;
        private float mCurrentSidewaysTranslateTargetOffset;
        // -1:left, 0 mid, 1:right
        [SerializeField]
        private int mCurrentHeadStatus = 0;

        private Vector3 mCameraBobTargetPosition;
        #endregion

        // Called By PlayerManager Each Frame
        public void HandlePlayerLocomotion()
        {
            // ground
            OnGroundCheck();
            // Handle Input Reactions
            HandlePlayerLocomotionsInternal();
            // Interactable object
            CheckForInteractableObject();
        }
        
        private void Start()
        {
            mHeadOriginPostition = mHead.transform.localPosition;

            mPlayerEntity = GetComponent<PlayerEntity>();
            
            mPlayerInputHandler = GetComponent<PlayerInputHandler>();
            mPlayerInputHandler.OnSidewayAction += OnPlayerSidewayAction;
            
            mCharacterController = GetComponent<CharacterController>();
            mCharacterController.enableOverlapRecovery = true;
            mPlayerWeaponController = GetComponent<PlayerWeaponController>();

            mPlayerHUD = GetComponent<PlayerHUD>();
            
            // reset
            mCharacterController.height = 1.8f;
            mCharacterController.center = Vector3.up * mCharacterController.height * 0.5f;
    
        }
        
        private void OnDisable()
        {
            if (mPlayerInputHandler != null)
            {
                mPlayerInputHandler.OnSidewayAction -= OnPlayerSidewayAction;
            }
        }

        private void OnGroundCheck()
        {
            float checkDis = mIsOnGround
                ? mCharacterController.skinWidth + mGroundCheckDis
                : mGroundCheckDisInAir;
            
            mIsOnGround = false;
            mGroundNormal = Vector3.up;
            
            // stop check for a while when player jump
            if (Time.time >= mLastJumpTime + mJumpGroundPreventionTime)
            {
                if (Physics.CapsuleCast(CharacterCapsuleBottomHemiSphere(),
                    CharacterCapsuleTopHemiSphere(), mCharacterController.radius,
                    Vector3.down, out RaycastHit hit, 
                    checkDis, -1, QueryTriggerInteraction.Ignore))
                {
                    mGroundNormal = hit.normal;
                    if (Vector3.Dot(hit.normal, transform.up) > 0 &&
                        Vector3.Angle(transform.up, hit.normal) <= mCharacterController.slopeLimit)
                    {
                        mIsOnGround = true;
                        // correct player position to the ground
                        if (hit.distance > mCharacterController.skinWidth)
                        {
                            mCharacterController.Move(Vector3.down * hit.distance);
                        }
                    }
                }
            }
            
        }
        
        private void HandlePlayerLocomotionsInternal()
        {
            HandleCameraRotation();
            OnPlayerMove();
        }
        
        private void HandleCameraRotation()
        {
            // rotate head
            OnCameraRotate();
            OnPlayerSideWays();
            // change camera directly
            ShakeCameraAsPlayerMove();
        }
        
        private void OnCameraRotate()
        {
            /*
            // Horizontal
            transform.Rotate(
                new Vector3(0, 
                    mPlayerInputHandler.MouseX * HorizontalRotateSpeed, 
                    0), Space.Self);
            // Vertical
            mCameraVerticalAngle += ( IsInvertY ? -1.0f : 1.0f ) *
                                    mPlayerInputHandler.MouseY * VerticalRotateSpeed;
            mCameraVerticalAngle = 
                Mathf.Clamp(mCameraVerticalAngle, -90, 90);
            mHead.localEulerAngles =
                new Vector3(mCameraVerticalAngle, 0, 0) + 
                mPlayerWeaponController.WeaponRecoilOffsets;
            */
            
            // Horizontal
            mCameraHorizontalAngle += 
                mPlayerInputHandler.MouseX * Time.deltaTime * HorizontalRotateSpeed;
            transform.localRotation = Quaternion.Euler(0,
                mCameraHorizontalAngle,
                0);
            // Vertical
            // Input
            float inputY = ( IsInvertY ? -1.0f : 1.0f ) *
                mPlayerInputHandler.MouseY * Time.deltaTime * VerticalRotateSpeed;
            // consider weapon recoil
            if (mPlayerWeaponController.WeaponTotalRecoilOffset.x > 0 &&
                mPlayerInputHandler.MouseY < 0)
            {
                mPlayerWeaponController.WeaponTotalRecoilOffset.x += 
                    inputY;
            }
            else
            {
                mCameraVerticalAngle += inputY;
            }
            mPlayerWeaponController.RecoverWeaponRecoil();

            mHead.localRotation = Quaternion.Euler(
                Mathf.Clamp(mCameraVerticalAngle + mPlayerWeaponController.WeaponRecoilOffsets.x,
                    -90.0f,90.0f),
                mPlayerWeaponController.WeaponRecoilOffsets.y,
                mPlayerWeaponController.WeaponRecoilOffsets.z);
            
        }
        
        private void OnPlayerSideWays()
        {
            // lerp 
            mCurrentSidewaysRotateOffset = Mathf.Lerp(
                mCurrentSidewaysRotateOffset, 
                mCurrentSidewaysRotateTargetOffset,
                PlayerSidewaysRotateVelocity * Time.deltaTime);
            mCurrentSidewaysTranslateOffset = Mathf.Lerp(
                mCurrentSidewaysTranslateOffset,
                mCurrentSidewaysTranslateTargetOffset, 
                PlayerSidewaysTranslateVelocity * Time.deltaTime);
            // set
            mHead.transform.Rotate(
                0,0,mCurrentSidewaysRotateOffset,Space.Self);
            //Vector3 translateOffset = 
            //    mHead.transform.TransformVector(
            //        mCurrentSidewaysTranslateOffset, 0, 0);
            //Debug.LogError(translateOffset);
            mHead.transform.localPosition = mHeadOriginPostition +
                                            new Vector3(
                                                mCurrentSidewaysTranslateOffset, 0, 0);
        }

        private void OnPlayerSidewayAction(bool isRight)
        {
            switch (mCurrentHeadStatus)
            {
                case -1:
                    if (isRight)
                    {
                        mCurrentSidewaysRotateTargetOffset = -HeadRotationOffset;
                        mCurrentSidewaysTranslateTargetOffset = HeadTranslateOffset;
                        mCurrentHeadStatus = 1;
                    }
                    else
                    {
                        mCurrentSidewaysRotateTargetOffset = 0;
                        mCurrentSidewaysTranslateTargetOffset = 0;
                        mCurrentHeadStatus = 0;
                    }
                    break;
                case 0:
                    if (isRight)
                    {
                        mCurrentSidewaysRotateTargetOffset = -HeadRotationOffset;
                        mCurrentSidewaysTranslateTargetOffset = HeadTranslateOffset;
                        mCurrentHeadStatus = 1;
                    }
                    else
                    {
                        mCurrentSidewaysRotateTargetOffset = HeadRotationOffset;
                        mCurrentSidewaysTranslateTargetOffset = -HeadTranslateOffset;
                        mCurrentHeadStatus = -1;
                    }
                    break;
                case 1:
                    if (isRight)
                    {
                        mCurrentSidewaysRotateTargetOffset = 0;
                        mCurrentSidewaysTranslateTargetOffset = 0;
                        mCurrentHeadStatus = 0;
                    }
                    else
                    {
                        mCurrentSidewaysRotateTargetOffset = HeadRotationOffset;
                        mCurrentSidewaysTranslateTargetOffset = -HeadTranslateOffset;
                        mCurrentHeadStatus = -1;
                    }
                    break;
            }
        }

        private void ShakeCameraAsPlayerMove()
        {
            if (PlayerVelocity.magnitude <= 5)
            {
                return;
            }

            Vector3 offset = ShakeOffset();
            if (mPlayerInputHandler.IsSpeedUp)
            {
                offset *= RunningBobMultiper;
            }

            mCameraBobTargetPosition = mHead.transform.position + offset;
            PlayerMainCamera.transform.position = 
                Vector3.Lerp(PlayerMainCamera.transform.position, mCameraBobTargetPosition, HeadBobSmoothing);
            
            if ((PlayerMainCamera.transform.position - mCameraBobTargetPosition).magnitude <= 0.001f)
            {
               PlayerMainCamera.transform.position = mCameraBobTargetPosition;
            }
           
        }

        private Vector3 ShakeOffset()
        {
            float time = Time.time;
            float tmp_HorizontalOffset = 0;
            float tmp_VerticalOffset = 0;

            Vector3 tmp_Offset = Vector3.zero;
            tmp_HorizontalOffset = Mathf.Cos(time * BobFrequency) * BobHorizontalAmplitude;
            tmp_VerticalOffset = Mathf.Sin(time * BobFrequency) * BobVerticalAmplitude;
            tmp_Offset = mHead.transform.right * tmp_HorizontalOffset + mHead.transform.up * tmp_VerticalOffset;

            return tmp_Offset;
        }

        private void OnPlayerMove()
        {
            //
            float speedFactor = 1.0f;
            // aim + 'Shift' is other behaviour
            if (mPlayerInputHandler.IsAim == false &&
                mPlayerInputHandler.IsSpeedUp)
            {
                speedFactor = SpeedUpMultiplier;
            }
            Vector3 moveInput = transform.TransformVector(mPlayerInputHandler.MovementInput);
            
            if(mIsOnGround)
            {
                Vector3 targetVelocity = speedFactor * MoveSpeed *moveInput;
                targetVelocity = targetVelocity.magnitude * ReDirectVeloictyOnSlope(targetVelocity, mGroundNormal);
                // v = v0 + at
                PlayerVelocity = Vector3.Lerp(PlayerVelocity, targetVelocity, 
                    MoveAccelerate * Time.deltaTime);
                
                // jump
                if (mPlayerInputHandler.IsJumpStart)
                {
                    // erase velocity on Y-axis and jump
                    PlayerVelocity.y = JumpImpulse;
                    mLastJumpTime = Time.time;
    
                    mIsOnGround = false;
                    mGroundNormal = Vector3.up;
                }
    
            }
            else
            {
                // in the air
                PlayerVelocity += moveInput * MoveAccelerateInAir * Time.deltaTime;
                float vertical = PlayerVelocity.y;
                Vector3 horizontal = Vector3.ProjectOnPlane(PlayerVelocity, Vector3.up);
                horizontal = Vector3.ClampMagnitude(horizontal, MoveSpeedInAir * speedFactor);
                PlayerVelocity = horizontal + vertical * Vector3.up;
                PlayerVelocity.y -= Gravity * Time.deltaTime;
            }
    
            Vector3 capsuleMoveBefore1 = CharacterCapsuleBottomHemiSphere();
            Vector3 capsuleMoveBefore2 = CharacterCapsuleTopHemiSphere();
            // apply velocity to player
            mCharacterController.Move(PlayerVelocity * Time.deltaTime);
            
            mLastImpactSpeed = Vector3.zero;
            if (Physics.CapsuleCast(capsuleMoveBefore1, capsuleMoveBefore2,
                mCharacterController.radius, PlayerVelocity.normalized,
                out RaycastHit hitInfo, 
                PlayerVelocity.magnitude * Time.deltaTime,
                -1, QueryTriggerInteraction.Ignore))
            {
                mLastImpactSpeed = PlayerVelocity;
                PlayerVelocity = Vector3.ProjectOnPlane(PlayerVelocity, hitInfo.normal);
            }
    
        }

        
        public void CheckForInteractableObject()
        {
            RaycastHit hit;
            
            if(Physics.Raycast(PlayerMainCamera.ScreenPointToRay(
                    new Vector3(Screen.width / 2.0f,Screen.height / 2.0f,0)),
                out hit,
                2.0f,GameWorld.TheGameWorld.PickupItemLayer,
                QueryTriggerInteraction.Collide))
            {
 
                if(hit.collider.tag == FrameworkConstants.PickUpItemsLayerName)
                {
                    ItemEntity pickupItem = hit.collider.GetComponent<ItemEntity>();
                    if (pickupItem != null)
                    {
                        // Debug.LogError(hit.collider.gameObject.name);
                        // active UI text
                        mPlayerHUD.EnableShowInteractableInfo(
                            GameplayConstants.PlayerInteractableHUDShowText 
                            + hit.collider.gameObject.name);
                        // Interact
                        if (mPlayerInputHandler.IsInteract)
                        {
                            pickupItem.OnPlayerInteract(mPlayerEntity);
                        }
                    }
                    
                }
                
            }
            else
            {
                mPlayerHUD.DisableShowInteractableInfo();
            }
        }


        
        #region HelperFuncs
    
        private Vector3 ReDirectVeloictyOnSlope(Vector3 direction, Vector3 normal)
        {
            return Vector3.Cross(normal, 
                Vector3.Cross(direction, transform.up)).normalized;
        }
        
        private Vector3 CharacterCapsuleBottomHemiSphere()
        {
            return transform.position + (transform.up * mCharacterController.radius);
        }
    
        private Vector3 CharacterCapsuleTopHemiSphere()
        {
            return transform.position + (transform.up * (mCharacterController.height - mCharacterController.radius));
        }
        
        #endregion
    
        }

}
