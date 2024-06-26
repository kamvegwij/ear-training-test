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
        GameManager gameManager = new GameManager();

        totalXP = gameManager.totalXP;
        username = gameManager.username;
        gameMode = gameManager.gameMode;
        password= gameManager.password;
    }
}
