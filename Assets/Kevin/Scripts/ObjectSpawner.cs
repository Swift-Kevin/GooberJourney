using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 spawnAreaMin = Vector3.zero;
    [SerializeField] private Vector3 spawnAreaMax = Vector3.one;
    
    public GameObject objectPrefab;
    public int initialNumberOfObjects = 10;
    public float spawnInterval = 5f;
    public float objectLifetime = 15f;
    public Vector3 spawnArea;
    public LayerMask layerMask;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < initialNumberOfObjects; i++)
        {
            SpawnObject();
        }

        StartCoroutine(SpawnObjectCoroutine());
        StartCoroutine(DeleteOldestObjectCoroutine());
    }

    private void SpawnObject()
    {
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y), Random.Range(spawnAreaMin.z, spawnAreaMax.z));
        RaycastHit hit;

        if (Physics.Raycast(spawnPosition + Vector3.up * 100f, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            float height = hit.point.y;
            if (height < 0)
            {
                height = 0;
            }

            spawnPosition.y = height + objectPrefab.transform.localScale.y / 2f;
        }

        GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        newObject.layer = LayerMask.NameToLayer("Grapplable");

        spawnedObjects.Add(newObject);
    }

    private IEnumerator SpawnObjectCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObject();
        }
    }

    private IEnumerator DeleteOldestObjectCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(objectLifetime);

            if (spawnedObjects.Count > 0)
            {
                GameObject oldestObject = spawnedObjects[0];
                spawnedObjects.Remove(oldestObject);
                Destroy(oldestObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 spawnBoxCenter = transform.position + new Vector3((spawnAreaMin.x + spawnAreaMax.x) / 2f, (spawnAreaMin.y + spawnAreaMax.y) / 2f, (spawnAreaMin.z + spawnAreaMax.z) / 2f);
        Vector3 spawnBoxSize = new Vector3(Mathf.Abs(spawnAreaMax.x - spawnAreaMin.x), Mathf.Abs(spawnAreaMax.y - spawnAreaMin.y), Mathf.Abs(spawnAreaMax.z - spawnAreaMin.z));
        Gizmos.DrawWireCube(spawnBoxCenter, spawnBoxSize);
    }

}