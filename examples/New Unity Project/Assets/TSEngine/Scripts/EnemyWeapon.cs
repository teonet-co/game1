using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyWeapon", menuName = "TSEngine/EnemyWeapon")]
public class EnemyWeapon : ScriptableObject {

    public enum ShootAbilities
    {
        None, FreezeTarget, PoisonTarget, SlowDownTarget
    }
    public ShootAbilities shootAbilities;
    public float bulletDamage;
    public Transform bullet;
    public Transform bulletImpact;
    public bool parentImpact;
    public float bulletImpactDuration;
    public float accuracy;
    public float distance;
    public float shootInterval;
    public string bulletSpawnPoint;
    public Transform muzzleFlash;
    public float freezeDuration;
    public float poisonDuration;
    public float poisonDamage;
    public float slowdownDuration;
    public float speedDecrease;
    public string enemyMeleeSound;
    public string enemyShootSound;
    public string playerImpactSound;
    public string deathSound;
}
