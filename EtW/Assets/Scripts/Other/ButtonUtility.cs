using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonUtility : MonoBehaviour
{
    [SerializeField] private float _time       = 0.0f;
    [SerializeField] private float _size       = 0.2f;
    [SerializeField] private float _upSizeTime = 1.3f;
    
    public Button      button;

    public GameObject videoPlayer;
    public bool       isPlayerStarted = false;
    public bool       stopPlaying     = false;

    public GameObject exitBtn;

    private void Start()
    {
        button = GetComponent<Button>();
      
        button.onClick.AddListener(OnClinkEvent);
    }
    
    private void OnClinkEvent()
    {
        GameManager.Instance.GOMapManager.SetActive(false);
        videoPlayer.GetComponent<VideoControl>().PlayOutro();
    }

    private void Update()
    {
        if (_time <= _upSizeTime)
        {
            transform.localScale = Vector3.one * ( 1 + _size * _time );
        }
        else if (_time <= _upSizeTime * 2)
        {
            transform.localScale = Vector3.one * (2 * _size * _upSizeTime + 1 - _time * _size);
        }
        else
        {
            transform.localScale = Vector3.one;
            _time                = 0.0f;
        }

        _time += Time.deltaTime;

        if (stopPlaying)
        {
            return;
        }
        
        Debug.Log("isplayerstarted : " + isPlayerStarted + " video : " + (videoPlayer.GetComponent<VideoControl>().outroVP.isPlaying ));
        
        if (isPlayerStarted == false && (videoPlayer.GetComponent<VideoControl>().outroVP.isPlaying ))
        {
            Debug.Log("called loop1");
            isPlayerStarted = true;
        }

        if (isPlayerStarted == true && (!videoPlayer.GetComponent<VideoControl>().outroVP.isPlaying ))
        {
            Debug.Log("called loop2");
            videoPlayer.GetComponent<VideoControl>().outroVP.gameObject.SetActive(false);
            GameManager.Instance.GOMapManager.SetActive(true);
            //캔버스 끄면 버튼이 사라져 정상적으로 종료 불가
            //GameManager.Instance.ccanvas.SetActive(true);
            stopPlaying = true;
            
            GameManager.SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Base);
            GameManager.Instance.WhatDoesTheRabbitSay("드디어 탈출이구나! 정말 축하해",4.0f);   
            exitBtn.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
