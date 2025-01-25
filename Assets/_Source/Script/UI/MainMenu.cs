using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public sGameConfig gameConfig;
    public string gameplayScene = "03 Gameplay" ;

    public void PlayGame()
    {
        gameConfig.LoadScene(gameplayScene);
    }
}