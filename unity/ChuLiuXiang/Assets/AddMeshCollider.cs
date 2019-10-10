using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMeshCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MeshFilter[] mfs = GetComponentsInChildren<MeshFilter>();
        for (int i = 0; i < mfs.Length; i++)
        {
            var _m = mfs[i];
            MeshCollider mc =  _m.gameObject.AddComponent<MeshCollider>();
            if (null != mc)
            {
                mc.sharedMesh = _m.sharedMesh;
            }
            



        }
		
	}
	
	 
}
