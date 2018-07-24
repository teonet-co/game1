using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGeneration : MonoBehaviour {

    public GameObject[] pickUps;
    [Tooltip("Time between each spawn(in seconds);Random number between values")]
    public Vector2 timeInterval;

    public Vector2 maximumRange;

	// Use this for initialization
	void Start () {
        StartCoroutine(PickupSpawner());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator PickupSpawner()
    {
        while (true)
        {
            SpawnPickup();
            yield return new WaitForSeconds(Random.Range(timeInterval.x,timeInterval.y));
        }
    }

    void SpawnPickup()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(transform.position.x - maximumRange.x, transform.position.x + maximumRange.x), Random.Range(transform.position.y - maximumRange.y, transform.position.y + maximumRange.y));
        Instantiate(pickUps[Random.Range(0, pickUps.Length)], spawnPosition, Quaternion.identity);
    }
}
