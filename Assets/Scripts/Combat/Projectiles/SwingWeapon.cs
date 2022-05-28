using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingWeapon : MonoBehaviour
{
    [SerializeField] float swingSpeed;
    [SerializeField] float dmg;
    [SerializeField] int swingsRemaining;

    private float curSwingTime;
    private bool SameSwingDirection;
    Weapon baseWeapon;

    Quaternion startingRotation;
    Quaternion endingRotation;
    Quaternion midPoint;

    public void Setup(int swings, float dmg, float swingSpeed, Weapon baseWeapon, float swingRadius, bool SameSwingDirection)
    {
        this.SameSwingDirection = SameSwingDirection;
        this.baseWeapon = baseWeapon;
        swingsRemaining = swings;
        baseWeapon.swingsRemaining = swingsRemaining;
        this.dmg = dmg;
        this.swingSpeed = swingSpeed;
        if(SameSwingDirection)
        {
            startingRotation = this.transform.rotation * Quaternion.Euler(0, 0, swingRadius);
            midPoint = this.transform.rotation * Quaternion.Euler(0, 0, swingRadius/3);
            endingRotation = this.transform.rotation * Quaternion.Euler(0, 0, 2 * swingRadius/3);
        }
        else 
        {
            startingRotation = this.transform.rotation * Quaternion.Euler(0, 0, swingRadius/2);
            endingRotation = this.transform.rotation * Quaternion.Euler(0, 0, -swingRadius/2);
        }

        curSwingTime = swingSpeed; 

        if(swings % 2 == 0)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

    }

    // Update is called once per frame
    void Update()
    {
        if(swingsRemaining <= 0)
        {
            baseWeapon.swingsRemaining = 0;
            Destroy(this.gameObject);
            return;
        }
        if(SameSwingDirection)
        {
            //Swing right   
            if((curSwingTime -= Time.deltaTime)  > 2 * swingSpeed / 3)
            {
                float swing = curSwingTime - (2 * swingSpeed/3);
                transform.rotation = Quaternion.Lerp(endingRotation, midPoint, swing/(swingSpeed/3));
            }
            else if(curSwingTime > swingSpeed / 3)
            {
                float swing = curSwingTime - (swingSpeed/3);
                transform.rotation = Quaternion.Lerp(startingRotation, endingRotation, swing/(swingSpeed/3));
            }
            else
            {
                transform.rotation = Quaternion.Lerp(midPoint, startingRotation, curSwingTime/(swingSpeed/3));
            }

            if(curSwingTime <= 0)
            {
                swingsRemaining--;
                if(swingsRemaining > 0)
                {
                    curSwingTime = swingSpeed;
                }
            }

            return;
        }

        if(swingsRemaining % 2 == 1)
        {
            //Swing right
            transform.rotation = Quaternion.Lerp(startingRotation, endingRotation, (curSwingTime -= Time.deltaTime)/swingSpeed);
            if(curSwingTime <= 0)
            {
                swingsRemaining--;
                if(swingsRemaining > 0)
                {
                    if(!SameSwingDirection)
                    {
                        FlipSprite();
                    }
                    curSwingTime = swingSpeed;
                }
            }
        }
        else
        {
            //Swing left
            transform.rotation = Quaternion.Lerp(endingRotation, startingRotation, (curSwingTime -= Time.deltaTime)/swingSpeed);
            if(curSwingTime <= 0)
            {
                swingsRemaining--;
                if(swingsRemaining > 0)
                {

                    FlipSprite();
                    curSwingTime = swingSpeed;
                }
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
