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

    //배너
    AdRequest request;

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
        _rewardedAdUnitId = "ca-app-pub-3940256099942544/1712485313";
        _GoOutADSid = "ca-app-pub-3940256099942544/6978759866";

        LoadRewardedInterstitialAd();
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

        //Debug.Log("상태보기 : " + "Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(_rewardedAdUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

                Debug.Log("상태보기 : " + "Rewarded ad loaded with response : " + ad.GetResponseInfo());

                rewardedAd = ad;
            });

        RegisterEventHandlers(rewardedAd); //이벤트 등록
    }




    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            //Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Rewarded ad failed to open full screen content " + "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
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
            LoadRewardedAd();
            //Debug.Log("광고닫기");
        };
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
        Debug.Log("상태보기ㅣㅣㅣㅣㅣ");
    }


    IEnumerator LoadADSstart()
    {
        yield return new WaitForSeconds(2f);
        LoadRewardedAd();
        LoadRewardedInterstitialAd();
        Debug.Log("상태보기ㅣㅣㅣㅣㅣ");
    }


    public void showAdmobVideo()
    {
        Debug.Log("상태보기 : " + rewardedAd);

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

        //Debug.Log("Loading the rewarded interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedInterstitialAd.Load(_GoOutADSid, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("rewarded interstitial ad failed to load an ad " + "with error : " + error);
                    return;
                }

                Debug.Log("상태보기 : Rewarded interstitial ad loaded with response : " + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
            });
        RegisterEventHandlers(rewardedInterstitialAd); //이벤트 등록
    }



    //보상형 전면 광고 보여주기
    public void ShowRewardedInterstitialAd()
    {
        PlayerPrefs.SetInt("wait", 1);

        Debug.Log("상태보기 : " + rewardedInterstitialAd);
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
            LoadRewardedInterstitialAd();

            //Debug.Log("Interstitial ad full screen content closed.");
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Interstitial ad failed to open full screen content " + "with error : " + error);
        };
    }










    private void RegisterReloadHandler(RewardedInterstitialAd ad)
    {
        ad.OnAdFullScreenContentClosed += (null);
        {
            //Debug.Log("Interstitial Ad full screen content closed.");

            LoadRewardedInterstitialAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Interstitial ad failed to open full screen content " + "with error : " + error);

            LoadRewardedInterstitialAd();
        };
    }


}
