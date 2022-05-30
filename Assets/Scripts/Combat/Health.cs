using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float curHealth{get; private set;}
    public float maxHealth = 10;
    [SerializeField] AudioClip dmgSound;


    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    public void HealFull()
    {
        curHealth = maxHealth;
    }

    public void AddMaxHealth(float amt)
    {
        maxHealth += amt;
        curHealth += amt;
    }

    public void TakeDamage(float amount)
    {
        if(curHealth <= 0)
        {
            return;
        }

        curHealth -= amount;
        AudioPlayer.PlayClipAtPoint(dmgSound, this.transform.position);
        if(curHealth <= 0)
        {
            GetComponent<BaseStateMachine>().OnDie();
        }
    } 
}
