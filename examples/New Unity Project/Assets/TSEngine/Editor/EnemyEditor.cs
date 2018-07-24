using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(EnemyCharacteristics))]
public class EnemyEditor : Editor {

    EnemyCharacteristics enemyChar;
    [SerializeField]

    public override void OnInspectorGUI()
    {
        enemyChar = (EnemyCharacteristics)target;
        var textDimensions = GUI.skin.label.CalcSize(new GUIContent("Length:"));
        EditorGUIUtility.labelWidth = textDimensions.x + 10;

        GUILayout.BeginVertical("HelpBox");
        GUILayout.BeginVertical("GroupBox");
        enemyChar.length = EditorGUILayout.IntField("Length:", enemyChar.length);
        enemyChar.length = Mathf.Clamp(enemyChar.length, 1, 3);
        if (enemyChar.length != enemyChar.abilities.Count)
        {
            while (enemyChar.abilities.Count != enemyChar.length)
            {
                if (enemyChar.abilities.Count < enemyChar.length)
                    enemyChar.abilities.Add(EnemyCharacteristics.SpecialAbilities.Chase);
                else
                {
                    enemyChar.abilities.RemoveAt(enemyChar.abilities.Count - 1);
                }
            }
        }

        for (int i = 0; i < enemyChar.abilities.Count; i++)
        {
            textDimensions = GUI.skin.label.CalcSize(new GUIContent("   Ability "+i));
            EditorGUIUtility.labelWidth = textDimensions.x + 6;
            enemyChar.abilities[i] = (EnemyCharacteristics.SpecialAbilities)EditorGUILayout.EnumPopup("   Ability " + i,enemyChar.abilities[i]);
        }

        GUILayout.EndVertical();

        textDimensions = GUI.skin.label.CalcSize(new GUIContent("Melee Damage"));
        EditorGUIUtility.labelWidth = textDimensions.x + 6;

        EditorGUILayout.Space();

        GUILayout.BeginVertical("HelpBox");
        EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
        enemyChar.maxHealth = EditorGUILayout.FloatField("Max Health", enemyChar.maxHealth);
        EditorGUILayout.Space();
        GUILayout.EndVertical();

        if (ContainsAbility(enemyChar.abilities,EnemyCharacteristics.SpecialAbilities.Chase))
        {
            /*
            {
                EditorGUILayout.Space();
                GUILayout.BeginVertical("HelpBox");
                EditorGUILayout.LabelField("Chase Ability", EditorStyles.boldLabel);
                enemyChar.chaseSpeed = EditorGUILayout.FloatField("Chase Speed", enemyChar.chaseSpeed);
                if (!ContainsAbility(enemyChar.abilities, EnemyCharacteristics.SpecialAbilities.SelfDestroyOnImpact))
                    enemyChar.damage = EditorGUILayout.FloatField("Melee Damage", enemyChar.damage);
                EditorGUILayout.Space();
                GUILayout.EndVertical();
            }*/
            
        }

        if (ContainsAbility(enemyChar.abilities, EnemyCharacteristics.SpecialAbilities.shoot))
        {
            textDimensions = GUI.skin.label.CalcSize(new GUIContent("Shoot Interval"));
            EditorGUIUtility.labelWidth = textDimensions.x + 9;

            EditorGUILayout.Space();
            GUILayout.BeginVertical("HelpBox");
            EditorGUILayout.LabelField("Shooting", EditorStyles.boldLabel);
            enemyChar.enemyWeapon = (EnemyWeapon)EditorGUILayout.ObjectField("Enemy Weapon", enemyChar.enemyWeapon, typeof(EnemyWeapon), true);
            EditorGUILayout.Space();
            GUILayout.EndVertical();
        }

        if (ContainsAbility(enemyChar.abilities, EnemyCharacteristics.SpecialAbilities.SelfDestroyOnImpact))
        {
            textDimensions = GUI.skin.label.CalcSize(new GUIContent("Explosion duration "));
            EditorGUIUtility.labelWidth = textDimensions.x + 9;

            EditorGUILayout.Space();

            GUILayout.BeginVertical("HelpBox");
            EditorGUILayout.LabelField("Self Destroy", EditorStyles.boldLabel);
            enemyChar.explosionEffect = (Transform)EditorGUILayout.ObjectField("Explosion Effect", enemyChar.explosionEffect, typeof(Transform), true);
            enemyChar.explosionDamage = EditorGUILayout.FloatField("Explosion Damage", enemyChar.explosionDamage);
            enemyChar.explosionDuration = EditorGUILayout.FloatField("Explosion Duration", enemyChar.explosionDuration);
            EditorGUILayout.Space();
            GUILayout.EndVertical();
        }
        GUILayout.EndVertical();
        if(GUI.changed)
        EditorUtility.SetDirty(enemyChar);

    }

    public bool ContainsAbility(List<EnemyCharacteristics.SpecialAbilities> abilities, EnemyCharacteristics.SpecialAbilities ability)
    {
        foreach (EnemyCharacteristics.SpecialAbilities ab in abilities)
        {
            if (ab == ability)
                return true;
        }

        return false;
    }
}
