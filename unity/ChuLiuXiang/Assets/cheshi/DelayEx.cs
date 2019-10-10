using UnityEngine;
using System.Collections;

public class DelayEx : MonoBehaviour {
    // 延迟类型
    public enum DelayType
    {
        OneTimes,    // 第一次加载对象时延迟一次
        EveryTimes,   // 每次显示对象都会延迟一次
    }
    public DelayType delayType = DelayType.EveryTimes;
    public float delayTime; // 延迟时间
    private float duration = 0f;
    private GameObject effectObj = null;
    private bool isDelay = false;

	// Use this for initialization
	void Start () {
        if (delayType == DelayType.OneTimes)
        {
            StartDelay();
        }
        
    }

    void OnEnable()
    {
        if (delayType == DelayType.EveryTimes)
        {
            StartDelay();
        }
    }

    void StartDelay()
    {
        if (effectObj == null)
        {
            if (transform.childCount == 0)
            {
                Debug.LogError("子节点对象不能为0");
                return;
            }
            effectObj = transform.GetChild(0).gameObject;
        }

        isDelay = true;
        duration = delayTime;
        effectObj.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isDelay)
        {
            return;
        }

        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            effectObj.SetActive(true);
            isDelay = false;
        }
	}
}
