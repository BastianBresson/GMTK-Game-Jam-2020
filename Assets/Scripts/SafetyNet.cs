using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyNet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.OnPlayerFall();
        }
        else if (other.CompareTag("ProjectileCube"))
        {
            other.gameObject.SetActive(false);
        }

        

    }

}
