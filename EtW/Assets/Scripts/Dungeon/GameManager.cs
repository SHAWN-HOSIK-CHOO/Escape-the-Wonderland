/*
 * Author : Hosik Choo
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Material[] skyboxArray = new Material[4]; 
    
    private static GameManager _instance;
    public static  MapManager  SMapManager;
    public static  GameObject  SPlayer;
    public         GameObject  dungeonSelectText;
    public         GameObject  videoController;

    public GameObject ccanvas;

    //첫 세이브인가
    public bool isFirstTimePlaying;
    public bool shouldPlayEnding;
    
    //게임 클리어 통행증
    [Header("1st Pass to Clear Game")]
    public int firstClearPass;
    [Header("2nd Pass to Clear Game")]
    public int secondClearPass;
    [Header("3rd Pass to Clear Game")]
    public int thirdClearPass;
    [Header("4th Pass to Clear Game")] 
    public int fourthClearPass;
    
    //통행증이 모두 모였는가
    public bool isAllPassClear;
    
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject GOMapManager;
    private                 Camera     _camera;

    [Header("토끼의 메세지")] public GameObject rabbitMsg;
    [Header("엔딩 버튼")]   public GameObject endingBtn;

    public static GameManager Instance => _instance == null ? null : _instance;

    public bool IsPlayerDead
    {
        get;
        set;
    }
    
    public bool IsBossCleared
    {
        get;
        set;
    }
    
    void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        SPlayer              = player;
        SMapManager          = GOMapManager.GetComponent<MapManager>();
        
        SMapManager.Init();
        isAllPassClear = false;
        
        SMapManager.playerLocation = ePlayerLocation.Base;

        if (PlayerPrefs.HasKey("CanSkipIntroduction"))
        {
            isFirstTimePlaying = false;
        }
        else
        {
            PlayerPrefs.SetInt("CanSkipIntroduction", 1);
        }

        IsPlayerDead     = false;
        IsBossCleared    = false;
        shouldPlayEnding = true;
        rabbitMsg.SetActive(false);
        //TODO: ERASE 현재 player prefs를 시스템에서 사용하기 때문에 디버깅을 위해서 아래 코드를 추가함
        isFirstTimePlaying = true;

        if (!isFirstTimePlaying)
        {
            videoController.SetActive(false);
            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Base);
        }
        else
        {
            videoController.GetComponent<VideoControl>().PlayIntro();
        }
        
        Load();
    }

    void Start()
    {
        _camera            = Camera.main;
        CheckGameStatus();
        SMapManager.playerLocation = ePlayerLocation.Number; // NONE
    }
    
    private void Update()
    {
        
#if UNITY_EDITOR
        //TODO: ERASE
        //Debug Codes-------------------------------------------
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SMapManager.IsCurrentFloorCleared = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Base);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //중요!!! Clear 반드시 해줘야 함 아니면 충돌 발생
            SMapManager.roomDungeonGen.placeablePositions.Clear();
            SMapManager.roomDungeonGen.allWallPositions.Clear();
            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Dungeon2);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Boss2);
        }
        // Debug Codes end----------------------------------------
        //TODO: ERASE
#endif
        
        if (isAllPassClear && shouldPlayEnding)
        {
            shouldPlayEnding = false;
            WhatDoesTheRabbitSay("...",1.0f);
            endingBtn.SetActive(true);
            return;
        }
        
        ProcessInput();
        CheckPlayerStatus();
        CheckGameStatus();
        CheckMapEvents();
    }

    private void ProcessInput()
    {
        if ((SMapManager.playerLocation is ePlayerLocation.Dungeon0 or ePlayerLocation.Dungeon1 or ePlayerLocation.Dungeon2 or ePlayerLocation.Dungeon3) &&
            Input.GetKeyDown(KeyCode.M))
        {
            DungeonQuestManager.Instance.OpenQuestUI();
        }
    }
    
    private void CheckPlayerStatus()
    {
        //TODO: 만약 플레이어가 죽었다면 외부에서 GameManager.IsPlayerDead을 true로 만들어 줄 것임
        //TODO: 예를 들어 Player Script에서 Hp가 0이 되면 GameManager.IsPlayerDead = true;로 설정

        if (IsPlayerDead)
        {
            WhatToDoWhenPlayerDead();
        }
    }

    private void CheckGameStatus()
    {
        if (fourthClearPass == 1 && thirdClearPass == 1 && secondClearPass == 1 && firstClearPass == 1)
        {
            isAllPassClear = true;
        }

        if (   SMapManager.playerLocation == ePlayerLocation.Dungeon0 
            || SMapManager.playerLocation == ePlayerLocation.Dungeon1
            || SMapManager.playerLocation == ePlayerLocation.Dungeon2
            || SMapManager.playerLocation == ePlayerLocation.Dungeon3)
        {
            if(DungeonQuestManager.Instance.IsCurrentQuestCleared())
                SMapManager.IsCurrentFloorCleared = true;
        }
    }

    private void CheckMapEvents()
    {
        if (SMapManager.playerLocation == ePlayerLocation.Dungeon0 
            || SMapManager.playerLocation == ePlayerLocation.Dungeon1
            || SMapManager.playerLocation == ePlayerLocation.Dungeon2
            ||SMapManager.playerLocation == ePlayerLocation.Dungeon3  )
        {
            //SMapManager.GenerateMapAndPlaceCharacter(...)함수에서 SMapManager.IsCurrentFloorCleared를 다시 false로 돌려놓음
            if (SMapManager.IsCurrentFloorCleared)
            {
                SMapManager.IsCurrentFloorCleared = false;
                
                SMapManager.roomDungeonGen.placeablePositions.Clear();
                SMapManager.roomDungeonGen.roamablePositions.Clear();
                SMapManager.roomDungeonGen.allWallPositions.Clear();
                
                if (SMapManager.CurrentDungeonFloorCount == 6)
                {
                    SMapManager.CurrentDungeonFloorCount = 0;
                    switch (SMapManager.playerLocation)
                    {
                        case ePlayerLocation.Dungeon0:
                            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Boss1);
                            break;
                        case ePlayerLocation.Dungeon1:
                            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Boss2);
                            break;
                        case ePlayerLocation.Dungeon2:
                            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Boss3);
                            break;
                        case ePlayerLocation.Dungeon3:
                            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Boss4);
                            break;
                        default:
                            Debug.Log("Wrong location called from GameManager Update");
                            break;
                    }
                }
                else
                { 
                    SMapManager.GenerateMapAndPlaceCharacter(SMapManager.playerLocation);
                }
            }
        }
        else if (SMapManager.playerLocation == ePlayerLocation.Boss1 
                 || SMapManager.playerLocation == ePlayerLocation.Boss2
                 || SMapManager.playerLocation == ePlayerLocation.Boss3
                 || SMapManager.playerLocation == ePlayerLocation.Boss4)
        {
            //TODO: Boss Script에서 BOSS의 hp가 0이 되면 GameManager.IsBossCleared = true로 설정해야 함
            
            if (IsBossCleared)
            {
                WhatDoesTheRabbitSay("좋아 " + SMapManager.playerLocation+" 의 통행증을 얻었어",4.0f);
                switch (SMapManager.playerLocation)
                {
                    case ePlayerLocation.Boss1:
                        firstClearPass = 1;
                        break;
                    case ePlayerLocation.Boss2:
                        secondClearPass = 1;
                        break;
                    case ePlayerLocation.Boss3:
                        thirdClearPass = 1;
                        break;
                    case ePlayerLocation.Boss4:
                        fourthClearPass = 1;
                        break;
                    default:
                        Debug.Log("Invalid Boss room index : called from gamemanager checkmapevents()");
                        break;
                }
                
                SMapManager.playerLocation = ePlayerLocation.Base;
                SMapManager.roomDungeonGen.placeablePositions.Clear();
                SMapManager.roomDungeonGen.roamablePositions.Clear();
                SMapManager.roomDungeonGen.allWallPositions.Clear();
                SMapManager.GenerateMapAndPlaceCharacter(SMapManager.playerLocation);
            }
            //보스 클리어 플래그 되돌리기
            IsBossCleared              = false; 
        }
        else if (SMapManager.playerLocation == ePlayerLocation.Base)
        {
            SMapManager.CurrentDungeonFloorCount = 0;
        }
        else if (SMapManager.playerLocation == ePlayerLocation.Number)
        {
            
        }
    }

    private void OnDisable()
    {
        Save();
    }

    private void OnDestroy()
    {
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("FirstClearPass", firstClearPass);
        PlayerPrefs.SetInt("SecondClearPass", secondClearPass);
        PlayerPrefs.SetInt("ThirdClearPass", thirdClearPass);
        PlayerPrefs.SetInt("FourthClearPass", fourthClearPass);
        // PlayerPrefs.SetInt("PlayerStatATK", PlayerManager.instance.playerStatATK);
        // PlayerPrefs.SetInt("PlayerStatDEF", PlayerManager.instance.playerStatDEF);
        // PlayerPrefs.SetInt("PlayerStatAGI", PlayerManager.instance.playerStatAGI);
        // PlayerPrefs.SetInt("PlayerStatHP", PlayerManager.instance.playerStatHP);
        // PlayerPrefs.SetInt("PlayerStatMP", PlayerManager.instance.playerStatMP);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("FirstClearPass"))
        {
            firstClearPass = PlayerPrefs.GetInt("FirstClearPass");
        }
        else
        {
            firstClearPass = 0;
            PlayerPrefs.SetInt("FirstClearPass", firstClearPass);
        }

        if (PlayerPrefs.HasKey("SecondClearPass"))
        {
            secondClearPass = PlayerPrefs.GetInt("SecondClearPass");
        }
        else
        {
            secondClearPass = 0;
            PlayerPrefs.SetInt("SecondClearPass", secondClearPass);
        }

        if (PlayerPrefs.HasKey("ThirdClearPass"))
        {
            thirdClearPass = PlayerPrefs.GetInt("ThirdClearPass");
        }
        else
        {
            thirdClearPass = 0;
            PlayerPrefs.SetInt("ThirdClearPass", thirdClearPass);
        }

        if (PlayerPrefs.HasKey("FourthClearPass"))
        {
            fourthClearPass = PlayerPrefs.GetInt("FourthClearPass");
        }
        else
        {
            fourthClearPass = 0;
            PlayerPrefs.SetInt("FourthClearPass", fourthClearPass);
        }

        // if (PlayerPrefs.HasKey("PlayerStatATK"))
        // {
        //     PlayerManager.instance.playerStatATK = PlayerPrefs.GetInt("PlayerStatATK");
        // }
        // else
        // {
        //     PlayerPrefs.SetInt("PlayerStatATK", PlayerManager.instance.playerStatATK);
        // }

        // if (PlayerPrefs.HasKey("PlayerStatDEF"))
        // {
        //     PlayerManager.instance.playerStatDEF = PlayerPrefs.GetInt("PlayerStatDEF");
        // }
        // else
        // {
        //     PlayerPrefs.SetInt("PlayerStatDEF", PlayerManager.instance.playerStatDEF);
        // }

        // if (PlayerPrefs.HasKey("PlayerStatAGI"))
        // {
        //     PlayerManager.instance.playerStatAGI = PlayerPrefs.GetInt("PlayerStatAGI");
        // }
        // else
        // {
        //     PlayerPrefs.SetInt("PlayerStatAGI", PlayerManager.instance.playerStatAGI);
        // }

        // if (PlayerPrefs.HasKey("PlayerStatHP"))
        // {
        //     PlayerManager.instance.playerStatHP = PlayerPrefs.GetInt("PlayerStatHP");
        // }
        // else
        // {
        //     PlayerPrefs.SetInt("PlayerStatHP", PlayerManager.instance.playerStatHP);
        // }

        // if (PlayerPrefs.HasKey("PlayerStatMP"))
        // {
        //     PlayerManager.instance.playerStatMP = PlayerPrefs.GetInt("PlayerStatMP");
        // }
        // else
        // {
        //     PlayerPrefs.SetInt("PlayerStatMP", PlayerManager.instance.playerStatMP);
        // }
    }

    private void WhatToDoWhenPlayerDead()
    {
        SMapManager.playerLocation = ePlayerLocation.Base;
        SMapManager.roomDungeonGen.placeablePositions.Clear();
        SMapManager.roomDungeonGen.roamablePositions.Clear();
        SMapManager.roomDungeonGen.allWallPositions.Clear();
        SMapManager.CurrentDungeonFloorCount = 0;
        PlayerManager.instance.notDead = false;
        SMapManager.GenerateMapAndPlaceCharacter(SMapManager.playerLocation);
        StartCoroutine(DisplayRabbitMsg("야 잘 좀 해봐...... 그래가지고 탈출 할 수 있겠어?", 3.0f));
        IsPlayerDead = false;
        PlayerManager.instance.notDead = true;
    }

    // 외부 클래스에서 부르는 용도
    public void WhatDoesTheRabbitSay(string msg, float time = 3.0f)
    {
        StartCoroutine(DisplayRabbitMsg(msg, time));
    }
    
    IEnumerator DisplayRabbitMsg(string msg, float time)
    {
        rabbitMsg.GetComponentInChildren<TMP_Text>().SetText(msg);
        rabbitMsg.SetActive(true);
        yield return new WaitForSeconds(time);
        rabbitMsg.SetActive(false);
    }
}
