using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldGeneration))]
public class WorldGeneratorEditor : Editor {

    WorldGeneration wg;

    public override void OnInspectorGUI()
    {
        wg = (WorldGeneration)target;

        base.OnInspectorGUI();

        if (GUILayout.Button("Generate"))
        {
            wg.GenerateMap();
        }
    }

}
