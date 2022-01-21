using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour {

	public Camera Main_camera;
	public GameObject canvas_obj, blackCanvas_obj;

	void Awake(){
		canvas_obj = GameObject.Find ("MainCanvas");
        blackCanvas_obj=GameObject.Find("검은화면캔버스");

    }
	// Use this for initialization
	void Start () {
		
		canvas_obj.GetComponent<Canvas>().worldCamera = Main_camera;
        blackCanvas_obj.GetComponent<Canvas>().worldCamera = Main_camera;
    }
	

}
