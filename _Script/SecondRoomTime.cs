﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondRoomTime : MonoBehaviour {

    //이동
    public float moveX1, moveX2, moveY;
    public GameObject dust1_obj, dust2_obj,dustY_obj;
    public int randDust1_i, randDust2_i,dustPos_i, dustPos2_i;
    public Sprite[] dust_spr;

    //요리
    public Text beadalTime_txt;


    //식물
    public GameObject plant_obj, plantWin_obj, plantBtn_obj, platMemo_obj;
    int b;
    public Sprite[] plant_spr;
    string str;

    // Use this for initialization
    void Start ()
    {

        PlayerPrefs.SetFloat("iosdusty", dust1_obj.transform.position.y);

        //식물
        if (PlayerPrefs.GetInt("leafget", 0) >= 1)
        {
            PlayerPrefs.SetInt("storg", 1);
            plant_obj.SetActive(true);
            if (PlayerPrefs.GetInt("leafget", 0) == 1)
            {
                platMemo_obj.SetActive(true);
            }


            if (PlayerPrefs.GetInt("putleaf", 1) == 0)
            {
                plant_obj.SetActive(false);
            }
        }

        str = PlayerPrefs.GetString("code", "");
        StartCoroutine("Updadust");
        //업데이트대신쓴다
        StartCoroutine("UpdateSec");
    }

    /// <summary>
    /// 1초당변경하는것들
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateSec()
    {

        //식물
        if (PlayerPrefs.GetInt("leafget", 0) >= 1)
        {
            plant();
        }

        int a = 0;
        while (a == 0)
        {
            beadal();
            if (randDust1_i == 1)
            {

                bool deviceIsIpad = UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
                if (deviceIsIpad)
                {
                    dust1_obj.transform.position = new Vector3(dustPos_i, dustY_obj.transform.position.y, dust1_obj.transform.position.z);

                    dust1_obj.SetActive(true);
                }
                else
                {
                    StopCoroutine("goDust1");
                    StartCoroutine("goDust1");
                }
            }
            else
            {
                randDust1_i = Random.Range(0, 44);
                dustPos_i = Random.Range(-2, 2);
            }
            if (randDust2_i == 1)
            {

                bool deviceIsIpad = UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
                if (deviceIsIpad)
                {
                    dust2_obj.transform.position = new Vector3(dustPos2_i, dustY_obj.transform.position.y, dust2_obj.transform.position.z);

                    dust2_obj.SetActive(true);
                }
                else
                {

                    StopCoroutine("goDust2");
                    StartCoroutine("goDust2");
                }
            }
            else
            {
                randDust2_i = Random.Range(0, 44);
                dustPos2_i = Random.Range(-2, 2);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    //음식시간아랫방
    void beadal()
    {
        System.DateTime lastDateTime = System.DateTime.Parse(PlayerPrefs.GetString("cookLastTime", System.DateTime.UtcNow.ToString()));
        System.TimeSpan compareTime = System.DateTime.UtcNow - lastDateTime;
        int m = (int)compareTime.TotalMinutes;
        int sec = (int)compareTime.TotalSeconds;
        sec = sec - (sec / 60) * 60;
        sec = 59 - sec;
        m = 59 - m;
        string strb = string.Format(@"{0:00}" + ":", m) + string.Format(@"{0:00}", sec);
        beadalTime_txt.text = strb;
        if (m < 0)
        {
            beadalTime_txt.text = "00:00";
            //배달이 온
            PlayerPrefs.SetInt("cooked", 0);
            //PlayerPrefs.Save();
        }
    }
    IEnumerator goDust1()
    {
        while (randDust1_i == 1)
        {
            if (PlayerPrefs.GetInt("front", 1) == 2)
            {
                dust1_obj.SetActive(true);
            }
            else
            {
                dust1_obj.SetActive(false);
            }
                moveX1 = moveX1 + 0.06f;
            if (moveX1 >= 9.4)
            {
                moveX1 = -9.4f;
                randDust1_i = 0;
            }
            moveY = PlayerPrefs.GetFloat("iosdusty", dust1_obj.transform.position.y);
            dust1_obj.transform.position = new Vector3(moveX1, moveY, dust1_obj.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator goDust2()
    {
        while (randDust2_i == 1)
        {
            if (PlayerPrefs.GetInt("front", 1) == 2)
            {
                dust2_obj.SetActive(true);
            }
            else
            {
                dust2_obj.SetActive(false);
            }

            moveX2 = moveX2 - 0.06f;
            if (moveX2 <= -9.4)
            {
                moveX2 = 9.4f;
                randDust2_i = 0;
            }
            moveY = PlayerPrefs.GetFloat("iosdusty", dust1_obj.transform.position.y);
            dust2_obj.transform.position = new Vector3(moveX2, moveY, dust1_obj.transform.position.z);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Updadust()
    {
        int dc = 0;
        int cc = 0;
        while (cc == 0)
        {

            if (dc == 0)
            {
                dust1_obj.GetComponent<Image>().sprite = dust_spr[0];
                dust2_obj.GetComponent<Image>().sprite = dust_spr[5];
                dc++;
            }
            else if(dc==1)
            {
                dust1_obj.GetComponent<Image>().sprite = dust_spr[1];
                dust2_obj.GetComponent<Image>().sprite = dust_spr[4];
                dc++;
            }
            else if (dc == 2)
            {
                dust1_obj.GetComponent<Image>().sprite = dust_spr[2];
                dust2_obj.GetComponent<Image>().sprite = dust_spr[3];
                dc++;
            }
            else if (dc == 3)
            {
                dust1_obj.GetComponent<Image>().sprite = dust_spr[3];
                dust2_obj.GetComponent<Image>().sprite = dust_spr[2];
                dc++;
            }
            else if (dc == 4)
            {
                dust1_obj.GetComponent<Image>().sprite = dust_spr[4];
                dust2_obj.GetComponent<Image>().sprite = dust_spr[1];
                dc++;
            }
            else if (dc == 5)
            {
                dust1_obj.GetComponent<Image>().sprite = dust_spr[5];
                dust2_obj.GetComponent<Image>().sprite = dust_spr[0];
                dc = 0;
            }
            yield return new WaitForSeconds(0.07f);
        }
    }
        //식물시간
        void plant()
    {
        System.DateTime lastDateTime = System.DateTime.Parse(PlayerPrefs.GetString("plantLastTime", System.DateTime.UtcNow.ToString()));
        System.TimeSpan compareTime = System.DateTime.UtcNow - lastDateTime;
        int m = (int)compareTime.TotalMinutes;
        int sec = (int)compareTime.TotalSeconds;
        sec = sec - (sec / 60) * 60;
        sec = 59 - sec;
        m = 4 - m;

        b = 0;
        while (m < 0)
        {
            m = m + 5;
            b = b + 1;
        }
        if (b >= 5)
        {
            b = 4;
        }
        plant_obj.GetComponent<Image>().sprite = plant_spr[b];
        if (b >= 1)
        {
            plantBtn_obj.SetActive(true);
        }
        else
        {
            plantBtn_obj.SetActive(false);
        }
    }


    public void GetPlant()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        int ph = PlayerPrefs.GetInt(str1 + "h", 0);
        ph = ph + b;
        PlayerPrefs.SetInt(str1 + "h", ph);
        PlayerPrefs.SetString("plantLastTime", System.DateTime.UtcNow.ToString());
        plant_obj.GetComponent<Image>().sprite = plant_spr[0];
        plantBtn_obj.SetActive(false);
    }

    public void OpenPlantMemo()
    {
        PlayerPrefs.SetString("plantLastTime", System.DateTime.UtcNow.ToString());
        PlayerPrefs.SetInt("leafget", 2);
        if (plantWin_obj.activeSelf == false)
        {
            plantWin_obj.SetActive(true);
            platMemo_obj.SetActive(false);
        }
        else
        {
            plantWin_obj.SetActive(false);
            platMemo_obj.SetActive(false);
        }
    }

}
