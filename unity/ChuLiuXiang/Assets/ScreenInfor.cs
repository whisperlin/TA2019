using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInfor : MonoBehaviour {

    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            
            Debug.LogError
                (res.width + "x" + res.height);
        }
        Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
    }
}
