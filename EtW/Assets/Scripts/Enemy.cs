using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp = 10;
    public void StartEnemyRoutine() {
        StartCoroutine("EnemyRoutine");
    }

    void Start()
    {
        //StartEnemyRoutine();
    }


    void Update()
    {
        
    }

    IEnumerator EnemyRoutine() {
        while (true) {




            yield return new WaitForSeconds(3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            Hp -= weapon.ATK;
            if (Hp <= 0) {
                Destroy(gameObject);
            }

            Destroy(other.gameObject);
        }
    }
}