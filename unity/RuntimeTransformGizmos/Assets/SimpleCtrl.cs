using RTEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCtrl : MonoBehaviour {

    public EditorGizmoSystem sys;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void OnGUI () {

        if (GUILayout.Button("平移"))
        {
            sys.ChangeActiveGizmo(GizmoType.Translation);
        }
        if (GUILayout.Button("旋转"))
        {
            sys.ChangeActiveGizmo(GizmoType.Rotation);
        }
        if (GUILayout.Button("缩放"))
        {
            sys.ChangeActiveGizmo(GizmoType.Scale);
        }

    }
}
