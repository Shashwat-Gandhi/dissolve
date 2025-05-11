using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuParticleEffect : MonoBehaviour
{
    public ParticleSystem[] particles;
    public Color[] colors;
    private int current_color = 0;
    private void Start()
    {
        foreach(ParticleSystem particle in particles)
        {
            particle.startColor = colors[0];
        }
        InvokeRepeating("ChangeColor", 1f,1f);
    }
    private void ChangeColor()
    {
        foreach (ParticleSystem particle in particles)
        {
            current_color = (current_color + 1) % colors.Length;
            particle.startColor = colors[current_color];
        }
    }
}
