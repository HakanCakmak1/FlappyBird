using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawnRate;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        StopSpawn();
    }

    private void Spawn()
    {
        GameObject pipe = Instantiate(pipePrefab, transform.position, Quaternion.identity);
        pipe.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }

    public void StopSpawn()
    {
        CancelInvoke(nameof(Spawn));
    }
}
