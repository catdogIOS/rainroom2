using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBGMSetter : MonoBehaviour
{
    [Header("재생할 배경음악")]
    public AudioClip sceneMusic;

    [Header("재생할 빗소리")]
    public AudioClip RainMusic;

    void Start()
    {
        setSceneMusic();

    }

    void setSceneMusic()
    {
        GameObject soundObj = GameObject.Find("SoundControl");

        if (soundObj != null)
        {
            SoundHandler handler = soundObj.GetComponent<SoundHandler>();

            if (handler != null && sceneMusic != null)
            {
                handler.ChangeBGM(sceneMusic);
            }
            if (handler != null && RainMusic != null)
            {
                handler.ChangeBGS(RainMusic);
            }
        }
    }

}
