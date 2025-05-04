using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;
[CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction<Vector2, bool> Look = delegate { };
    public event UnityAction<bool> Jump = delegate { };
    public event UnityAction<bool> Dash = delegate { };
    public event UnityAction Attack = delegate { };

    PlayerInputActions inputActions;

    public Vector3 Direction => inputActions.Player.Move.ReadValue<Vector2>();

    void OnEnable() {
        if (inputActions == null) {
            inputActions = new PlayerInputActions();
            inputActions.Player.SetCallbacks(this);
        }
        EnablePlayerActions();
    
    }

    void OnDisable()
    {
        DisablePlayerActions();
    }

    public void EnablePlayerActions() {
        inputActions.Enable();
    }

    public void DisablePlayerActions() {
        inputActions.Disable();
    }
    

    bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";

    public void OnMove(InputAction.CallbackContext context) {
        Move.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context) {
        Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) {
            Attack.Invoke();
        }
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //noop
    }

    

    public void OnCrouch(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //noop
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //noop
    }
}
