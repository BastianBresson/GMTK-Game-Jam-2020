using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyNet : MonoBehaviour
{
    private GameObject playerGO;
    private Player player;
    private Vector3 playerStartPosition;


    private void Start()
    {
        playerGO = GameObject.FindWithTag("Player");
        player = playerGO.GetComponent<Player>();

        playerStartPosition = playerGO.transform.position;
        playerStartPosition.y = 2;
    }


    private void OnPlayerFall()
    {
        ResetPlayerPostion();
    }

    private void ResetPlayerPostion()
    {
        Vector3 checkpointPosition = CheckPoint.LatestCheckpointPosition;

        if (checkpointPosition != null && checkpointPosition != Vector3.zero)
        {
            ResetPlayerPositionToCheckpoint();
        }
        else
        {
            ResetPlayerPositionToStart();
        }
    }


    private void ResetPlayerPositionToStart()
    {
        player.ResetPosition(playerStartPosition);
    }


    private void ResetPlayerPositionToCheckpoint()
    {
        player.ResetPosition(CheckPoint.LatestCheckpointPosition);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerFall();
        }
        else if (other.CompareTag("ProjectileCube"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
