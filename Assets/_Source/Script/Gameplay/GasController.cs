using System;
using UnityEngine;

public class GasController : MonoBehaviour
{
    public int playerId;
    public Rigidbody2D rb;
    public float accel = 20f;
    public SpriteRenderer ownerIndicator;
    public sGameConfig gameConfig;
    private SheepController playerOrigin;

    private float maxVelocity = 5f;

    private void OnEnable()
    {
        GameEvents.OnGameOver.AddListener(OnGameOver);
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver.RemoveListener(OnGameOver);
    }

    private void OnGameOver(int player)
    {
        rb.velocity = Vector2.zero;
    }

    public void SetupGasFromPlayer(SheepController origin)
    {
        playerId = origin.playerId;
        playerOrigin = origin;
        switch (playerId)
        {
            case 1:
                ownerIndicator.color = gameConfig.player1;
                break;
            case 2:
                ownerIndicator.color = gameConfig.player2;
                break;
        }
    }

    public void LaunchGas()
    {
        rb.AddForce(-playerOrigin.currentDir * accel * maxVelocity);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var otherPlayer = other.gameObject.GetComponent<SheepController>();
        if (otherPlayer)
        {
            if (playerId != otherPlayer.playerId)
            {
                gameObject.SetActive(false);
                Debug.Log($"Gas {playerId} Collide with {otherPlayer.playerId}");
                otherPlayer.DoDamage();
            }
        }
    }

    private void Update()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }
}