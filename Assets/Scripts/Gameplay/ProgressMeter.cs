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
        HandleGameMode(currentFill);
    }

    private void HandleProgressMeter()
    {
        currentFill = GameManager.totalXP;

        float currentOffset = currentFill - minFill;
        float maxOffset = maxFill - minFill;
        float fillAmount = (float)currentFill / (float)maxFill;
        barMaskObject.fillAmount = fillAmount;
    }
    private void HandleGameMode(int fill)
    {
        switch (fill)
        {
            case 0:
                GameManager.SwitchGameMode(0);
                break;
            case 50:
                GameManager.SwitchGameMode(1);
                break;
            case 75:
                GameManager.SwitchGameMode(2);
                break;
            default:
                break;
        }

    }
}
