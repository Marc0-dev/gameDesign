using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutSceneSkip : MonoBehaviour
{
    private VideoPlayer thisPlayer;
    double time;
    double currentTime;
    void Start()
    {
        thisPlayer = this.gameObject.GetComponent<VideoPlayer>();
        time = thisPlayer.clip.length;
    }
    void Update()
    {
        currentTime = thisPlayer.time;
        if(currentTime >= time || Input.anyKeyDown){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
