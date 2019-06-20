using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {

	void Start () {
        GameObject.Destroy(gameObject, Random.Range(5.0f, 10.0f));
	}

}
