using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public sGameConfig gameConfig;
    public string gameplayScene = "03 Gameplay" ;

    public void PlayGame()
    {
        gameConfig.LoadScene(gameplayScene);
    }
}