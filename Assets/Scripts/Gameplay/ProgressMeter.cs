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

    GameManager gameManager;
    private void Awake()
    {
        gameManager = new GameManager();
    }
    private void Update()
    {
        HandleProgressMeter();
        HandleGameMode();
    }

    private void HandleProgressMeter()
    {
        currentFill = gameManager.totalXP;
        float currentOffset = currentFill - minFill;
        float maxOffset = maxFill - minFill;
        float fillAmount = (float)currentFill / (float)maxFill;
        barMaskObject.fillAmount = fillAmount;
    }
    private void HandleGameMode()
    {
        switch (currentFill)
        {
            case 0:
                gameManager.gameMode = 0;
                break;
            case 50:
                gameManager.gameMode = 1;
                break;
            case 75:
                gameManager.gameMode = 2;
                break;
            default:
                break;
        }

    }
}
