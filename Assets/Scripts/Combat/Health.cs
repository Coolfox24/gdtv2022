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

    public void TakeDamage(float amount)
    {
        curHealth -= amount;
        AudioPlayer.PlayClipAtPoint(dmgSound, this.transform.position);
        if(curHealth <= 0)
        {
            GetComponent<BaseStateMachine>().OnDie();
        }
    } 
}
