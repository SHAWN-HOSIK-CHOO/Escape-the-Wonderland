using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    public VideoPlayer introVP;
    public bool        isPlayerStarted = false;
    public bool        stopPlaying     = false;

    public VideoPlayer outroVP;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stopPlaying )
        {
            return;
        }
        
        if (isPlayerStarted == false && (introVP.isPlaying ))
        {
            isPlayerStarted = true;
        }

        if (isPlayerStarted == true && (!introVP.isPlaying ))
        {
            introVP.gameObject.SetActive(false);
            GameManager.Instance.ccanvas.SetActive(true);
            stopPlaying = true;
            
            GameManager.SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Base);
            GameManager.Instance.WhatDoesTheRabbitSay("탈출하고 싶으면 1번부터 4번 던전까지의 통행증을 모아야 할거야. 집으로 돌아가고 싶으면 잘 해보라고",5.0f);
        }
    }

    public void PlayIntro()
    {
        Debug.Log("playintro called");
        GameManager.Instance.ccanvas.SetActive(false);
        introVP.Play();
    }

    public void PlayOutro()
    {
        Debug.Log("playoutro called");
        //GameManager.Instance.ccanvas.SetActive(false);
        outroVP.gameObject.SetActive(true);
        outroVP.Play();
    }
}
