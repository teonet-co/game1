using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Enemy : MonoBehaviour {
    
    public EnemyCharacteristics EnemyAbilities;
    [HideInInspector]
    public float currentHealth;
    GameObject player;

    Transform bulletSpawn;

    Shooting sh;

    float timeInterval;
    float poisonInterval;

    public Animator anim;

    public Image HealthBar;

    [HideInInspector]
    public float accuracy;

    PlayerController pc;



    bool functional = true;
    bool poison = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sh = player.GetComponentInChildren<Shooting>();
        pc = player.GetComponent<PlayerController>();
        
        currentHealth = EnemyAbilities.maxHealth;
        bulletSpawn = transform.Find(EnemyAbilities.enemyWeapon.bulletSpawnPoint);

        timeInterval = EnemyAbilities.enemyWeapon.shootInterval;

        if (ContainsAbility(EnemyAbilities.abilities, EnemyCharacteristics.SpecialAbilities.Chase))
        {
            Chase();
        }

        UpdateHealth();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            AudioManager.instance.Play(EnemyAbilities.enemyWeapon.deathSound);
            anim.SetBool("Died", true);
            functional = false;
            Destroy(gameObject,anim.GetCurrentAnimatorStateInfo(0).length);
        }

        if (functional)
        {
            if (ContainsAbility(EnemyAbilities.abilities, EnemyCharacteristics.SpecialAbilities.Chase))
            {
                anim.SetBool("Move", true);
                Chase();
            }

            if (ContainsAbility(EnemyAbilities.abilities, EnemyCharacteristics.SpecialAbilities.shoot))
            {
                if (Time.time > timeInterval)
                {
                    AudioManager.instance.Play(EnemyAbilities.enemyWeapon.enemyShootSound);
                    anim.SetTrigger("Shoot");
                    Shoot();
                    timeInterval = Time.time + EnemyAbilities.enemyWeapon.shootInterval;
                }

                if (poison)
                    PoisonTarget();
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (ContainsAbility(EnemyAbilities.abilities, EnemyCharacteristics.SpecialAbilities.SelfDestroyOnImpact))
            {
                functional = false;
                if (anim != null)
                    anim.SetBool("Died", true);
                pc.health -= EnemyAbilities.explosionDamage;
                GameObject clone = Instantiate(EnemyAbilities.explosionEffect.gameObject, transform.position, transform.rotation);
                Destroy(clone, EnemyAbilities.explosionDuration);
                Destroy(gameObject, EnemyAbilities.explosionDuration+.5f);
            }
            else
            {
                AudioManager.instance.Play(EnemyAbilities.enemyWeapon.enemyMeleeSound);
                if (anim != null)
                    anim.SetTrigger("Melee");
                pc.health -= EnemyAbilities.damage * Time.deltaTime;
                pc.ShowHealth();
                //Debug.Log(pc.health);
            }
        }
    }

    public bool ContainsAbility(List<EnemyCharacteristics.SpecialAbilities> abilities,EnemyCharacteristics.SpecialAbilities ability)
    {
        foreach(EnemyCharacteristics.SpecialAbilities ab in abilities)
        {
            if (ab == ability)
                return true;
        }

        return false;
    }

    void Chase()
    {

        /*
        AIPath aiPath;
        aiPath = GetComponent<AIPath>();
        aiPath.enabled = true;
        aiPath.speed = EnemyAbilities.chaseSpeed;
        */
        
    }

    void Shoot()
    {

        accuracy = Random.Range(-EnemyAbilities.enemyWeapon.accuracy, EnemyAbilities.enemyWeapon.accuracy);

        Vector2 bulletSpawnPoint = new Vector2(bulletSpawn.position.x, bulletSpawn.position.y);
        Vector3 offset = new Vector3(accuracy, accuracy);

        RaycastHit2D hit = Physics2D.Raycast(bulletSpawnPoint, transform.up + offset, EnemyAbilities.enemyWeapon.distance);

        Effect();

        if (hit.collider != null)
        {
            Debug.DrawLine(bulletSpawn.position, hit.point, Color.red);
            if (hit.collider.tag == "Player")
            {
                GameObject impact = Instantiate(EnemyAbilities.enemyWeapon.bulletImpact.gameObject, hit.point, Quaternion.identity) as GameObject;
                if (EnemyAbilities.enemyWeapon.parentImpact)
                    impact.transform.parent = hit.collider.transform;
                AudioManager.instance.Play(EnemyAbilities.enemyWeapon.playerImpactSound);
                Destroy(impact, EnemyAbilities.enemyWeapon.bulletImpactDuration);
                pc.health -= EnemyAbilities.enemyWeapon.bulletDamage;
                pc.ShowHealth();
                if (EnemyAbilities.enemyWeapon.shootAbilities == EnemyWeapon.ShootAbilities.FreezeTarget)
                {
                    StartCoroutine(FreezeTarget(pc));
                }
                else if(EnemyAbilities.enemyWeapon.shootAbilities == EnemyWeapon.ShootAbilities.PoisonTarget)
                {
                    poison = true;
                }
                else if (EnemyAbilities.enemyWeapon.shootAbilities == EnemyWeapon.ShootAbilities.SlowDownTarget)
                {
                    StartCoroutine(SlowDown());
                }
            }
        }

    }

    void PoisonTarget()
    {
        poisonInterval = EnemyAbilities.enemyWeapon.poisonDuration + Time.time;
        if (Time.time < poisonInterval)
        {
            pc.health -= Time.deltaTime * EnemyAbilities.enemyWeapon.poisonDamage;
            pc.ShowHealth();
        }
        else
        {
            poison = false;
        }
    }

    IEnumerator FreezeTarget(PlayerController pc)
    {
        pc.frozen = true;
        sh.canShot = false;
        yield return new WaitForSeconds(EnemyAbilities.enemyWeapon.freezeDuration);
        sh.canShot = true;
        pc.frozen = false;
    }

    IEnumerator SlowDown()
    {
        pc.movementSpeed -= EnemyAbilities.enemyWeapon.speedDecrease;
        yield return new WaitForSeconds(EnemyAbilities.enemyWeapon.slowdownDuration);
        Debug.Log("returned");
        pc.movementSpeed += EnemyAbilities.enemyWeapon.speedDecrease;
    }

    void Effect()
    {

        Instantiate(EnemyAbilities.enemyWeapon.bullet, bulletSpawn.position, bulletSpawn.rotation);
        if (EnemyAbilities.enemyWeapon.muzzleFlash != null)
        {
            Transform flash = Instantiate(EnemyAbilities.enemyWeapon.muzzleFlash, bulletSpawn.position, bulletSpawn.rotation) as Transform;
            flash.parent = bulletSpawn;
            float size = Random.Range(0.6f, 0.9f);
            flash.localScale = new Vector3(size, size, size);
            Destroy(flash.gameObject, 0.02f);
        }

    }

    public void UpdateHealth()
    {
        HealthBar.fillAmount = Remap(currentHealth, 0, EnemyAbilities.maxHealth, 0, 1);
    }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
