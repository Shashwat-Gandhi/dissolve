using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Game_Manager game_manager;
    [SerializeField] private GameObject broken_glass_prefab;
    [SerializeField] private float crack_vel;
    private SpriteRenderer sprite_renderer;
    [SerializeField] private Sprite crack_sprite;
    [SerializeField] private float break_vel;
    private bool broken = false; //we keep track of this as if it collides with two at the same
                                //time it 'WILL' break twice which we don't want to do.
    private bool is_cracked = false;
    private void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        if (game_manager.cooldown_on == false && Time.timeScale != 0)
        {
            game_manager.ResetCooldown();
            game_manager.x_score = 1;
            Break();
        }
        
    }
    private void Break()
    {
        broken = true;
        GameObject broken_glass = Instantiate(broken_glass_prefab, transform.position + transform.up.normalized * transform.localScale.y/2,Quaternion.identity);
        
        broken_glass.GetComponent<Explode>().ExplodeNow();
       
           
        broken_glass.SetActive(true);
        game_manager.ShatterTile();

        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bar"))
        {
            game_manager.GameOver();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude > crack_vel &&
            collision.relativeVelocity.magnitude < break_vel)
        {
            if (!is_cracked)
            {
                is_cracked = true;
                sprite_renderer = GetComponent<SpriteRenderer>();
                sprite_renderer.sprite = crack_sprite;
                if (this.gameObject.CompareTag("Bomb"))
                    Debug.Log("Bomb rvel ; " + collision.relativeVelocity);
                return;
            }
            else
            {
                if(!broken)
                    Break();
                return;
            }
        }
        else if(collision.relativeVelocity.magnitude > break_vel)
        {
            if(!broken)
                Break();
            return;
        }
      
    }
}
