using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject cooldown;
    public float cooldown_time;
    public bool cooldown_on = false;
    private int tiles_num;
    private int score;
    [SerializeField] private Text score_text;
    [SerializeField] private int tile_break_points;
    public int x_score = 1;
    [SerializeField] private int star1_score;
    [SerializeField] private int star2_score;
    [SerializeField] private int star3_score;
    [SerializeField] private Image star1_image;
    [SerializeField] private Image star2_image;
    [SerializeField] private Image star3_image;
    [SerializeField] private Sprite star_sprite;
    [SerializeField] private Button pause_img_btn;
    [SerializeField] private GameObject final_canvas;
    [SerializeField] private Image final_canvas_star1;
    [SerializeField] private Image final_canvas_star2;
    [SerializeField] private Image final_canvas_star3;
    public int level_num;
    public static int trial = 0;
    public static int trial_6 = 0;
    [SerializeField] private Button final_canvas_skip_level_image_button;
    public int next_level = 0;
    private void Start()
    {
        tiles_num = GameObject.FindGameObjectsWithTag("Tile").Length;
        score = 0;
        x_score = 1;
        score_text.text = score.ToString();
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void ShatterTile()
    {
        tiles_num--;
        UpdateScore();
        if (tiles_num == 0)
        {
            FinishLevel();
        }
        else if (tiles_num < 0)
        {
            Debug.Log("Something is wrong, tiles_num :" + tiles_num);
        }
    }
    private void UpdateScore()
    {
        score += tile_break_points * x_score;
        score_text.text = score.ToString();
        x_score *= 2;
        Debug.Log("Score : " + score);
        if (score >= star1_score)
            star1_image.sprite = star_sprite;
        if (score >= star2_score)
            star2_image.sprite = star_sprite;
        if (score >= star3_score)
            star3_image.sprite = star_sprite;
    }
    private void FinishLevel()
    {
        PlayerPrefs.SetInt("Level_" + level_num + "_completion_status", 1);
        
        if(next_level != 0)
            PlayerPrefs.SetInt("Level_" + (level_num + 1) + "_unlocked",1);     //unlock the next level
        
        int s = PlayerPrefs.GetInt("Level_" + level_num + "_highscore", 0);
        if (score > s)
            PlayerPrefs.SetInt("Level_" + level_num + "_highscore", score);
        s = PlayerPrefs.GetInt("Level_" + level_num + "_highscore", 0);
        int st_n = 0;
        if (s >= star1_score)
            st_n = 1;
        if (s >= star2_score)
            st_n = 2;
        if (s >= star3_score)
            st_n = 3;
        if (st_n > PlayerPrefs.GetInt("Level_" + level_num + "_stars_acquired", 0))
            PlayerPrefs.SetInt("Level_" + level_num + "_stars_acquired", st_n);

        GetComponent<PlayAd>().ShowInterstitialAd();
        Debug.Log("Level Completed");
        pause_img_btn.interactable = false;
        EnableFinalCanvas();
    }
    public void GameOver()
    {
        trial = (trial + 1) % 6;
        

        if (PlayerPrefs.GetInt("AutoRetry", 0) == 1 && trial != 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            trial_6 = (trial_6 + 1) % 5;
            if (trial_6 == 4)
                GetComponent<PlayAd>().ShowRewardedAd(level_num+1,null,false);
            else
                GetComponent<PlayAd>().ShowInterstitialAd();
            
            
            Debug.Log("Level Failed trial :" + trial + " trial_6: " + trial_6 );
            Time.timeScale = 0;
            pause_img_btn.interactable = false;
            EnableFinalCanvas();
        }
    }
    //Sets active the final canvas and fills up the stars
    private void EnableFinalCanvas()
    {
        final_canvas.SetActive(true);
        if (score >= star1_score)
            final_canvas_star1.sprite = star_sprite;
        if (score >= star2_score)
            final_canvas_star2.sprite = star_sprite;
        if (score >= star3_score)
            final_canvas_star3.sprite = star_sprite;
        if (PlayerPrefs.GetInt("level_" + level_num + "_completion_status", 0) == 1 ||
                    Advertisement.IsReady())
            final_canvas_skip_level_image_button.interactable = true;
        else
            final_canvas_skip_level_image_button.interactable = false;
    }
   
    public void ResetCooldown()
    {
        cooldown_on = true;
        Invoke("FinishCooldown", cooldown_time);
        cooldown.GetComponent<Cooldown>().Reset();
    }
    private void FinishCooldown()
    {
        cooldown_on = false;
        cooldown.GetComponent<Cooldown>().Finish();
    }
}
