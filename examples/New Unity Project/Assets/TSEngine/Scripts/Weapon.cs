using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName ="Weapon",menuName ="TSEngine/Weapon")]
public class Weapon : ScriptableObject {
    
    [SerializeField]
    public enum WeaponType
    {
        Gun,Shotgun,Grenade
    }

    [SerializeField]
    [Tooltip("Weapon name")]
    public string tag;

    [SerializeField]
    public WeaponType weaponType;

    [Space()]

    [SerializeField]
    public int animationID;
    [SerializeField]
    public int fireRate;
    [SerializeField]
    public float damage;
    public float distance;

    [SerializeField]
    [Range(1f,0)]
    [Tooltip("Lower the accuracy,more accurate the weapon")]
    public float accuracy;

    [Space()]

    [SerializeField]
    [Range(0,0.2f)]
    [Tooltip("How long will the camera shake")]
    public float cameraShakeLength = 0.1f;

    [SerializeField]
    [Range(0,0.2f)]
    [Tooltip("How much will camera shake")]
    public float cameraShakeAmount;

    [Space()]

    [SerializeField]
    public Transform BulletTrailPrefab;
    [SerializeField]
    public Transform objectImpact;
    [SerializeField]
    public Transform enemyImpact;
    [SerializeField]
    public bool parentImpact;
    [Tooltip("Duration of impact effect")]
    [SerializeField]
    public float objectImpactDuration;
    [SerializeField]
    public float enemyImpactDuration;
    [SerializeField]
    public Transform muzzleFlash;

    [Space()]

    [SerializeField]
    [HideInInspector]
    public int currentAmmo;
    [HideInInspector]
    [SerializeField]
    public int availableAmmo;

    [SerializeField]
    [Tooltip("Maximum ammunation per clip")]
    public int maxAmmo;
    [SerializeField]
    public int maxAvailableAmmo;
    [Tooltip("Ammo decrease per shoot")]
    [SerializeField]
    public int ammoDecrease;
    [SerializeField]
    public float reloadDuration;

    [Space()]

    [SerializeField]
    public string weaponSound;
    [SerializeField]
    public string noAmmoSound;
    [SerializeField]
    public string reloadSound;
    [SerializeField]
    public string impactSound;
    [SerializeField]
    public string enemyImpactSound;

    [Space()]
    [SerializeField]
    public Sprite weaponIcon;

    [Space()]
    [SerializeField]
    [Tooltip("Is this weapon available for use")]
    public bool unlocked;
    [SerializeField]
    public int bulletCount;

    [Space()]
    [SerializeField]
    public int grenadeCount;
    public int currentGrenadeCount;
    public float maxThrowForce;
    public float minThrowForce;
    public float grenadeRadius;
    public float timeToExplode;
    public GameObject grenadePrefab;
    public GameObject explosionEffect;
    public float explosionDuration;
    public float explosionDamage;
    public float throwForce;
    public float throwForceMultiplier;
    public string throwSound;
    public string explosionSound;

}
