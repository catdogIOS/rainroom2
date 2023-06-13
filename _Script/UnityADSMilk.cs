using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityADSMilk : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{

    private string gameId = "2883787";//★ Window > Services 설정 테스트 바꿀것 (test용 1486550) //2883787
    public int soundck;
    public GameObject ad_obj, radio_ani, adBtn_obj;


	int sG,mG;
    int sG2, mG2;
   
    Color color;
    public GameObject Toast_obj;

    public string _adUnitId = "rewardedVideo";

    private void Awake()
    {
        Advertisement.Initialize(gameId, false, this); //테스트모드 true
    }

    //스프라이트 이미지로 변경
    public Sprite radioMove1_spr, radioMove2_spr;

    // Use this for initialization
    void Start () {
        color = new Color(1f, 1f, 1f);
        
      }
      
    // Update is called once per frame


    public void ShowRewardedAd()
    {
        PlayerPrefs.SetInt("wait", 1);
        Advertisement.Show("rewardedVideo", this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        PlayerPrefs.SetInt("wait", 2);
        StartCoroutine("ToastImgFadeOut");
    }

    public void adYN()
    {
        ad_obj.SetActive(true);
    }
    public void closeAdYN()
    {
        ad_obj.SetActive(false);
    }
    public void adYes()
    {
        ShowRewardedAd();
        //ad_obj.SetActive(false);
    }


    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("milkadc", 1);
            PlayerPrefs.SetInt("setmilkadc", 0);
        }
    }

    public void Admob()
    {
        radio_ani.SetActive(false);
        adBtn_obj.SetActive(false);
        StopCoroutine("adTimeFlow");
        StopCoroutine("adAniTime");
        StartCoroutine("adTimeFlow");
        StartCoroutine("adAniTime");
        PlayerPrefs.SetInt("talk", 5);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            PlayerPrefs.SetInt("secf", 240);
        }
    }

    

	IEnumerator adTimeFlow(){
		while (mG>-1) {
			sG = PlayerPrefs.GetInt("secf", 240);
            //Debug.Log(sG);
            mG = (int)(sG / 60);
			sG = sG-(sG / 60)*60;
			if (sG < 0) {
				sG = 0;
				mG = 0;
            } else {
                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
			sG = PlayerPrefs.GetInt("secf", 240);
			sG = sG - 1;
			if (sG < 0) {
				sG = -1;
			}
			PlayerPrefs.SetInt("secf",sG);
			yield return new WaitForSeconds(1f);
            //Debug.Log("sg" + sG);
        }
	}
    IEnumerator adAniTime()
    {
        int w = 0;
        while (w == 0)
        {
            if (sG < 0)
            {
                if (PlayerPrefs.GetInt("outtrip", 0) == 1)
                {
                    radio_ani.SetActive(true);
                    adBtn_obj.SetActive(true);
                }
                else
                {
                    if (PlayerPrefs.GetInt("front", 0) == 1)
                    {
                        radio_ani.SetActive(true);
                        adBtn_obj.SetActive(true);
                    }
                    else
                    {
                        radio_ani.SetActive(false);
                        adBtn_obj.SetActive(false);
                    }
                }
            }
            yield return null;
        }

    }

    //이미지 1초마다 바꿔주기
    IEnumerator radioImageChange()
    {
        int rd = 0;
        int rdc = 0;
        while (rd == 0)
        {
            if (rdc == 0)
            {
                radio_ani.GetComponent<Image>().sprite = radioMove1_spr;
                rdc = 1;
            }
            else
            {
                radio_ani.GetComponent<Image>().sprite = radioMove2_spr;
                rdc = 0;
            }
            yield return new WaitForSeconds(1f);
        }
    }


    IEnumerator adTimeFlow2()
    {
        while (mG2 > -1)
        {
            sG2 = PlayerPrefs.GetInt("secf2", 240);
            //Debug.Log(sG);
            mG2= (int)(sG2 / 60);
            sG2 = sG2 - (sG2 / 60) * 60;
            if (sG2 < 0)
            {
                sG2 = 0;
                mG2 = 0;
            }
            else
            {
                radio_ani.SetActive(false);
                adBtn_obj.SetActive(false);
            }
            sG2 = PlayerPrefs.GetInt("secf2", 240);
            sG2 = sG2 - 1;
            if (sG2 < 0)
            {
                sG2 = -1;
            }
            PlayerPrefs.SetInt("secf2", sG2);
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
                if (PlayerPrefs.GetInt("outtrip", 0) == 1)
                {
                } else if (PlayerPrefs.GetInt("front", 0) == 1)
                    {
                        radio_ani.SetActive(true);
                        adBtn_obj.SetActive(true);
                    }
                    else
                    {
                        radio_ani.SetActive(false);
                        adBtn_obj.SetActive(false);
                    }
                    
            }

            yield return null;
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

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }



    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            PlayerPrefs.SetInt("milkadc", 1);
            PlayerPrefs.SetInt("setmilkadc", 0);
            PlayerPrefs.SetInt("adrunout", 0);
            Advertisement.Load(_adUnitId, this);
        }
    }


    public void OnInitializationComplete()
    {
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
    }
}
