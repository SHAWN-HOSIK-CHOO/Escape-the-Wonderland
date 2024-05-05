using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterPcg : MonoBehaviour
{
   [SerializeField] public RoomDungeonGen   roomDungeonGen;
   [SerializeField] public List<GameObject> placeableMonsters;
   [SerializeField] public int              monsterPerRoom;
   
   public Dictionary<Vector2Int, HashSet<Vector2Int>> roomAndFloorsToPlaceMonsters;
   
   public void ProceduralGenMonsters()
   {
      roomAndFloorsToPlaceMonsters = roomDungeonGen.placeablePositions;
      //Debug_Dictionary();
      PlaceMonsters();
   }

   private void PlaceMonsters()
   {
      foreach (var room in roomAndFloorsToPlaceMonsters)
      {
         List<Vector2Int> positionList     = room.Value.ToList();
         List<GameObject> selectedMonsters = SelectMonsters(monsterPerRoom);

         foreach (var monster in selectedMonsters)
         {
            Vector2Int position = positionList[Random.Range(0, positionList.Count - 1)];
            //TODO: 주위 8방향 픽셀 위치도 제거할 것 (몬스터 픽셀 크기 고려)
            positionList.Remove(position);
            // Placing function
            GameObject monsterPrefab = Instantiate(monster);
            monsterPrefab.transform.position = new Vector3(position.x, position.y, 1);
         }
      }
   }

   private List<GameObject> SelectMonsters(int monsterCount)
   {
      List<GameObject> retSelectedMonsters = new List<GameObject>();

      for (int i = 0; i < monsterCount; i++)
      {
         GameObject monster = placeableMonsters[Random.Range(0, placeableMonsters.Count - 1)];
         retSelectedMonsters.Add(monster);
      }
      
      return retSelectedMonsters;
   }

   private void Debug_Dictionary()
   {
      foreach (var position in roomAndFloorsToPlaceMonsters)
      {
         int count = 0;
         foreach (var pos in position.Value)
         {
            count++;
         }
         Debug.Log(position.Key+" + Room's Floors : Room Has "+count+" floors");
      }
   }
   
   
}
