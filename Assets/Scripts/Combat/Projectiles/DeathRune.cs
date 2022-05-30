using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRune : MonoBehaviour
{
    public float damage;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Deal Dmg");
    }
}
