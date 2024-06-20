/*
 * Author : Hosik Choo
 */
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonPortal : MonoBehaviour
{
  public  ePortalType portalType;
  private bool        _isPortalCalled;
  public enum ePortalType
  {
    BasePortal,
    DungeonPortal
  }

  private void Start()
  {
    _isPortalCalled = false;
  }

  private void Update()
  {
    if (_isPortalCalled)
    {
      if (Input.GetKeyDown(KeyCode.Alpha1))
      {
        RenderSettings.skybox = GameManager.Instance.skyboxArray[0];
        Debug.Log("Alpha1 called");
        GameManager.Instance.dungeonSelectText.SetActive(false);
        GameManager.SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Dungeon0);
      }
      else if (Input.GetKeyDown(KeyCode.Alpha2))
      {
        RenderSettings.skybox = GameManager.Instance.skyboxArray[1];
        GameManager.Instance.dungeonSelectText.SetActive(false);
        GameManager.SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Dungeon1);
      }
      else if (Input.GetKeyDown(KeyCode.Alpha3))
      {
        RenderSettings.skybox = GameManager.Instance.skyboxArray[2];
        GameManager.Instance.dungeonSelectText.SetActive(false);
        GameManager.SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Dungeon2);
      }
      else if (Input.GetKeyDown(KeyCode.Alpha4))
      {
        RenderSettings.skybox = GameManager.Instance.skyboxArray[3];
        GameManager.Instance.dungeonSelectText.SetActive(false);
        GameManager.SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Dungeon3);
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      if (portalType == ePortalType.DungeonPortal)
      {
        Debug.Log("Dungeon Portal Entered");
        DungeonQuestManager.Instance.foundPortal = true;
      }
      else if (portalType == ePortalType.BasePortal)
      {
        Debug.Log("Base Portal Entered");
        GameManager.Instance.WhatDoesTheRabbitSay("그래서 어디로 갈건데? 1번 던전, 2번 던전, 3번 던전, 4번 던전 중에서 알아서 골라");
        _isPortalCalled = true;
      }
    }
  }
  
}
