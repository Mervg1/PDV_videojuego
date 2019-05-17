using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void NewGame(string newGameName)
    {
        SceneManager.LoadScene(newGameName);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        SceneManager.LoadScene(data.level);
    }
}
