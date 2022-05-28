using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float timeToLive = 10f;
    protected float dmg;
    protected float remainingHits = 1;
    public float speed;

    public virtual void Setup(float amount, int hits)
    {
        dmg = amount;
        remainingHits = hits;
    }

    public virtual void Setup(float amount)
    {
        dmg = amount;
    }

    private void Update()
    {
        if((timeToLive -= Time.deltaTime) < 0)
        {
            //Destroy Projectile
            //TODO Possibly change this and spawning to use a pool of a large amount rather than destroying
            GameObject.Destroy(this.gameObject);
        }   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Projectile" || other.gameObject.tag == "IgnoreProjectiles")
        {
            return;
        }
        if(other.gameObject.tag == "Enemy" && remainingHits > 0)
        {
            remainingHits--;
            other.GetComponent<Health>().TakeDamage(dmg);
        }

        if(remainingHits <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

}
