using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Text videoID;

    void Start(){
    
    }
    public void PlayGame(){
        //Debug.Log(videoID.text);
        PlayerPrefs.SetString("video_ID", videoID.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
