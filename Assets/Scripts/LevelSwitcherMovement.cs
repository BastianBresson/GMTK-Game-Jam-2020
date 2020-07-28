using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcherMovement : MonoBehaviour
{
    [SerializeField] private Vector3 offsetFromPlayer = default;

    [SerializeField] private float followSpeed = default;
    [SerializeField] private float heightChangeThreshold = default;

    private GameObject player;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    private void Update()
    {
        Move();
    }


    private void Move()
    {
        Vector3 targetPosition = TargetPosition();

        MoveToTargetPosition(targetPosition);
    }


    private void MoveToTargetPosition(Vector3 targetPosition)
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

    }

    private Vector3 TargetPosition()
    {
        Vector3 targetPosition = player.transform.position + offsetFromPlayer;

        targetPosition.x = transform.position.x;

        if (Math.Abs(player.transform.position.y - transform.position.y) < heightChangeThreshold)
        {
            targetPosition.y = transform.position.y;
        }

        return targetPosition;
    }

}
