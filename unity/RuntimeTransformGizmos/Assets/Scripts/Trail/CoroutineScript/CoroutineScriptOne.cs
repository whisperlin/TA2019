using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineScriptOne : MonoBehaviour {

	
	void Start () {
        Debug.Log("test one");
        StartCoroutine("TestTwo");
      
        StartCoroutine("TestThree");
        Debug.Log("test four");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine("TestThree");
            Debug.Log("Coroutine was stoped!,But mabey continue...");
        }
    }
    IEnumerator TestThree()
    {
          yield return new WaitForSeconds(1);
        Debug.Log("test three");
          yield return new WaitForSeconds(0);
        Debug.Log("test five , and Coroutine was end!");
    }
    IEnumerator TestTwo()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("test two");
    }
 
}
