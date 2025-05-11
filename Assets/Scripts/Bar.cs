using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private Transform target_pos;
    [SerializeField] private float speed;
    private void Start()
    {
        
    }
    private void Update()
    {
        transform.position += (target_pos.position - transform.position).normalized * speed * Time.deltaTime;
        if((transform.position - target_pos.position).magnitude < 0.1f)
        {
            Debug.Log("Finish Line Reached");
            this.GetComponent<Bar>().enabled = false;
        }
    }
}
