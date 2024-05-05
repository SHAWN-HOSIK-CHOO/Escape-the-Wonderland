/*
 * Source Code Originally by SunnyValleyStudio
 * Modified for usage by Ho-Sik Choo
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGen : SimpleRandomWalkMapGen
{
   [SerializeField]                  private int          _corridorCount  = 14;
   [SerializeField]                  private int          _corridorLength = 5;
   [SerializeField] [Range(0.1f, 1)] private float        _roomPercent    = 0.8f;
   
   protected override void RunProceduralGen()
   {
      CorridorFirstGen();
   }

   private void CorridorFirstGen()
   {
      HashSet<Vector2Int> floorPos     = new HashSet<Vector2Int>();
      HashSet<Vector2Int> potentialPos = new HashSet<Vector2Int>();

      CreateCorridors(floorPos, potentialPos);

      HashSet<Vector2Int> roomPos    = CreateRooms(potentialPos);
      List<Vector2Int>    deadEndPos = FindAllDeadEnds(floorPos);

      CreateRoomsAtDeadEnds(deadEndPos, roomPos);
      
      floorPos.UnionWith(roomPos);
      
      _tileMapVisualizer.PaintFloorTiles(floorPos);
      WallGen.CreateWalls(floorPos, _tileMapVisualizer);
   }

   private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEndPos, HashSet<Vector2Int> roomPos)
   {
      foreach (Vector2Int position in deadEndPos)
      {
         if (roomPos.Contains(position) == false)
         {
            HashSet<Vector2Int> room = RunRandomWalk(_randomWalkSO, position);
            roomPos.UnionWith(room);
         }
      }
   }

   private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPos)
   {
      List<Vector2Int> retDeadEndsList = new List<Vector2Int>();
      
      foreach (Vector2Int position in floorPos)
      {
         int neighborCount = 0;
         foreach (Vector2Int direction in Direction2D.dirList)
         {
            if (floorPos.Contains(position + direction))
               neighborCount++;
         }
         
         if(neighborCount == 1)
            retDeadEndsList.Add(position);
      }

      return retDeadEndsList;
   }

   private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialPos)
   {
      HashSet<Vector2Int> roomPos           = new HashSet<Vector2Int>();
      int                 roomToCreateCount = Mathf.RoundToInt(potentialPos.Count * _roomPercent);

      List<Vector2Int> roomsToCreate = potentialPos.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

      foreach (Vector2Int position in roomsToCreate)
      {
         var roomFloor = RunRandomWalk(_randomWalkSO, position);
         roomPos.UnionWith(roomFloor);
      }

      return roomPos;
   }

   private void CreateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialPos)
   {
      Vector2Int curPos = _startPos;
      potentialPos.Add(curPos);

      for (int i = 0; i < _corridorCount; i++)
      {
         List<Vector2Int> corridor = ProceduralRoomGen.RandomWalkCorridor(curPos,_corridorLength);
         curPos = corridor[corridor.Count - 1];
         potentialPos.Add(curPos);
         floorPos.UnionWith(corridor);
      }
   }
}
