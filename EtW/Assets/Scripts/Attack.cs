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

    private CameraAction cameraAction;
    public HitBoxSpawner spawner;

    public int megazinCapacity = 10;
    public float reloadTime = 1f;
    private float reloadTimer = 0f;
    private float shootTimer = 0f;
    private float shootInterval = 0.2f;

    void Start()
    {
        cameraAction = GameObject.FindWithTag("MainCamera").GetComponent<CameraAction>();
    }


    void Update()
    {

    }

    private void LateUpdate() {
        if (megazinCapacity > 0) {
            if (shootTimer <= 0f) {
                if (GameManager.instance.fever) {
                    if (Input.GetButtonDown("Fire1")) {
                        Instantiate(bullets[1], shootTransform.position, Quaternion.identity);
                        cameraAction.VibrateForTime(0.5f);
                        shootTimer = 0.1f;
                    }
                } else {
                    if (!GameManager.instance.isReady) {
                        if (Input.GetButtonDown("Fire1")) {
                            Instantiate(bullets[0], shootTransform.position, Quaternion.identity);
                            GameManager.instance.meditationCount += 1;
                            // megazinCapacity--;
                            shootTimer = shootInterval;
                        }
                    } else if (GameManager.instance.isReady) {
                        if (Input.GetButtonUp("Fire1")) {
                            if (GameManager.instance.isreinforced) {
                                Instantiate(bullets[1], shootTransform.position, Quaternion.identity);
                                cameraAction.VibrateForTime(0.5f);
                                GameManager.instance.meditationCount = 0;
                                GameManager.instance.isReady = false;
                                GameManager.instance.isreinforced = false;
                                // megazinCapacity--;
                                shootTimer = shootInterval;
                            } else {
                                Instantiate(bullets[0], shootTransform.position, Quaternion.identity);
                                GameManager.instance.meditationCount = 0;
                                GameManager.instance.isReady = false;
                                // megazinCapacity--;
                                shootTimer = shootInterval;
                            }
                        }
                    }
                }
            } else {
                shootTimer -= Time.unscaledDeltaTime;
            }
        } else {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > reloadTime) {
                Reload();
                reloadTimer = 0f;
            }
        }
    }

    private void Reload() {
        megazinCapacity = 4;
    }
}
