using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyWeapon))]
public class EnemyWeaponEditor : Editor {

    EnemyWeapon enemyWeapon;

    public override void OnInspectorGUI()
    {
        enemyWeapon = (EnemyWeapon)target;

        GUILayout.BeginVertical("HelpBox");
        var textDimensions = GUI.skin.label.CalcSize(new GUIContent("Bullet Spawn Name "));
        EditorGUIUtility.labelWidth = textDimensions.x + 6;
        enemyWeapon.shootAbilities =(EnemyWeapon.ShootAbilities) EditorGUILayout.EnumPopup("Shoot Ability", enemyWeapon.shootAbilities);

        EditorGUILayout.Space();
        GUILayout.BeginVertical("HelpBox");
        enemyWeapon.bulletDamage = EditorGUILayout.FloatField("Damage", enemyWeapon.bulletDamage);
        enemyWeapon.bullet = (Transform)EditorGUILayout.ObjectField("Bullet", enemyWeapon.bullet, typeof(Transform), true);
        enemyWeapon.bulletSpawnPoint = EditorGUILayout.TextField("Bullet Spawn Name", enemyWeapon.bulletSpawnPoint);
        enemyWeapon.bulletImpact= (Transform)EditorGUILayout.ObjectField("Bullet Impact", enemyWeapon.bulletImpact, typeof(Transform), true);
        textDimensions = GUI.skin.label.CalcSize(new GUIContent("Parent Bullet Impact To Player "));
        EditorGUIUtility.labelWidth = textDimensions.x + 6;
        enemyWeapon.parentImpact = EditorGUILayout.Toggle("Parent Bullet Impact To Player", enemyWeapon.parentImpact);
        textDimensions = GUI.skin.label.CalcSize(new GUIContent("Player Impact Sound "));
        EditorGUIUtility.labelWidth = textDimensions.x + 6;
        enemyWeapon.bulletImpactDuration = EditorGUILayout.FloatField("Impact Duration", enemyWeapon.bulletImpactDuration);
        enemyWeapon.muzzleFlash= (Transform)EditorGUILayout.ObjectField("Muzzle Flash", enemyWeapon.muzzleFlash, typeof(Transform), true);
        enemyWeapon.accuracy = EditorGUILayout.Slider("Accuracy", enemyWeapon.accuracy, 0.4f, 0);
        enemyWeapon.distance = EditorGUILayout.FloatField("Distance", enemyWeapon.distance);
        enemyWeapon.shootInterval = EditorGUILayout.FloatField("Shoot Interval", enemyWeapon.shootInterval);
        enemyWeapon.enemyMeleeSound = EditorGUILayout.TextField("Melee Sound", enemyWeapon.enemyMeleeSound);
        enemyWeapon.enemyShootSound = EditorGUILayout.TextField("Shoot Sound", enemyWeapon.enemyShootSound);
        enemyWeapon.playerImpactSound = EditorGUILayout.TextField("Player Impact Sound", enemyWeapon.playerImpactSound);
        enemyWeapon.deathSound = EditorGUILayout.TextField("Death Sound", enemyWeapon.deathSound);
        EditorGUILayout.Space();
        GUILayout.EndVertical();
        
        switch (enemyWeapon.shootAbilities)
        {
            case EnemyWeapon.ShootAbilities.FreezeTarget:
                GUILayout.BeginVertical("HelpBox");
                EditorGUILayout.LabelField("Freeze Ability", EditorStyles.boldLabel);
                enemyWeapon.freezeDuration = EditorGUILayout.FloatField("Freeze Duration", enemyWeapon.freezeDuration);
                GUILayout.EndVertical();
                break;
            case EnemyWeapon.ShootAbilities.PoisonTarget:
                GUILayout.BeginVertical("HelpBox");
                EditorGUILayout.LabelField("Poison Ability", EditorStyles.boldLabel);
                enemyWeapon.poisonDuration = EditorGUILayout.FloatField("Poison Duration", enemyWeapon.poisonDuration);
                enemyWeapon.poisonDamage = EditorGUILayout.FloatField("Poison Damage", enemyWeapon.poisonDamage);
                GUILayout.EndVertical();
                break;
            case EnemyWeapon.ShootAbilities.SlowDownTarget:
                GUILayout.BeginVertical("HelpBox");
                EditorGUILayout.LabelField("SlowDown Ability", EditorStyles.boldLabel);
                textDimensions = GUI.skin.label.CalcSize(new GUIContent("SlowDown Duration "));
                EditorGUIUtility.labelWidth = textDimensions.x + 6;
                enemyWeapon.slowdownDuration = EditorGUILayout.FloatField("SlowDown Duration", enemyWeapon.slowdownDuration);
                enemyWeapon.speedDecrease = EditorGUILayout.FloatField("Speed Decrease", enemyWeapon.speedDecrease);
                GUILayout.EndVertical();
                break;
        }
        EditorGUILayout.EndVertical();
        if (GUI.changed)
            EditorUtility.SetDirty(enemyWeapon);
    }
}
