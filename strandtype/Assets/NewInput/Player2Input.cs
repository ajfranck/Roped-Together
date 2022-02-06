// GENERATED AUTOMATICALLY FROM 'Assets/NewInput/Player2Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Player2Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player2Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player2Input"",
    ""maps"": [
        {
            ""name"": ""CharacterController"",
            ""id"": ""bf84b95a-5977-41a2-884f-3ce85b45465c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""98491cd6-4a85-40a7-839a-04437c30a625"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""205eb99a-0bd8-4ede-807b-f74613924bcf"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""950f4a0a-c49b-48bd-bb26-bc9f37eb8b2e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a4a9d8ff-cc30-4b6d-9e2d-2f8481cab9c1"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""86626f4c-be20-4d15-a802-5087dd675bb6"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7753763f-6453-4f41-bef1-d1160dc6770b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterController
        m_CharacterController = asset.FindActionMap("CharacterController", throwIfNotFound: true);
        m_CharacterController_Move = m_CharacterController.FindAction("Move", throwIfNotFound: true);
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

    // CharacterController
    private readonly InputActionMap m_CharacterController;
    private ICharacterControllerActions m_CharacterControllerActionsCallbackInterface;
    private readonly InputAction m_CharacterController_Move;
    public struct CharacterControllerActions
    {
        private @Player2Input m_Wrapper;
        public CharacterControllerActions(@Player2Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterController_Move;
        public InputActionMap Get() { return m_Wrapper.m_CharacterController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControllerActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControllerActions instance)
        {
            if (m_Wrapper.m_CharacterControllerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_CharacterControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public CharacterControllerActions @CharacterController => new CharacterControllerActions(this);
    public interface ICharacterControllerActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
