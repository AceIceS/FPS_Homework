
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace FPS_Homework_Player
{

public class PlayerInputHandler : MonoBehaviour
{
    #region Fields

    
    
    // Input Properties
    public Vector3 MovementInput
    {
        get
        {
            return mMovement;
        }
    }
    public float MouseX
    {
        get
        {
            return mCameraInput.x;
        }
    }
    public float MouseY
    {
        get
        {
            return mCameraInput.y;
        }
    }
    public bool IsSpeedUp
    {
        get
        {
            return mIsSpeedUp;
        }
    }
    public bool IsJumpStart
    {
        get
        {
            return mIsJump;
        }
    }
    public bool IsJumpKeyHeld
    {
        get
        {
            return mIsJumpKeyHeld;
        }
    }
    public bool IsAim
    {
        get
        {
            return mIsAim;
        }
        set
        {
            mIsAim = value;
        }
    }

    public UnityAction<bool> OnAimAction;
    
    public bool Isfire
    {
        get
        {
            return mIsFire;
        }
    }
    
    public UnityAction<bool> OnSidewayAction;
    
    public bool TryReload
    {
        get
        {
            return mTryReload;
        }
    }

    public UnityAction<bool> OnSwitchWeaponAction;

    public bool IsInteract
    {
        get
        {
            return mIsInteract;
        }
        set
        {
            mIsInteract = value;
        }
    }
    
    // raw input 
    private PlayerInputActions mPlayerInputActions;
    
    private Vector3 mMovement;
    private Vector2 mMovementInput;
    private Vector2 mCameraInput;
    
    private bool mIsSpeedUp;
    
    private bool mIsJump;
    private bool mIsJumpKeyHeld;
    
    private bool mIsAim = false;
    private bool mIsFire = false;
    
    private bool mTryReload;

    private bool mIsInteract;
    
    #endregion
    
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (mPlayerInputActions == null)
        {
            mPlayerInputActions = new PlayerInputActions();
            // bind event callbacks
            mPlayerInputActions.PlayerMovement.Movement.performed += 
                OnReadPlayerMovementInput;
            mPlayerInputActions.PlayerMovement.Camera.performed +=
                OnReadPlayerCameraInput;
            // speed up check
            mPlayerInputActions.PlayerMovement.SpeedUp.started +=
                OnReadPlayerSpeedUpInputStart;
            mPlayerInputActions.PlayerMovement.SpeedUp.canceled +=
                OnReadPlayerSpeedUpInputEnd;
            // jump
            mPlayerInputActions.PlayerMovement.Jump.started +=
                OnReadPlayerJumpInputStart;
            mPlayerInputActions.PlayerMovement.Jump.performed +=
                OnReadPlayerJumpInputHolding;
            mPlayerInputActions.PlayerMovement.Jump.canceled +=
                OnReadPlayerJumpInputEnd;
            // fire
            mPlayerInputActions.PlayerInstructions.Fire.started +=
                OnReadPlayerFireStart;
            mPlayerInputActions.PlayerInstructions.Fire.canceled +=
                OnReadPlayerFireEnd;
            // aim
            mPlayerInputActions.PlayerInstructions.Aim.started +=
                OnReadPlayerAimInput;
            // sideways
            mPlayerInputActions.PlayerInstructions.SidewayLeft.started +=
                OnReadPlayerSidewaytLeftInput;
            mPlayerInputActions.PlayerInstructions.SidewayRight.started +=
                OnReadPlaterSidewayRightInput;
            // reload
            mPlayerInputActions.PlayerInstructions.Reload.started +=
                OnReadPlayerReloadInput;
            // switch weapon
            mPlayerInputActions.PlayerInstructions.SwitchWeapon.performed += OnSwitchWeapon;
            // press F
            mPlayerInputActions.PlayerInstructions.Interact.performed += OnInteractObject;
            
            mPlayerInputActions.Enable();
        }
    }

    private void OnDisable()
    {
        if (mPlayerInputActions != null)
        {
            // unbind event callbacks
            mPlayerInputActions.PlayerMovement.Movement.performed -= 
                OnReadPlayerMovementInput;
            mPlayerInputActions.PlayerMovement.Camera.performed -=
                OnReadPlayerCameraInput;
            // speed up check
            mPlayerInputActions.PlayerMovement.SpeedUp.started -=
                OnReadPlayerSpeedUpInputStart;
            mPlayerInputActions.PlayerMovement.SpeedUp.canceled -=
                OnReadPlayerSpeedUpInputEnd;
            // jump
            mPlayerInputActions.PlayerMovement.Jump.started -=
                OnReadPlayerJumpInputStart;
            mPlayerInputActions.PlayerMovement.Jump.performed -=
                OnReadPlayerJumpInputHolding;
            mPlayerInputActions.PlayerMovement.Jump.canceled -=
                OnReadPlayerJumpInputEnd;
            // fire
            mPlayerInputActions.PlayerInstructions.Fire.started -=
                OnReadPlayerFireStart;
            mPlayerInputActions.PlayerInstructions.Fire.canceled -=
                OnReadPlayerFireEnd;
            // aim
            mPlayerInputActions.PlayerInstructions.Aim.started -=
                OnReadPlayerAimInput;
            mPlayerInputActions.PlayerInstructions.SidewayLeft.started -=
                OnReadPlayerSidewaytLeftInput;
            mPlayerInputActions.PlayerInstructions.SidewayRight.started -=
                OnReadPlaterSidewayRightInput;
            // reload
            mPlayerInputActions.PlayerInstructions.Reload.started -=
                OnReadPlayerReloadInput;
            // switch weapon 
            mPlayerInputActions.PlayerInstructions.SwitchWeapon.performed -=
                OnSwitchWeapon;
            // press F
            mPlayerInputActions.PlayerInstructions.Interact.performed -= 
                OnInteractObject;
            
            mPlayerInputActions.Disable();
        }
    }

    public void HandleRawInputs(float delta)
    {
        HandleMoveRawInput();
    }

    public void ResetInputActionsInLateUpdate()
    {
        mTryReload = false;
        mIsInteract = false;
    }
    
    #region Handle Raw Input Values

    private void HandleMoveRawInput()
    {
        mMovement = Vector3.ClampMagnitude(
            new Vector3(mMovementInput.x, 0, mMovementInput.y), 1.0f);
    }

    #endregion
    
    
    #region Read Input Callback

    private void OnReadPlayerMovementInput(InputAction.CallbackContext actions)
    {
        mMovementInput = actions.ReadValue<Vector2>();
        //Debug.LogError(m_MovementInput);
    }

    private void OnReadPlayerCameraInput(InputAction.CallbackContext actions)
    {
        mCameraInput = actions.ReadValue<Vector2>();
        //Debug.LogError(m_CameraInput);
    }

    private void OnReadPlayerSpeedUpInputStart(InputAction.CallbackContext actions)
    {
        mIsSpeedUp = true;
    }

    private void OnReadPlayerSpeedUpInputEnd(InputAction.CallbackContext actions)
    {
        mIsSpeedUp = false;
    }

    private void OnReadPlayerJumpInputStart(InputAction.CallbackContext actions)
    {
        mIsJump = true;
    }

    private void OnReadPlayerJumpInputEnd(InputAction.CallbackContext actions)
    {
        mIsJump = false;
        mIsJumpKeyHeld = false;
    }

    private void OnReadPlayerJumpInputHolding(InputAction.CallbackContext actions)
    {
        mIsJumpKeyHeld = true;
    }

    private void OnReadPlayerAimInput(InputAction.CallbackContext actions)
    {
        mIsAim = !mIsAim;
        OnAimAction.Invoke(!mIsAim);
    }

    private void OnReadPlayerFireStart(InputAction.CallbackContext actions)
    {
        mIsFire = true;
    }

    private void OnReadPlayerFireEnd(InputAction.CallbackContext actions)
    {
        mIsFire = false;
    }

    private void OnReadPlayerSidewaytLeftInput(InputAction.CallbackContext actions)
    {
        if (OnSidewayAction != null)
        {
            OnSidewayAction(false);
        }
    }

    private void OnReadPlaterSidewayRightInput(InputAction.CallbackContext actions)
    {
        if (OnSidewayAction != null)
        {
            OnSidewayAction(true);
        }
    }

    private void OnReadPlayerReloadInput(InputAction.CallbackContext actions)
    {
        mTryReload = true;
    }

    private void OnSwitchWeapon(InputAction.CallbackContext action)
    {
        if (OnSwitchWeaponAction != null)
        {
            OnSwitchWeaponAction(action.ReadValue<float>() < 0);
        }
        
    }
    
    private void OnInteractObject(InputAction.CallbackContext action)
    {
        mIsInteract = true;
    }
    
    #endregion

}

}
