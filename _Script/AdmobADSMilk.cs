using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdmobADSMilk : MonoBehaviour {

    public GameObject GM;


    //영상
    private RewardedAd rewardedAd;
    private string _rewardedAdUnitId;


    int rewardCoin;
    Color color;
    public GameObject Toast_obj, Toast_obj2, Toast_contain, Toast_contain2, Toast_contain3;
    public Text Toast_txt;

    public Button milkad_btn;



    // Use this for initialization 앱 ID
    void Start() {
        color = new Color(1f, 1f, 1f);


        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

        _rewardedAdUnitId = "ca-app-pub-3940256099942544/1712485313";


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
                    //Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("상태보기 : " + "Rewarded ad loaded with response : " + ad.GetResponseInfo());

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
            PlayerPrefs.SetInt("adrunout", 0);
            LoadRewardedAd();
            //Debug.Log("광고닫기");
        };
    }





    public void ShowRewardedInterstitialAd()
    {
        //Debug.Log("상태보기 : " + rewardedAd);

        PlayerPrefs.SetInt("wait", 1);
        if (rewardedAd != null)
        {
            rewardedAd.Show((Reward reward) =>
            {
                //Toast_contain3.SetActive(true);
                //Toast_contain2.SetActive(false);
                PlayerPrefs.SetInt("milkadc", 1);
                PlayerPrefs.SetInt("setmilkadc", 0);
                Toast_obj2.SetActive(true);
                GM.GetComponent<WindowMiniGame>().MilkYes();
                PlayerPrefs.SetInt("setmilkadc", 1);
                StartCoroutine("LoadADSmilk");
            });
        }
        else
        {
            Toast_obj.SetActive(true);
            falsetoast();
            Toast_contain.SetActive(true);
            Toast_txt.text = "아직 볼 수 없다. 나중에 시도하자.";
            PlayerPrefs.SetInt("wait", 2);
            LoadRewardedAd();
        }

    }


    IEnumerator LoadADSstart()
    {
        yield return new WaitForSeconds(2f);
        LoadRewardedAd();
    }


    IEnumerator LoadADSmilk()
    {
        yield return new WaitForSeconds(60f);
        LoadRewardedAd();
    }


    public void touchToastEvt()
    {
        Toast_obj.SetActive(false);
        Toast_obj2.SetActive(false);
    }



    public void falsetoast()
    {
        Toast_contain.SetActive(false);
        Toast_contain2.SetActive(false);
        Toast_contain3.SetActive(false);
    }

}
