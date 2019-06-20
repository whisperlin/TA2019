using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testure1 : MonoBehaviour {

    //进入宿主后的颜色变化

    private GUITexture m_GUITexture;
    private Transform m_Transform;
    private Color m_Color;
    void Start()
    {
        m_GUITexture = gameObject.GetComponent<GUITexture>();
        m_Transform = gameObject.GetComponent<Transform>();
        m_Color = m_GUITexture.color;
    }
    private void OnMouseEnter()
    {
        m_GUITexture.color = Color.green;
    }
    private void OnMouseExit()
    {
        m_GUITexture.color = m_Color;
    }
    private void OnMouseDown()
    {
        m_GUITexture.color = Color.black;
    }
    private void OnMouseDrag()
    {
        //   m_Transform.position(0, 0, 0);
    }
}
