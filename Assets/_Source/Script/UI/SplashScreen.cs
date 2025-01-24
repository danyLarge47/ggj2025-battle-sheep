using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public float waitTime;
    public string mainMenuSceneName;
    void Start()
    {
        Invoke("LoadMainMenu" ,waitTime );
    }

    // Update is called once per frame
    void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);

    }
}
