using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Slider HealthBar;
    public Slider TimeBar;
    public Slider ExpBar;

    void Start()
    {
        ExpBar.value = 0;
    }

    public void UpdateHealth(int health)
    {
        HealthBar.value = health;
    }

    public void UpdateTime(int time)
    {
        TimeBar.value = time;
    }

    public void UpdateExp(int exp)
    {
        ExpBar.value = exp;
    }
}
