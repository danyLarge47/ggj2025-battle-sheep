using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CombatSceneManager : MonoBehaviour
{
    public PoolAbleObject gasPrefab;
    public PoolAbleObject powerUpPrefab;
    public sGameConfig gameConfig;
    public float tickTimer;
    public float tickTarget;
    public string resultScreenName = "04 Result Screen";

    private void OnEnable()
    {
        GameEvents.SpawnGas.AddListener(ListenerSpawnFart);
        GameEvents.OnGameOver.AddListener(OnGameOver);
    }

    private void OnDisable()
    {
        GameEvents.SpawnGas.RemoveListener(ListenerSpawnFart);
        GameEvents.OnGameOver.RemoveListener(OnGameOver);
    }

    private void ListenerSpawnFart(SheepController origin)
    {
        GameEvents.SpawnGasPool.Invoke(gasPrefab, origin);
    }

    private void OnGameOver(int playerThatLose)
    {
        gameConfig.isGameOn = false;
        Debug.Log($"GAME OVER , Player {playerThatLose} Lose");
        if (playerThatLose == 1)
            gameConfig.playerWinId = 2;
        else if  (playerThatLose == 2)
            gameConfig.playerWinId = 1;

        gameConfig.LoadScene(resultScreenName);
    }

    private void Start()
    {
        tickTimer = 0;
        tickTarget = Random.Range(1, 4);
        gameConfig.isGameOn = true;
    }

    private void Update()
    {
        if (!gameConfig.isGameOn) return;
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickTarget)
        {
            tickTimer = 0;
            tickTarget = Random.Range(3, 8);
            SpawnPowerUps();
        }
    }

    void SpawnPowerUps()
    {
        Vector3 spawnPos = Random.insideUnitSphere * 5f;
        spawnPos.z = 0;
        GameEvents.SpawnPowerUps.Invoke(powerUpPrefab, spawnPos);
    }
}