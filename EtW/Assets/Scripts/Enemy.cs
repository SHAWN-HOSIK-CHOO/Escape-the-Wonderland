using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp = 10;

    public Transform target;

    public float rotateSpeed = 10f;
    public float moveSpeed = 5f;

    public float fiedlOfVision = 5f;
    public bool isHit = false;

    private float timer = 0f;
    void Start()
    {
        
    }


    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= fiedlOfVision) {
            MoveToTarget();
        }
        if (isHit) {
            if (timer >= 0.01f) {
                timer = 0f;
                isHit = false;
            } else {
                timer += Time.deltaTime;
            }
        }
    }

    private void MoveToTarget() {
        Vector2 direction = new Vector2(transform.position.x - target.position.x, transform.position.y - target.position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);

        transform.rotation = rotation;

        Vector3 dir = direction;
        transform.position += (-dir.normalized * moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon") {
            // if (!isHit) {
            //     isHit = true;
            //     Debug.Log("hit");
            //     AttackTest attack = other.gameObject.GetComponent<AttackTest>();
            //     Hp -= attack.dmg;
            // }
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            Hp -= weapon.dmg;
            if (Hp <= 0) {
                Destroy(gameObject);
            }

            Destroy(other.gameObject);
        }
    }
}