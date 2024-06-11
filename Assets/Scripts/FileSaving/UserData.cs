using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int totalXP;
    public string username;

    public UserData()
    {
        totalXP = GameManager.totalXP;
        username = GameManager.username;
    }
}
