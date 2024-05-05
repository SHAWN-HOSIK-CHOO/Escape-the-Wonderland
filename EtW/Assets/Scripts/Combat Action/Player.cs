using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveX = new Vector3(horizontalInput, 0f, 0f);
        Vector3 moveY = new Vector3(0f, verticalInput, 0f);
        transform.position += moveX * moveSpeed * Time.deltaTime;
        transform.position += moveY * moveSpeed * Time.deltaTime;
    }

}
