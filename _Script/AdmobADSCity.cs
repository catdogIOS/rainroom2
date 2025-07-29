﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using GoogleMobileAds.Api.Mediation.IronSource;


public class AdmobADSCity : MonoBehaviour {

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

    public GameObject ad_obj, radio_ani, adBtn_obj;

    int sG, mG;
    int sG2, mG2;

    public GameObject GM;

    void Awake()
    {       
        GoogleMobileAds.Mediation.IronSource.Api.IronSource.SetConsent(true);
    }

    // Use this for initialization 앱 ID
    void Start () {
        color = new Color(1f, 1f, 1f);

        StopCoroutine("adTimeFlow2");
        StopCoroutine("adAniTime2");
        StartCoroutine("adTimeFlow2");
        StartCoroutine("adAniTime2");

        
        _rewardedAdUnitId = "ca-app-pub-9179569099191885/8344969668";
        _GoOutADSid = "ca-app-pub-9179569099191885/5519050563";

        LoadRewardedAd();
        LoadRewardedInterstitialAd();

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
                RegisterEventHandlers(ad); //이벤트 등록
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                rewardedAd = ad;
            });

        //RegisterEventHandlers(rewardedAd); //이벤트 등록
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
                        PlayerPrefs.SetInt("secf3", 240);
                    }
                    ad_obj.SetActive(false);
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

   



    public void OpenAd()
    {

        PlayerPrefs.SetInt("adrunout", 0);
        ad_obj.SetActive(true);
    }


    IEnumerator adTimeFlow2()
    {
        while (mG2 > -1)
        {
            sG2 = PlayerPrefs.GetInt("secf3", 0);
            if (sG2 < 0)
            {
            }
            else
            {
                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
            sG2 = PlayerPrefs.GetInt("secf3", 0);
            sG2 = sG2 - 1;
            if (sG2 < 0)
            {
                sG2 = -1;
            }
            PlayerPrefs.SetInt("secf3", sG2);
            //Debug.Log("sg2" + sG2);
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator adAniTime2()
    {
        int w = 0;
        while (w == 0)
        {
            if (sG2 < 0)
            {
                    radio_ani.SetActive(true);
                    adBtn_obj.SetActive(true);
            }

            yield return null;
        }

    }

    public void close()
    {
        ad_obj.SetActive(false);
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
                RegisterEventHandlers(ad); //이벤트 등록
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("rewarded interstitial ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded interstitial ad loaded with response : " + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
            });
       // RegisterEventHandlers(rewardedInterstitialAd); //이벤트 등록
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
                // TODO: Reward the user.
                PlayerPrefs.SetInt("seatime", 4);
                Toast_obj2.SetActive(true);
                LoadRewardedInterstitialAd();

            se_back.mute = false;
            se_back2.mute = false;
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


    private void RegisterEventHandlers(RewardedInterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {

        };
        ad.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Interstitial ad recorded an impression.");
        };
        ad.OnAdClicked += () =>
        {
            //Debug.Log("Interstitial ad was clicked.");
        };
        ad.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("Interstitial ad full screen content opened.");
        };
        ad.OnAdFullScreenContentClosed += () =>
        {
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Interstitial ad failed to open full screen content " + "with error : " + error);
        };
    }


}
