using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text2 : MonoBehaviour {

    //进入宿主后的颜色变化

    private GUIText m_GUIText;
    private Transform m_Transform;
    private Color m_Color;
    void Start()
    {
        m_GUIText = gameObject.GetComponent<GUIText>();
        m_Transform = gameObject.GetComponent<Transform>();
        m_Color = m_GUIText.color;
    }
    private void OnMouseEnter()
    {
        m_GUIText.color = Color.green;
    }
    private void OnMouseExit()
    {
        m_GUIText.color = m_Color;
    }
    private void OnMouseDown()
    {
        m_GUIText.color = Color.black;
    }
    private void OnMouseDrag()
    {
        //   m_Transform.position(0, 0, 0);
    }
}
