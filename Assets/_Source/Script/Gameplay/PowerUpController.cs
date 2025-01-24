using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
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
