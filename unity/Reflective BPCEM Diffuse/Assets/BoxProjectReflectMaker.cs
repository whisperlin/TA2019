using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class BoxProjectReflectMaker : MonoBehaviour {
    public Vector3 scale = new Vector3(10,10,10);

    public Cubemap cube;
    Camera cam;
    // Use this for initialization
    void Start () {
        
         
    }
	
	// Update is called once per frame
	void Update () {
        if (null == cam)
        {
            cam = gameObject.GetComponent<Camera>();
            if (null == cam)
            {
                cam = gameObject.AddComponent<Camera>();
            }
            cam.enabled = false;
        }

        this.transform.forward = Vector3.forward;
#if UNITY_EDITOR
        cam.RenderToCubemap(cube);

#endif
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.2F);
        Gizmos.DrawCube(transform.position, scale);
    }

#endif


}
