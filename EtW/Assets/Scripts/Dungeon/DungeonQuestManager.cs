/*
 *Author : Hosik Choo
 */
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonQuestManager : MonoBehaviour
{
   private static DungeonQuestManager _instance;
   
   [SerializeField] private bool       _isCurrentQuestCleared;

   [Header("For Eliminate Quest")] public int  TargetCount;
   [Header("For Hunt Quest")]      public bool isGateKeeperDead;
   [Header("For Find Quest")]      public bool foundPortal;

   [Header("Quest Type Notification")] public TMP_Text questText;
   
   public eQuestType CurrentQuestType
   {
      get;
      set;
   }

   public enum eQuestType
   {
      Hunt, //게이트 키퍼 몬스터 제거
      Find, // 계단 찾기
      Eliminate, // 특정 수 만큼 몬스터 제거
      None
   }

   public static DungeonQuestManager Instance => _instance == null ? null : _instance;

   private void Awake()
   {
      if (_instance == null)
      {
         _instance = this;
         DontDestroyOnLoad(this.gameObject);
      }
      else
      {
         Destroy(this.gameObject);
      }
      
      questText.gameObject.SetActive(false);
   }

   public void OpenQuestUI()
   {
      
      switch (CurrentQuestType)
      {
         case eQuestType.Eliminate:
            GameManager.Instance.WhatDoesTheRabbitSay(   "아무 몬스터"+ TargetCount +" 마리만 남겨봐", 2.5f);
            break;
         case eQuestType.Find:
            GameManager.Instance.WhatDoesTheRabbitSay("이번에는 포탈을 찾아봐", 2.5f);
            break;
         case eQuestType.Hunt:
            GameManager.Instance.WhatDoesTheRabbitSay("이번 층을 막고 있는 몬스터가 있어, 게이트키퍼를 제거해!", 2.5f);
            break;
         default:
            Debug.Log("Wrong type called from DungeonQuestManager.ShowQuestType()");
            break;
      }
   }
   
   public void GenerateQuest()
   {
      int index = Random.Range(0, 2);
      
      if (index == 0)
      {
         CurrentQuestType = eQuestType.Hunt;
      }
      else if (index == 1)
      {
         CurrentQuestType = eQuestType.Find;
      }
      else if (index == 2)
      {
         CurrentQuestType = eQuestType.Eliminate;
      }
      else
      {
         Debug.Log("Wrong Quest Assigned Called from DungeonQuestManager.GenerateQuest");
         CurrentQuestType = eQuestType.None;
      }
      
      //Debug.Log("Current Quest is : " + CurrentQuestType);
   }

   public bool IsCurrentQuestCleared()
   {
      switch (CurrentQuestType)
      {
         case eQuestType.Hunt:
            _isCurrentQuestCleared = isGateKeeperDead;
            break;
         case eQuestType.Find:
            _isCurrentQuestCleared = foundPortal;
            break;
         case eQuestType.Eliminate:
            _isCurrentQuestCleared = GameManager.SMapManager.monsterPcg.ChildCount < TargetCount;
            break;
         case eQuestType.None:
         default:
            Debug.Log("Wrong Quest Type");
            break;
      }
      return _isCurrentQuestCleared;
   }
}
