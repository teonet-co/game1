using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class PlayerController : MonoBehaviour {

    public float maxHealth;
    [HideInInspector]
    public float health;

    public float rotationOffset;
    public float movementSpeed;
    float startSpeed;
    float diagonalSpeed;

    float inputV;
    float inputH;

    Rigidbody2D rb;
    Animator anim;

    public Image healthBarValue;

    public Transform rotationOrigin;
    public float minDistance = 2;

    [HideInInspector]
    public bool frozen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        health = maxHealth;
        startSpeed = movementSpeed;
        diagonalSpeed = startSpeed * 0.66f;

        ShowHealth();
    }

    void FixedUpdate()
    {
        if (!frozen)
        {
            if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), rotationOrigin.position) > minDistance)
            {
                Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rotationOrigin.position;
                difference.Normalize();
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ + rotationOffset);
            }

            rb.angularVelocity = 0;

            inputV = Input.GetAxis("Vertical");
            inputH = Input.GetAxis("Horizontal");

            if (inputV != 0 || inputH != 0)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
            if (Mathf.Abs(inputH) > 0 && Mathf.Abs(inputV) > 0)
            {
                movementSpeed = diagonalSpeed;
            }
            else
            {
                movementSpeed = startSpeed;
            }
        }
    }

    void Update()
    {
        if (!frozen)
        {
            rb.AddForce(Vector2.up * inputV * movementSpeed * Time.deltaTime);
            rb.AddForce(Vector2.right * inputH * movementSpeed * Time.deltaTime);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ShowHealth()
    {
        if (healthBarValue != null)
        {
            healthBarValue.fillAmount = Remap(health,0,maxHealth,0,1);
        }
    }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
