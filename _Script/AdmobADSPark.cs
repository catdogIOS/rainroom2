using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;


public class AdmobADSPark : MonoBehaviour {


    //보상형 전면 광고
    private RewardedInterstitialAd rewardedInterstitialAd;

    AdRequest request;

    //영상
    private RewardedAd rewardedAd;
    string adUnitIdvideo;

    int rewardCoin;
    Color color;
    public GameObject Toast_obj, Toast_obj2;
    public Text Toast_txt;
    public GameObject GM;

    // Use this for initialization 앱 ID
    void Start () {
        color = new Color(1f, 1f, 1f);

#if UNITY_ANDROID
        string appId = "ca-app-pub-9179569099191885~5921342761";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-9179569099191885~3667358059";
#else
        string appId = "unexpected_platform";
#endif
        // Initialize the Google Mobile Ads SDK.

        RequestRewardedVideo();


        //보상형 전면 광고
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        RewardedInterstitialAd.LoadAd("ca-app-pub-9179569099191885/5519050563", request, adLoadCallback);


    }



    
    //동영상
    private void RequestRewardedVideo()
    {

#if UNITY_ANDROID
            adUnitIdvideo = "ca-app-pub-3940256099942544/5224354917"; // 
#elif UNITY_IPHONE
        adUnitIdvideo = "ca-app-pub-9179569099191885/8344969668";
#else
        adUnitIdvideo = "unexpected_platform";
#endif




        this.rewardedAd = new RewardedAd(adUnitIdvideo);


        // Called when the user should be rewarded for watching a video.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        // Create an empty ad request.
        request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    //시청보상
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        PlayerPrefs.SetInt("talk", 5);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            PlayerPrefs.SetInt("secf0", 240);
        }
    }

    //동영상닫음
    private void HandleRewardedAdClosed(object sender, System.EventArgs args)
    {
        RequestRewardedVideo();
        Toast_obj.SetActive(true);
        Toast_txt.text = "대화 횟수가 5로 다시 복구되었다.";
        StartCoroutine("ToastImgFadeOut");
    }

    public void showAdmobVideo()
    {
        if (PlayerPrefs.GetInt("talk", 5) >= 5) //횟수 5면 광고시청 불가
        {
            Toast_obj.SetActive(true);
            Toast_txt.text = "대화 횟수가 이미 최대값이라 시청할 수 없다.";
            StartCoroutine("ToastImgFadeOut");
        }
        else
        {
        PlayerPrefs.SetInt("wait", 1);
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            //StartCoroutine("ToastImgFadeOut");
            GM.GetComponent<UnityADSPark>().Wating();
            PlayerPrefs.SetInt("wait", 2);
        }

        }
    }
    


    
    IEnumerator ToastImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        Toast_obj.GetComponent<Image>().color = color;
        Toast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            Toast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        Toast_obj.SetActive(false);
    }
    


    /*
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        interstitial.Destroy();
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }
    */

    //보상형 전면 광고
    private void adLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error)
    {
        if (error == null)
        {
            rewardedInterstitialAd = ad;
            rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresent;

        }
    }
    public void ShowRewardedInterstitialAd()
    {
        PlayerPrefs.SetInt("wait", 1);
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Show(userEarnedRewardCallback);
        }
        else
        {
            GM.GetComponent<UnityADSPark>().Wating();
            PlayerPrefs.SetInt("wait", 2);
        }
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        // TODO: Reward the user.
        PlayerPrefs.SetInt("foresttime", 4);

        Toast_obj2.SetActive(true);
    }

    public void touchToastEvt()
    {
        Toast_obj2.SetActive(false);
    }

    private void HandleAdFailedToPresent(object sender, AdErrorEventArgs args)
    {
        //MonoBehavior.print("Rewarded interstitial ad has failed to present.");
    }

}
