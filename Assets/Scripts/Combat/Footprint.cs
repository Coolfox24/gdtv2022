using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    [SerializeField] float timeRemaining = 5f;
    [SerializeField] float curTime;
    [SerializeField] public SpriteRenderer sr;

    public FootprintSpawner Spawner;
    // Update is called once per frame
    void Start()
    {

    }
    
    public void Setup()
    {
        curTime = timeRemaining;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
    }

    void Update()
    {
        curTime -= Time.deltaTime;
        if(curTime < 0)
        {
            //Disable
            gameObject.SetActive(false);

            Spawner.UnregisterFootprint(this.gameObject);
        }
        if(curTime < timeRemaining / 2)
        {
            //Set alpha
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1 * (curTime / 2) / (timeRemaining/2));
        }
    }
}
