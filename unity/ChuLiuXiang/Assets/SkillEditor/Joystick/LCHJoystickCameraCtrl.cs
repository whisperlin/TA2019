using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LCHJoystickCameraCtrl : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public LCharacterFellowCamera fellowCanera;
    public UnityEngine.UI.Image panel;
    private Vector2 beginPos;

    public float yRotScale = 0.1f;
    public float xRotScale = 0.1f;
    void Start()
    {
        if (null != panel)
        {
            panel.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height );

            
        }

 
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
   
        beginPos = eventData.position;
         
    }

    public void OnDrag(PointerEventData eventData)
    {
       
        var p = eventData.position;
        Vector2 dir = (p - beginPos);
        //Debug.LogError("OnDrag " + dir);
        //fellowCanera.xRot += p.x;
        // Debug.LogError("fellowCanera.yRot " + fellowCanera.yRot);
        fellowCanera.xRot += dir.x * xRotScale;
        fellowCanera.yRot -= dir.y* yRotScale;
        //Debug.LogError(">fellowCanera.yRot " + fellowCanera.yRot);
        fellowCanera.SetToTarget();
        beginPos = p;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
         
    }
     
}
