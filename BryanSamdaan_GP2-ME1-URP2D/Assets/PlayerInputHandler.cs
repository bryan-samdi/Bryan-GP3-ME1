using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerInputHandler : MonoBehaviour
{
   /*private PlayerInput playerInput;
    private PlayerInputController controller;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var controllers = FindObjectsOfType<PlayerInputController>();
        var index = playerInput.playerIndex;
        controller = controllers.FirstOrDefault(c => c.GetPlayerIndex() == index);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (controller != null)
            controller.OnMove(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (controller != null)
            controller.OnLook(context.ReadValue<Vector2>());
    }*/
}
