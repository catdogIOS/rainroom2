using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour {

	AsyncOperation async;
	Color color;
	public GameObject logoImg, prolouge_obj, logocanvas;
    public Animator anim;


    private void Awake()
    {
        PlayerPrefs.SetInt("titlesets", 1);
    }

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("logofirst", 0) == 99)
        {
            prolouge_obj.SetActive(false);
        }
        else
        {
            logocanvas.SetActive(false);
        }

		StartCoroutine(imgFadeIn());



            Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.97f)
        {
            prolouge_obj.SetActive(false);
            StartCoroutine(LoadCount());
        }

        //최초로 프롤로그 실행되는 코드, 로고 애니메이션 false
            
    }

	IEnumerator Load()
	{
		async = SceneManager.LoadSceneAsync("Main");
		while (!async.isDone)
		{
			yield return true;
		}

	}
	IEnumerator LoadCount()
    {
        yield return null;
        StartCoroutine(Load());
	}

	IEnumerator imgFadeIn()
	{
		color = logoImg.GetComponent<Image>().color;
		for (float i = 0f; i < 1f; i += 0.05f)
		{
			color.a = Mathf.Lerp(0f, 1f, i);
			logoImg.GetComponent<Image>().color = color;
			yield return new WaitForSeconds(0.025f);
		}


        //최초로 프롤로그 실행되는 코드, 로고 애니메이션 false
        yield return new WaitForSeconds(2f);
        logocanvas.SetActive(false);
        if (PlayerPrefs.GetInt("logofirst") == 99)
        {
            StartCoroutine(Load());
        }
        PlayerPrefs.SetInt("logofirst", 99);
        PlayerPrefs.Save();
    }


    public void nextScene()
    {
        //StartCoroutine(LoadCount());

        /*
            float screenNum = (float)Screen.height / (float)Screen.width;
            if (screenNum < 0.47f)
            {

                Screen.SetResolution(Screen.width, Screen.width / 39 * 18, true);

            }
            else if (screenNum >= 0.5f && screenNum < 0.62f)
            {

                Screen.SetResolution(Screen.width, Screen.width / 16 * 9, true);

            }
            else if (screenNum >= 0.6f && screenNum < 0.7f)
            {

                Screen.SetResolution(Screen.width, Screen.width / 16 * 10, true);

            }
            else if (screenNum >= 0.7f && screenNum < 0.8f)
            {

                Screen.SetResolution(Screen.width, Screen.width / 4 * 3, true);

            }
            else if (screenNum >= 0.8f)
            {

                Screen.SetResolution(Screen.width, Screen.width / 4 * 3, true);

            }
            */
    }

}
