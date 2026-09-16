using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image HealthImage;
    public Image HealthDelayImage;
    public Image PowerImage;

    private void Update()
    {
        if(HealthDelayImage.fillAmount > HealthImage.fillAmount)
        {
            HealthDelayImage.fillAmount -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 接收health的变更百分比
    /// </summary>
    /// <param name="persentage"></param>
    public void OnHealthChange(float persentage)
    {
        HealthImage.fillAmount = persentage;
    }
}
