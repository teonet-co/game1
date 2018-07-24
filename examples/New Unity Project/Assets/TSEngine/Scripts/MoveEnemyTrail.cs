using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyTrail : MonoBehaviour {

    public int speed = 230;
    public GameObject enemyObject;
    Vector3 direction;
    Enemy enemy;

    // Update is called once per frame
    private void Start()
    {
        enemy = enemyObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Vector3 offset = new Vector3(enemy.accuracy, enemy.accuracy);
            direction = Vector3.up + offset;
        }
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        Destroy(gameObject, enemy.EnemyAbilities.enemyWeapon.distance / speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Destructible" || collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
