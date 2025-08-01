﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class secondRoomFunction : CavasData
{

    //앞뒤 캐릭터
    public GameObject character_obj;


    public GameObject GMNotdistroy;

    public int window_i, book_i, gasrange_i, icebox_i, shelf_i, drawing_i, mat_i, mat2_i, flower_i, light_i, umbrella_i, drawer_i, wall_i;
    public GameObject bookImg_obj, windowImg_obj, drawerImg_obj, windowImg2_obj, gasrangeImg_obj, iceboxImg_obj, shelfImg_obj, drawingImg_obj, matImg_obj, matImg2_obj, flowerImg_obj, lightImg_obj, lightImg2_obj, umbrellaImg_obj, WaterCan_obj, WaterPurifiler_obj;
    public GameObject wallImg_obj, wallImg2_obj;
    public GameObject switch_obj;
    public Sprite[] switch_spr;
    public Sprite[] wall_spr, wall2_spr;
    public GameObject iceBoxBtn_obj;
    //애완동물
    public GameObject roomTutle_obj, roomGoldfish_obj;

    //정수기
    public GameObject WaterPurifilerWindow_obj;
    public GameObject coldToHot_obj, hotToCold_obj;
    public Text WaterPurifilerH_txt, WaterPurifilerC_txt;
    public GameObject hot_obj, cold_obj;

    public GameObject needhRain_obj, needcRain_obj, needMore_obj;

    public GameObject[] secondRoomItem_obj;

    public GameObject seedBox_obj, iceBoxBox_obj, gasrangeBox_obj, drawerBox_obj, doorBox_obj;
    public GameObject boxClean_obj;
    public string boxName_str;
    public GameObject Audio_obj;

    public GameObject close_obj;
    public GameObject umb_obj;

    public Sprite[] lightMax_spr, lightMax2_spr;

    //물부족창
    public GameObject needToast_obj;
    Color color;

    //씬이동
    public GameObject menuBlock_obj;
    public Vector2 menuBlock_vet;
    public Sprite menuOut_spr;
    //미리 씬을 불러오기
    AsyncOperation async;

    public Sprite menuShop_spr;

    //하트
    public int heart_i;
    public Text boxHeart_txt;
    public Text boxTotal_txt, boxTxt_txt;


    //도움말
    public GameObject Help_obj;

    public int boxs_i;
    public Text boxLv_txt;
    public GameObject boxLv_obj;

    public GameObject goOutWindow_obj;

    public GameObject GM2, GMTag;

    //밤
    public GameObject dayRoom;
    public Sprite[] day_spr;
    public GameObject switchToast_obj;
    Color colorS;
    Color colorL;

    //외출
    public Text outPrice_txt, outTime_txt, place_txt;
    public GameObject outP_obj, outGo_obj, outAd_obj, outAdBtn_obj;

    public Sprite[] matPaint_spr, mat2Paint_spr, shelfPaint_spr;
    int changeOut_i;

    //리폼
    public Sprite[] reformWindow_spr, reformWindow2_spr, reformWall_spr, reformWall2_spr, reformDrawer_spr, reformMat1_spr, reformMat2_spr, reformShelf_spr;

    //외출로 얻은물건들
    public GameObject goodsWindow_obj, goodsImg_obj, goodsDesk_obj;
    public Sprite[] goods_spr;
    public int itemIndex_i;
    public Text goods_txt;
    public string[] goods_str, goodsHint_str;
    public GameObject[] goodsGet_obj;
    //public GameObject _obj;

    //엔딩
    public GameObject endWindow_obj;
    public Sprite[] end_spr;
    public int end_i = 0;
    public GameObject endR_obj, endL_obj, endClose_obj;
    public GameObject[] ani_obk;
    public AudioSource m_end;
    public AudioClip sp_end, sp_original;

    public GameObject moveBack_obj, tre2_obj, nightchangeWindow, nightchangeWindow2;


    #region

    public void setIndex0()
    {
        itemIndex_i = 0;
    }
    public void setIndex1()
    {
        itemIndex_i = 1;
    }
    public void setIndex2()
    {
        itemIndex_i = 2;
    }
    public void setIndex3()
    {
        itemIndex_i = 3;
    }
    public void setIndex4()
    {
        itemIndex_i = 4;
    }
    public void setIndex5()
    {
        itemIndex_i = 5;
    }
    public void setIndex6()
    {
        itemIndex_i = 6;
    }
    public void setIndex7()
    {
        itemIndex_i = 7;
    }
    public void setIndex8()
    {
        itemIndex_i = 8;
    }

    public void setIndex9()
    {
        itemIndex_i = 9;
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        outGo_obj.GetComponent<Button>().interactable = true;

        PlayerPrefs.SetInt("adrunout", 0);
        //외출시 스페이드 얻기 초기화
        PlayerPrefs.SetInt("outspade", 2);
        
        //외출굿즈
        if (PlayerPrefs.GetInt("setoutgoods", 0) > 0)
        {
            CheckOutGoods();
            setTextGoods();
            goodsDesk_obj.SetActive(true);
            goodsImg_obj.GetComponent<Image>().sprite = goods_spr[PlayerPrefs.GetInt("setoutgoods", 0)];
        }
        
            //checkEnd();


        colorL = new Color(1f, 1f, 1f);
        color = new Color(1f, 1f, 1f);
        colorS = new Color(1f, 1f, 1f);
        //씬이동
        if (menuBlock_obj == null)
        {
            menuBlock_obj = GameObject.FindGameObjectWithTag("scene");
        }
        menuBlock_vet.y = menuBlock_obj.transform.position.y;
        menuBlock_vet.x = -4000f;
        menuBlock_obj.transform.position = menuBlock_vet;
        if (GMTag == null)
        {
            GMTag = GameObject.FindGameObjectWithTag("GMtag");
        }
        GMTag.GetComponent<MainBtnEvt>().menuBack_obj.GetComponent<Image>().sprite = menuShop_spr;



        GMTag.GetComponent<MainShop>().RabbitColo();
        GMTag.GetComponent<MainShop>().TutleColo();
        GMTag.GetComponent<MainShop>().MarimoColo();
        GMTag.GetComponent<MainShop>().FishColo();

        //GM을 찾아불러온 데이터들 가져오기
        GMNotdistroy = GameObject.FindGameObjectWithTag("loadGM");
        window_i = PlayerPrefs.GetInt("windowlv", 0);
        gasrange_i = PlayerPrefs.GetInt("gasrangelv", 0);
        icebox_i = PlayerPrefs.GetInt("iceboxlv", 0);
        shelf_i = PlayerPrefs.GetInt("shelflv", 0);
        //drawing_i = PlayerPrefs.GetInt ("drawing",0);
        wall_i = PlayerPrefs.GetInt("walllv", 0);
        mat_i = PlayerPrefs.GetInt("mat1lv", 0);
        mat2_i = PlayerPrefs.GetInt("mat2lv", 0);
        flower_i = PlayerPrefs.GetInt("seedlv", 0);
        light_i = PlayerPrefs.GetInt("lightlv", 0);
        umbrella_i = PlayerPrefs.GetInt("umbrellalv", 0);
        drawer_i = PlayerPrefs.GetInt("drawerlv", 0);


        wallImg_obj.GetComponent<Image>().sprite = wall_spr[wall_i];
        wallImg2_obj.GetComponent<Image>().sprite = wall2_spr[wall_i];
        drawerImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().drawer_spr[drawer_i];
        windowImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().window_spr[window_i];
        windowImg2_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().window2_spr[window_i];
        gasrangeImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().gasrange_spr[gasrange_i];
        iceboxImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().icebox_spr[icebox_i];
        shelfImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().shelf_spr[shelf_i];
        //drawing_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().drawing_spr [drawing_i];
        matImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().mat_spr[mat_i];
        if (PlayerPrefs.GetInt("setmatpalette", 0) >= 1)
        {
            matImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().mat_spr[mat_i];
        }
        matImg2_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().mat2_spr[mat2_i];
        if (PlayerPrefs.GetInt("setmatpalette", 0) >= 1)
        {
            matImg2_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().mat2_spr[mat2_i];
        }
        if (PlayerPrefs.GetInt("seedbox", 0) == -10)
        {
            flowerImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().flower_spr[0];
        }
        else
        {
            flowerImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().flower_spr[flower_i];
        }
        lightImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().light_spr[light_i];
        lightImg2_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().light_spr[light_i];
        //umbrellaImg_obj.GetComponent<Image> ().sprite = GMNotdistroy.GetComponent<LoadingData> ().umbrella_spr [umbrella_i];

        if (PlayerPrefs.GetInt("wateringcanshop", 0) == 2)
        {
            WaterCan_obj.SetActive(true);
            if (PlayerPrefs.GetInt("putwatercan", 1) == 0)
            {
                WaterCan_obj.SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("waterpurifiershop", 0) == 2)
        {
            WaterPurifiler_obj.SetActive(true);
        }

        if (PlayerPrefs.GetInt("switchshop", 0) == 2)
        {
            switch_obj.SetActive(true);
            if (PlayerPrefs.GetInt("lightover", 0) == 1)
            {
                switch_obj.GetComponent<Image>().sprite = switch_spr[1];
            }

        }
        //박스
        if (PlayerPrefs.GetInt("iceboxbox", 0) == 10)
        {
            iceBoxBox_obj.SetActive(true);
        }
        else
        {
            iceBoxBtn_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("gasrangebox", 0) == 10)
        {
            gasrangeBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("drawerbox", 0) == 10)
        {
            drawerBox_obj.SetActive(true);
        }
        else
        {
            drawerBox_obj.SetActive(false);
        }
        if (PlayerPrefs.GetInt("seedbox", 0) == -10)
        {
            seedBox_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("doorbox", 0) == 1)
        {
            doorBox_obj.SetActive(false);
            umb_obj.SetActive(true);
        }
        else
        {
            umb_obj.SetActive(false);
        }

        //애완동물
        if (PlayerPrefs.GetInt("settutle", 0) == 1)
        {
            roomTutle_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("setgoldfish", 0) == 1)
        {
            roomGoldfish_obj.SetActive(true);
        }
        //낮밤
        setDay();


        //리폼

        //도어매트
        if (PlayerPrefs.GetInt("setmatpalette", 0) >= 1)
        {
            matImg_obj.GetComponent<Image>().sprite = matPaint_spr[1];
            switch (PlayerPrefs.GetInt("setmatpalette", 0))
            {
                case 1:
                    matImg_obj.GetComponent<Image>().color = new Color(0.99f, 0.81f, 0.80f);
                    break;
                case 2:
                    matImg_obj.GetComponent<Image>().color = new Color(0.80f, 0.9f, 0.99f);
                    break;
                case 3:
                    matImg_obj.GetComponent<Image>().color = new Color(0.99f, 0.9f, 0.80f);
                    break;
                case 4:
                    matImg_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    matImg_obj.GetComponent<Image>().sprite = reformMat1_spr[PlayerPrefs.GetInt("setmatpalette", 0)];
                    break;
                case 5:
                    matImg_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    matImg_obj.GetComponent<Image>().sprite = reformMat1_spr[PlayerPrefs.GetInt("setmatpalette", 0)];
                    break;
            }
        }
        //부엌
        if (PlayerPrefs.GetInt("setmat2palette", 0) >= 1)
        {
            matImg2_obj.GetComponent<Image>().sprite = mat2Paint_spr[1];
            switch (PlayerPrefs.GetInt("setmat2palette", 0))
            {
                case 1:
                    matImg2_obj.GetComponent<Image>().color = new Color(0.99f, 0.81f, 0.80f);
                    break;
                case 2:
                    matImg2_obj.GetComponent<Image>().color = new Color(0.80f, 0.9f, 0.99f);
                    break;
                case 3:
                    matImg2_obj.GetComponent<Image>().color = new Color(0.99f, 0.9f, 0.80f);
                    break;
                case 4:
                    matImg2_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    matImg2_obj.GetComponent<Image>().sprite = reformMat2_spr[PlayerPrefs.GetInt("setmat2palette", 0)];
                    break;
                case 5:
                    matImg2_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    matImg2_obj.GetComponent<Image>().sprite = reformMat2_spr[PlayerPrefs.GetInt("setmat2palette", 0)];
                    break;
            }
        }
        //선반
        if (PlayerPrefs.GetInt("setshelfpalette", 0) >= 1)
        {
            shelfImg_obj.GetComponent<Image>().sprite = shelfPaint_spr[1];
            switch (PlayerPrefs.GetInt("setshelfpalette", 0))
            {
                case 1:
                    shelfImg_obj.GetComponent<Image>().color = new Color(0.99f, 0.81f, 0.80f);
                    break;
                case 2:
                    shelfImg_obj.GetComponent<Image>().color = new Color(0.80f, 0.94f, 0.99f);
                    break;
                case 3:
                    shelfImg_obj.GetComponent<Image>().color = new Color(0.99f, 0.9f, 0.75f);
                    break;
                case 4:
                    shelfImg_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    shelfImg_obj.GetComponent<Image>().sprite = reformShelf_spr[PlayerPrefs.GetInt("setshelfpalette", 0)];
                    break;
                case 5:
                    shelfImg_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    shelfImg_obj.GetComponent<Image>().sprite = reformShelf_spr[PlayerPrefs.GetInt("setshelfpalette", 0)];
                    break;
            }
        }


        //전등
        if (PlayerPrefs.GetInt("lightmaxlv", 0) >= 1)
        {
            lightImg_obj.GetComponent<Image>().sprite = lightMax2_spr[PlayerPrefs.GetInt("lightmaxlv", 0)];
            lightImg2_obj.GetComponent<Image>().sprite = lightMax2_spr[PlayerPrefs.GetInt("lightmaxlv", 0)];
        }
        if (PlayerPrefs.GetInt("shoppalette3", 0) >= 1)
        {
            lightImg_obj.GetComponent<Image>().sprite = lightMax_spr[PlayerPrefs.GetInt("setlightpalette", 0)];
            lightImg2_obj.GetComponent<Image>().sprite = lightMax_spr[PlayerPrefs.GetInt("setlightpalette", 0)];
        }

        //창문
        if (PlayerPrefs.GetInt("shoppalette4", 0) >= 1)
        {
            windowImg_obj.GetComponent<Image>().sprite = reformWindow_spr[PlayerPrefs.GetInt("setwindowpalette", 0)];
            windowImg2_obj.GetComponent<Image>().sprite = reformWindow2_spr[PlayerPrefs.GetInt("setwindowpalette", 0)];
        }
        //장식장
        if (PlayerPrefs.GetInt("shoppalette5", 0) > 0)
        {
            switch (PlayerPrefs.GetInt("setdrawerpalette", 0))
            {
                case 1:
                    drawerImg_obj.GetComponent<Image>().color = new Color(0.75f, 0.95f, 0.99f);
                    break;
                case 2:
                    drawerImg_obj.GetComponent<Image>().color = new Color(0.99f, 0.85f, 0.60f);
                    break;
                case 3:
                    drawerImg_obj.GetComponent<Image>().color = new Color(0.70f, 0.70f, 0.70f);
                    break;
                case 4:
                    drawerImg_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    drawerImg_obj.GetComponent<Image>().sprite = reformDrawer_spr[PlayerPrefs.GetInt("setdrawerpalette", 0)];
                    break;
                case 5:
                    drawerImg_obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    drawerImg_obj.GetComponent<Image>().sprite = reformDrawer_spr[PlayerPrefs.GetInt("setdrawerpalette", 0)];
                    break;
            }
        }
        //벽지
        if (PlayerPrefs.GetInt("shoppalette9", 0) > 0)
        {
            wallImg_obj.GetComponent<Image>().sprite = reformWall_spr[PlayerPrefs.GetInt("setwallpalette", 0)];
            wallImg2_obj.GetComponent<Image>().sprite = reformWall2_spr[PlayerPrefs.GetInt("setwallpalette", 0)];
        }

        //보물찾기
        if (PlayerPrefs.GetInt("gettre2", 0) == 1)
        {
            tre2_obj.SetActive(false);
        }
    }

    //보물찾기
    public void OpenTra2()
    {
        if (GMTag == null)
        {
            GMTag = GameObject.FindGameObjectWithTag("GMtag");
        }
        tre2_obj.SetActive(false);
        PlayerPrefs.SetInt("gettre2", 1);
        GMTag.GetComponent<MainBtnEvt>().CheckTre();
    }

    /// <summary>
    /// 업적 팝업 터치 인포 뒷면 보기
    /// </summary>
    public void AcBtnShowInfoBack()
    {
        if (GMTag == null)
        {
            GMTag = GameObject.FindGameObjectWithTag("GMtag");
        }
        if (GMTag.GetComponent<MainBtnEvt>().MainWindow_obj[0].activeSelf)
        {
            GMTag.GetComponent<MainInfo>().infoWindowTurn();
        }
        else
        {
            AllClose();
            GMTag.GetComponent<MainInfo>().infoShow();
            GMTag.GetComponent<MainBtnEvt>().openInfoWindow();
            GMTag.GetComponent<MainInfo>().infoWindowTurn();
            GM2.GetComponent<GasrangeEvt>().CloseIceBox();
            GM2.GetComponent<TalkEvt>().closeTalkBoon();
        }
    }


    //공원대화500회 1
    //도시대화500회 2
    //전단지100회 3
    //도시음식20회 4
    //공원음식20회 5
    //바다조개30회 6
    //바다병50회 7
    //숲속다람쥐30회 8
    //숲속표지판50회 9

    public void Opengoods()
    {
        goods_txt.text = "";
        goodsWindow_obj.SetActive(true);
    }

    public void GoodsHint()
    {
        goods_txt.text = goodsHint_str[itemIndex_i];
    }
    public void SetGoods()
    {
        goods_txt.text = goods_str[itemIndex_i];
        itemIndex_i++;
        goodsImg_obj.GetComponent<Image>().sprite = goods_spr[itemIndex_i];
        PlayerPrefs.SetInt("setoutgoods", itemIndex_i);
    }

    void setTextGoods()
    {
        goodsHint_str[0] = "공원에서 대화를 많이 나누자";
        goods_str[0] = "바깥 풍경을 보며 대화하는 것도 나쁘지 않지";
        goodsHint_str[1] = "도시에서 대화를 많이 나누자";
        goods_str[1] = "건물 밑에서 대화를 나누는 것도 나쁘지 않아";
        goodsHint_str[2] = "전단지를 많이 주워보자";
        goods_str[2] = "인형 탈을 쓴 사람이\n전단지를 나눠주고 있었어";
        goodsHint_str[3] = "푸드트럭에 자주 가보자";
        goods_str[3] = "작은 트럭 미니어처야.\n푸드트럭에 자주가서 받았어";
        goodsHint_str[4] = "야시장에 자주 가보자";
        goods_str[4] = "작은 파라솔 미니어처야.\n야시장에 자주가서 받았어";
        goodsHint_str[5] = "바다에서 모래를 많이 찾아보자";
        goods_str[5] = "모래 속에는 다양한 조개들이 숨어 있어";
        goodsHint_str[6] = "바다에서 유리병을 많이 보자";
        goods_str[6] = "병 안에 있는 쪽지는 신기해";
        goodsHint_str[7] = "숲에서 다람쥐와 친해지자";
        goods_str[7] = "작은 다람쥐가 나에게 선물해줬어";
        goodsHint_str[8] = "숲에서 표지판을 많이 보자";
        goods_str[8] = "표지판에 누가 적어두는 걸까";
    }



    /// <summary>
    /// 단칸방 스위치 켜기
    /// </summary>
    public void TurnOnSwitch()
    {
        if (PlayerPrefs.GetInt("dayday", 0) == 1)
        {
            //밤
            if (PlayerPrefs.GetInt("lightover", 0) == 1)
            {
                //꺼짐
                dayRoom.GetComponent<Image>().sprite = day_spr[0];
                switch_obj.GetComponent<Image>().sprite = switch_spr[0];
                PlayerPrefs.SetInt("lightover", 0);
            }
            else
            {
                //켜짐
                dayRoom.GetComponent<Image>().sprite = day_spr[1];

                switch_obj.GetComponent<Image>().sprite = switch_spr[1];
                PlayerPrefs.SetInt("lightover", 1);
            }
        }
        else
        {
            //낮
            StopCoroutine("SwitchToastFadeOut");
            StartCoroutine("SwitchToastFadeOut");
        }



    }


    /// <summary>
    /// 정수기로 찬물 따듯한물을 바꿔준다
    /// </summary>
    public void OpenWaterPurifiler()
    {
        if (WaterPurifilerWindow_obj.activeSelf == true)
        {
            WaterPurifilerWindow_obj.SetActive(false);
            CloseWaterYN();
        }
        else
        {
            string str1;
            str1 = PlayerPrefs.GetString("code", "");
            coldRain_i = PlayerPrefs.GetInt(str1 + "c", 0);
            hotRain_i = PlayerPrefs.GetInt(str1 + "h", 0);
            WaterPurifilerC_txt.text = "" + coldRain_i;
            WaterPurifilerH_txt.text = "" + hotRain_i;
            WaterPurifilerWindow_obj.SetActive(true);
        }
    }
    public void ColdToHot()
    {
        coldToHot_obj.SetActive(true);
    }
    public void HotToCold()
    {
        hotToCold_obj.SetActive(true);
    }

    public void ColdToHotYes()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str1 + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str1 + "h", 0);
        if (coldRain_i >= 400)
        {
            hot_obj.SetActive(true);
            coldRain_i = coldRain_i - 400;
            hotRain_i = hotRain_i + 10;
            PlayerPrefs.SetInt(str1 + "c", coldRain_i);
            PlayerPrefs.SetInt(str1 + "h", hotRain_i);
            PlayerPrefs.Save();
            WaterPurifilerC_txt.text = "" + coldRain_i;
            WaterPurifilerH_txt.text = "" + hotRain_i;
            Audio_obj.GetComponent<SoundEvt>().waterSound();
        }
        else
        {
            Audio_obj.GetComponent<SoundEvt>().cancleSound();
            //찬물부족
            needcRain_obj.SetActive(true);
            StartCoroutine("toastCImgFadeOut");
        }
        coldToHot_obj.SetActive(false);
    }

    public void HotToColdYes()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        coldRain_i = PlayerPrefs.GetInt(str1 + "c", 0);
        hotRain_i = PlayerPrefs.GetInt(str1 + "h", 0);
        if (hotRain_i >= 20)
        {
            cold_obj.SetActive(true);
            coldRain_i = coldRain_i + 200;
            hotRain_i = hotRain_i - 20;
            PlayerPrefs.SetInt(str1 + "c", coldRain_i);
            PlayerPrefs.SetInt(str1 + "h", hotRain_i);
            PlayerPrefs.Save();
            WaterPurifilerC_txt.text = "" + coldRain_i;
            WaterPurifilerH_txt.text = "" + hotRain_i;
            Audio_obj.GetComponent<SoundEvt>().waterSound();
        }
        else
        {

            Audio_obj.GetComponent<SoundEvt>().cancleSound();
            //따듯한물부족
            needhRain_obj.SetActive(true);
        }

        hotToCold_obj.SetActive(false);
    }

    public void closeCH()
    {
        hot_obj.SetActive(false);
        cold_obj.SetActive(false);
    }

    public void CloseWaterYN()
    {
        coldToHot_obj.SetActive(false);
        hotToCold_obj.SetActive(false);
    }

    public void needClose()
    {
        needcRain_obj.SetActive(false);
        needhRain_obj.SetActive(false);

    }


    public void boxOpen()
    {
        boxHeart_txt.text = "" + boxs_i;

        boxClean_obj.SetActive(true);
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        boxTotal_txt.text = "" + heart_i;
    }
    public void boxSeed()
    {
        boxName_str = "seed";
        boxs_i = 4;
        boxLv_txt.text = "";
    }
    public void boxDrawer()
    {
        boxName_str = "drawer";
        boxs_i = 1;
        boxLv_txt.text = "";
    }
    public void boxGas()
    {
        boxName_str = "gasrange";
        boxs_i = 4;
        boxLv_txt.text = "";
    }
    public void boxIce()
    {
        boxName_str = "icebox";
        boxs_i = 4;
        boxLv_txt.text = "";
    }

    public void boxDoor()
    {
        boxName_str = "door";
        boxs_i = 10;
        boxLv_txt.text = "호감Lv.6 달성하기";
    }


    public void boxYes()
    {
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        int heart_i;
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        boxTxt_txt.text = "" + boxs_i;
        if (boxName_str == "door")
        {
            if (PlayerPrefs.GetInt("lovelv", 0) >= 6)//6
            {
                if (heart_i >= boxs_i)
                {
                    Audio_obj.GetComponent<SoundEvt>().boxSound();
                    heart_i = heart_i - boxs_i;
                    PlayerPrefs.SetInt(str1 + "ht", heart_i);
                    PlayerPrefs.SetInt(boxName_str + "box", 1);
                    PlayerPrefs.SetInt(boxName_str + "lv", 1);
                    checkach();
                    PlayerPrefs.Save();
                    if (PlayerPrefs.GetInt("doorbox", 0) == 1)
                    {
                        doorBox_obj.SetActive(false);
                        umb_obj.SetActive(true);
                    }
                    boxClean_obj.SetActive(false);
                }
                else
                {
                    needMoney();
                    boxClean_obj.SetActive(false);
                    Audio_obj.GetComponent<SoundEvt>().cancleSound();
                }
            }
            else
            {
                StopCoroutine("toastLadderFadeOut");
                StartCoroutine("toastLadderFadeOut");
                boxLv_obj.SetActive(true);
            }
        }
        else if (heart_i >= boxs_i)
        {
            Audio_obj.GetComponent<SoundEvt>().boxSound();
            heart_i = heart_i - boxs_i;
            PlayerPrefs.SetInt(str1 + "ht", heart_i);

            PlayerPrefs.SetInt(boxName_str + "box", 1);
            PlayerPrefs.SetInt(boxName_str + "lv", 1);
            checkach();
            PlayerPrefs.Save();
            if (PlayerPrefs.GetInt("seedbox", 0) == 1)
            {
                seedBox_obj.SetActive(false);
            }
            if (PlayerPrefs.GetInt("drawerbox", 0) == 1)
            {
                drawerBox_obj.SetActive(false);
            }
            if (PlayerPrefs.GetInt("gasrangebox", 0) == 1)
            {
                gasrangeBox_obj.SetActive(false);
            }
            if (PlayerPrefs.GetInt("iceboxbox", 0) == 1)
            {
                iceBoxBox_obj.SetActive(false);
                iceBoxBtn_obj.SetActive(true);
                PlayerPrefs.SetInt("egg", 1);
            }
            if (PlayerPrefs.GetInt("doorbox", 0) == 1)
            {
                doorBox_obj.SetActive(false);
                umb_obj.SetActive(true);
            }
            flower_i = PlayerPrefs.GetInt("seedlv", 0);
            drawer_i = PlayerPrefs.GetInt("drawerlv", 0);
            gasrange_i = PlayerPrefs.GetInt("gasrangelv", 0);
            icebox_i = PlayerPrefs.GetInt("iceboxlv", 0);

            flowerImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().flower_spr[flower_i];
            drawerImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().drawer_spr[drawer_i];
            gasrangeImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().gasrange_spr[gasrange_i];
            iceboxImg_obj.GetComponent<Image>().sprite = GMNotdistroy.GetComponent<LoadingData>().icebox_spr[icebox_i];

            boxClean_obj.SetActive(false);
        }
        else
        {
            Audio_obj.GetComponent<SoundEvt>().cancleSound();
            needMoney();
            boxClean_obj.SetActive(false);
            //돈부족
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


    void needMoney()
    {
        StopCoroutine("toastNImgFadeOut");
        StartCoroutine("toastNImgFadeOut");
    }

    void CheckOutGoods()
    {
        //얻자마자 세팅해주기
        PlayerPrefs.GetInt("setoutgoods", 0);
        PlayerPrefs.GetInt("outgoods", 0);

        //공원대화500회 1
        if (PlayerPrefs.GetInt("ptfin", 0) == 1)
        {
            goodsGet_obj[0].SetActive(true);
        }
        //도시대화500회 2
        if (PlayerPrefs.GetInt("ctfin", 0) == 1)
        {
            goodsGet_obj[1].SetActive(true);
        }
        //전단지100회 3
        if (PlayerPrefs.GetInt("cpfin", 0) == 1)
        {
            goodsGet_obj[2].SetActive(true);
        }
        //도시음식20회 4
        if (PlayerPrefs.GetInt("cffin", 0) == 1)
        {
            goodsGet_obj[3].SetActive(true);
        }
        //공원음식20회 5
        if (PlayerPrefs.GetInt("pffin", 0) == 1)
        {
            goodsGet_obj[4].SetActive(true);
        }
        //바다조개30회 6
        if (PlayerPrefs.GetInt("crfin", 0) == 1)
        {
            goodsGet_obj[5].SetActive(true);
        }
        //바다병50회 7
        if (PlayerPrefs.GetInt("bottlefin", 0) == 1)
        {
            goodsGet_obj[6].SetActive(true);
        }
        //숲속다람쥐30회 8
        if (PlayerPrefs.GetInt("sqfin", 0) == 1)
        {
            goodsGet_obj[7].SetActive(true);
        }
        //숲속표지판50회 9
        if (PlayerPrefs.GetInt("signfin", 0) == 1)
        {
            goodsGet_obj[8].SetActive(true);
        }
    }

    //외출페이드아웃
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

    //토스트페이드아웃
    IEnumerator toastNImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        needToast_obj.GetComponent<Image>().color = color;
        needToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            needToast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        needToast_obj.SetActive(false);
    }

    //스위치토스트페이드아웃
    IEnumerator SwitchToastFadeOut()
    {
        colorS.a = Mathf.Lerp(0f, 1f, 1f);
        switchToast_obj.GetComponent<Image>().color = colorS;
        switchToast_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            colorS.a = Mathf.Lerp(0f, 1f, i);
            switchToast_obj.GetComponent<Image>().color = colorS;
            yield return null;
        }
        switchToast_obj.SetActive(false);
    }

    public void boxClose()
    {
        boxClean_obj.SetActive(false);
    }

    public void OpenClose()
    {
        close_obj.SetActive(true);
    }


    public void AllClose()
    {
        close_obj.SetActive(false);
        WaterPurifilerWindow_obj.SetActive(false);
        CloseWaterYN();
        goOutWindow_obj.SetActive(false);
        goodsWindow_obj.SetActive(false);
    }


    //토스트가 사라지게
    IEnumerator toastCImgFadeOut()
    {
        color.a = Mathf.Lerp(0f, 1f, 1f);
        needcRain_obj.GetComponent<Image>().color = color;
        needcRain_obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            needcRain_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        needcRain_obj.SetActive(false);
    }

    //캐릭터회전
    public void characterTurn()
    {
        if (character_obj.transform.rotation.y == 0)
        {
            character_obj.transform.rotation = new Quaternion(0, 180, 0, 0);
            //dayRoom.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            character_obj.transform.rotation = new Quaternion(0, 0, 0, 0);
            //dayRoom.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void OpenHelpCook()
    {
        Help_obj.SetActive(true);
    }
    public void CloseHelp()
    {
        Help_obj.SetActive(false);
    }

    //외출창띄우기
    public void OpenGoOut()
    {

        //나갈장소 어디인지  1공원 2도시
        changeOut_i = 1;
        place_txt.text = "공원";

        StopCoroutine("outTime");
        StartCoroutine("outTime");
        outPrice_txt.text = "20";
        if (PlayerPrefs.GetInt("bouttime", 14) == 9)
        {
            outPrice_txt.text = "10";
        }
        goOutWindow_obj.SetActive(true);
        if (GMTag == null)
        {
            GMTag = GameObject.FindGameObjectWithTag("GMtag");
        }
        //메뉴
        if (GMTag.GetComponent<MainBtnEvt>().MainBtn_obj[0].activeSelf == false)
        {
            GMTag.GetComponent<MainBtnEvt>().showButtons();
        }
        if (PlayerPrefs.GetInt("showhavec", 0)==0)
        {
            GMTag.GetComponent<MainBtnEvt>().closeShow();
            PlayerPrefs.SetInt("showhavec", 1);
        }

        //외출 문제해결
        Invoke("gooutfix",5f);
    }
    void gooutfix()
    {
        if (outTime_txt.text == "00:00")
        {
            outGo_obj.GetComponent<Button>().interactable = true;
        }
    }
    public void CloseGoOut()
    {
        goOutWindow_obj.SetActive(false);
        outP_obj.SetActive(false);
    }

    //업적
    void checkach()
    {
        checkbox();
        int cts = PlayerPrefs.GetInt("countboxst", 0);
        if (cts >= 10 && PlayerPrefs.GetInt("boxst", 0) < 3)
        {
            PlayerPrefs.SetInt("boxst", 3);
            GM2.GetComponent<AchievementShow>().achievementCheck(1, 2);
        }
        else if (cts >= 5 && PlayerPrefs.GetInt("boxst", 0) < 2)
        {
            PlayerPrefs.SetInt("boxst", 2);
            GM2.GetComponent<AchievementShow>().achievementCheck(1, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("boxst", 0) < 1)
        {
            PlayerPrefs.SetInt("boxst", 1);
            GM2.GetComponent<AchievementShow>().achievementCheck(1, 0);
        }
    }

    //낮밤
    public void setDay()
    {
        System.DateTime time = System.DateTime.Now;
        if (int.Parse(time.ToString("HH")) >= 12)
        {
            int Hourcheck = int.Parse(time.ToString("HH"));
            if (Hourcheck >= 18 || Hourcheck < 6)
            {
                dayRoom.SetActive(true);
                nightchangeWindow.SetActive(true);
                nightchangeWindow2.SetActive(true);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                nightchangeWindow.SetActive(false);
                nightchangeWindow2.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }
        else
        {
            int Hourcheck = int.Parse(time.ToString("HH"));
            if (Hourcheck >= 18 || Hourcheck < 6)
            {
                dayRoom.SetActive(true);
                nightchangeWindow.SetActive(true);
                nightchangeWindow2.SetActive(true);
                PlayerPrefs.SetInt("dayday", 1);
            }
            else
            {
                //낮
                dayRoom.SetActive(false);
                nightchangeWindow.SetActive(false);
                nightchangeWindow2.SetActive(false);
                PlayerPrefs.SetInt("dayday", 0);
            }
        }

        if (PlayerPrefs.GetInt("lightover", 0) == 1)
        {
            //켜짐
            dayRoom.GetComponent<Image>().sprite = day_spr[1];
            switch_obj.GetComponent<Image>().sprite = switch_spr[1];
        }
    }


    /// <summary>
    /// 외출함수
    /// </summary>
    public void walkOut()
    {
        //
        string str1;
        str1 = PlayerPrefs.GetString("code", "");
        int heart_i;
        heart_i = PlayerPrefs.GetInt(str1 + "ht", 0);
        int hp_i = 20;
        if (PlayerPrefs.GetInt("bouttime", 14) == 9)
        {
            hp_i = hp_i - 10;
        }
        if (heart_i >= hp_i)//30테스트
        {
            PlayerPrefs.SetString("outLastTime", System.DateTime.UtcNow.ToString());
            PlayerPrefs.SetInt("outtrip", 1);
            PlayerPrefs.SetInt("outorhome", 1);

            //외출 장소 정하기
            if (changeOut_i == 2)
            {
                PlayerPrefs.SetInt("outtrip", 2);
                PlayerPrefs.SetInt("outorhome", 2);
            }


            heart_i = heart_i - hp_i;
            PlayerPrefs.SetInt(str1 + "ht", heart_i);
            PlayerPrefs.SetInt("bouttime", 14);
            //외출업적
            PlayerPrefs.SetInt("acgocheck", 1);
            //checkachOut();
            StartCoroutine("LoadOut");
            GMTag.GetComponent<MainBtnEvt>().menuBack_obj.GetComponent<Image>().sprite = menuOut_spr;
        }
        else
        {
            Audio_obj.GetComponent<SoundEvt>().cancleSound();
            needMoney();
            goOutWindow_obj.SetActive(false);
            //돈부족
        }
        if (GMTag == null)
        {
            GMTag = GameObject.FindGameObjectWithTag("GMtag");
        }
    }

    public void OpenWalkOut()
    {
        outP_obj.SetActive(true);
    }
    public void OpenOutAd()
    {
        outAd_obj.SetActive(true);
    }
    public void CloseOutAd()
    {
        outAd_obj.SetActive(false);

    }

    public void WaitShow()
    {
        if (PlayerPrefs.GetInt("wait", 0) == 2)
        {
            outAd_obj.SetActive(true);
        }
    }

    IEnumerator outTime()
    {
        int a = 0;
        while (a == 0)
        {
            if (PlayerPrefs.GetInt("bouttime", 14) == 9)
            {
                outAdBtn_obj.GetComponent<Button>().interactable = false;
                outPrice_txt.text = "10";
            }
            if (PlayerPrefs.GetInt("outtimeon", 0) == 0)
            {
                outTime_txt.text = "00:00";
                outGo_obj.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("outtimeon", 1);
            }
            else
            {
                
                System.DateTime dateTime = System.DateTime.UtcNow.AddHours(-1);
                string lastTime = PlayerPrefs.GetString("outLastTime", dateTime.ToString());
                System.DateTime lastDateTime;
                try
                {
                    lastDateTime = System.DateTime.Parse(lastTime);
                }
                catch (System.Exception)
                {
                    lastTime = System.DateTime.UtcNow.AddHours(-1).ToString();
                }
                lastDateTime = System.DateTime.Parse(lastTime);

                System.TimeSpan compareTime = System.DateTime.UtcNow - lastDateTime;
                int m = (int)compareTime.TotalMinutes;
                int sec = (int)compareTime.TotalSeconds;
                sec = sec - (sec / 60) * 60;
                sec = 59 - sec;
                m = PlayerPrefs.GetInt("bouttime", 14) - m;
                string strb = string.Format(@"{0:00}" + ":", m) + string.Format(@"{0:00}", sec);
                outTime_txt.text = strb;
                if (m < 0)
                {
                    outTime_txt.text = "00:00";
                    PlayerPrefs.SetInt("outtimeon", 0);
                    if (outGo_obj.GetComponent<Button>().interactable == false)
                    {
                        PlayerPrefs.Save();
                    }
                    outGo_obj.GetComponent<Button>().interactable = true;
                }
                else
                {
                    outGo_obj.GetComponent<Button>().interactable = false;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }





    IEnumerator LoadOut()
    {
        async = SceneManager.LoadSceneAsync("SubLoadOut");
        while (!async.isDone)
        {
            yield return true;
        }
    }

    //외출업적
    void checkachOut()
    {
        int cts = PlayerPrefs.GetInt("countgooutst", 0);
        cts++;
        PlayerPrefs.SetInt("countgooutst", cts);
        //Debug.Log("tal" + PlayerPrefs.GetInt("gooutst", 0) + "cts" + cts);
        if (cts >= 100 && PlayerPrefs.GetInt("gooutst", 0) < 3)
        {
            PlayerPrefs.SetInt("gooutst", 3);
            GM2.GetComponent<AchievementShow>().achievementCheck(7, 2);
        }
        else if (cts >= 30 && PlayerPrefs.GetInt("gooutst", 0) < 2)
        {
            PlayerPrefs.SetInt("gooutst", 2);
            GM2.GetComponent<AchievementShow>().achievementCheck(7, 1);
        }
        else if (cts >= 1 && PlayerPrefs.GetInt("gooutst", 0) < 1)
        {
            PlayerPrefs.SetInt("gooutst", 1);
            GM2.GetComponent<AchievementShow>().achievementCheck(7, 0);
        }
        PlayerPrefs.Save();
    }

    public void GoCity()
    {
        PlayerPrefs.SetString("outLastTime", System.DateTime.UtcNow.ToString());
        PlayerPrefs.SetInt("outtrip", 2);
        PlayerPrefs.SetInt("bouttime", 14);
        //외출업적
        PlayerPrefs.SetInt("acgocheck", 1);
        StartCoroutine("LoadOut");
        GMTag.GetComponent<MainBtnEvt>().menuBack_obj.GetComponent<Image>().sprite = menuOut_spr;
    }

    //테스트//////////////////////////////////////////
    public void GoParkmini()
    {
        PlayerPrefs.SetInt("outtrip", 3);
        StartCoroutine("LoadOut");
    }

    public void OutChange()//공원1 도시2
    {
        if (changeOut_i >= 0)
        {
            changeOut_i++;
        }
        if (changeOut_i == 2)
        {
            place_txt.text = "도시";
        }
        if (changeOut_i >= 3)
        {
            changeOut_i = 1;
            place_txt.text = "공원";
        }
    }


    void checkEnd()
    {
        int a = 0;
        if (PlayerPrefs.GetInt("endbefore", 0) == 0)
        {

            //대화
            if (PlayerPrefs.GetInt("talkending", 0) == 1)
            {
                a++;
            }
            //첫공원
            if (PlayerPrefs.GetInt("parkending", 0) == 1)
            {
                a++;
            }
            //첫도시
            if (PlayerPrefs.GetInt("cityending", 0) == 1)
            {
                a++;
            }
            //우유10번
            if (PlayerPrefs.GetInt("milkending", 0) == 1)
            {
                a++;
            }
            //바다10번
            if (PlayerPrefs.GetInt("seaending", 0) == 1)
            {
                a++;
            }
            //나뭇잎40번
            if (PlayerPrefs.GetInt("leafending", 0) == 1)
            {
                a++;
            }
            //그림모두
            if (PlayerPrefs.GetInt("pictureending", 0) == 1)
            {
                a++;
            }
            //모든요리
            if (PlayerPrefs.GetInt("cookending", 0) == 1)
            {
                a++;
            }
            //호감도
            if (PlayerPrefs.GetInt("likeending", 0) == 1)
            {
                a++;
            }
            if (a >= 9)
            {
                PlayerPrefs.SetInt("endbefore", 1);
            }
        }
        else if(PlayerPrefs.GetInt("endafter", 0) == 1)
        {
            //소리
            m_end.clip = sp_end;
            m_end.Play();
            endWindow_obj.SetActive(true);
        }
    }


    public void CloseEnd()
    {
        endWindow_obj.SetActive(false);
        Audio_obj.GetComponent<SoundEvt>().cancleSound();
        m_end.clip = sp_original;
        m_end.Play();
    }

    public void endR()
    {
        PlayerPrefs.SetInt("endoverever", 1);
        Audio_obj.GetComponent<SoundEvt>().turnSound();
        if (end_i == 1)
        {
            endR_obj.SetActive(false);
            endClose_obj.SetActive(true);
            end_i++;
            ani_obk[0].SetActive(false);
            ani_obk[1].SetActive(false);
            ani_obk[2].SetActive(false);
            ani_obk[end_i].SetActive(true);
        }
        else
        {
            endL_obj.SetActive(true);
            end_i++;
            ani_obk[0].SetActive(false);
            ani_obk[1].SetActive(false);
            ani_obk[2].SetActive(false);
            ani_obk[end_i].SetActive(true);
        }
    }
    public void endL()
    {
        Audio_obj.GetComponent<SoundEvt>().turnSound();
        endClose_obj.SetActive(false);
        if (end_i == 1)
        {
            endL_obj.SetActive(false);
            end_i--;
            ani_obk[0].SetActive(false);
            ani_obk[1].SetActive(false);
            ani_obk[2].SetActive(false);
            ani_obk[end_i].SetActive(true);
        }
        else
        {
            endR_obj.SetActive(true);
            end_i--;
            ani_obk[0].SetActive(false);
            ani_obk[1].SetActive(false);
            ani_obk[2].SetActive(false);
            ani_obk[end_i].SetActive(true);
        }
    }
}
