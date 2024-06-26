using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager
{
    public GameManager()
    {
        totalXP = this.totalXP;
        username = this.username;
        password = this.password;
        isPlaying = this.isPlaying;
        gameMode = this.gameMode;
    }

    public int totalXP { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public bool isPlaying { get; set; }
    public int gameMode { get; set; }
}
