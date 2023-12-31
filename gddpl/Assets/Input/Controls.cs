// GENERATED AUTOMATICALLY FROM 'Assets/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""0d81c4c9-8f2c-4419-9b35-818f8bf37b1e"",
            ""actions"": [
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""89267d9a-225a-44c2-ab55-1262bfe5b102"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Melee"",
                    ""type"": ""Button"",
                    ""id"": ""a42369f6-e57a-41c0-a5d1-1cba9f69c8e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Power"",
                    ""type"": ""Button"",
                    ""id"": ""b1a7926b-3913-4780-a4c2-c787d30b63e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e097d1b0-8178-4558-819b-2f85959183c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""2459092c-c465-4869-ab84-08daca60cd4d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActivateGodMode"",
                    ""type"": ""Button"",
                    ""id"": ""dce12d57-225f-4321-a279-3837394d43ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""ac983ce0-3565-4a34-93d5-a7f6da09ffbc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b9b68dae-5193-4e41-a55c-db81e3e36976"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""17dace6a-b569-4d91-be25-81fdd7b0585c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cba01535-6920-4040-b893-3876d8018be1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1acf05a1-bcf2-459b-9307-68ca6cce514a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Power"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0a32d38-6af2-4951-9212-234ba017ef5f"",
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
                    ""id"": ""0da3611e-7cc8-44ab-ab89-950e662bf35a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8edc701-2418-4083-9e5e-8a58d0de535e"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateGodMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Run = m_Gameplay.FindAction("Run", throwIfNotFound: true);
        m_Gameplay_Melee = m_Gameplay.FindAction("Melee", throwIfNotFound: true);
        m_Gameplay_Power = m_Gameplay.FindAction("Power", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_MousePosition = m_Gameplay.FindAction("MousePosition", throwIfNotFound: true);
        m_Gameplay_ActivateGodMode = m_Gameplay.FindAction("ActivateGodMode", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Run;
    private readonly InputAction m_Gameplay_Melee;
    private readonly InputAction m_Gameplay_Power;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_MousePosition;
    private readonly InputAction m_Gameplay_ActivateGodMode;
    public struct GameplayActions
    {
        private @Controls m_Wrapper;
        public GameplayActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Run => m_Wrapper.m_Gameplay_Run;
        public InputAction @Melee => m_Wrapper.m_Gameplay_Melee;
        public InputAction @Power => m_Wrapper.m_Gameplay_Power;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @MousePosition => m_Wrapper.m_Gameplay_MousePosition;
        public InputAction @ActivateGodMode => m_Wrapper.m_Gameplay_ActivateGodMode;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Run.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRun;
                @Melee.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMelee;
                @Melee.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMelee;
                @Melee.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMelee;
                @Power.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPower;
                @Power.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPower;
                @Power.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPower;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @MousePosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @ActivateGodMode.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActivateGodMode;
                @ActivateGodMode.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActivateGodMode;
                @ActivateGodMode.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActivateGodMode;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Melee.started += instance.OnMelee;
                @Melee.performed += instance.OnMelee;
                @Melee.canceled += instance.OnMelee;
                @Power.started += instance.OnPower;
                @Power.performed += instance.OnPower;
                @Power.canceled += instance.OnPower;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @ActivateGodMode.started += instance.OnActivateGodMode;
                @ActivateGodMode.performed += instance.OnActivateGodMode;
                @ActivateGodMode.canceled += instance.OnActivateGodMode;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnRun(InputAction.CallbackContext context);
        void OnMelee(InputAction.CallbackContext context);
        void OnPower(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnActivateGodMode(InputAction.CallbackContext context);
    }
}
