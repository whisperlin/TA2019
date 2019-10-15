using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWeaterPerview : MonoBehaviour {

    public UnityEngine.UI.Slider sliderW;
    public UnityEngine.UI.Slider sliderR;
    public UnityEngine.UI.Slider sliderS;

    public GameObject _rain;
    public GameObject _snow;

    private void Start()
    {
        sliderW.onValueChanged.AddListener(delegate {
            wp.weatherColorIntensity = sliderW.value;
            wp.UploadParams();
        } );
        sliderR.onValueChanged.AddListener(delegate {
            //Debug.LogError("sliderR.value =");
            //Debug.LogError(sliderR.value);
            wp.rainBumpPower = sliderR.value;
            wp.UploadParams();
        });
        sliderS.onValueChanged.AddListener(delegate {
            wp._SnowPower = sliderS.value;
            wp.UploadParams();
        });

    }
    public WeatherPreview wp;
	// Use this for initialization
	public void hidleAll () {
        SetRainShow(false, false);

    }
    public void rain()
    {
        SetRainShow(true, false);

    }
    void SetRainShow(bool r,bool s)
    {
        if (null != _rain) _rain.SetActive(r);
        if (null != _snow) _snow.SetActive(s);
        wp.openRain = r;
        wp.openSnow = s;
    }
    public void snow()
    {
        
        SetRainShow(false, true);

    }
    public void rainPower(float f)
    {
        wp.rainBumpPower = f;
    }
    public void weathPower(float f)
    {
        wp.weatherColorIntensity = f;
    }
    public void snowPower(float f)
    {
        wp._SnowPower = f;
    }

     


}
