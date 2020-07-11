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
        controls.Inputs.Movement.performed += context => InputDirection(context.ReadValue<Vector2>());
        controls.Inputs.Menu.performed += _ => InputMenu();
    }

    private void InputDirection(Vector2 inputDirection)
    {
        player.SetMoveDireciton(inputDirection.y);
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
