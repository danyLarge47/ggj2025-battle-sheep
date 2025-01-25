using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "Dany Custom/Game Configs")]
public class sGameConfig : SerializedScriptableObject
{
    public int targetFrameRate = 60;
    public Color player1;
    public Color player2;
    public bool isGameOn;
    public int playerWinId;


    public Essentials essentials;


    public void LoadScene(string sceneName)
    {
        essentials.LoadScene(sceneName);
    }



}