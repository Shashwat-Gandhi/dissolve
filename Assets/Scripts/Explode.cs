using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private Rigidbody[] pieces;
    public float explosion_force;
    public float explosion_radius;
    public Transform origin;
    private void Start()
    {
    }
    public void ExplodeNow()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
            GetComponent<AudioSource>().mute = false;
        else
            GetComponent<AudioSource>().mute = true;

        foreach (Rigidbody piece in pieces)
        {
            piece.AddExplosionForce(explosion_force, origin.position, explosion_radius);
        }
        Invoke("Delete", 1f);
    }
    void Delete()
    {
        Destroy(this.gameObject);
    }
}
