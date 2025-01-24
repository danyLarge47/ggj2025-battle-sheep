using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionMap;
    [SerializeField] private InputActionReference input_Movement_P1;
    [SerializeField] private InputActionReference input_Movement_P2;
    private Vector2 input_p1;
    private Vector2 input_p2;

    public void OnEnable()
    {
        actionMap.Enable();
        input_Movement_P1.action.performed += Movement_P1;
        input_Movement_P2.action.performed += Movement_P2;
    }

    private void OnDisable()
    {
        input_Movement_P1.action.performed -= Movement_P1;
        input_Movement_P2.action.performed -= Movement_P2;
    }

    public void Movement_P1(InputAction.CallbackContext context)
    {
        input_p1 = context.ReadValue<Vector2>();
        GameEvents.OnInputAction_Movement_P1.Invoke(input_p1);
    }

    public void Movement_P2(InputAction.CallbackContext context)
    {
        input_p2 = context.ReadValue<Vector2>();
        GameEvents.OnInputAction_Movement_P2.Invoke(input_p2);
    }
}