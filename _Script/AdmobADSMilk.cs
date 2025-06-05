using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

using UnityEngine.UI;
using System;

public class AdmobADSMilk : MonoBehaviour {

    public GameObject GM;

    public AudioSource se_back, se_back2;

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

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/8344969668";


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

        //Debug.Log("상태보기 : " + "Loading the rewarded ad.");

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

                //Debug.Log("상태보기 : " + "Rewarded ad loaded with response : " + ad.GetResponseInfo());

                rewardedAd = ad;
            });

       // RegisterEventHandlers(rewardedAd); //이벤트 등록
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

        };
    }


    public void ShowRewardedInterstitialAd()
    {
        //Debug.Log("상태보기 : " + rewardedAd);

        PlayerPrefs.SetInt("wait", 1);
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
                se_back.mute = true;
                se_back2.mute = true;
            rewardedAd.Show((Reward reward) =>
            {
                //Toast_contain3.SetActive(true);
                //Toast_contain2.SetActive(false);
                PlayerPrefs.SetInt("milkadc", 1);
                PlayerPrefs.SetInt("setmilkadc", 0);
                Toast_obj2.SetActive(true);
                GM.GetComponent<WindowMiniGame>().MilkYes();
                PlayerPrefs.SetInt("setmilkadc", 1);
            se_back.mute = false;
            se_back2.mute = false;
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
