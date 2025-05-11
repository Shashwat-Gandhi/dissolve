using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private int no_of_frames;
    [SerializeField] private Animator animator;
    [SerializeField] private Game_Manager game_manager;
    private float cooldown_time;
    void Start()
    {
        cooldown_time = game_manager.cooldown_time;
        float speed = no_of_frames / cooldown_time;
        speed /= 12;
        animator.speed = speed;
        Debug.Log(speed);
        animator.StopPlayback();
    }
    public void Reset()
    {
        animator.Play("cooldown_filling_anim");
    }
    public void Finish()
    {
      //  animator.StopPlayback();
    }
}
