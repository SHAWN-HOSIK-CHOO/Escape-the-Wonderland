using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject _topDoor;
    [SerializeField] private GameObject _bottomDoor;
    [SerializeField] private GameObject _leftDoor;
    [SerializeField] private GameObject _rightDoor;

    public Vector2Int RoomIndex { get; set; }

    public void OpenDoor(Vector2Int dir)
    {
        if (dir == Vector2Int.up)
        {
            _topDoor.SetActive(true);
        }

        if (dir == Vector2Int.down)
        {
            _bottomDoor.SetActive(true);
        }

        if (dir == Vector2Int.left)
        {
            _leftDoor.SetActive(true);
        }

        if (dir == Vector2Int.right)
        {
            _rightDoor.SetActive(true);
        }
    }
    
}
