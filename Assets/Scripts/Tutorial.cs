using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    //for level 1 show the image before starting the level

    [SerializeField] private GameObject img;
    [SerializeField] private GameObject img2;
    [SerializeField] private GameObject[] bars;
    [SerializeField] GameObject[] tiles;
    private void Start()
    {
        foreach(GameObject tile in tiles)
        {
            tile.SetActive(false);
        }
        Invoke("Unpause", 4f);
    }
    private void Unpause()
    {
        foreach (GameObject tile in tiles)
        {
            tile.SetActive(true);
        }
        Destroy(img);
        foreach(GameObject bar in bars)
        {
            bar.GetComponent<Bar>().enabled = true;
        }
    }
    private void OnMouseDown()
    {
        Destroy(img2);
        foreach(GameObject tile in tiles)
        {
            tile.GetComponent<Tile>().enabled = true;
        }
    }
}
