using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    private int totalXP;
    private string username;

    public UserData()
    {
        totalXP = GameManager.totalXP;
        username = GameManager.username;
    }
}
