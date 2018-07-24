using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor {

    Weapon weapon;

    public override void OnInspectorGUI()
    {
        weapon = (Weapon)target;
        GUILayout.BeginVertical("HelpBox");
        var textDimensions = GUI.skin.label.CalcSize(new GUIContent("Animation tag"));
        EditorGUIUtility.labelWidth = textDimensions.x + 9;
        weapon.weaponType = (Weapon.WeaponType)EditorGUILayout.EnumPopup("Weapon Type", weapon.weaponType);
        EditorGUILayout.Space();

        GUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Animation", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        weapon.tag = EditorGUILayout.TextField("Animation Tag", weapon.tag);
        weapon.animationID = EditorGUILayout.IntField("Animation ID", weapon.animationID);
        weapon.animationID = Mathf.Clamp(weapon.animationID, 0, int.MaxValue);
        EditorGUILayout.Space();
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        if (weapon.weaponType == Weapon.WeaponType.Gun || weapon.weaponType == Weapon.WeaponType.Shotgun)
        {
            weapon.fireRate = EditorGUILayout.IntField("Fire Rate", weapon.fireRate);
            weapon.damage = EditorGUILayout.FloatField("Damage", weapon.damage);
            weapon.distance = EditorGUILayout.FloatField("Distance", weapon.distance);         
            weapon.maxAmmo = EditorGUILayout.IntField("Max Ammo", weapon.maxAmmo);
            textDimensions = GUI.skin.label.CalcSize(new GUIContent("Max Available Ammo"));
            EditorGUIUtility.labelWidth = textDimensions.x + 9;
            weapon.maxAvailableAmmo = EditorGUILayout.IntField("Max Available Ammo", weapon.maxAvailableAmmo);
        }
        if (weapon.weaponType == Weapon.WeaponType.Grenade)
        {
            textDimensions = GUI.skin.label.CalcSize(new GUIContent("Explosion Duration"));
            EditorGUIUtility.labelWidth = textDimensions.x + 9;
            weapon.maxThrowForce = EditorGUILayout.FloatField("Max Throw Force", weapon.maxThrowForce);
            weapon.minThrowForce = EditorGUILayout.FloatField("Min Throw Force", weapon.minThrowForce);
            weapon.throwForceMultiplier = EditorGUILayout.FloatField("Throw Multiplier", weapon.throwForceMultiplier);
            weapon.timeToExplode = EditorGUILayout.FloatField("Time To Explode", weapon.timeToExplode);
            weapon.grenadeCount = EditorGUILayout.IntField("Grenade Count", weapon.grenadeCount);
            weapon.grenadeRadius = EditorGUILayout.FloatField("Grenade Radius", weapon.grenadeRadius);
            weapon.explosionDamage = EditorGUILayout.FloatField("Explosion Damage", weapon.explosionDamage);
            weapon.explosionDuration = EditorGUILayout.FloatField("Explosion Duration", weapon.explosionDuration);
        }
        weapon.accuracy = EditorGUILayout.Slider("Accuracy", weapon.accuracy, 1, 0);
        textDimensions = GUI.skin.label.CalcSize(new GUIContent("Animation tag "));
        EditorGUIUtility.labelWidth = textDimensions.x + 9;
        weapon.reloadDuration = EditorGUILayout.FloatField("Reload Duration", weapon.reloadDuration);
        if (weapon.weaponType == Weapon.WeaponType.Shotgun)
            weapon.bulletCount = EditorGUILayout.IntField("Bullet Count", weapon.bulletCount);
        textDimensions = GUI.skin.label.CalcSize(new GUIContent("Unlocked Weapon?"));
        EditorGUIUtility.labelWidth = textDimensions.x + 9;
        weapon.unlocked = EditorGUILayout.Toggle("Unlocked Weapon?", weapon.unlocked);
        EditorGUILayout.Space();
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Camera Shake", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        textDimensions = GUI.skin.label.CalcSize(new GUIContent("Camera Shake Length"));
        EditorGUIUtility.labelWidth = textDimensions.x + 9;
        weapon.cameraShakeLength = EditorGUILayout.Slider("Camera Shake Length", weapon.cameraShakeLength, 0, 0.2f);
        weapon.cameraShakeAmount = EditorGUILayout.Slider("Camera Shake Amount", weapon.cameraShakeAmount, 0, 0.2f);
        EditorGUILayout.Space();
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Visual Effects", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        textDimensions = GUI.skin.label.CalcSize(new GUIContent("Object Impact Duration "));
        EditorGUIUtility.labelWidth = textDimensions.x + 9;
        if (weapon.weaponType == Weapon.WeaponType.Grenade)
        {
            weapon.grenadePrefab = (GameObject)EditorGUILayout.ObjectField("Grenade Prefab", weapon.grenadePrefab, typeof(GameObject), true);
            weapon.explosionEffect = (GameObject)EditorGUILayout.ObjectField("Explosion Effect", weapon.explosionEffect, typeof(GameObject), true);
        }
        if (weapon.weaponType != Weapon.WeaponType.Grenade)
        {
            weapon.BulletTrailPrefab = (Transform)EditorGUILayout.ObjectField("Bullet Prefab", weapon.BulletTrailPrefab, typeof(Transform), true);
        }
        weapon.objectImpact = (Transform)EditorGUILayout.ObjectField("Object Impact", weapon.objectImpact, typeof(Transform), true);
        weapon.objectImpactDuration = EditorGUILayout.FloatField("Object Impact Duration", weapon.objectImpactDuration);
        weapon.enemyImpact = (Transform)EditorGUILayout.ObjectField("Enemy Impact", weapon.enemyImpact, typeof(Transform), true);
        weapon.enemyImpactDuration = EditorGUILayout.FloatField("Enemy Impact Duration", weapon.enemyImpactDuration);
        textDimensions = GUI.skin.label.CalcSize(new GUIContent("Parent Impact To Enemy "));
        EditorGUIUtility.labelWidth = textDimensions.x + 9;
        weapon.parentImpact = EditorGUILayout.Toggle("Parent Impact To Enemy", weapon.parentImpact);
        textDimensions = GUI.skin.label.CalcSize(new GUIContent("Object Impact Duration "));
        EditorGUIUtility.labelWidth = textDimensions.x + 9;
        if (weapon.weaponType != Weapon.WeaponType.Grenade)
        {
            weapon.muzzleFlash = (Transform)EditorGUILayout.ObjectField("Muzzle Flash", weapon.muzzleFlash, typeof(Transform), true);
        }
        weapon.weaponIcon = (Sprite)EditorGUILayout.ObjectField("Weapon Icon", weapon.weaponIcon, typeof(Sprite), true);
        EditorGUILayout.Space();
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Sound Effects", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        textDimensions = GUI.skin.label.CalcSize(new GUIContent("Enemy Impact Sound "));
        EditorGUIUtility.labelWidth = textDimensions.x + 9;
        if (weapon.weaponType != Weapon.WeaponType.Grenade)
        {
            weapon.weaponSound = EditorGUILayout.TextField("Shoot Sound", weapon.weaponSound);
            weapon.reloadSound = EditorGUILayout.TextField("Reload Sound", weapon.reloadSound);
        }
        else
        {
            weapon.throwSound = EditorGUILayout.TextField("Throw Sound", weapon.throwSound);
            weapon.explosionSound = EditorGUILayout.TextField("Explosion Sound", weapon.explosionSound);
        }
        weapon.noAmmoSound = EditorGUILayout.TextField("No Ammo Sound", weapon.noAmmoSound);
        weapon.impactSound = EditorGUILayout.TextField("Impact Sound", weapon.impactSound);
        weapon.enemyImpactSound = EditorGUILayout.TextField("Enemy Impact Sound", weapon.enemyImpactSound);
        EditorGUILayout.Space();
        GUILayout.EndVertical();

        GUILayout.EndVertical();
        if (GUI.changed)
            EditorUtility.SetDirty(weapon);
    }

}
