﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstRoomFunction : CavasData {
    //앞뒤바꾸기
    public GameObject character_obj;

    //배달
    public GameObject beadalWindow_obj, beadalType1_obj, beadalType2_obj, beadalYesNo_obj, beadalIllust_obj, beadalFood_obj;
    public int buyFood_i, beadalType_i;
    public int point_i;
    public Sprite[] beadalYN_spr,beadalFood_spr;
    public Sprite beadalT1_spr, beadalT2_spr;
    public GameObject dish_obj, beadalYet_obj;
    public Text[] heart_txt;

    public GameObject beadalTime_obj;

    //쿠폰
    public GameObject[] couponType1_obj, couponType2_obj, couponComplete_obj;
    //public GameObject useCoupon_obj;
    public GameObject coupon_obj;
    public GameObject couponFood_obj;
    public Sprite[] couponFood_spr;
    
    //모두모으면 버튼이나타나게 한다

	public GameObject GMNotdistroy, firstGM;


    public GameObject needToast_obj, beadalYetToast_obj;
    Color colorN,colorB,colorL,colorA;

    //아티팩트
    public GameObject atoast_obj;

    //하트
    public int heart_i;

    public Text boxHeart_txt;
    public Text boxTotal_txt;

    public GameObject[] fisrtRoomItem_obj;


    public int window_i, book_i, bed_i, desk_i, stand_i, tapestry_i, rug_i, poster_i, cabinet_i, wall_i;
    public GameObject windowImg_obj, bookImg_obj, deskImg_obj, standImg_obj, tapestryImg_obj, bedImg_obj, rugImg_obj, cabinetImg_obj, rugImg2_obj, wallImg_obj, wallImg2_obj;
    public GameObject ladderImg_obj;
    public Sprite ladder_spr;
    public GameObject moreCoinWindow_obj;
    public GameObject bookcase_obj;
    public Sprite bookcase_spr;
    public GameObject bedMax_obj;
    public Sprite[] bedMax_spr;

    public int bookBox_i;
    public GameObject bookBox_obj, bedBox_obj, deskBox_obj, cabinetBox_obj, ladderBox_obj;
    public GameObject needMore_obj;
    public GameObject boxClean_obj;
    //public Sprite[] boxItem_spr;
    public GameObject boxLv_obj;
    
    public string boxName_str;
    public int boxs_i;
    public Text boxTxt_txt, boxneed_txt;

    public GameObject roomRabbit_obj, roomMarimo_obj;

    public GameObject loadGM;

    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;

    //업적액자
    public GameObject frame_obj;
    public Sprite frameOpen_spr, frameEnd_spr;


    //도움말열기
    public GameObject Help_obj;
    public Sprite[] help_spr;

    public GameObject GM, GMTag;

    //밤
    public GameObject dayRoom;

    //소리
    public GameObject audio_obj;

    //리폼
    public Sprite[] reformWall_spr, reformWall2_spr, reformBookcase_spr, reformDesk_spr, reformBed_spr,reformRug_spr, reformCabinet_spr;

    //책출판기념
    public GameObject bookEvent_obj, bookEventUp_obj, bookEventWindow_obj,bookTxt_obj;
    public Sprite[] bookTxt_spr;
    int booke_i=0;

    public GameObject wScarf_obj, sHat_obj, map_obj, tre0_obj, tre1_obj;
    public GameObject window_season_obj, mapWin_obj;

    public GameObject notice_obj, backagain_obj, backWhere_obj;

    //미리 씬을 불러오기
    AsyncOperation async;
    public Sprite menuOut_spr, city_spr, park_spr;

    //타이틀닫기
    public GameObject titleImg;
    public void closeTitle()
    {
        titleImg.SetActive(false);
    }



    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("parkgock",0);

        //다시 나가시겠습니까?
        if (PlayerPrefs.GetInt("outorhome", 0) >= 1)
        {
            if (PlayerPrefs.GetInt("outorhome", 0) == 2)
            {
                //도시
                backWhere_obj.GetComponent<SpriteRenderer>().sprite = city_spr;
            }
            else
            {
                //공원
                backWhere_obj.GetComponent<SpriteRenderer>().sprite = park_spr;
            }
            backagain_obj.SetActive(true);
        }

        //겨울목도리
        if (PlayerPrefs.GetInt("putwinterc", 0) == 1)
        {
            wScarf_obj.SetActive(true);
        }
        //여름모자
        if (PlayerPrefs.GetInt("putsummerhat", 0) == 1)
        {
            sHat_obj.SetActive(true);
        }

        //러그
        if (PlayerPrefs.GetInt("putrug", 1) == 1)
        {
            rugImg_obj.SetActive(true);
            rugImg2_obj.SetActive(true);
        }
        else
        {
            rugImg_obj.SetActive(false);
            rugImg2_obj.SetActive(false);
        }

        //보물지도
        if (PlayerPrefs.GetInt("putmap", 0) == 1)
        {
            map_obj.SetActive(true);
        }
        else
        {
            map_obj.SetActive(false);
        }



        /*
        //가을창문
        if (PlayerPrefs.GetInt("windowfall", 0) == 1)
        {
            window_season_obj.SetActive(true);
        }
        */

        PlayerPrefs.SetInt("outtrip", 0);
        // PlayerPrefs.SetInt("downst", 0);
        //PlayerPrefs.SetInt("countladderst", 0);
        //만약 잠을 자고 있다면 들어왔을때 첫화면이 침대쪽 화면으로
        if (PlayerPrefs.GetInt("nowsleep", 0) == 1)
        {
            changeSight();
            characterTurn();
        }
        colorA = new Color(1f, 1f, 1f);
        colorN = new Color(1f, 1f, 1f);
        colorB = new Color(1f, 1f, 1f);
        colorL = new Color(1f, 1f, 1f);
        PlayerPrefs.SetInt("place", 0);
        //씬이동
        if (menuBlock_obj == null)
        {
            menuBlock_obj = GameObject.FindGameObjectWithTag("scene");
        }
        menuBlock_vet.y = menuBlock_obj.transform.position.y;
        menuBlock_vet.x = -4000f;
        menuBlock_obj.transform.position = menuBlock_vet;

        
        //장소초기화
        PlayerPrefs.SetInt("place", 0);

        //로딩화면에서 불러온 정보를 찾아오기 위해서 태그로 지엠을 찾아준다
        GMNotdistroy = GameObject.FindGameObjectWithTag ("GMtag");
        loadGM = GameObject.Find("loadGM");

        GMNotdistroy.GetComponent<MainShop>().RabbitColo();
        GMNotdistroy.GetComponent<MainShop>().TutleColo();
        GMNotdistroy.GetComponent<MainShop>().MarimoColo();
        GMNotdistroy.GetComponent<MainShop>().FishColo();

        //방에 처음 들어왔을때 각각 단계에 따라 이미지 바꿔주기

        //window_i = PlayerPrefs.GetInt ("windowlv", 0);
        book_i = PlayerPrefs.GetInt ("booklv",0);
		bed_i = PlayerPrefs.GetInt ("bedlv",0);
		rug_i = PlayerPrefs.GetInt ("ruglv",0);
        wall_i = PlayerPrefs.GetInt("walllv", 0);
		poster_i = PlayerPrefs.GetInt ("posterlv",0);
		desk_i = PlayerPrefs.GetInt ("desklv",0);
		tapestry_i = PlayerPrefs.GetInt ("tapestrylv",0);
		stand_i = PlayerPrefs.GetInt ("standlv",0);
        cabinet_i = PlayerPrefs.GetInt("cabinetlv", 0);
        //액자
        if (PlayerPrefs.GetInt("frameopen", 0) == 1)
        {
            frame_obj.GetComponent<Image>().sprite = frameOpen_spr;

            int v = PlayerPrefs.GetInt("allacvdone", 0);
            if (v >= 30)
            {
                frame_obj.GetComponent<Image>().sprite = frameEnd_spr;
            }
            frame_obj.GetComponent<Button>().interactable = true;
        }

        //여기에 박스인것들은 대화버튼들 비활성화시켜놓기
        //박스
        if (PlayerPrefs.GetInt("bedbox", 0)==10)
        {
            bedBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("cabinetbox", 0) == 10)
        {
            cabinetBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("deskbox", 0) == 10)
        {
            deskBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("bookbox", 0) == 10)
        {
            bookBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("ladderbox", 0) == 0)
        {
            ladderBox_obj.SetActive(true);
        }
        setItems();

        //낮밤
        setDay();

        //보물찾기
        if (PlayerPrefs.GetInt("gettre0", 0) == 1)
        {
            tre0_obj.SetActive(false);
        }
        if (PlayerPrefs.GetInt("gettre1", 0) == 1)
        {
            tre1_obj.SetActive(false);
        }

    }

    public void OutAgainY()
    {

        if (PlayerPrefs.GetInt("outorhome", 0) == 2)
        {
            PlayerPrefs.SetInt("outtrip", 2);
        }
        else
        {
            PlayerPrefs.SetInt("outtrip", 1);
        }
        PlayerPrefs.SetInt("front", 1);
        PlayerPrefs.SetInt("place", 1);
        PlayerPrefs.SetString("outLastTime", System.DateTime.UtcNow.ToString());
        PlayerPrefs.SetInt("bouttime", 14);
        StartCoroutine("LoadOut");

        if (GMTag == null)
        {
            GMTag = GameObject.FindGameObjectWithTag("GMtag");
        }
        GMTag.GetComponent<MainBtnEvt>().menuBack_obj.GetComponent<Image>().sprite = menuOut_spr;

        //backagain_obj.SetActive(false);
    }
    public void OutAgainN()
    {
        //외출중
        PlayerPrefs.SetInt("outorhome", 0);
        backagain_obj.SetActive(false);
        PlayerPrefs.SetInt("outtrip", 0);
    }
    IEnumerator LoadOut()
    {
        async = SceneManager.LoadSceneAsync("SubLoadOut");
        while (!async.isDone)
        {
            yield return true;
        }
    }

    public void setItems()
    {
        //windowImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData> ().window_spr [window_i];
        if (PlayerPrefs.GetInt("booklv", 0) >= 15)
        {
            bookcase_obj.GetComponent<Image>().sprite = bookcase_spr;
            bookcase_obj.SetActive(true);
            bookImg_obj.SetActive(false);
        }
        else
        {
            bookImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().book_spr[book_i];
        }
        bedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().bed_spr[bed_i];
        deskImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().desk_spr[desk_i];

        //쿠폰이있나
        if (PlayerPrefs.GetInt("10016", 0) == 50)
        {
            //꺼내져 있나
            if (PlayerPrefs.GetInt("puteventbook", 1)==1)
            {
                if (desk_i <= 3)
                {
                    bookEvent_obj.SetActive(true);
                }
                else
                {
                    bookEventUp_obj.SetActive(true);
                }
            }
        }
        //standImg_obj.GetComponent<Image> ().sprite = loadGM.GetComponent<LoadingData> ().stand_spr [stand_i];
        rugImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[rug_i];
        rugImg2_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[rug_i];
        wallImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().wall_spr[wall_i];
        wallImg2_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().wall2_spr[wall_i];
        //tapestryImg_obj.GetComponent<Image> ().sprite = loadGM.GetComponent<LoadingData> ().tapestry_spr [tapestry_i];
        cabinetImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().cabinet_spr[cabinet_i];




        //벽지
        if (PlayerPrefs.GetInt("shoppalette9", 0) > 0)
        {
            wallImg_obj.GetComponent<Image>().sprite = reformWall_spr[PlayerPrefs.GetInt("setwallpalette", 0)];
            wallImg2_obj.GetComponent<Image>().sprite = reformWall2_spr[PlayerPrefs.GetInt("setwallpalette", 0)];
        }
        //러그
        if (PlayerPrefs.GetInt("shoppalette_rug", 0) > 0)
        {
            rugImg_obj.GetComponent<Image>().sprite = reformRug_spr[0];
            rugImg2_obj.GetComponent<Image>().sprite = reformRug_spr[0];
            switch (PlayerPrefs.GetInt("setrugpalette", 0))
            {
                case 1:
                    rugImg_obj.GetComponent<Image>().color = new Color(0.76f, 0.9f, 0.99f);
                    rugImg2_obj.GetComponent<Image>().color = new Color(0.76f, 0.9f, 0.99f);
                    break;
                case 2:
                    rugImg_obj.GetComponent<Image>().color = new Color(0.95f, 0.80f, 0.80f);
                    rugImg2_obj.GetComponent<Image>().color = new Color(0.95f, 0.80f, 0.80f);
                    break;
                case 3:
                    rugImg_obj.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f);
                    rugImg2_obj.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f);
                    break;
                case 4:
                    rugImg_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    rugImg2_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    rugImg_obj.GetComponent<Image>().sprite = reformRug_spr[4];
                    rugImg2_obj.GetComponent<Image>().sprite = reformRug_spr[4];
                    break;
                case 5:
                    rugImg_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    rugImg2_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    rugImg_obj.GetComponent<Image>().sprite = reformRug_spr[5];
                    rugImg2_obj.GetComponent<Image>().sprite = reformRug_spr[5];
                    break;
            }
        }

        //서랍장
        if (PlayerPrefs.GetInt("shoppalette_cab", 0) > 0)
        {
            cabinetImg_obj.GetComponent<Image>().sprite = reformCabinet_spr[0];
            switch (PlayerPrefs.GetInt("setcabinetpalette", 0))
            {
                case 1:
                    cabinetImg_obj.GetComponent<Image>().color = new Color(0.6f, 0.8f, 0.99f);
                    break;
                case 2:
                    cabinetImg_obj.GetComponent<Image>().color = new Color(0.99f, 0.80f, 0.80f);
                    break;
                case 3:
                    cabinetImg_obj.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
                    break;
                case 4:
                    cabinetImg_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    cabinetImg_obj.GetComponent<Image>().sprite = reformCabinet_spr[4];
                    break;
                case 5:
                    cabinetImg_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    cabinetImg_obj.GetComponent<Image>().sprite = reformCabinet_spr[5];
                    break;
            }
        }

        //침대
        if (PlayerPrefs.GetInt("bedmaxlv", 0) >= 1)
        {
            bedImg_obj.SetActive(false);
            bedMax_obj.SetActive(true);
            bedMax_obj.GetComponent<Image>().sprite = bedMax_spr[PlayerPrefs.GetInt("bedmaxlv", 0)];
        }

        //침대
        if (PlayerPrefs.GetInt("shoppalette7", 0) > 0)
        {
            bedMax_obj.GetComponent<Image>().sprite = reformBed_spr[PlayerPrefs.GetInt("setbedpalette", 0)];
        }

        if (PlayerPrefs.GetInt("shoppalette6", 0) > 0)
        {
            bookcase_obj.GetComponent<Image>().sprite = reformBookcase_spr[PlayerPrefs.GetInt("setbookpalette", 0)];
        }

        //책상
        if (PlayerPrefs.GetInt("shoppalette8", 0) > 0)
        {
            deskImg_obj.GetComponent<Image>().sprite = reformDesk_spr[PlayerPrefs.GetInt("setdeskpalette", 0)];
        }
        
        if (ladderBox_obj.activeSelf == false)
        {
            ladderImg_obj.GetComponent<Image>().sprite = ladder_spr;
        }
        //애완동물
        if (PlayerPrefs.GetInt("setmarimo", 0) == 1)
        {
            roomMarimo_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("setrabbit", 0) == 1)
        {
            roomRabbit_obj.SetActive(true);
        }
    }

    //전단지열기
    public void openBeadal(){
        ShowCoupon();
        if (PlayerPrefs.GetInt("beadal", 0)==0)
        {
            string str1;
            str1 = PlayerPrefs.GetString("code", "");
            heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
            heart_txt[0].text = "" + heart_i;
            heart_txt[1].text = "" + heart_i;
            if (beadalWindow_obj.activeSelf == true)
            {
                beadalWindow_obj.SetActive(false);
            }
            else
            {
                beadalWindow_obj.SetActive(true);
            }
        }
        else
        {
            StopCoroutine("toastBImgFadeOut");
            beadalYet_obj.SetActive(true);
            StartCoroutine("toastBImgFadeOut");
            //아직배부름
        }
	}


    //배달시키기
#region
    public void BuyFood1() {
        buyFood_i = 1;
    }
    public void BuyFood2()
    {
        buyFood_i = 2;
    }
    public void BuyFood3()
    {
        buyFood_i = 3;
    }
    public void BuyFood4()
    {
        buyFood_i = 4;
    }
    public void BuyFood5()
    {
        buyFood_i = 5;
    }
    public void BuyFood6()
    {
        buyFood_i = 6;
    }
    public void BuyFood7()
    {
        buyFood_i = 7;
    }
    public void BuyFood8()
    {
        buyFood_i = 8;
    }
#endregion
    //어떤음식인지 받아온 숫자로 판단해서 그음식에 맞게 처리를 해준다
    public void BuyFoodYes()
    {
        beadalYesNo_obj.SetActive(false);
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        switch (buyFood_i)
        {
            case 1:
                if (heart_i >= 4)
                {
                    heart_i = heart_i - 4;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 3;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[0];
                    int cop = PlayerPrefs.GetInt("coupon1", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon1", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 2:
                if (heart_i >= 6)
                {
                    heart_i = heart_i - 6;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 7;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[0];
                    int cop = PlayerPrefs.GetInt("coupon1", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon1", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 3:
                if (heart_i >= 7)
                {
                    heart_i = heart_i - 7;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 9;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[0];
                    int cop = PlayerPrefs.GetInt("coupon1", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon1", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 4:
                if (heart_i >= 8)
                {
                    heart_i = heart_i - 8;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 11;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[0];
                    int cop = PlayerPrefs.GetInt("coupon1", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon1", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 5:
                if (heart_i >= 6)
                {
                    heart_i = heart_i - 6;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 7;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[1];
                    int cop = PlayerPrefs.GetInt("coupon2", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon2", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 6:
                if (heart_i >= 8)
                {
                    heart_i = heart_i - 8;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 11;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[1];
                    int cop = PlayerPrefs.GetInt("coupon2", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon2", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 7:
                if (heart_i >= 7)
                {
                    heart_i = heart_i - 7;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 9;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[1];
                    int cop = PlayerPrefs.GetInt("coupon2", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon2", cop);
                }
                else
                {
                    needMoney();
                }
                break;
            case 8:
                if (heart_i >= 10)
                {
                    heart_i = heart_i - 10;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    point_i = PlayerPrefs.GetInt("lovepoint", 0);
                    point_i = point_i + 13;
                    BeadalYesF();
                    beadalYesNo_obj.GetComponent<Image>().sprite = beadalYN_spr[1];
                    int cop = PlayerPrefs.GetInt("coupon2", 0);
                    cop++;
                    PlayerPrefs.SetInt("coupon2", cop);
                }
                else
                {
                    needMoney();
                }
                break;
        }
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        heart_txt[0].text = "" + heart_i;
        heart_txt[1].text = "" + heart_i;
        PlayerPrefs.Save();
    }

    void BeadalYesF()
    {
        PlayerPrefs.SetInt("beadal", 1);
        PlayerPrefs.SetInt("lovepoint", point_i);
        closeBeadal();
        beadalIllust_obj.SetActive(true);
        PlayerPrefs.SetString("foodLastTime", System.DateTime.UtcNow.ToString());
        audio_obj.GetComponent<SoundEvt>().foodSound();
    }

    public void CleanDish()
    {
        float xx = dish_obj.transform.position.x;
        float yy = dish_obj.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);

        PlayerPrefs.SetInt("dishw", 1);
        GM.GetComponent<GetFadeout>().getRainFade();
        dish_obj.SetActive(false);
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        coldRain_i = coldRain_i + 20;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.Save();
    }

    public void CloseBeadalIllust()
    {
        beadalIllust_obj.SetActive(false);
        dish_obj.SetActive(true);
        closeBeadal();
    }

    public void buyFoodNo()
    {
        beadalYesNo_obj.SetActive(false);
    }

    public void beadalType1()
    {
        beadalType1_obj.SetActive(true);
        beadalType_i = 0;
    }
    public void beadalType2()
    {
        beadalType2_obj.SetActive(true);
        beadalType_i = 1;
    }

    public void BeadalTypeClose()
    {
        beadalType1_obj.SetActive(false);
        beadalType2_obj.SetActive(false);
        beadalYesNo_obj.SetActive(false);
    }

    public void closeBeadal(){
		beadalWindow_obj.SetActive (false);
        beadalType1_obj.SetActive(false);
        beadalType2_obj.SetActive(false);
        beadalYesNo_obj.SetActive(false);
    }

    public void OpenBeadalYN()
    {
        beadalFood_obj.GetComponent<Image>().sprite = beadalFood_spr[buyFood_i];
        beadalYesNo_obj.SetActive(true);
        if (beadalType_i == 0)
        {
            beadalYesNo_obj.GetComponent<Image>().sprite = beadalT1_spr;
        }
        else
        {

            beadalYesNo_obj.GetComponent<Image>().sprite = beadalT2_spr;
        }
    }

    void needMoney()
    {
        StopCoroutine("toastNImgFadeOut");
        StartCoroutine("toastNImgFadeOut");
        audio_obj.GetComponent<SoundEvt>().cancleSound();
    }

	public void boxOpen(){
        boxTxt_txt.text = ""+ boxs_i;

        boxClean_obj.SetActive (true);
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        boxTotal_txt.text = ""+heart_i;
	}

    public void boxBed()
    {
        boxName_str = "bed";
        boxneed_txt.text = "";
        boxs_i = 10;
    }
    public void boxCabinet()
    {
        boxName_str = "cabinet";
        boxneed_txt.text = "";
        boxs_i = 1;
    }
    public void boxBook()
    {
        boxName_str = "book";
        boxneed_txt.text = "";
        boxs_i = 1;
    }
    public void boxDesk()
    {
        boxName_str = "desk";
        boxneed_txt.text = "";
        boxs_i = 1;
    }
    public void boxLadder()
    {
        boxName_str = "ladder";
        boxneed_txt.text = "호감Lv.3 달성하기";
        boxs_i = 10;
    }

    public void boxYes(){

		string str1;
		str1 = PlayerPrefs.GetString ("code", "");
        heart_i = PlayerPrefs.GetInt (str1+"ht", 0);
        if (boxName_str == "ladder")
        {
            if (PlayerPrefs.GetInt("lovelv", 0) >= 3)
            {
                if (heart_i >= boxs_i)
                {
                    heart_i = heart_i - boxs_i;
                    boxHeart_txt.text = "" + boxs_i;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    PlayerPrefs.SetInt(boxName_str + "box", 1);
                    PlayerPrefs.SetInt(boxName_str + "lv", 1);
                    checkach();
                    PlayerPrefs.Save();
                    //소리
                    audio_obj.GetComponent<SoundEvt>().boxSound();
                    if (PlayerPrefs.GetInt("bedbox", 0) == 1)
                    {
                        bedBox_obj.SetActive(false);
                    }
                    if (PlayerPrefs.GetInt("cabinetbox", 0) == 1)
                    {
                        cabinetBox_obj.SetActive(false);
                    }
                    if (PlayerPrefs.GetInt("deskbox", 0) == 1)
                    {
                        deskBox_obj.SetActive(false);
                    }
                    if (PlayerPrefs.GetInt("bookbox", 0) == 1)
                    {
                        bookBox_obj.SetActive(false);
                    }
                    if (PlayerPrefs.GetInt("ladderbox", 0) == 1)
                    {
                        ladderBox_obj.SetActive(false);
                        ladderImg_obj.GetComponent<Image>().sprite = ladder_spr;
                    }
                    book_i = PlayerPrefs.GetInt("booklv", 0);
                    if (book_i >= 15)
                    {

                    }
                    else
                    {
                        bookImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().book_spr[book_i];
                    }
                    bed_i = PlayerPrefs.GetInt("bedlv", 0);
                    desk_i = PlayerPrefs.GetInt("desklv", 0);
                    cabinet_i = PlayerPrefs.GetInt("cabinetlv", 0);
                    rug_i = PlayerPrefs.GetInt("ruglv", 0);

                    bedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().bed_spr[bed_i];
                    deskImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().desk_spr[desk_i];
                    cabinetImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().cabinet_spr[cabinet_i];

                    rugImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[rug_i];
                    boxClean_obj.SetActive(false);

                }
                else
                {
                    needMoney();
                    boxClean_obj.SetActive(false);
                    audio_obj.GetComponent<SoundEvt>().cancleSound();
                }
            }
            else
            {
                StopCoroutine("toastLadderFadeOut");
                StartCoroutine("toastLadderFadeOut");
                boxLv_obj.SetActive(true);
            }
        }
        else
        {

            if (heart_i >= boxs_i)
            {
                heart_i = heart_i - boxs_i;
                boxHeart_txt.text = "" + boxs_i;
                PlayerPrefs.SetInt(str1 + "ht", heart_i);
                PlayerPrefs.SetInt(boxName_str + "box", 1);
                PlayerPrefs.SetInt(boxName_str + "lv", 1);
                checkach();
                PlayerPrefs.Save();
                //소리
                audio_obj.GetComponent<SoundEvt>().boxSound();
                if (PlayerPrefs.GetInt("bedbox", 0) == 1)
                {
                    bedBox_obj.SetActive(false);
                }
                if (PlayerPrefs.GetInt("cabinetbox", 0) == 1)
                {
                    cabinetBox_obj.SetActive(false);
                }
                if (PlayerPrefs.GetInt("deskbox", 0) == 1)
                {
                    deskBox_obj.SetActive(false);
                }
                if (PlayerPrefs.GetInt("bookbox", 0) == 1)
                {
                    bookBox_obj.SetActive(false);

                }
                if (PlayerPrefs.GetInt("ladderbox", 0) == 1)
                {
                    ladderBox_obj.SetActive(false);
                    ladderImg_obj.GetComponent<Image>().sprite = ladder_spr;
                }
                book_i = PlayerPrefs.GetInt("booklv", 0);
                bed_i = PlayerPrefs.GetInt("bedlv", 0);
                desk_i = PlayerPrefs.GetInt("desklv", 0);
                cabinet_i = PlayerPrefs.GetInt("cabinetlv", 0);
                rug_i = PlayerPrefs.GetInt("ruglv", 0);
                if (book_i<15)
                {
                    bookImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().book_spr[book_i];
                }
                bedImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().bed_spr[bed_i];
                deskImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().desk_spr[desk_i];
                cabinetImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().cabinet_spr[cabinet_i];

                rugImg_obj.GetComponent<Image>().sprite = loadGM.GetComponent<LoadingData>().rug_spr[rug_i];
                boxClean_obj.SetActive(false);

            }
            else
            {
                needMoney();
                audio_obj.GetComponent<SoundEvt>().cancleSound();
                boxClean_obj.SetActive(false);
            }

        }
    }

    public void checkbox()
    {
        int cb = 0;
        if (PlayerPrefs.GetInt("bedbox", 0) == 1)
        {
            cb++;
        }
        if (PlayerPrefs.GetInt("cabinetbox", 0) == 1)
        {
            cb++;
        }
        if (PlayerPrefs.GetInt("deskbox", 0) == 1)
        {
            cb++;
        }
        if (PlayerPrefs.GetInt("bookbox", 0) == 1)
        {
            cb++;
        }
        if (PlayerPrefs.GetInt("ladderbox", 0) == 1)
        {
            cb++;
        }
        if (PlayerPrefs.GetInt("seedbox", 0) == 1)
        {
            cb++;
        }
        if (PlayerPrefs.GetInt("drawerbox", 0) == 1)
        {
            cb++;
        }
        if (PlayerPrefs.GetInt("gasrangebox", 0) == 1)
        {
            cb++;
        }
        if (PlayerPrefs.GetInt("iceboxbox", 0) == 1)
        {
            cb++;
        }
        if (PlayerPrefs.GetInt("doorbox", 0) == 1)
        {
            cb++;
        }
        PlayerPrefs.SetInt("countboxst", cb);
        PlayerPrefs.Save();
    }

    public void boxNo(){
		boxClean_obj.SetActive (false);
        needMore_obj.SetActive(false);
    }

    public void closeNeed()
    {
        needMore_obj.SetActive(false);
        beadalYet_obj.SetActive(false);
    }

    //책상위 책
    public void ActBookEvent()
    {
        if (bookEventWindow_obj.activeSelf == true)
        {
            bookEventWindow_obj.SetActive(false);
        }
        else
        {
            bookEventWindow_obj.SetActive(true);
            bookTxt_obj.GetComponent<Image>().sprite = bookTxt_spr[booke_i];
            booke_i++;
            if (booke_i >= 5)
            {
                booke_i = 0;
            }
        }
    }

    

    
    IEnumerator toastBImgFadeOut()
    {
        colorB.a = Mathf.Lerp(0f, 1f, 1f);
        beadalYetToast_obj.GetComponent<Image>().color = colorB;
        beadalTime_obj.GetComponent<Image>().color = colorB;
        beadalYetToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorB.a = Mathf.Lerp(0f, 1f, i);
            beadalYetToast_obj.GetComponent<Image>().color = colorB;
            beadalTime_obj.GetComponent<Image>().color = colorB;
            yield return null;
        }
        beadalYetToast_obj.SetActive(false);
    }

    //토스트페이드아웃
    IEnumerator toastNImgFadeOut()
    {
        colorN.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = colorN;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorN.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = colorN;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }
    //사다리페이드아웃
    IEnumerator toastLadderFadeOut()
    {
        colorL.a = Mathf.Lerp(0f, 1f, 1f);
        boxLv_obj.GetComponent<Image>().color = colorL;
        boxLv_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorL.a = Mathf.Lerp(0f, 1f, i);
            boxLv_obj.GetComponent<Image>().color = colorL;
            yield return null;
        }
        boxLv_obj.SetActive(false);
    }

    //아티팩트페이드아웃
    IEnumerator toastAFadeOut()
    {
        colorA.a = Mathf.Lerp(0f, 1f, 1f);
        atoast_obj.GetComponent<Image>().color = colorA;
        atoast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorA.a = Mathf.Lerp(0f, 1f, i);
            atoast_obj.GetComponent<Image>().color = colorA;
            yield return null;
        }
        atoast_obj.SetActive(false);
    }

    public void AtoastShow()
    {
        StopCoroutine("toastAFadeOut");
        StartCoroutine("toastAFadeOut");

    }

    public void OpenCoupon()
    {
        if (coupon_obj.activeSelf == true)
        {
            coupon_obj.SetActive(false);
        }
        else
        {
            if (beadalType_i == 0)
            {
                couponFood_obj.GetComponent<Image>().sprite = couponFood_spr[0];
            }
            else
            {
                couponFood_obj.GetComponent<Image>().sprite = couponFood_spr[1];

            }
            coupon_obj.SetActive(true);
        }
    }
    public void OpenHelpBeadal()
    {
        Help_obj.SetActive(true);
        Help_obj.GetComponent<Image>().sprite = help_spr[0];
    }
    public void CloseHelp()
    {
        Help_obj.SetActive(false);
    }

    public void useCouponY()
    {
        audio_obj.GetComponent<SoundEvt>().foodSound();
        point_i = PlayerPrefs.GetInt("lovepoint", 0);
        point_i = point_i + 3;
        PayCoupon();
        BeadalYesF();
        coupon_obj.SetActive(false);
    }
    public void useCouponN()
    {
        coupon_obj.SetActive(false);
    }

    void ShowCoupon()
    {
        int coup = PlayerPrefs.GetInt("coupon1", 0);
        for(int i = 0; i < coup; i++)
        {
            if (coup > 10)
            {                
            }
            else
            {
                couponType1_obj[i].SetActive(true);
            }
            
        }
        if (coup >= 10)
        {
            couponComplete_obj[0].SetActive(true);
        }
        coup = PlayerPrefs.GetInt("coupon2", 0);
        for (int i = 0; i < coup; i++)
        {
            if (coup > 10)
            {
            }
            else
            {
                couponType2_obj[i].SetActive(true);
            }
            
        }
        if (coup >= 10)
        {
            couponComplete_obj[1].SetActive(true);
        }
    }
    void PayCoupon()
    {
        if (beadalType_i == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                couponType1_obj[i].SetActive(false);
            }
            PlayerPrefs.SetInt("coupon1", 0);
            couponComplete_obj[0].SetActive(false);
        }
        
        if (beadalType_i == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                couponType2_obj[i].SetActive(false);
            }
            PlayerPrefs.SetInt("coupon2", 0);
            couponComplete_obj[1].SetActive(false);
        }
        
    }
    
    public void characterTurn()
    {
        if (character_obj.transform.rotation.y == 0)
        {
            character_obj.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            character_obj.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    
    //업적
    void checkach()
    {

        checkbox();

        int cts = PlayerPrefs.GetInt("countboxst", 0);
        if (cts >= 10 && PlayerPrefs.GetInt("boxst", 0) < 3)
        {
            PlayerPrefs.SetInt("boxst", 3);
            firstGM.GetComponent<AchievementShow>().achievementCheck(1, 2);
        }
        else if (cts >= 5 && PlayerPrefs.GetInt("boxst", 0) < 2)
        {
            PlayerPrefs.SetInt("boxst", 2);
            firstGM.GetComponent<AchievementShow>().achievementCheck(1, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("boxst", 0) < 1)
        {
            PlayerPrefs.SetInt("boxst", 1);
            firstGM.GetComponent<AchievementShow>().achievementCheck(1, 0);
        }
    }

    //밤낮
    public void setDay()
    {
        System.DateTime time = System.DateTime.Now;
        if (int.Parse(time.ToString("HH")) >= 12)
        {
            int Hourcheck = int.Parse(time.ToString("HH"));
            if (Hourcheck >= 18 || Hourcheck < 6)
            {
                dayRoom.SetActive(true);
                GM.GetComponent<WindowMiniGame>().nightchangeWindow.SetActive(true);
                GM.GetComponent<WindowMiniGame>().nightchangeWindow2.SetActive(true);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                GM.GetComponent<WindowMiniGame>().nightchangeWindow.SetActive(false);
                GM.GetComponent<WindowMiniGame>().nightchangeWindow2.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
        else
        {
            int Hourcheck = int.Parse(time.ToString("HH"));
            if (Hourcheck >= 18 || Hourcheck < 6)
            {
                dayRoom.SetActive(true);
                GM.GetComponent<WindowMiniGame>().nightchangeWindow.SetActive(true);
                GM.GetComponent<WindowMiniGame>().nightchangeWindow2.SetActive(true);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                GM.GetComponent<WindowMiniGame>().nightchangeWindow.SetActive(false);
                GM.GetComponent<WindowMiniGame>().nightchangeWindow2.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
    }


    public void OpenMap()
    {
        mapWin_obj.SetActive(true);
    }

    public void CloseMap()
    {
        mapWin_obj.SetActive(false);
    }


    public void OpenTra()
    {
        if (GMNotdistroy == null)
        {
            GMNotdistroy = GameObject.FindGameObjectWithTag("GMtag");
        }
        tre0_obj.SetActive(false);
        PlayerPrefs.SetInt("gettre0", 1);
        GMNotdistroy.GetComponent<MainBtnEvt>().CheckTre();
    }

    public void OpenTra1()
    {
        if (GMTag == null)
        {
            GMTag = GameObject.FindGameObjectWithTag("GMtag");
        }
        tre1_obj.SetActive(false);
        PlayerPrefs.SetInt("gettre1", 1);
        GMTag.GetComponent<MainBtnEvt>().CheckTre();
    }

    /// <summary>
    /// 업적 팝업 터치 인포 뒷면 보기
    /// </summary>
    public void AcBtnShowInfoBack()
    {
        if (GMNotdistroy == null)
        {
            GMNotdistroy = GameObject.FindGameObjectWithTag("GMtag");
        }
        if (GMNotdistroy.GetComponent<MainBtnEvt>().MainWindow_obj[0].activeSelf)
        {
            GMNotdistroy.GetComponent<MainInfo>().infoWindowTurn();
        }
        else
        {
            GMNotdistroy.GetComponent<MainInfo>().infoShow();
            GMNotdistroy.GetComponent<MainBtnEvt>().openInfoWindow();
            GMNotdistroy.GetComponent<MainInfo>().infoWindowTurn();
            firstGM.GetComponent<TalkEvt>().closeTalkBoon();
            firstGM.GetComponent<EndingBox>().CloseEnding();
            firstGM.GetComponent<WindowMiniGame>().CloseMiniGame();
        }
    }

}
