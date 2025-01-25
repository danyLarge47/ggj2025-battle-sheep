using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    public sGameConfig gameConfig;
    public TextMeshProUGUI txtTittle;
    public GameObject winner_p1;
    public GameObject winner_p2;
    
    void Start()
    {
        txtTittle.text = $"Player {gameConfig.playerWinId} Wins!";
        
        winner_p1.SetActive(gameConfig.playerWinId == 1);
        winner_p2.SetActive(gameConfig.playerWinId == 2);
    }

 
}
