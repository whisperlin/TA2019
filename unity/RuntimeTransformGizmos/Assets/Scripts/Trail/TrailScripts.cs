using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScripts : MonoBehaviour {

    private Rigidbody m_Rigidbody;
    private Transform m_Transform;

	void Start () {
      m_Rigidbody = gameObject.GetComponent<Rigidbody>();
      m_Transform = gameObject.GetComponent<Transform>();

	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("MoveSelf", 0.1f,0.5f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            CancelInvoke();
        }
	}
    void MoveSelf()
    {
        Vector3 position = new Vector3(Random.Range(-8f, 8f), Random.Range(1f, 3.6f), Random.Range(-8f, 6f));
        m_Rigidbody.MovePosition(m_Transform.position + position *0.5f);
    }
}
