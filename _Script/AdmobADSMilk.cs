using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdmobADSMilk : MonoBehaviour {

    public GameObject GM;

    //보상형 전면 광고
    private RewardedInterstitialAd rewardedInterstitialAd;

    AdRequest request;

    //영상
    string adUnitIdvideo;
    private RewardedAd rewardedAd;


    int rewardCoin;
    Color color;
    public GameObject Toast_obj;
    public Text Toast_txt;

    public Button milkad_btn;



    // Use this for initialization 앱 ID
    void Start() {
        color = new Color(1f, 1f, 1f);

#if UNITY_ANDROID
        string appId = "ca-app-pub-3940256099942544~3347511713"; 
#elif UNITY_IPHONE
        string appId = "ca-app-pub-9179569099191885~3667358059"; //ㅌㅔ스트용 ca-app-pub-3940256099942544~1458002511
#else
        string appId = "unexpected_platform";
#endif
        // Initialize the Google Mobile Ads SDK.

        //RequestRewardedVideo();
        loadAd();

    }


    /*
    //동영상
    private void RequestRewardedVideo()
    {

#if UNITY_ANDROID
            adUnitIdvideo = "ca-app-pub-3940256099942544/5224354917"; 
#elif UNITY_IPHONE
        adUnitIdvideo = "ca-app-pub-9179569099191885/8344969668";//테스트용 ca-app-pub-3940256099942544/1712485313
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
            PlayerPrefs.SetInt("milkadc", 1);
            PlayerPrefs.SetInt("setmilkadc", 0);
    }

    //동영상닫음
    private void HandleRewardedAdClosed(object sender, System.EventArgs args)
    {
        RequestRewardedVideo();
        Toast_txt.text = "우유 보상 두배 효과가 적용되었다.";
        StartCoroutine("ToastImgFadeOut");
        milkad_btn.interactable = false;
        PlayerPrefs.SetInt("setmilkadc", 1);
    }

    public void showAdmobVideo()
    {
        PlayerPrefs.SetInt("wait", 1);
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            //GM.GetComponent<UnityADSMilk>().adYes();
            Toast_txt.text = "아직 볼 수 없다. 나중에 시도하자.";
            PlayerPrefs.SetInt("wait", 2);
        }
    }


    public void CallBeforAd()
    {
        //RequestRewardedVideo();
    }



    IEnumerator ToastImgFadeOut()
    {
        if (PlayerPrefs.GetInt("setmilkadc", 0) == 1)
        {
            milkad_btn.interactable = true;
            PlayerPrefs.SetInt("setmilkadc", 0);
        }

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

    */

    //보상형 전면 광고
    private void adLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error)
    {
        if (error == null)
        {
            rewardedInterstitialAd = ad;

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
            Toast_obj.SetActive(true);
            Toast_txt.text = "아직 볼 수 없다. 나중에 시도하자.";
            PlayerPrefs.SetInt("wait", 2);
        }
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        PlayerPrefs.SetInt("milkadc", 1);
        PlayerPrefs.SetInt("setmilkadc", 0);
        Toast_obj.SetActive(true);
        Toast_txt.text = "우유 보상 두배 효과가 적용되었다.";
        //StartCoroutine("ToastImgFadeOut");
        milkad_btn.interactable = false;
        PlayerPrefs.SetInt("setmilkadc", 1);
        loadAd();
    }

    public void touchToastEvt()
    {
        Toast_obj.SetActive(false);
    }


    void loadAd()
    {
        //보상형 전면 광고
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        RewardedInterstitialAd.LoadAd("ca-app-pub-9179569099191885/1419243518", request, adLoadCallback);
    }


}
