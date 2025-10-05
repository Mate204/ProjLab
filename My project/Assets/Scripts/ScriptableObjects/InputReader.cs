using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, InputSystem_Actions.ISecondGameActions
{
    private InputSystem_Actions gameInput;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new InputSystem_Actions();

            gameInput.SecondGame.SetCallbacks(this);
        }

        gameInput.SecondGame.Enable();
    }

    private void OnDisable()
    {
        gameInput.SecondGame.Disable();
    }

    public Action InspectPerformed = delegate { };
    public void OnInspect(InputAction.CallbackContext context)
    {
        if (context.performed)
            InspectPerformed?.Invoke();
    }

    public Action InteractPerformed = delegate { };
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            InteractPerformed?.Invoke();
    }

    public Action<Vector2> Look = delegate { };
    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
            Look?.Invoke(context.ReadValue<Vector2>());
    }
}
