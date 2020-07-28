using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject player = default;

    [SerializeField] private int projectilePoolSize = default;
    [SerializeField] private GameObject projectile = default;

    [SerializeField] private float launchForce = default;

    [SerializeField] private float secondsBeforeLaunching = default;
    [SerializeField] private float secondsBetweenLaunchesMin = default;
    [SerializeField] private float secondsBetweenLaunchesMax = default;

    [SerializeField] private Vector3 launchDirection = default;

    private List<GameObject> projectilePool = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject projectileGO = Instantiate(projectile, transform);
            projectileGO.SetActive(false);

            projectilePool.Add(projectileGO);
        }

        StartCoroutine(LaunchProjectilesRoutine());
    }

    private GameObject getPooledProjectile()
    {
        for (int i = 0; i < projectilePool.Count; i++)
        {
            if (!projectilePool[i].activeInHierarchy)
            {
                return projectilePool[i];
            }
        }

        return null;
    }


    private IEnumerator LaunchProjectilesRoutine()
    {
        yield return new WaitForSeconds(secondsBeforeLaunching);

        while (true)
        {
            Vector3 spawnPosition = RandomPointInBox();
            spawnPosition.z = Random.Range(player.transform.position.z - 2, player.transform.position.z + 4);

            GameObject projectileToLaunch = getPooledProjectile();

            if (projectileToLaunch != null)
            {
                projectileToLaunch.SetActive(true);
                projectileToLaunch.transform.position = spawnPosition;
                projectileToLaunch.transform.rotation = Random.rotation;

                Rigidbody projectileRb = projectileToLaunch.GetComponent<Rigidbody>();
                projectileRb.velocity = Vector3.zero;

                projectileRb.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(Random.Range(secondsBetweenLaunchesMin, secondsBetweenLaunchesMax));
        }

    }


    private Vector3 RandomPointInBox()
    {
        Vector3 center = transform.position;
        Vector3 size = transform.localScale;

        return center + new Vector3
        (
           (Random.value - 0.5f) * size.x,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }


    private void OnDestroy()
    {
        StopCoroutine(LaunchProjectilesRoutine());
    }

}
