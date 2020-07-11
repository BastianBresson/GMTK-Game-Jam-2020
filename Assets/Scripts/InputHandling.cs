using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandling : MonoBehaviour
{
    [SerializeField] GameObject UI = default;

    private InputMaster controls;
    private Player player;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Inputs.Movement.performed += context => InputMovement(context.ReadValue<Vector2>());
        controls.Inputs.Steering.performed += context => InputSteering(context.ReadValue<Vector2>());
        controls.Inputs.Menu.performed += _ => InputMenu();
    }

    private void InputMovement(Vector2 inputMovement)
    {
        player.SetMoveDireciton(inputMovement.y);
    }


    private void InputSteering(Vector2 inputSteering)
    {
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
