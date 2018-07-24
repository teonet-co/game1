using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pickup))]
public class PickupEditor : Editor {

    static Pickup pickup;

    private void OnEnable()
    {
        pickup = (Pickup)target;
    }

    public override void OnInspectorGUI()
    {   
        pickup.pickupType = (Pickup.PickupType)EditorGUILayout.EnumPopup(pickup.pickupType);

        switch (pickup.pickupType)
        {
            case Pickup.PickupType.Medkit:
                pickup.pickupType = Pickup.PickupType.Medkit;
                pickup.healthIncrease = EditorGUILayout.FloatField("Health Increase",pickup.healthIncrease);
                pickup.lifeTime = EditorGUILayout.FloatField("Lifetime", pickup.lifeTime);
                pickup.pickupEffect = EditorGUILayout.ObjectField("Pickup Effect",pickup.pickupEffect,typeof(GameObject),true) as GameObject;
                pickup.effectDuration = EditorGUILayout.FloatField("Effect Duration", pickup.effectDuration);
                break;
            case Pickup.PickupType.AmmoIncrease:
                pickup.pickupType = Pickup.PickupType.AmmoIncrease;
                pickup.ammoIncrease = EditorGUILayout.IntField("Ammo Increase", pickup.ammoIncrease);
                pickup.lifeTime = EditorGUILayout.FloatField("Lifetime", pickup.lifeTime);
                pickup.pickupEffect = EditorGUILayout.ObjectField("Pickup Effect", pickup.pickupEffect, typeof(GameObject), true) as GameObject;
                pickup.effectDuration = EditorGUILayout.FloatField("Effect Duration", pickup.effectDuration);
                break;
            case Pickup.PickupType.Shield:
                pickup.pickupType = Pickup.PickupType.Shield;
                pickup.shieldDuration = EditorGUILayout.FloatField("Shield Duration", pickup.shieldDuration);
                pickup.lifeTime = EditorGUILayout.FloatField("Lifetime", pickup.lifeTime);
                pickup.targetTag = EditorGUILayout.TextField("Target Tag", pickup.targetTag);
                pickup.shieldTag = EditorGUILayout.TextField("Shield Tag", pickup.shieldTag);
                pickup.pickupEffect = EditorGUILayout.ObjectField("Pickup Effect", pickup.pickupEffect, typeof(GameObject), true) as GameObject;
                pickup.effectDuration = EditorGUILayout.FloatField("Effect Duration", pickup.effectDuration);
                break;
        }

        if (GUI.changed)
            EditorUtility.SetDirty(pickup);

    }

}
