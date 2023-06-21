using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdmobADS : MonoBehaviour {

    //보상형 전면 광고
    private RewardedInterstitialAd rewardedInterstitialAd;
    private string _GoOutADSid;

    //영상
    private RewardedAd rewardedAd;
    //private RewardedAd rewardedAd_2;
    private string _rewardedAdUnitId;
    //private string _rewardedAdUnitId2;


    int rewardCoin;
    Color color;
    public GameObject Toast_obj, Toast_obj2;
    public Text Toast_txt;

    public Button milkad_btn;

    public GameObject GM;



    // Use this for initialization 앱 ID
    void Start () {
        color = new Color(1f, 1f, 1f);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
        _rewardedAdUnitId = "ca-app-pub-9179569099191885/8344969668";
        _GoOutADSid = "ca-app-pub-9179569099191885/2021864778";

        StartCoroutine("LoadADSstart2");
        StartCoroutine("LoadADSstart");
    }



    public void LoadRewardedAd()
    {

        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }


        // create our request used to load the ad.
        var adRequest2 = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(_rewardedAdUnitId, adRequest2,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("상태로드: " + "Rewarded ad loaded with response : " + ad.GetResponseInfo());

                rewardedAd = ad;
            });

        //RegisterEventHandlers(rewardedAd); //이벤트 등록
    }






    void giveMeReward()
    {
        Toast_obj.SetActive(true);
        PlayerPrefs.SetInt("adrunout", 0);
        Toast_txt.text = "대화 횟수가 5로 다시 복구되었다.";
        StartCoroutine("ToastImgFadeOut");
        StartCoroutine("LoadADStalk");
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

        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            Toast_obj.SetActive(true);
            Toast_txt.text = "대화 횟수가 이미 최대값이라 시청할 수 없다.";
            StartCoroutine("ToastImgFadeOut");
        }
        else
        {
            PlayerPrefs.SetInt("wait", 1);

            if (rewardedAd != null)
            {
                rewardedAd.Show((Reward reward) =>
                {
                    PlayerPrefs.SetInt("adrunout", 0);
                    if (PlayerPrefs.GetInt("place", 0) == 0)
                    {
                        PlayerPrefs.SetInt("talk", 5);
                        PlayerPrefs.Save();
                        if (PlayerPrefs.GetInt("talk", 5) >= 5)
                        {
                            PlayerPrefs.SetInt("secf", 240);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("talk", 5);
                        PlayerPrefs.Save();
                        if (PlayerPrefs.GetInt("talk", 5) >= 5)
                        {
                            PlayerPrefs.SetInt("secf2", 240);
                        }
                    }
                    giveMeReward();
                });
            }
            else
            {
                //StartCoroutine("ToastImgFadeOut");
                GM.GetComponent<UnityADS>().Wating();
                PlayerPrefs.SetInt("wait", 2);
                LoadRewardedAd();
            }
        }
    }


    public void MilkToast()
    {
        if (PlayerPrefs.GetInt("wait", 0) == 2)
        {
            StartCoroutine("ToastImgFadeOut");
        }
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



    public void LoadRewardedInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
        }


        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedInterstitialAd.Load(_GoOutADSid, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                   // Debug.LogError("rewarded interstitial ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("상태보기 : Rewarded interstitial ad loaded with response : " + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
            });
        //RegisterEventHandlers(rewardedInterstitialAd); //이벤트 등록
    }



    //보상형 전면 광고 보여주기
    public void ShowRewardedInterstitialAd()
    {
        PlayerPrefs.SetInt("wait", 1);

        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                PlayerPrefs.SetInt("bouttime", 9);
                Toast_obj2.SetActive(true);

            });
        }
        else
        {
            GM.GetComponent<UnityADS>().Wating();
            PlayerPrefs.SetInt("wait", 2);
            LoadRewardedInterstitialAd();
        }

    }


    public void touchToastEvt()
    {
        Toast_obj2.SetActive(false);
    }





}
