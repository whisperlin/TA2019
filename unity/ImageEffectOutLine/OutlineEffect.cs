//#define __DEBUGGING___
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class OutlineEffect : MonoBehaviour {

    public LayerMask mark = -1;
    [Header("描边物体所在层")]
    public Color color = Color.red;
    public Shader shader;
    public Shader shader2;
   
#if __DEBUGGING___
    public RenderTexture rt0;
    public Material mat;
#else
     Material mat;
#endif

    static Camera cam;
	// Use this for initialization
	void Start () {
		
	}

    private void OnDestroy()
    {
        if (null != mat)
        {
            GameObject.DestroyImmediate(mat, true);
        }
    }
            
    private void OnPostRender()
    {
        if (null == cam)
        {
            GameObject g = new GameObject("Camera");
            cam = g.AddComponent<Camera>();
            g.hideFlags = HideFlags.HideAndDontSave;
        }
        cam.CopyFrom(Camera.main);
        cam.transform.position = Camera.main.transform.position;
        cam.transform.forward = Camera.main.transform.forward;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.black;
        cam.cullingMask = mark;
        cam.enabled = false;
        if (null == shader)
            shader = Shader.Find("Hidden/Write");
        if (null == shader2)
            shader2 = Shader.Find("Hidden/OutlineEffect");
        if (null == mat)
            mat = new Material(shader2);
#if __DEBUGGING___
        if (null == rt0)
        {
            rt0 = new  RenderTexture(Screen.width/4,Screen.height/4,16);
            
        }
#else

         RenderTexture rt0 =  RenderTexture.GetTemporary(Screen.width/4,Screen.height/4,16);

#endif

        mat.SetColor("_Color", color);
        cam.SetReplacementShader(shader, null);
        cam.targetTexture = rt0;
        cam.Render();

        GL.PushMatrix();
        GL.LoadOrtho();
        mat.SetTexture("_MainTex",rt0);
        for (var i = 0; i < mat.passCount; ++i)
        {
            mat.SetPass(i);
            GL.Begin(GL.QUADS);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-1, -1, 0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-1, 1, 0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1, 1, 0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1, -1, 0);

 
            GL.End();
        }
        GL.PopMatrix();



#if __DEBUGGING___

#else
        RenderTexture.ReleaseTemporary(rt0);
       
       
#endif

    }

    // Update is called once per frame
    void Update () {


#if __DEBUGGING___


#endif

 

    }
}
