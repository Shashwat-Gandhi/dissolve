using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class PlayAd : MonoBehaviour,IUnityAdsListener
{
  
    private string
             androidGameId = "100",
             iosGameId = "100";

 
    private bool testMode = true;
    public int level_to_unlock = 0;
    public Button button_to_unlock = null;
    public bool to_give_reward = true;
    void Start()
    {
        string gameId = null;

        #if UNITY_ANDROID
            gameId = androidGameId;
#elif UNITY_IOS
             gameId = iosGameId;
#endif

        Advertisement.AddListener(this);
        if (Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId, true);
        }
  
    }
    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady())
        {
            to_give_reward = false;
            Advertisement.Show("video");
        }
        else
        {
            Debug.Log("Interstitial ad is not ready");
        }
    }
    public void ShowRewardedAd(int level_to_unlock_from_reward, Button button_to_unlock_from_reward,bool reward = true)
    {
        to_give_reward = reward;
        if (Advertisement.IsReady())
        {
            level_to_unlock = level_to_unlock_from_reward;
            button_to_unlock = button_to_unlock_from_reward;
            Advertisement.Show("rewardedVideo");
        }
        else {
            Debug.Log("Rewarded Video is not ready right now.");
        }
    }
   
    public void RewardFromAd()
    {
        if(level_to_unlock != 0)
        {
            if (PlayerPrefs.GetInt("Level_" + level_to_unlock + "_unlocked", 0) == 1)
                Debug.Log("Level is already unlocked...hehe;)...");
            else
            {
                PlayerPrefs.SetInt("Level_" + (level_to_unlock-1) + "_completion_status", 1);
                PlayerPrefs.SetInt("Level_" + (level_to_unlock-1) + "_highscore", 0);
                PlayerPrefs.SetInt("Level_" + (level_to_unlock-1) + "_stars_acquired", 0);
                PlayerPrefs.SetInt("Level_" + level_to_unlock + "_unlocked", 1);
                if (button_to_unlock != null)
                    button_to_unlock.interactable = true;
            }
        }
    }
    

    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("finished");
            if (to_give_reward)
                RewardFromAd();
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("skipped,no reward");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
}
