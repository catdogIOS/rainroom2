using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api.Mediation.IronSource;

public class AdmobADS : MonoBehaviour {
    public AudioSource se_back, se_back2;
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


    void Awake()
    {       
        GoogleMobileAds.Mediation.IronSource.Api.IronSource.SetConsent(true);
    }

    // Use this for initialization 앱 ID
    void Start () {
        
        color = new Color(1f, 1f, 1f);

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/8344969668";
        _GoOutADSid = "ca-app-pub-9179569099191885/2021864778";
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadRewardedInterstitialAd();
        });

        if (PlayerPrefs.GetInt("lraset", 0) == 0)
        {
            PlayerPrefs.SetInt("lraset", 1);
            Invoke("rewardInvoke", 1f);
        }
    }

    public void rewardInvoke()
    {
        PlayerPrefs.SetInt("lraset", 0);
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


        // create our request used to load the ad.
        var adRequest2 = new AdRequest();

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
        PlayerPrefs.SetInt("adrunout", 0);
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

            if (rewardedAd != null && rewardedAd.CanShowAd())
            {

                se_back.mute = true;
                se_back2.mute = true;
                rewardedAd.Show((Reward reward) =>
                {
                    PlayerPrefs.SetInt("adrunout", 0);
                    if (PlayerPrefs.GetInt("place", 0) == 0)
                    {
                        PlayerPrefs.SetInt("talk", 5);
                        if (PlayerPrefs.GetInt("talk", 5) >= 5)
                        {
                            PlayerPrefs.SetInt("secf", 240);
                        }
                        PlayerPrefs.Save();
                    }
                    else
                    {
                        PlayerPrefs.SetInt("talk", 5);
                        if (PlayerPrefs.GetInt("talk", 5) >= 5)
                        {
                            PlayerPrefs.SetInt("secf2", 240);
                        }
                        PlayerPrefs.Save();
                    }
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
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedInterstitialAd.Load(_GoOutADSid, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                RegisterEventHandlers(ad); //이벤트 등록
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


    //보상형 전면 광고 보여주기
    public void ShowRewardedInterstitialAd()
    {
        PlayerPrefs.SetInt("wait", 1);

        if (rewardedInterstitialAd != null&& rewardedInterstitialAd.CanShowAd())
        {
                se_back.mute = true;
                se_back2.mute = true;
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                PlayerPrefs.SetInt("bouttime", 9);
                Toast_obj2.SetActive(true);

            se_back.mute = false;
            se_back2.mute = false;
                LoadRewardedInterstitialAd();
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
