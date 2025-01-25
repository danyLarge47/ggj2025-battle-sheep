using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "Dany Custom/Game Configs")]
public class sGameConfig : SerializedScriptableObject
{
    public int targetFrameRate = 60;
    public Color player1;
    public Color player2;
    public Sprite costume1;
    public Sprite costume2;
    public bool isGameOn;
    public int playerWinId;

    public List<Sprite> faceExpressions;

    public Essentials essentials;

    public void LoadScene(string sceneName)
    {
        essentials.LoadScene(sceneName);
    }

    public Sprite GetRandomExpression()
    {
        return faceExpressions[Random.Range(0, faceExpressions.Count)];
    }
}