using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeFootprints;
    [SerializeField] private List<GameObject> disabledFootprints;
    [SerializeField] private Transform footprintPos;
    [SerializeField] private Transform footprintPos2;
    [SerializeField] private Transform footprints;
    [SerializeField] private int maxFootprints = 20;
    private int step;
    [SerializeField] private GameObject footprintPrefab;

    public AudioClip footstep1;
    public AudioClip footstep2;
    
    private void Start()
    {
        //Initialize Footprints
        activeFootprints = new List<GameObject>();
        disabledFootprints = new List<GameObject>();
        for(int i = 0; i < maxFootprints; i++)
        {
            GameObject foot = Instantiate(footprintPrefab);
            foot.GetComponent<Footprint>().Spawner = this;
            foot.SetActive(false);
            foot.transform.SetParent(footprints);
            disabledFootprints.Add(foot);
        }
    }
    
    public void SpawnFootprint(Vector2 facing, Vector2 position)
    {
        if(disabledFootprints.Count == 0)
        {
            return;
        }

        float angle = Mathf.Atan2(facing.x, -facing.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler (new Vector3(0f,0f,angle));

        GameObject foot = disabledFootprints[0];
        foot.GetComponent<Footprint>().Setup();
        disabledFootprints.Remove(foot);
        activeFootprints.Add(foot);
        foot.SetActive(true);
        foot.transform.rotation = targetRotation;

        if((step++ %2) == 0)
        {
            foot.transform.position = footprintPos.position;
            AudioPlayer.PlayClipAtPoint(footstep1, transform.position, 0.05f);
        }
        else
        {
            foot.transform.position = footprintPos2.position;
            AudioPlayer.PlayClipAtPoint(footstep2, transform.position, 0.05f);
        }

        if(step == 256)
        {
            step = 0;
        }
    }

    public void UnregisterFootprint(GameObject footprint)
    {
        activeFootprints.Remove(footprint);
        disabledFootprints.Add(footprint);
    }
}
