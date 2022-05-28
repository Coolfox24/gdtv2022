using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GameMusic : MonoBehaviour
{
    public Tilemap floor;
    public AudioClip town;
    public RuleTile townTile;
    public AudioClip plains;
    public RuleTile plainsTile;
    public AudioClip beach;
    public RuleTile beachTile;
    public AudioClip desert;
    public RuleTile desertTile;
    public AudioClip bandits;
    public RuleTile banditTile;
    public AudioClip snow;
    public RuleTile snowTile;
    public AudioClip deathFight;
    public bool InDeathFight = false;

    public AudioClip afterlife;
    public RuleTile afterLifeTile;

    MusicPlayer player;
    RuleTile lastTile;
    void Start()
    {
        player = FindObjectOfType<MusicPlayer>();
        Debug.Log(player);
    }

    void Update()
    {
        if(InDeathFight)
        {
            //play death music if not already playing
            if(player.IsPlaying(deathFight))
            {
                return;
            }
            player.PlayMusic(deathFight);

            return;
        }

        //Get tile we're on
        RuleTile t = (RuleTile)floor.GetTile(floor.WorldToCell(this.transform.position));
        if(lastTile == t)
        {
            return;
        }
        lastTile = t;

        if(t == desertTile)
        {
            player.PlayMusic(desert);
        }
        else if(t == plainsTile)
        {
            player.PlayMusic(plains);
        }
        else if(t == beachTile)
        {
            player.PlayMusic(beach);
        }
        else if(t == snowTile)
        {
            player.PlayMusic(snow);
        }
        else if(t == banditTile)
        {
            player.PlayMusic(bandits);
        }
        else if(t == afterLifeTile)
        {
            player.PlayMusic(afterlife);
        }
        else 
        {            
            if(player.IsPlaying(town))
            {
                return;
            }
            player.PlayMusic(town);
        }
    }
}
