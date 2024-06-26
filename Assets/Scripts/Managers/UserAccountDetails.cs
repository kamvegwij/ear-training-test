using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserAccountDetails : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameText;

    private void Start()
    {
        GameManager gameManager = new GameManager();
        usernameText.text = gameManager.username;
    }
}
