using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabWeapon : MonoBehaviour
{
    [SerializeField] float swingSpeed;
    [SerializeField] float dmg;
    [SerializeField] int swingsRemaining;

    private float curSwingTime;
    private bool SameSwingDirection;
    Weapon baseWeapon;

    Vector2 OffSet;

    public void Setup(int swings, float dmg, float swingSpeed, Weapon baseWeapon, float stabLength)
    {
        this.baseWeapon = baseWeapon;
        swingsRemaining = swings;
        baseWeapon.swingsRemaining = swingsRemaining;
        this.dmg = dmg;
        this.swingSpeed = swingSpeed;

        OffSet = transform.up * stabLength;

        curSwingTime = swingSpeed; 
    }

    public void Update()
    {
        curSwingTime -= Time.deltaTime;
        
        
        if(curSwingTime > swingSpeed/2)
        {
            Vector2 t = Vector2.Lerp(Vector2.zero, OffSet, (curSwingTime / 2) / (swingSpeed / 2));
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y) + t * Time.deltaTime;
        }
        else
        {
            Vector2 t = Vector2.Lerp(OffSet, Vector2.zero, (curSwingTime / 2) / (swingSpeed / 2));
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y) - t * Time.deltaTime;
        }

        if(curSwingTime < 0)
        {
            if(swingsRemaining > 0)
            {
                swingsRemaining --;
                curSwingTime = swingSpeed;
            }
            else
            {
                baseWeapon.swingsRemaining = 0;
                Destroy(this.gameObject);
            }
        }  
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Health>().TakeDamage(dmg);
        }
    }
}

