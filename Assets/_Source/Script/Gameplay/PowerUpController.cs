using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpController : MonoBehaviour
{
    
    public List<SpriteRenderer> spriteVariations;

    private void OnEnable()
    {

        int randomId = Random.Range(0, spriteVariations.Count);
        for (int i = 0; i < spriteVariations.Count; i++)
        {
            spriteVariations[i].gameObject.SetActive( i == randomId);
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherPlayer = other.gameObject.GetComponent<SheepController>();
        if (otherPlayer)
        {
            otherPlayer.Fart();
            gameObject.SetActive(false);
        }
    }

 
    
    
}
