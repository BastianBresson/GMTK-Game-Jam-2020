using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = default;
    [SerializeField] float unstableAmount = default;
    [SerializeField] float steeringSpeed = default;
    private float movementDirection = 0;
    private float steeringDirection = 0;

    private Rigidbody rb;


    public void SetMoveDireciton(float direction)
    {
        movementDirection = direction;
    }


    public void SetSteeringDirection(float steering)
    {
        steeringDirection = steering;
    }

    public void ResetPosition(Vector3 position)
    {
        rb.velocity = Vector3.zero;
        transform.position = position;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (movementDirection != 0)
        {
            float unstability = Random.Range(-unstableAmount, unstableAmount);
            rb.AddTorque(movementDirection * movementSpeed * Time.deltaTime, 0, unstability);
        }

        if (steeringDirection != 0)
        {
            rb.AddTorque(0, steeringDirection * steeringSpeed * Time.deltaTime, 0);
        }
    }
}
