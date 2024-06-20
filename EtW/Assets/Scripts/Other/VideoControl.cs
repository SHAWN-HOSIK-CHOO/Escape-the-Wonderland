using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    public VideoPlayer introVP;
    public bool        isPlayerStarted = false;
    public bool        stopPlaying     = false;
    public GameObject  ccanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stopPlaying)
        {
            return;
        }
        
        if (isPlayerStarted == false && introVP.isPlaying)
        {
            isPlayerStarted = true;
        }

        if (isPlayerStarted == true && !introVP.isPlaying)
        {
            introVP.gameObject.SetActive(false);
            ccanvas.SetActive(true);
            stopPlaying = true;
            
            GameManager.SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Base);
        }
    }

    public void PlayIntro()
    {
        Debug.Log("playintro called");
        ccanvas.SetActive(false);
        introVP.Play();
    }
}
