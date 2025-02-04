using System.Collections.Generic;
using GameCreator.Runtime.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class SheepController : MonoBehaviour
{
    public int playerId;
    public Rigidbody2D rb2d;
    public Transform spriteParent;
    public SpriteRenderer costume;
    public SpriteRenderer faceExpression;
    public float accel;
    public float maxVelocity;
    public float spawnFartRange;
    public float currentVelocity;
    public Vector2 currentDir;
    private Vector3 tempScale = Vector3.one;
    public sGameConfig gameConfig;
    public int currentHealth;
    public List<SpriteRenderer> health;
    public Actions onHit;

    private void OnEnable()
    {
        switch (playerId)
        {
            case 1:
                GameEvents.OnInputAction_Movement_P1.AddListener(MovePlayer);
                costume.sprite = gameConfig.costume1;
                break;
            case 2:
                GameEvents.OnInputAction_Movement_P2.AddListener(MovePlayer);
                costume.sprite = gameConfig.costume2;
                break;
            default:
                Debug.Log($"Player {playerId} undefined");
                break;
        }

        GameEvents.OnGameOver.AddListener(OnGameOver);
    }

    private void OnDisable()
    {
        switch (playerId)
        {
            case 1:
                GameEvents.OnInputAction_Movement_P1.RemoveListener(MovePlayer);
                break;
            case 2:
                GameEvents.OnInputAction_Movement_P2.RemoveListener(MovePlayer);
                break;
            default:
                Debug.Log($"Player {playerId} undefined");
                break;
        }

        GameEvents.OnGameOver.RemoveListener(OnGameOver);
    }

    private void OnGameOver(int player)
    {
        rb2d.linearVelocity = Vector2.zero;
    }

    private void Start()
    {
        currentHealth = health.Count;
        UpdateHealthUI();
        rb2d.AddForce(Random.insideUnitCircle * accel);
    }

    public void MovePlayer(Vector2 input)
    {
        if (!gameConfig.isGameOn) return;

        // Debug.Log($"MovePlayer {input}");
        rb2d.AddForce(input * accel);
        tempScale.x = input.x < 0 ? 1 : -1;
        spriteParent.transform.localScale = tempScale;
        currentDir = input;
    }

    private void Update()
    {
        // Clamp velocity
        if (rb2d.linearVelocity.magnitude > maxVelocity)
            rb2d.linearVelocity = rb2d.linearVelocity.normalized * maxVelocity;
        currentVelocity = rb2d.linearVelocity.magnitude;
    }

    [Button(ButtonSizes.Large)]
    public void Fart()
    {
        faceExpression.sprite = gameConfig.GetRandomExpression();
        GameEvents.SpawnGas.Invoke(this);
    }

    public Vector3 GetGasSpawnPos()
    {
        // var spawnOffset = Random.insideUnitCircle.normalized * spawnFartRange;
        var spawnOffset = -currentDir.normalized * spawnFartRange;
        return transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0);
    }

    public void DoDamage()
    {
        faceExpression.sprite = gameConfig.GetRandomExpression();
        onHit?.Run();
        currentHealth--;
        UpdateHealthUI();
        if (currentHealth <= 0) GameEvents.OnGameOver.Invoke(playerId);
    }

    private void UpdateHealthUI()
    {
        for (var i = 0; i < health.Count; i++) health[i].color = i < currentHealth ? Color.green : Color.black;
    }
}