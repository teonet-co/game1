using UnityEngine;
using System.Collections;

public class MoveTrail : MonoBehaviour {

    public int speed = 230;
    Vector3 dir;
    Shooting shooting;
    Rigidbody2D rb;
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooting = FindObjectOfType<Shooting>();
    }

    public void Setup(Vector3 direc)
    {
        dir = direc;
        dir.Normalize();
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);
    }

    void Update () {
        rb.velocity = transform.up * Time.deltaTime * speed;
        Destroy(gameObject, shooting.currentWeapon.distance / rb.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Destructible"||collision.tag=="Enemy")
        {
            Destroy(gameObject);
        }
    }
}
