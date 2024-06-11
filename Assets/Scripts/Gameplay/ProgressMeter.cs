using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMeter : MonoBehaviour
{
    public int maxFill = 100;
    public int minFill = 0;
    public int currentFill;

    [SerializeField] private Image barMaskObject;

    private void Update()
    {
        HandleProgressMeter();
    }

    private void HandleProgressMeter()
    {
        float currentOffset = currentFill - minFill;
        float maxOffset = maxFill - minFill;
        float fillAmount = (float)currentFill / (float)maxFill;
        barMaskObject.fillAmount = fillAmount;
    }
}
