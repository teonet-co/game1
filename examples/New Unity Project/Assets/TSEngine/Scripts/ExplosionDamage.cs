using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ExplosionDamage : MonoBehaviour {

    CircleCollider2D col2D;
    Shooting shooting;
    Weapon weapon;
	// Use this for initialization
	void Start () {
        col2D = GetComponent<CircleCollider2D>();
        shooting = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Shooting>();
        weapon = shooting.currentWeapon;
        col2D.radius = weapon.grenadeRadius;
	}
    private void Update()
    {
        //weapon = shooting.currentWeapon;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.health -= weapon.explosionDamage;
                pc.ShowHealth();
            }
        }
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.currentHealth -= weapon.explosionDamage;
                AudioManager.instance.Play(weapon.enemyImpactSound);
                enemy.UpdateHealth();
                if (weapon.enemyImpact != null)
                {
                    Transform clone = Instantiate(weapon.enemyImpact, collision.transform.position, Quaternion.identity);
                    if (weapon.parentImpact)
                        collision.transform.parent = clone.transform;
                    Destroy(clone, weapon.enemyImpactDuration);                 
                }

            }
        }

        if (collision.tag == "Explosive")
        {
            //Debug.Log("Yes");
            BarrelExplode be = collision.gameObject.GetComponent<BarrelExplode>();
            be.health -= weapon.explosionDamage;
        }

        if (collision.tag == "Destructible")
        {
            AudioManager.instance.Play(weapon.impactSound); 
            if (weapon.objectImpact != null)
            {
                Transform clone = Instantiate(weapon.objectImpact, collision.transform.position, Quaternion.identity);
                Destroy(clone, weapon.objectImpactDuration);
            }
            
        }

    }
    
}
