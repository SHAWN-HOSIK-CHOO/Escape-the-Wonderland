/*
 * Modified for usage. Code Originally by Rootbin https://www.youtube.com/watch?v=eK2SlZxNjiU&t=1193s
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject _roomPrefab;

    [SerializeField] private int _maxRooms = 15;

    [SerializeField] private int _minRooms = 10;

    private float _roomWidth  = 20.5f;

    private float _roomHeight = 18.5f;

    private int _gridX = 10;

    private int _gridY = 10;

    private List<GameObject>  _roomObjects = new List<GameObject>();
    private Queue<Vector2Int> _roomQueue   = new Queue<Vector2Int>();

    private int[,] _roomGrid;

    private int _roomCount;

    private bool _generationComplete = false;
    // Start is called before the first frame update
    private void Start()
    {
        _roomGrid  = new int[_gridX,_gridY];
        _roomQueue = new Queue<Vector2Int>();

        Vector2Int firstRoomIndex = new Vector2Int(_gridX / 2, _gridY / 2);
        StartRoomGenerationFromRoom(firstRoomIndex);
    }

    private void Update()
    {
        if (_roomQueue.Count > 0 && _roomCount < _maxRooms && !_generationComplete)
        {
            Vector2Int curIndex = _roomQueue.Dequeue();
            int        x        = curIndex.x;
            int        y        = curIndex.y;

            TryGenerateRoom(new Vector2Int(x + 1, y));
            TryGenerateRoom(new Vector2Int(x - 1, y));
            TryGenerateRoom(new Vector2Int(x,     y + 1));
            TryGenerateRoom(new Vector2Int(x,     y - 1));
        }
        else if (_roomCount < _minRooms)
        {
            ReGenerateRooms();
        }
        else if (!_generationComplete)
        {
            Debug.Log($"Room Generation Complete, {_roomCount} rooms generated");
            _generationComplete = true;
        }
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        _roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        _roomGrid[x, y] = 1;
        _roomCount++;
        var initialRoom = Instantiate(_roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name                           = $"Room-{_roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        _roomObjects.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x < 0 || y < 0 || x >= _gridX || y >= _gridY)
        {
            return false;
        }

        if (_roomGrid[x,y] != 0)
        {
            return false;
        }

        if (Random.value < 0.5f && roomIndex != Vector2Int.zero)
        {
            return false;
        }

        if (CalculateAdjRoomCount(roomIndex) > 1)
            return false;

        _roomQueue.Enqueue(roomIndex);
        _roomGrid[x, y] = 1;
        _roomCount++;
        var newRoom = Instantiate(_roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.name                           = $"Room-{_roomCount}";
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        _roomObjects.Add(newRoom);
        
        OpenDoors(newRoom,x,y);
        
        return true;
    }

    public void ReGenerateRooms()
    {
        _roomObjects.ForEach(Destroy);
        _roomObjects.Clear();
        _roomGrid  = new int[_gridX, _gridY];
        _roomCount = 0;
        _roomQueue.Clear();
        _generationComplete = false;

        Vector2Int startRoomIndex = new Vector2Int(_gridX / 2, _gridY / 2);
        StartRoomGenerationFromRoom(startRoomIndex);
    }

    private int CalculateAdjRoomCount(Vector2Int roomIndex)
    {
        int count = 0;
        int x     = roomIndex.x;
        int y     = roomIndex.y;

        if (x > 0 && _roomGrid[x - 1, y] != 0)
            count++;
        if (x < _gridX - 1 && _roomGrid[x + 1, y] != 0)
            count++;
        if (y > 0 && _roomGrid[x, y - 1] != 0)
            count++;
        if (y < _gridY - 1 && _roomGrid[x, y + 1] != 0)
            count++;

        return count;
    }

    private void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        Room leftRoomScript   = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript  = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript    = GetRoomScriptAt(new Vector2Int(x,     y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x,     y - 1));

        if (x > 0 && _roomGrid[x - 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }

        if (x < _gridX - 1 && _roomGrid[x + 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);
        }

        if (y > 0 && _roomGrid[x, y - 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }

        if (y < _gridY - 1 && _roomGrid[x, y + 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    private Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject retRoom = _roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (retRoom != null)
            return retRoom.GetComponent<Room>();
        return null;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int posX = gridIndex.x;
        int posY = gridIndex.y;

        return new Vector3(_roomWidth * (posX - _gridX / 2), _roomHeight * (posY - _gridY / 2));
    }
    
}
