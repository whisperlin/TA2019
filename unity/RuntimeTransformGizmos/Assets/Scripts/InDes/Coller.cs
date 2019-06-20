using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coller : MonoBehaviour {


    public GameObject GoPrefab;
    private void Start()
    {
        //  Invoke("creatSphere", 1.5f);
        InvokeRepeating("creatSphere", 1.5f, 1.0f);
    }

    	void Update () {
            if (Input.GetKeyDown(KeyCode.Space))
            {
            CancelInvoke();
            }
        }
       
    void creatSphere()
    {
        for(int i = 0; i < 5; i++)
        {
            Vector3 position = new Vector3(Random.Range(-2.0f, 10.0f), 10.0f, Random.Range(-11.0f, 0.0f));
            GameObject.Instantiate(GoPrefab, position, Quaternion.identity);
        }
    }

    
}
