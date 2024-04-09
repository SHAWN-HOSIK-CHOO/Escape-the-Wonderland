using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    public int ATK = 1;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 moveTo = new Vector3(1f, 0f, 0f);

        transform.position += moveTo * moveSpeed * Time.deltaTime;

        if (transform.position.x > 10) {
            Destroy(gameObject);
        }
    }
}