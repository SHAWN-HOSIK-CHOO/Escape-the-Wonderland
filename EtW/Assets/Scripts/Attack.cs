using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Attack : MonoBehaviour
{
    public GameObject[] bullets;

    [SerializeField]
    private Transform shootTransform;

    private int magazinCapacity = 5;

    private float timer = 0f;

    private float reloadTime = 3f;

    private CameraAction cameraAction;

    void Start()
    {
        cameraAction = GameObject.FindWithTag("MainCamera").GetComponent<CameraAction>();
    }


    void Update()
    {
        // if (magazinCapacity > 0) {

        //     if (GameManager.instance.isreinforced) {
        //         if (Input.GetButtonDown("Fire1")) {
        //             Instantiate(bullets[1], shootTransform.position, Quaternion.identity);
        //             GameManager.instance.isreinforced = false;
        //             magazinCapacity--;
        //         }
        //     } else {
        //         if (Input.GetButtonDown("Fire1")) {
        //             Instantiate(bullets[0], shootTransform.position, Quaternion.identity);
        //             magazinCapacity--;
        //         }
        //     }

        // } else {
        //     timer += Time.deltaTime;
        //     if (timer > reloadTime) {
        //         Reload();
        //         timer = 0f;
        //     }
        // }
    }

    private void LateUpdate() {
        if (magazinCapacity > 0) {

            if (GameManager.instance.isreinforced) {
                if (Input.GetButtonDown("Fire1")) {
                    Instantiate(bullets[1], shootTransform.position, Quaternion.identity);
                    cameraAction.VibrateForTime(0.5f);
                    GameManager.instance.isreinforced = false;
                    //magazinCapacity--;
                }
            } else {
                if (Input.GetButtonDown("Fire1")) {
                    Instantiate(bullets[0], shootTransform.position, Quaternion.identity);
                    //magazinCapacity--;
                }
            }

        } else {
            timer += Time.deltaTime;
            if (timer > reloadTime) {
                Reload();
                timer = 0f;
            }
        }
    }

    void Reload() {
        magazinCapacity = 5;
    }
}
