using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private GameObject  _manager1;
    [SerializeField] private GameObject  _manager2;
    
    public bool SwapRooms { get; set; }

    private void Awake()
    {
        _manager1.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        SwapRooms       = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Just for debugging purpose
        if (Input.GetKey(KeyCode.Q))
            SwapRooms = true;
        
        if (SwapRooms && _manager1.activeSelf)
        {
            SwapAndRegenerateRooms(_manager1);
            _manager2.SetActive(true);
        }
        else if (SwapRooms && _manager2.activeSelf)
        {
            SwapAndRegenerateRooms(_manager2);
            _manager1.SetActive(true);
        }
    }

    private void SwapAndRegenerateRooms(GameObject manager)
    {
        SwapRooms = false;
        manager.SetActive(false);
        var managerScript = manager.GetComponent<RoomManager>();
        managerScript.ReGenerateRooms();
    }
}
