using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandling : MonoBehaviour
{
    [SerializeField] GameObject UI = default;

    private InputMaster controls;
    private Player player;
    private bool isTouchEnabled = false;

    [SerializeField] private GameObject touchHolder = default;

    [SerializeField] private Joystick verticalJoystick = default;
    [SerializeField] private Joystick horisontalJoyStick = default;


    private void Awake()
    {
        controls = new InputMaster();
        controls.Inputs.Movement.performed += context => InputMovement(context.ReadValue<Vector2>());
        controls.Inputs.Steering.performed += context => InputSteering(context.ReadValue<Vector2>());
        controls.Inputs.Menu.performed += _ => InputMenu();
        controls.Inputs.Touch.performed += _ => OnTouch();
    }

    private void OnTouch()
    {
        touchHolder.SetActive(true);
        isTouchEnabled = true;
    }

    private void Update()
    {
        if (!isTouchEnabled) return;

        player.SetMoveDireciton(verticalJoystick.Vertical);

        player.SetSteeringDirection(horisontalJoyStick.Horizontal);
    }

    private void InputMovement(Vector2 inputMovement)
    {
        if (isTouchEnabled) return;
        player.SetMoveDireciton(inputMovement.y);
    }


    private void InputSteering(Vector2 inputSteering)
    {
        if (isTouchEnabled) return;

        player.SetSteeringDirection(inputSteering.x);
    }


    private void InputMenu()
    {
        UI.SetActive(!UI.activeInHierarchy);
    }


    private void Start()
    {
        player = GetComponent<Player>();
    }


    private void OnEnable()
    {
        controls.Enable();
    }


    private void OnDisable()
    {
        controls.Disable();
    }
}
