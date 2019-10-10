using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCharacterFellowCamera : MonoBehaviour {

 
    public Transform cam;
    public Transform target;
    public Vector3 offset;
    public float distance = 10f;
    public float xRot = 45f;
    public float yRot = 30f;


    public float distanceDelta = 10f;
    public float RotDelta = 45;

    float cur_distance = 10f;
    float cur_xRot = 45f;
    float cur_yRot = 30f;

    public bool slowly = true;
    public static bool notUpdateCamera = false;
    public static float moveCameraBack = -1f;


  
    float RotTo(float des,float src,float moveDelta)
    {
        while (des < src)
        {
            src -= 360f;
        }
        while (des > src + 360f)
        {
            src += 360f;
        }
        float delta0 = des - src;
        //选择旋转方向.
        if (delta0 > 180f)
        {
            src += 360f;
            delta0 = des - src;
      
        }
        return  MoveToLerp(des, src, moveDelta) ;
    }

    float MoveToLerp(float des, float src, float moveDelta)
    {
        float delta = des - src;
        if (delta == 0)
        {
            return des;
        }
        
        float absDelta = Mathf.Abs(delta);
        float sign = delta > 0 ? 1f : -1;
        if (absDelta < moveDelta)
        {
            return des;
        }
        else
        {
            return src + moveDelta * sign;
        }
        
    }
    static Transform cmp;
    public void GoToTarget()
    {
        
        //确保大于0f.
        if (distanceDelta < 0.001f)
            distanceDelta = 0.001f;
        if (RotDelta < 0.001f)
            RotDelta = 0.001f;

        if (yRot > 90f)
            yRot = 90f;

        if (yRot < -90f)
            yRot = -90f;



        cur_distance = MoveToLerp(distance, cur_distance, Time.deltaTime * distanceDelta);
        if (slowly)
        {
            cur_xRot = RotTo(xRot, cur_xRot, Time.deltaTime * RotDelta);
            cur_yRot = RotTo(yRot, cur_yRot, Time.deltaTime * RotDelta);
        }
        else
        {
            cur_xRot = xRot;
            cur_yRot = yRot;
        }
       
 
    }
    public void SetToTarget()
    {
        cur_distance = distance;
        cur_xRot = xRot;
        cur_yRot = yRot;
    }

    private void Start()
    {
  
        SetToTarget();
    }
    

    void LateUpdate () {

        if (notUpdateCamera)
            return;
        if (null == cam)
        {
            cam = Camera.main.transform;
        }
        if (null == cam)
            return;
        if (null == target)
            return;

        
        /*if (null == sphereCollider)
        {
            GameObject g = new GameObject("ForEvent");
            sphereCollider = g.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            g.transform.parent = target;
            Rigidbody r = g.AddComponent<Rigidbody>();
            r.isKinematic = true;
            r.useGravity = false;
            g.layer = eventLayer;
            SimpleTarget st = g.AddComponent<SimpleTarget>();
            st.sfc = this;
            g.transform.localPosition = Vector3.zero;
        }*/
        GoToTarget();

        if (null != target.parent)
        {
            target.parent = null;
        }

        if (null == cmp)
        {
            GameObject g = new GameObject();
            g.name = "Cmp Cam";
            g.hideFlags = HideFlags.HideAndDontSave;
            cmp = g.transform;
        }

        cmp.position = target.position+ offset;
        cmp.rotation = Quaternion.identity;
        cmp.Rotate(Vector3.up, cur_xRot );
        cmp.Rotate(Vector3.right, cur_yRot);
        cmp.position = cmp.position - cmp.forward * cur_distance;


        if (moveCameraBack > 0f)
        {
 
            cam.position = Vector3.MoveTowards(cam.position, cmp.position, 0.1f);
            cam.rotation = Quaternion.RotateTowards(cam.rotation, cmp.rotation, RotDelta * 0.0175f);
            moveCameraBack -= Time.deltaTime;
 
        }
        else
        {
            cam.position = cmp.position;
            cam.forward = cmp.forward;
        }
       
    }
}
