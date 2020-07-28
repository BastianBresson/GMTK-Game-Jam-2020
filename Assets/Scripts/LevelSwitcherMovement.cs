using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcherMovement : MonoBehaviour
{
    [SerializeField] private Vector3 positionFromPlayerOffset = default;

    [SerializeField] private float followSpeed = default;
    [SerializeField] private float heightChangeThreshold = default;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    private void Update()
    {
        Vector3 targetPosition = player.transform.position + positionFromPlayerOffset;

        targetPosition.x = transform.position.x;

        if (Math.Abs(player.transform.position.y - transform.position.y) < heightChangeThreshold)
        {
            targetPosition.y = transform.position.y;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
