﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchdust : MonoBehaviour {
	public GameObject GM;
    public GameObject audio_obj;
    public GameObject dust1, dust2;

	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown() {
		string str = gameObject.name;
		if (str.Equals ("먼지1")) {
            float xx = gameObject.transform.position.x;
            float yy = gameObject.transform.position.y;
            PlayerPrefs.SetFloat("watposx", xx);
            PlayerPrefs.SetFloat("watposy", yy);
            GM.GetComponent<SecondRoomTime> ().moveX1 = -12f;
			GM.GetComponent<SecondRoomTime> ().randDust1_i = 0;
            gameObject.transform.position = new Vector3 (-12f, -4f, gameObject.transform.position.z);
		} else {
            float xx = gameObject.transform.position.x;
            float yy = gameObject.transform.position.y;
            PlayerPrefs.SetFloat("watposx", xx);
            PlayerPrefs.SetFloat("watposy", yy);
            GM.GetComponent<SecondRoomTime> ().moveX2 = 12f;
			GM.GetComponent<SecondRoomTime> ().randDust2_i = 0;
			gameObject.transform.position = new Vector3 (12f, -4f, gameObject.transform.position.z);
		}
        audio_obj.GetComponent<SoundEvt>().spiderSound();
        str = PlayerPrefs.GetString ("code", "");
		int coldRain_i = PlayerPrefs.GetInt (str+"c", 0);
		int hotRain_i = PlayerPrefs.GetInt (str+"h", 0);
		coldRain_i = coldRain_i + 5;
		hotRain_i = hotRain_i + 3;
		PlayerPrefs.SetInt (str+"c", coldRain_i);
		PlayerPrefs.SetInt (str+"h", hotRain_i);
		PlayerPrefs.Save ();
        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
        
    }

    public void touchDust1()
    {
        float xx = dust1.transform.position.x;
        float yy = dust1.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);
        GM.GetComponent<SecondRoomTime>().moveX1 = -12f;
        GM.GetComponent<SecondRoomTime>().randDust1_i = 0;
        dust1.transform.position = new Vector3(-12f, -4f, dust1.transform.position.z);
        getDust();
    }
    public void toucgDust2()
    {
        float xx = dust2.transform.position.x;
        float yy = dust2.transform.position.y;
        PlayerPrefs.SetFloat("watposx", xx);
        PlayerPrefs.SetFloat("watposy", yy);
        GM.GetComponent<SecondRoomTime>().moveX2 = 12f;
        GM.GetComponent<SecondRoomTime>().randDust2_i = 0;
        dust2.transform.position = new Vector3(12f, -4f, dust2.transform.position.z);
        getDust();
    }

    void getDust()
    {
        audio_obj.GetComponent<SoundEvt>().spiderSound();
        string str = PlayerPrefs.GetString("code", "");
        int coldRain_i = PlayerPrefs.GetInt(str + "c", 0);
        int hotRain_i = PlayerPrefs.GetInt(str + "h", 0);
        coldRain_i = coldRain_i + 5;
        hotRain_i = hotRain_i + 3;
        PlayerPrefs.SetInt(str + "c", coldRain_i);
        PlayerPrefs.SetInt(str + "h", hotRain_i);
        PlayerPrefs.Save();
        //돈+표시
        GM.GetComponent<GetFadeout>().getRainFade();
    }
}
