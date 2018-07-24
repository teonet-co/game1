using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyCharacteristics",menuName ="TSEngine/EnemyCharacteristics")]
public class EnemyCharacteristics : ScriptableObject {

    [SerializeField]
    public enum SpecialAbilities
    {
        shoot,SelfDestroyOnImpact,Chase
    }

    [SerializeField]
    public int length;
    public List<SpecialAbilities> abilities;
    public float maxHealth;
    public float damage;
    public float chaseSpeed;
    public EnemyWeapon enemyWeapon;
    public float explosionDamage;
    public Transform explosionEffect;
    public float explosionDuration;
}
