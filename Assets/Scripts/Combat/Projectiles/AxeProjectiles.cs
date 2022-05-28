using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectiles : Projectile
{
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((timeToLive -= Time.deltaTime) < 0)
        {
            Destroy(this.gameObject);
            return;
        }


        body.velocity -= new Vector2(0, .05f);
    }
}
