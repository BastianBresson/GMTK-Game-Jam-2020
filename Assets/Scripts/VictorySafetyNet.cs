using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySafetyNet : MonoBehaviour
{
    [SerializeField] private GameObject player = default;

    private Vector3 playerStartPosition;

    private void Start()
    {
        playerStartPosition = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = playerStartPosition;
        }
        else
        {
            other.gameObject.SetActive(false);
        }
    }
}
