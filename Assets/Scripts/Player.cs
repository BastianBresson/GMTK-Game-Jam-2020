using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float rotationSpeed = default;
    private Vector3 movementDirection = new Vector3();

    private Rigidbody rb;


    public void SetMoveDireciton(float direction)
    {
        movementDirection.z = direction;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddTorque(movementDirection.z * rotationSpeed * Time.deltaTime, 0, 0);
    }
}
