//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/GameLogic/Player/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""6c9e9041-cfb8-46ad-ba32-55cfbfbc6ff0"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""44b026cc-c733-424c-8804-34040938584a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3ba75562-5413-4d24-be18-d44888305523"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""84d49a9d-adc2-4442-b88c-ec1850562fe1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""8eb6dd38-c7b4-4d1c-8968-e777744c436c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpeedUp"",
                    ""type"": ""Button"",
                    ""id"": ""4c45ff0c-98c9-431e-823b-03fbc1a94d7f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""dcb9f0dd-91f8-416d-a807-a6ae91aad217"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""43fda285-5e85-46bc-a850-be042a84aeea"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a6482b95-38bc-408b-8367-a8aa8773b483"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9340308f-7858-48d4-bc14-11e6436dbc29"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""834ea333-1dbb-4672-98b1-bda127a3f9d2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""11fdb90e-c9e0-4093-a608-7f3585983027"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2176ce70-7259-4233-a755-65b3217c5697"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd44c0d4-bd1c-458a-abbf-06fea3cdf0ac"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca3770fb-efa7-47c1-8f02-6abd14760338"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpeedUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerInstructions"",
            ""id"": ""e4fc5723-a100-4109-acd2-d4d5038a4907"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""a3eeb364-783f-4292-ae08-4fca485a3ae1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""3d5fb5d9-484f-4d4c-8fdf-542775c26a6d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""5d53d854-c38b-45c6-b1aa-3688469fd655"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SidewayLeft"",
                    ""type"": ""Button"",
                    ""id"": ""d4633e1b-4aca-441b-9e1c-415f2df20c14"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SidewayRight"",
                    ""type"": ""Button"",
                    ""id"": ""d9332c1a-0ebe-4011-a117-1461d2ee115a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""3b776f9f-cab3-42d9-948a-b3cd1114fc3a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""af62ef85-c45d-4f84-99ca-a57c775786f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CallMainMenu"",
                    ""type"": ""Button"",
                    ""id"": ""7b6663ec-7c21-4ba9-9518-17f3e9200276"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""d61301ba-5ba0-4fab-88b8-fcd4fc50f074"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""194ca902-3a25-4cce-967d-9557e7bd60c7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61fa5ad7-c83e-4f75-903e-6f17bb985592"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e912a2ac-33d1-4f8e-b83f-84147812e71f"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5775ea08-5734-4b04-82fc-23c490f29588"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SidewayLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bee1289-155d-416c-bcd0-74cc16327fa6"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SidewayRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76bf9939-8063-4985-bafc-9b4ab0ad7717"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2de60fd-b4ef-4097-8307-e39265ca58e3"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79cfb604-dc69-4050-91c7-8e6f830b04ee"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CallMainMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aca3181f-3ecb-455b-ae16-42c339f91675"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        m_PlayerMovement_Jump = m_PlayerMovement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMovement_Crouch = m_PlayerMovement.FindAction("Crouch", throwIfNotFound: true);
        m_PlayerMovement_SpeedUp = m_PlayerMovement.FindAction("SpeedUp", throwIfNotFound: true);
        // PlayerInstructions
        m_PlayerInstructions = asset.FindActionMap("PlayerInstructions", throwIfNotFound: true);
        m_PlayerInstructions_Fire = m_PlayerInstructions.FindAction("Fire", throwIfNotFound: true);
        m_PlayerInstructions_Aim = m_PlayerInstructions.FindAction("Aim", throwIfNotFound: true);
        m_PlayerInstructions_SwitchWeapon = m_PlayerInstructions.FindAction("SwitchWeapon", throwIfNotFound: true);
        m_PlayerInstructions_SidewayLeft = m_PlayerInstructions.FindAction("SidewayLeft", throwIfNotFound: true);
        m_PlayerInstructions_SidewayRight = m_PlayerInstructions.FindAction("SidewayRight", throwIfNotFound: true);
        m_PlayerInstructions_Reload = m_PlayerInstructions.FindAction("Reload", throwIfNotFound: true);
        m_PlayerInstructions_Interact = m_PlayerInstructions.FindAction("Interact", throwIfNotFound: true);
        m_PlayerInstructions_CallMainMenu = m_PlayerInstructions.FindAction("CallMainMenu", throwIfNotFound: true);
        m_PlayerInstructions_Pause = m_PlayerInstructions.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    private readonly InputAction m_PlayerMovement_Jump;
    private readonly InputAction m_PlayerMovement_Crouch;
    private readonly InputAction m_PlayerMovement_SpeedUp;
    public struct PlayerMovementActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerMovementActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputAction @Jump => m_Wrapper.m_PlayerMovement_Jump;
        public InputAction @Crouch => m_Wrapper.m_PlayerMovement_Crouch;
        public InputAction @SpeedUp => m_Wrapper.m_PlayerMovement_SpeedUp;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Jump.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCrouch;
                @SpeedUp.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSpeedUp;
                @SpeedUp.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSpeedUp;
                @SpeedUp.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSpeedUp;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @SpeedUp.started += instance.OnSpeedUp;
                @SpeedUp.performed += instance.OnSpeedUp;
                @SpeedUp.canceled += instance.OnSpeedUp;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // PlayerInstructions
    private readonly InputActionMap m_PlayerInstructions;
    private IPlayerInstructionsActions m_PlayerInstructionsActionsCallbackInterface;
    private readonly InputAction m_PlayerInstructions_Fire;
    private readonly InputAction m_PlayerInstructions_Aim;
    private readonly InputAction m_PlayerInstructions_SwitchWeapon;
    private readonly InputAction m_PlayerInstructions_SidewayLeft;
    private readonly InputAction m_PlayerInstructions_SidewayRight;
    private readonly InputAction m_PlayerInstructions_Reload;
    private readonly InputAction m_PlayerInstructions_Interact;
    private readonly InputAction m_PlayerInstructions_CallMainMenu;
    private readonly InputAction m_PlayerInstructions_Pause;
    public struct PlayerInstructionsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerInstructionsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_PlayerInstructions_Fire;
        public InputAction @Aim => m_Wrapper.m_PlayerInstructions_Aim;
        public InputAction @SwitchWeapon => m_Wrapper.m_PlayerInstructions_SwitchWeapon;
        public InputAction @SidewayLeft => m_Wrapper.m_PlayerInstructions_SidewayLeft;
        public InputAction @SidewayRight => m_Wrapper.m_PlayerInstructions_SidewayRight;
        public InputAction @Reload => m_Wrapper.m_PlayerInstructions_Reload;
        public InputAction @Interact => m_Wrapper.m_PlayerInstructions_Interact;
        public InputAction @CallMainMenu => m_Wrapper.m_PlayerInstructions_CallMainMenu;
        public InputAction @Pause => m_Wrapper.m_PlayerInstructions_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInstructions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInstructionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInstructionsActions instance)
        {
            if (m_Wrapper.m_PlayerInstructionsActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnFire;
                @Aim.started -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnAim;
                @SwitchWeapon.started -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.performed -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.canceled -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnSwitchWeapon;
                @SidewayLeft.started -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnSidewayLeft;
                @SidewayLeft.performed -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnSidewayLeft;
                @SidewayLeft.canceled -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnSidewayLeft;
                @SidewayRight.started -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnSidewayRight;
                @SidewayRight.performed -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnSidewayRight;
                @SidewayRight.canceled -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnSidewayRight;
                @Reload.started -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnReload;
                @Interact.started -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnInteract;
                @CallMainMenu.started -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnCallMainMenu;
                @CallMainMenu.performed -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnCallMainMenu;
                @CallMainMenu.canceled -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnCallMainMenu;
                @Pause.started -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerInstructionsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerInstructionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @SwitchWeapon.started += instance.OnSwitchWeapon;
                @SwitchWeapon.performed += instance.OnSwitchWeapon;
                @SwitchWeapon.canceled += instance.OnSwitchWeapon;
                @SidewayLeft.started += instance.OnSidewayLeft;
                @SidewayLeft.performed += instance.OnSidewayLeft;
                @SidewayLeft.canceled += instance.OnSidewayLeft;
                @SidewayRight.started += instance.OnSidewayRight;
                @SidewayRight.performed += instance.OnSidewayRight;
                @SidewayRight.canceled += instance.OnSidewayRight;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @CallMainMenu.started += instance.OnCallMainMenu;
                @CallMainMenu.performed += instance.OnCallMainMenu;
                @CallMainMenu.canceled += instance.OnCallMainMenu;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerInstructionsActions @PlayerInstructions => new PlayerInstructionsActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnSpeedUp(InputAction.CallbackContext context);
    }
    public interface IPlayerInstructionsActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnSwitchWeapon(InputAction.CallbackContext context);
        void OnSidewayLeft(InputAction.CallbackContext context);
        void OnSidewayRight(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnCallMainMenu(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
