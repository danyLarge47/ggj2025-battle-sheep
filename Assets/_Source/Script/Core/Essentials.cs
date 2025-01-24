using UnityEngine;

public class Essentials : MonoBehaviour
{
    static Essentials instance;
    private static bool applicationIsQuitting = false;
    public sGameConfig GameConfig;
    public static Essentials Instance
    {
        get
        {
            // Debug.Log($"Get Essentials Called applicationIsQuitting {applicationIsQuitting}");
            if (applicationIsQuitting) return null;
            if (instance == null)
            {
                var search = FindObjectsByType<Essentials>(FindObjectsSortMode.None);
                if (search.Length > 0)
                {
                    if (search[0] is Essentials) instance = search[0] as Essentials;
                }

                var prefab = Resources.Load("Essentials") as GameObject;
                var newObject = Instantiate(prefab);
                instance = newObject.GetComponent<Essentials>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        applicationIsQuitting = false;
        instance = this;
        Application.targetFrameRate = GameConfig.targetFrameRate;
        // Screen.SetResolution(1920, 1080, true);
        DontDestroyOnLoad(gameObject);
    }

  

    private void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}