using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThudPlayer : MonoBehaviour
{
    AudioSource thudSource;
    private Rigidbody rb;
    float startVolume;

    private void Start()
    {
        thudSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        startVolume = thudSource.volume;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            if (thudSource.isPlaying) return;

            float angularMagnitude = rb.angularVelocity.magnitude;
            float velocityMagnitude = rb.velocity.magnitude;

            float volumeMultiplier = angularMagnitude > velocityMagnitude ? angularMagnitude : velocityMagnitude;

            float volume = volumeMultiplier / 5f;
            volume = Mathf.Clamp(volume, 0, 1);
        
            float pitch = Random.Range(0.8f, 1.2f);

            thudSource.volume = volume * startVolume;
            thudSource.pitch = pitch;
            thudSource.Play();
        }
    }
}
