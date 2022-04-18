
using System;
using UnityEngine;

namespace FPS_Homework_Player
{

    public class PlayerLocomotionController : MonoBehaviour
    {
        
        #region Fields
        
        // Input Control Parameters
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
        
        // components
        private PlayerInputHandler mPlayerInputHandler;
        private CharacterController mCharacterController;
        private PlayerWeaponController mPlayerWeaponController;

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
        
        #endregion

        // Called By PlayerManager Each Frame
        public void HandlePlayerLocomotion()
        {
            // ground
            OnGroundCheck();
            // Handle Input Reactions
            HandlePlayerLocomotionsInternal();
        }
        
        private void Start()
        {
            mHeadOriginPostition = mHead.transform.localPosition;
            
            mPlayerInputHandler = GetComponent<PlayerInputHandler>();
            mPlayerInputHandler.OnSidewayAction += OnPlayerSidewayAction;
            
            mCharacterController = GetComponent<CharacterController>();
            mCharacterController.enableOverlapRecovery = true;
            mPlayerWeaponController = GetComponent<PlayerWeaponController>();
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
            OnCameraRotate();
            OnPlayerSideWays();
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
