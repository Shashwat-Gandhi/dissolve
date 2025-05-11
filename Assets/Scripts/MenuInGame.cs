using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class MenuInGame : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button pause;
    [SerializeField] private Button[] menu_in_game_buttons;
    [SerializeField] private Toggle auto_retry_toggle;
    [SerializeField] private Button skip_level_button;
    [SerializeField] private Game_Manager game_manager;
    //Pull the menu on screen (to the y2 pos)
    public void Pause()
    {
        animator.SetTrigger("on");
        pause.interactable = false;
        foreach (Button bt in menu_in_game_buttons)
        {
            bt.interactable = true;
        }
        skip_level_button.interactable = false;
        string key = "Level_" + game_manager.level_num + "_completion_status";
        string key2 = "Level_" + game_manager.next_level + "_unlocked";
        if (!Advertisement.IsReady() || PlayerPrefs.GetInt(key, 0) == 1 || PlayerPrefs.GetInt(key2,0)==1)
            skip_level_button.interactable = true;
        else
            skip_level_button.interactable = false;
    }
    private void Update()
    {
        if (Advertisement.IsReady())
            skip_level_button.interactable = true;
    }
    public void StopTime()
    {
        Time.timeScale = 0;
    }
    public void ResumeTime()
    {
        Time.timeScale = 1;
    }
    public void Unpause()
    {
        animator.SetTrigger("off");
        pause.interactable = true;
        foreach(Button bt in menu_in_game_buttons)
        {
            bt.interactable = false;
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;
        if (auto_retry_toggle.isOn)
            PlayerPrefs.SetInt("AutoRetry", 1);
        else
            PlayerPrefs.SetInt("AutoRetry", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel(int next_level)
    {
        if(next_level != 0 && PlayerPrefs.GetInt("Level_"+ next_level + "_unlocked",0) == 1)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level " + next_level);
        }
        else
        {
            game_manager.gameObject.GetComponent<PlayAd>().ShowRewardedAd(next_level, skip_level_button);
        }
    }
    public void OpenMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ShowAd()
    {
    }
}
