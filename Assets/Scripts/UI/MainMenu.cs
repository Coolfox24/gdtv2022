using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioClip clip;

    public void StartGame()
    {
        Debug.Log("Starting Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Start()
    {
        FindObjectOfType<MusicPlayer>().PlayMusic(clip);
    }
}
