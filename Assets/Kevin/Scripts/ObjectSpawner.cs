using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public int maxObjects = 10;
    public float timeBetweenSpawn = 5.0f;
    public float timeBetweenDeletion = 15.0f;
    public Vector3 spawnLimitsMin;
    public Vector3 spawnLimitsMax;
    public string layerMaskName = "Grapplable";

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < maxObjects; i++)
        {
            SpawnObject();
        }
        InvokeRepeating("SpawnObject", timeBetweenSpawn, timeBetweenSpawn);
    }

    private void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(spawnLimitsMin.x, spawnLimitsMax.x),
                                            Random.Range(spawnLimitsMin.y, spawnLimitsMax.y),
                                            Random.Range(spawnLimitsMin.z, spawnLimitsMax.z));
        GameObject newObject = Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPosition, Quaternion.identity);
        newObject.layer = LayerMask.NameToLayer(layerMaskName);
        spawnedObjects.Add(newObject);

        if (spawnedObjects.Count > maxObjects)
        {
            GameObject oldestObject = spawnedObjects[0];
            spawnedObjects.RemoveAt(0);
            if (oldestObject.activeInHierarchy)
            {
                StartCoroutine(DestroyAfterDelay(oldestObject, timeBetweenDeletion));
            }
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject objectToDestroy, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((spawnLimitsMax + spawnLimitsMin) * 0.5f, spawnLimitsMax - spawnLimitsMin);
    }
}
