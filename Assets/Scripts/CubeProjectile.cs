using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeProjectile : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayedThud = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator DisableAfterTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);

            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-0.5f, 0.5f, 0f) * 2, ForceMode.Impulse);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DisableAfterTime());
    }

    private void OnDisable()
    {
        StopCoroutine(DisableAfterTime());
        hasPlayedThud = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (hasPlayedThud) return;

        float pitch = Random.Range(0.8f, 1.2f);
        audioSource.pitch = pitch;
        audioSource.Play();
        hasPlayedThud = true;
    }
}
