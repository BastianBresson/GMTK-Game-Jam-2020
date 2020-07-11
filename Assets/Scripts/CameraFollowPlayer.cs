using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player = default;
    [SerializeField] Vector3 followOffset = default;
    [SerializeField] float followSpeed = default;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = player.transform.position + followOffset;
        newPosition.y = followOffset.y;

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * followSpeed);
    }
}
