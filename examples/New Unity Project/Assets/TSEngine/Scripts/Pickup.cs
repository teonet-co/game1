using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Pickup : MonoBehaviour {

    public enum PickupType
    {
        Medkit,AmmoIncrease,Shield
    }

    public GameObject target;

    public PickupType pickupType;

    public float healthIncrease;
    public int ammoIncrease;
    public string shieldTag;
    public float shieldDuration;
    public string targetTag;
    [Tooltip("Time to destroy pickup if it is not picked up")]
    public float lifeTime;

    bool collided = false;

    public GameObject pickupEffect;
    public float effectDuration;

    private void Start()
    {
        lifeTime += Time.time;
    }

    private void Update()
    {
        if (Time.time>lifeTime&&!collided)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == shieldTag)
        {
            collided = true;

            if (pickupType == PickupType.Medkit)
            {
                Medkit(collision);

                GameObject effect = Instantiate(pickupEffect, transform.position, transform.rotation);
                Destroy(effect, effectDuration);
                Destroy(gameObject);
            }
            else if (pickupType == PickupType.AmmoIncrease)
            { 
                AmmoIncrease(collision);

                GameObject effect = Instantiate(pickupEffect, transform.position, transform.rotation);
                Destroy(effect, effectDuration);
                Destroy(gameObject);
            }
            else if (pickupType == PickupType.Shield)
            {
                StartCoroutine(Shield(collision));
                
                GameObject effect = Instantiate(pickupEffect, transform.position, transform.rotation);
                Destroy(effect, effectDuration);

                SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
                sr.sprite = null;

                Collider2D c2D = gameObject.GetComponent<Collider2D>();
                c2D.enabled = false;

                Destroy(gameObject, shieldDuration + 3);
                Debug.Log(targetTag);
            }
        }
    }


    void Medkit(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController.health + healthIncrease < playerController.maxHealth)
        {
            playerController.health += healthIncrease;
        }
        else
        {
            playerController.health = playerController.maxHealth;
        }
        playerController.ShowHealth();

    }

    void AmmoIncrease(Collider2D collision)
    {
        Shooting shooting = collision.GetComponentInChildren<Shooting>();
        shooting.currentWeapon.availableAmmo += ammoIncrease;
        if (shooting.currentWeapon.currentAmmo <= 0)
            shooting.DecreaseAmmo();
        shooting.ammoText.text = shooting.currentWeapon.currentAmmo + "/" + shooting.currentWeapon.availableAmmo;
    }

    IEnumerator Shield(Collider2D collision)
    {
        collision.tag = shieldTag;
        yield return new WaitForSeconds(shieldDuration);
        collision.tag = targetTag;
        
    }
}
