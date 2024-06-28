using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int totalXP;
    public int gameMode;
    public string username;
    public string password;

    public UserData()
    {
        totalXP = GameManager.totalXP;
        username = GameManager.username;
        gameMode = GameManager.gameMode;
        password= GameManager.password;
    }
}
