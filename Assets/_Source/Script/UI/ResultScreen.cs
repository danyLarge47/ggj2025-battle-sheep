using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    public sGameConfig gameConfig;
    public TextMeshProUGUI txtTittle;
    
    void Start()
    {
        txtTittle.text = $"Player {gameConfig.playerWinId} Wins!";
    }

 
}
