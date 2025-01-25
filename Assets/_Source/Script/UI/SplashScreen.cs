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

    // Update is called once per frame
    void LoadMainMenu()
    {
        gameConfig.LoadScene(mainMenuSceneName);
    }
}