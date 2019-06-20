using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioer : MonoBehaviour {

    private AudioSource m_AudioSoruce;
	void Start () {
        m_AudioSoruce = gameObject.GetComponent<AudioSource>();
	}
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_AudioSoruce.Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_AudioSoruce.Stop();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_AudioSoruce.Pause();
        }
    }
}
