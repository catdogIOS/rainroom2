﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using GoogleMobileAds.Api.Mediation.IronSource;


public class AdmobADSPark : MonoBehaviour {

    public AudioSource se_back, se_back2;
    //보상형 전면 광고
    private RewardedInterstitialAd rewardedInterstitialAd;
    private string _GoOutADSid;

    //영상
    private RewardedAd rewardedAd;
    private string _rewardedAdUnitId;

    int rewardCoin;
    Color color;
    public GameObject Toast_obj, Toast_obj2;
    public Text Toast_txt;
    public GameObject GM;

    // Use this for initialization 앱 ID
    void Start () {
        color = new Color(1f, 1f, 1f);


        
        _rewardedAdUnitId = "ca-app-pub-9179569099191885/8344969668";
        _GoOutADSid = "ca-app-pub-9179569099191885/5519050563";


        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            LoadRewardedInterstitialAd();
            
            if (PlayerPrefs.GetInt("rewardInvoke_park", 0) == 0)
            {
                PlayerPrefs.SetInt("rewardInvoke_park", 1);
                Invoke("rewardInvoke_park", 1f);
            }
        }
        else
        {
            //인터넷연결안됨
        }
    }

    public void rewardInvoke_park()
    {
        PlayerPrefs.SetInt("rewardInvoke_park", 0);
        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        //Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_rewardedAdUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterEventHandlers(ad); //이벤트 등록
            });
    }


    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log("광고");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            //Debug.Log("광고닫기");
            giveMeReward();
        };
    }



    void giveMeReward()
    {
        se_back.mute = false;
        se_back2.mute = false;
        Toast_obj.SetActive(true);
        Toast_txt.text = "대화 횟수가 5로 다시 복구되었다.";
        StopCoroutine("ToastImgFadeOut");
        StartCoroutine("ToastImgFadeOut");
        LoadRewardedAd();
    }


    IEnumerator LoadADStalk()
    {
        yield return new WaitForSeconds(60f);
        LoadRewardedAd();
    }

    IEnumerator LoadADSstart()
    {
        yield return new WaitForSeconds(0.5f);
        LoadRewardedAd();
    }

    IEnumerator LoadADSstart2()
    {
        yield return new WaitForSeconds(1f);
        LoadRewardedInterstitialAd();
    }




    public void showAdmobVideo()
    {
        //Debug.Log("상태보기 : " + rewardedAd);

        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            Toast_obj.SetActive(true);
            Toast_txt.text = "대화 횟수가 이미 최대값이라 시청할 수 없다.";
            StartCoroutine("ToastImgFadeOut");
        }
        else
        {
            PlayerPrefs.SetInt("wait", 1);

            if (rewardedAd != null&& rewardedAd.CanShowAd())
            {
                se_back.mute = true;
                se_back2.mute = true;
                rewardedAd.Show((Reward reward) =>
                {
                    PlayerPrefs.SetInt("talk", 5);
                    if (PlayerPrefs.GetInt("talk", 5) >= 5)
                    {
                        PlayerPrefs.SetInt("secf0", 240);
                    }
                });
            }
            else
            {
                //StartCoroutine("ToastImgFadeOut");
                GM.GetComponent<UnityADSPark>().Wating();
                PlayerPrefs.SetInt("wait", 2);
                LoadRewardedAd();
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



    public void LoadRewardedInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
        }

        //Debug.Log("Loading the rewarded interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedInterstitialAd.Load(_GoOutADSid, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("rewarded interstitial ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded interstitial ad loaded with response : " + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
            });
        //RegisterEventHandlers(rewardedInterstitialAd); //이벤트 등록
    }



    //보상형 전면 광고 보여주기
    public void ShowRewardedInterstitialAd()
    {
        PlayerPrefs.SetInt("wait", 1);

        //Debug.Log("상태보기 : " + rewardedInterstitialAd);
        if (rewardedInterstitialAd != null&& rewardedInterstitialAd.CanShowAd())
        {
                se_back.mute = true;
                se_back2.mute = true;
            rewardedInterstitialAd.Show((Reward reward) =>
            {
            se_back.mute = false;
            se_back2.mute = false;
                // TODO: Reward the user.
                PlayerPrefs.SetInt("foresttime", 4);
                Toast_obj2.SetActive(true);
                LoadRewardedInterstitialAd();
            });
        }
        else
        {
            GM.GetComponent<UnityADSPark>().Wating();
            PlayerPrefs.SetInt("wait", 2);
            LoadRewardedInterstitialAd();
        }

    }


    public void touchToastEvt()
    {
        Toast_obj2.SetActive(false);
    }

    
}
