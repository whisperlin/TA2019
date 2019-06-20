using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject m_Gold;
    private Rigidbody m_Rigidbody;
    private Transform m_Transform;
    private int integral=0;

	void Start () {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Transform = gameObject.GetComponent<Transform>();
        Debug.Log("integral = " + integral + "  ;");
    }
	
	
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            MoveForword();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveBack();
        }
        if (Input.GetKey(KeyCode.W))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveLeft();
        }        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "sphere")
        {
            Vector3 position = collision.gameObject.transform.position;
            GameObject.Destroy(collision.gameObject,0.5f);
            GameObject.Instantiate(m_Gold, position+Vector3.forward*1.5f, Quaternion.identity);
        }   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Golds")
        {
            // integral++;
            GameObject.Destroy(other.gameObject, 0.8f);
            //   Debug.Log("integral = " + integral + "  ;");
            //  other.gameObject.SendMessage("AddScore");
            other.gameObject.GetComponent<Gold>().AddScore();//
        }
    }
    //Move 
    void MoveForword()
    {
        m_Rigidbody.MovePosition(m_Transform.position + Vector3.forward * 0.2f);
    }
 
    void MoveBack()
    {
        m_Rigidbody.MovePosition(m_Transform.position + Vector3.back * 0.2f);
    }
    void MoveRight()
    {
        m_Rigidbody.MovePosition(m_Transform.position + Vector3.right* 0.2f);
    }
    void MoveLeft()
    {
        m_Rigidbody.MovePosition(m_Transform.position + Vector3.left * 0.2f);
    }
}
