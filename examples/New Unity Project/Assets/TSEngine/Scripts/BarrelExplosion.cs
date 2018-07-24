using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour {

    public float explosionRadius;
    public float explosionDamage;

    public string explosionSound;
    public string impactSound;
    public string enemyImpactSound;

    public GameObject objectImpact;
    public float objectImpactDuration;

    public GameObject enemyImpact;
    public float enemyImpactDuration;
    public bool parentImpact;

    CircleCollider2D col;

	// Use this for initialization
	void Start () {
        col = GetComponent<CircleCollider2D>();
        col.radius = explosionRadius;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.health -= explosionDamage;
                pc.ShowHealth();
            }
        }
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.currentHealth -= explosionDamage;
                AudioManager.instance.Play(enemyImpactSound);
                enemy.UpdateHealth();
                if (enemyImpact != null)
                {
                    GameObject clone = Instantiate(enemyImpact, collision.transform.position, Quaternion.identity);
                    if (parentImpact)
                        collision.transform.parent = clone.transform;
                    Destroy(clone, enemyImpactDuration);
                }


            }
        }

        if (collision.tag == "Explosive")
        {
            BarrelExplode be = collision.gameObject.GetComponent<BarrelExplode>();
            be.health -= explosionDamage;
        }

        if (collision.tag == "Destructible")
        {
            AudioManager.instance.Play(impactSound);
            if (objectImpact != null)
            {
                GameObject clone = Instantiate(objectImpact, collision.transform.position, Quaternion.identity);
                Destroy(clone, objectImpactDuration);
            }

        }

    }
}
