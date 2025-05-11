using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels_txt_buttons;
    [SerializeField] private Button[] levels_image_buttons;
    private int level_unlocking = 0;
    private void Start()
    {
        
        for(int i=0;i < levels_txt_buttons.Length; i++)
        {
            if(PlayerPrefs.GetInt("Level_"+(i+1)+ "_unlocked", 0) == 1)
            {
                levels_txt_buttons[i].GetComponent<Text>().text = (i + 1).ToString();
                levels_image_buttons[i].interactable = true;
            }
            else
            {
                levels_txt_buttons[i].GetComponent<Text>().text = "Unlock";
                levels_image_buttons[i].interactable = false;
            }
        }
        
    }
    public void Unlock(int level)
    {
       level_unlocking = level;
       if(PlayerPrefs.GetInt("Level_"+level+ "_unlocked", 0) == 0)
            GetComponent<PlayAd>().ShowRewardedAd(level, levels_image_buttons[level - 1], true);
    }

    private void Update()
    {
        if (level_unlocking != 0)
        {
            if(PlayerPrefs.GetInt("Level_"+level_unlocking+ "_unlocked", 0)== 1)
            {
                levels_txt_buttons[level_unlocking - 1].GetComponent<Text>().text = level_unlocking.ToString();
                level_unlocking = 0;
            }
        }

    }
}
