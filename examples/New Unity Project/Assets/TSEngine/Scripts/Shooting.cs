using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour {

    public Weapon[] weapons;
    [HideInInspector]
    public Weapon currentWeapon;
    int currentWeaponIndex;

    public LayerMask whatToHit;

    float timeToFire = 0;

    public Transform spawnPoint;
    public Transform muzzleSpawn;
    Animator anim;

    [HideInInspector]
    public Vector3 dir;

    public GameObject cameraShakeObject;
    CameraShake cs;

    [HideInInspector]
    public float accuracy;

    [HideInInspector]
    public bool canShot = true;
    private bool resetForce = true;

    bool switchable = true;

    public Text ammoText;
    public Image weaponIcon;
    public Image throwForceBar;

    // Use this for initialization
    void Awake()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("No spawnPoint!");
        }
        cs = cameraShakeObject.GetComponent<CameraShake>();
    }

    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        //setting the ammo for each weapon
        for (int i = 0; i < weapons.Length; i++)
        {
            
            weapons[i].currentAmmo = weapons[i].maxAmmo;
            weapons[i].availableAmmo = weapons[i].maxAvailableAmmo;
            if (weapons[i].weaponType == Weapon.WeaponType.Grenade)
                weapons[i].currentGrenadeCount = weapons[i].grenadeCount;
            
        }

        //setting the first weapon
        for (int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i].unlocked)
            {
                currentWeapon = weapons[i];
                currentWeaponIndex = i;
                Debug.Log("CurrentWeapon:" + currentWeapon.tag + "\n" + currentWeapon.currentAmmo + " " + currentWeapon.availableAmmo);
                break;
            }
        }

        if(ammoText!=null)
        ammoText.text = currentWeapon.currentAmmo + "/" + currentWeapon.availableAmmo;
        if (weaponIcon != null)
            weaponIcon.sprite = currentWeapon.weaponIcon;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canShot)
        {
            if (currentWeapon.weaponType == Weapon.WeaponType.Gun)
            {
                
                Gun();
            }
            else if (currentWeapon.weaponType == Weapon.WeaponType.Shotgun)
            {

                Shotgun();
            }
            else if (currentWeapon.weaponType == Weapon.WeaponType.Grenade)
            {
                Grenade();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (switchable)
            {
                NextWeapon();
                if (weaponIcon != null)
                    weaponIcon.sprite = currentWeapon.weaponIcon;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (switchable)
            {
                PreviousWeapon();
                if (weaponIcon != null)
                    weaponIcon.sprite = currentWeapon.weaponIcon;
            }
        }

        if (anim != null)
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag(currentWeapon.tag))
            anim.SetInteger("AnimationID", -1);
    }

    void Gun()
    {
        //checking if the gun is automatic
        if (currentWeapon.fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (currentWeapon.availableAmmo <= 0 && currentWeapon.currentAmmo <= 0)
                {
                    if (anim != null)
                        anim.SetBool("IsShooting", false);
                    AudioManager.instance.Play(currentWeapon.noAmmoSound);
                }
                else
                {
                    AudioManager.instance.Play(currentWeapon.weaponSound);
                    Shoot();
                    if (anim != null)
                        anim.SetBool("IsShooting", true);
                }
            }
            else
            {
                if (anim != null)
                    anim.SetBool("IsShooting", false);
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                if (currentWeapon.availableAmmo <= 0 && currentWeapon.currentAmmo <= 0)
                {
                    if (anim != null)
                        anim.SetBool("IsShooting", false);
                    AudioManager.instance.Play(currentWeapon.noAmmoSound);
                }
                else
                {
                    AudioManager.instance.Play(currentWeapon.weaponSound);
                    timeToFire = Time.time + 1f / currentWeapon.fireRate;
                    Shoot();
                    if (anim != null)
                        anim.SetBool("IsShooting", true);
                }
            }
            else
            {
                if (anim != null)
                    anim.SetBool("IsShooting", false);
            }
        }
    }

    void Shotgun()
    {
        if (currentWeapon.fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (currentWeapon.availableAmmo <= 0 && currentWeapon.currentAmmo <= 0)
                {
                    if (anim != null)
                        anim.SetBool("IsShooting", false);
                    AudioManager.instance.Play(currentWeapon.noAmmoSound);
                }
                else
                {
                    AudioManager.instance.Play(currentWeapon.weaponSound);
                    for (int i = 0; i < currentWeapon.bulletCount; i++)
                        Shoot();
                    if (anim != null)
                        anim.SetBool("IsShooting", true);
                }
            }
            else
            {
                if (anim != null)
                    anim.SetBool("IsShooting", false);
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                if (currentWeapon.availableAmmo <= 0 && currentWeapon.currentAmmo <= 0)
                {
                    if (anim != null)
                        anim.SetBool("IsShooting", false);
                    AudioManager.instance.Play(currentWeapon.noAmmoSound);
                }
                else
                {
                    AudioManager.instance.Play(currentWeapon.weaponSound);
                    timeToFire = Time.time + 1f / currentWeapon.fireRate;
                    for (int i = 0; i < currentWeapon.bulletCount; i++)
                        Shoot();
                    if (anim != null)
                        anim.SetBool("IsShooting", true);
                }
            }
            else
            {
                if (anim != null)
                    anim.SetBool("IsShooting", false);
            }
        }
    }

    void Grenade()
    {
        GameObject grenadeClone=null;
        if (currentWeapon.weaponType == Weapon.WeaponType.Grenade)
        {
            if (currentWeapon.currentGrenadeCount > 0)
            {
                if (resetForce)
                    currentWeapon.throwForce = currentWeapon.minThrowForce;
                if (Input.GetButton("Fire1"))
                {
                    resetForce = false;
                    if (currentWeapon.throwForce < currentWeapon.maxThrowForce)
                        currentWeapon.throwForce += currentWeapon.throwForceMultiplier * Time.deltaTime;
                    else
                        currentWeapon.throwForce = currentWeapon.maxThrowForce;
                    throwForceBar.gameObject.SetActive(true);
                    throwForceBar.fillAmount = Remap(currentWeapon.throwForce, currentWeapon.minThrowForce, currentWeapon.maxThrowForce, 0, 1);
                }
                if (Input.GetButtonUp("Fire1"))
                {

                    throwForceBar.gameObject.SetActive(false);
                    throwForceBar.fillAmount = 0;
                    AudioManager.instance.Play(currentWeapon.throwSound);
                    Throw(ref grenadeClone);
                    StartCoroutine(Explode(grenadeClone));
                    resetForce = true;
                    currentWeapon.currentGrenadeCount--;
                    ammoText.text = currentWeapon.currentGrenadeCount.ToString();
                }
                canShot = true;
            }
            else
            {
                if (Input.GetButton("Fire1"))
                {
                    AudioManager.instance.Play(currentWeapon.noAmmoSound);
                }
            }
        }
    }

    void Throw(ref GameObject grenadeClone)
    {
        grenadeClone = Instantiate(currentWeapon.grenadePrefab, spawnPoint.transform.position, transform.parent.rotation);
        Rigidbody2D rb = grenadeClone.GetComponent<Rigidbody2D>();
        rb.AddForce(currentWeapon.throwForce * grenadeClone.transform.up,ForceMode2D.Impulse);
    }

    IEnumerator Explode(GameObject grenadeClone)
    {
        yield return new WaitForSeconds(currentWeapon.timeToExplode);
        GameObject explosionClone=null;
        if (currentWeapon.explosionEffect!=null)
        explosionClone = Instantiate(currentWeapon.explosionEffect, grenadeClone.transform.position, grenadeClone.transform.rotation);
        AudioManager.instance.Play(currentWeapon.explosionSound);
        Destroy(grenadeClone);
        if(currentWeapon.explosionEffect!=null)
        Destroy(explosionClone, currentWeapon.explosionDuration);
    }

    void NextWeapon()
    {
        //finding the next available weapon and setting it to the current weapon
        for (int i = currentWeaponIndex + 1; i < weapons.Length; i++)
        {
            if (weapons[i].unlocked)
            {
                currentWeapon = weapons[i];
                currentWeaponIndex = i;
                Debug.Log("CurrentWeapon:" + currentWeapon.tag);
                anim.SetInteger("AnimationID", currentWeapon.animationID);
                if (currentWeapon.weaponType != Weapon.WeaponType.Grenade)
                {
                    ammoText.text = currentWeapon.currentAmmo + "/" + currentWeapon.availableAmmo;
                    if (currentWeapon.availableAmmo > 0 && currentWeapon.currentAmmo > 0)
                    {
                        canShot = true;
                    }
                }
                else
                {
                    ammoText.text = currentWeapon.currentGrenadeCount.ToString();
                }
                break;

            }

        }
    }

    void PreviousWeapon()
    {
        //finding the previous available weapon and setting it to the current weapon
        for (int i = currentWeaponIndex - 1; i >= 0; i--)
        {
            if (weapons[i].unlocked)
            {
                currentWeapon = weapons[i];
                currentWeaponIndex = i;
                Debug.Log("CurrentWeapon:" + currentWeapon.tag);
                anim.SetInteger("AnimationID", currentWeapon.animationID);
                if (currentWeapon.weaponType != Weapon.WeaponType.Grenade)
                {
                    ammoText.text = currentWeapon.currentAmmo + "/" + currentWeapon.availableAmmo;
                }
                else
                {
                    ammoText.text = currentWeapon.currentGrenadeCount.ToString();
                }
                break;
            }
        }
    }

    void Shoot()
    {
        if (currentWeapon.currentAmmo == 0)
        {
            canShot = false;
            return;
        }
        accuracy = Random.Range(-currentWeapon.accuracy, currentWeapon.accuracy);
        Vector2 spawnPointPosition = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        //shooting offset
        Vector3 offset = new Vector3(accuracy, accuracy);
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - spawnPoint.position + offset;
        //shooting the ray from the spawn point in forward direction
        RaycastHit2D hit = Physics2D.Raycast(spawnPointPosition, dir, currentWeapon.distance, whatToHit);
        //Debug.Log("Shooting!");
        DecreaseAmmo();
        //setting all the visual effects
        Transform clone = Instantiate(currentWeapon.BulletTrailPrefab, spawnPoint.position, spawnPoint.rotation);
        MoveTrail mt = clone.gameObject.GetComponent<MoveTrail>();
        mt.Setup(dir);
        Effect();

        if (hit.collider != null)
        {
            Debug.DrawLine(spawnPointPosition, hit.point, Color.red);
            if (hit.collider.tag == "Destructible")
            {
                GameObject impact = Instantiate(currentWeapon.objectImpact.gameObject, hit.point, Quaternion.identity) as GameObject;
                AudioManager.instance.Play(currentWeapon.impactSound);
                Destroy(impact, currentWeapon.objectImpactDuration);
            }
            if (hit.collider.tag == "Enemy")
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.currentHealth -= currentWeapon.damage;
                enemy.UpdateHealth();
                GameObject impact = Instantiate(currentWeapon.enemyImpact.gameObject, hit.point, Quaternion.identity) as GameObject;
                if (currentWeapon.parentImpact)
                    impact.transform.parent = enemy.gameObject.transform;
                AudioManager.instance.Play(currentWeapon.enemyImpactSound);
                Destroy(impact, currentWeapon.enemyImpactDuration);
            }
            if (hit.collider.tag == "Explosive")
            {
                BarrelExplode be = hit.collider.gameObject.GetComponent<BarrelExplode>();
                be.health -= currentWeapon.damage;
            }
        }
    }

    void Effect()
    {

        
        if (currentWeapon.muzzleFlash != null)
        {
            Transform flash = Instantiate(currentWeapon.muzzleFlash, muzzleSpawn.position, muzzleSpawn.rotation) as Transform;
            flash.parent = spawnPoint;
            float size = Random.Range(0.6f, 0.9f);
            flash.localScale = new Vector3(size, size, size);
            Destroy(flash.gameObject, 0.02f);
        }
        if(cs!=null)
        cs.Shake(currentWeapon.cameraShakeAmount, currentWeapon.cameraShakeLength);
        
    }

    public void DecreaseAmmo()
    {

        if (currentWeapon.currentAmmo > 0)
        {
            currentWeapon.currentAmmo -= currentWeapon.ammoDecrease;
            if (ammoText != null)
                ammoText.text = currentWeapon.currentAmmo + "/" + currentWeapon.availableAmmo;
        }

        //checking if we need to reload weapon
        if (currentWeapon.currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            if (ammoText != null)
                ammoText.text = currentWeapon.currentAmmo + "/" + currentWeapon.availableAmmo;
        }

        if (currentWeapon.availableAmmo <= 0 && currentWeapon.currentAmmo <= 0)
        {
            if (anim != null)
                anim.SetBool("IsShooting", false);
        }

    }

    public IEnumerator Reload()
    {

        AudioManager.instance.Play(currentWeapon.reloadSound);

        switchable = false;
        canShot = false;

        if (anim != null)
            anim.SetBool("Reloading", true);

        yield return new WaitForSeconds(currentWeapon.reloadDuration);

        if (currentWeapon.availableAmmo - currentWeapon.maxAmmo > 0)
        {
            currentWeapon.currentAmmo += currentWeapon.maxAmmo;
            currentWeapon.availableAmmo -= currentWeapon.maxAmmo;
        }
        else
        {
            currentWeapon.currentAmmo += currentWeapon.availableAmmo;
            currentWeapon.availableAmmo = 0;
        }

        if (anim != null)
            anim.SetBool("Reloading", false);
        if (ammoText != null)
            ammoText.text = currentWeapon.currentAmmo + "/" + currentWeapon.availableAmmo;

        canShot = true;
        switchable = true;
    }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}

