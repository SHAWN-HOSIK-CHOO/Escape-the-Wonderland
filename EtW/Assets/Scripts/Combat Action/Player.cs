using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public  float       moveSpeed = 3.0f;
    private Rigidbody2D _rigidbody2D;
    private Vector2     _moveAmount;

    // Start is called before the first frame update
    void Start()
    { 
    }
    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        float   horizontal = Input.GetAxis("Horizontal");
        float   vertical   = Input.GetAxis("Vertical");
        Vector2 position   = transform.position;
        position.x         = position.x + moveSpeed * horizontal * Time.deltaTime;
        position.y         = position.y + moveSpeed * vertical   * Time.deltaTime;
        transform.position = position;
    }
    
}
