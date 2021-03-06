﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Vector3 LatestCheckpointPosition { get; private set; }

    private static CheckPoint activeCheckPoint;
    
    [SerializeField] private List<GameObject> acsents = default;

    [SerializeField] float targetEmissionIntensity = default;
    [SerializeField] float turnOnAcsentsTime = default;

    [SerializeField] private GameObject checkpointPrefap = default;

    private bool isActiveCheckPoint = false;

    private Color emissionColor;

    private void Start()
    {
        emissionColor = acsents[0].GetComponent<Renderer>().material.GetColor("_EmissionColor");

        foreach (GameObject acsent in acsents)
        {
            acsent.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissionColor / targetEmissionIntensity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActiveCheckPoint || !other.CompareTag("Player")) return;

        if (activeCheckPoint != null)
        {
            activeCheckPoint.TurnOff();
        }

        activeCheckPoint = this;
        LatestCheckpointPosition = transform.position;

        isActiveCheckPoint = true;
        StartCoroutine(TurnOnAcsentsCoroutine());
    }

    IEnumerator TurnOnAcsentsCoroutine()
    {
        emissionColor = acsents[0].GetComponent<Renderer>().material.GetColor("_EmissionColor");

        float elapsedTime = 0f;
        float multiplier = 1f;

        while (elapsedTime < turnOnAcsentsTime)
        {
            yield return null;
            elapsedTime = Time.deltaTime;
            multiplier = Mathf.Lerp(multiplier, targetEmissionIntensity, elapsedTime / turnOnAcsentsTime);
            foreach (GameObject acsent in acsents)
            {
                acsent.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissionColor * multiplier);
            }
        }

    }

    public static void ResetCheckpoint()
    {
        LatestCheckpointPosition = Vector3.zero;
    }

    public void TurnOff()
    {
        Instantiate(checkpointPrefap, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
