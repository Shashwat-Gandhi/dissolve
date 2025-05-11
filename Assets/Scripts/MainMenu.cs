using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text sound_text;
    private static bool sound = true;
    
    public void Play()
    {
        if (sound)
        {
            sound_text.text = "Sound : ON";
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            sound_text.text = "Sound : OFF";
            PlayerPrefs.SetInt("Sound", 0);
        }
        SceneManager.LoadScene("Levels");
    }
    public void Credits()
    {

    }
    public void Sound()
    {
        sound = !sound;
        if (sound)
        {
            sound_text.text = "Sound : ON";
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            sound_text.text = "Sound : OFF";
            PlayerPrefs.SetInt("Sound", 0);
        }
    }
    public void Quit() {
        Application.Quit();
    }
    public void StartLevel(int level)
    {
        string scene_name = "Level " + level.ToString();
        SceneManager.LoadScene(scene_name);
    }
    public void OpenMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Reset()
    {
        for(int i=1;i <=5;i++)
        {
            PlayerPrefs.SetInt("Level_" + i + "_completion_status", 0);
            PlayerPrefs.SetInt("Level_" + i + "_highscore", 0);
            PlayerPrefs.SetInt("Level_" + i + "_stars_acquired", 0);
            PlayerPrefs.SetInt("Level_" + i + "_unlocked", 0);
        }
        PlayerPrefs.SetInt("Level_" + 1 + "_unlocked", 1);
    }
}
