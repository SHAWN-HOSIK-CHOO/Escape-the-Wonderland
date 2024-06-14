/*
 *Author : Hosik Choo
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonQuestManager : MonoBehaviour
{
   private static DungeonQuestManager _instance;
   
   [SerializeField] private bool       _isCurrentQuestCleared;

   [Header("For Eliminate Quest")] public int  TargetCount;
   [Header("For Hunt Quest")]      public bool isGateKeeperDead;
   [Header("For Find Quest")]      public bool foundPortal;
   
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
   }

   public void GenerateQuest()
   {
      int index = Random.Range(0, 3);
      
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
      
      Debug.Log("Current Quest is : " + CurrentQuestType);
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
            _isCurrentQuestCleared = GameManager.SMapManager.monsterPcg.ChildCount <= TargetCount;
            break;
         case eQuestType.None:
         default:
            Debug.Log("Wrong Quest Type");
            break;
      }
      return _isCurrentQuestCleared;
   }
}
