using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public float waitTime;
    public string mainMenuSceneName;
    public sGameConfig gameConfig;

    void Start()
    {
        Invoke("LoadMainMenu", waitTime);
    }

    void LoadMainMenu()
    {
        gameConfig.LoadScene(mainMenuSceneName);
    }
}