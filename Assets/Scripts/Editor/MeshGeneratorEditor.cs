using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshGenerator))]
public class MeshGeneratorEditor : Editor
{
    MeshGenerator chunk;

    //public override void OnInspectorGUI()
    //{
    //    //using (var check = new EditorGUI.ChangeCheckScope())
    //    //{
    //    //    base.OnInspectorGUI();
    //    //    if (check.changed)
    //    //    {
    //    //        chunk.CreatShape();
    //    //    }
    //    //}
    //    if (GUILayout.Button("Regenerate Mesh"))
    //    {
    //        chunk.CreatShape();
    //    }

    //}
    private void OnEnable()
    {
        chunk = (MeshGenerator)target;
    }
}
