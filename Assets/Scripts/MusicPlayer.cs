using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioSource musicToPlay;
    float musicVolume = .20f;
    private void Awake()
    {
        if(FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        musicToPlay.volume = musicVolume * Settings.volumePct;
    }

    public void PlayMusic(AudioClip newClip)
    {
        musicToPlay.clip = newClip;
        musicToPlay.Play();
    }

    public bool IsPlaying(AudioClip clip)
    {
        if(musicToPlay.isPlaying && clip == musicToPlay.clip)
        {
            return true;
        }
        return false;
    }
}