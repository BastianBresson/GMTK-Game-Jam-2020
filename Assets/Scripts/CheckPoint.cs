using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private static CheckPoint activeCheckPoint;

    [SerializeField] private List<GameObject> acsents = default;

    [SerializeField] float targetEmissionIntensity = default;
    [SerializeField] float turnOnAcsentsTime = default;

    [SerializeField] private GameObject checkpointPrefap = default;

    private bool isActiveCheckPoint = false;

    private Color emisionColor;

    private void Start()
    {
        emisionColor = acsents[0].GetComponent<Renderer>().material.GetColor("_EmissionColor");

        foreach (GameObject acsent in acsents)
        {
            acsent.GetComponent<Renderer>().material.SetColor("_EmissionColor", emisionColor / targetEmissionIntensity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActiveCheckPoint) return;

        GameManager.Instance.OnCheckpointReached(this);

        if (activeCheckPoint != null)
        {
            activeCheckPoint.TurnOff();
        }

        activeCheckPoint = this;

        isActiveCheckPoint = true;
        StartCoroutine(TurnOnAcsentsCoroutine());
    }

    IEnumerator TurnOnAcsentsCoroutine()
    {
        emisionColor = acsents[0].GetComponent<Renderer>().material.GetColor("_EmissionColor");

        float elapsedTime = 0f;
        float multiplier = 1f;

        while (elapsedTime < turnOnAcsentsTime)
        {
            yield return null;
            elapsedTime = Time.deltaTime;
            multiplier = Mathf.Lerp(multiplier, targetEmissionIntensity, elapsedTime / turnOnAcsentsTime);
            foreach (GameObject acsent in acsents)
            {
                acsent.GetComponent<Renderer>().material.SetColor("_EmissionColor", emisionColor * multiplier);
            }
        }

    }

    public void TurnOff()
    {
        Instantiate(checkpointPrefap, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
