using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplode : MonoBehaviour {

    [HideInInspector]
    public bool explode = false;
    public float health;
    public GameObject explosionPrefab;
    public float explosionDuration;
    Collider2D col;
	// Use this for initialization
	void Start () {
        col = gameObject.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (health <= 0)
            explode = true;

        if (explode)
        {
            gameObject.SetActive(false);
            col.enabled = false;
            if (explosionPrefab != null)
            {
                GameObject clone = Instantiate(explosionPrefab, transform.position, transform.rotation);
                BarrelExplosion be = clone.GetComponent<BarrelExplosion>();
                AudioManager.instance.Play(be.explosionSound);
                Destroy(clone, explosionDuration);
            }
            Destroy(gameObject, explosionDuration);
        }
	}
}
