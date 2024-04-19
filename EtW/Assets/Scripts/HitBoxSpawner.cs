using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxSpawner : MonoBehaviour
{
    public GameObject prefabs;
    private GameObject hitbox;
    public Transform SpawnTransform;

    void Start()
    {
        
    }

    public void StartMeditation() {
        Time.timeScale = 0.1f;
        StartCoroutine("MeditationRoutine");
    }

    public void StopMeditation() {
        Time.timeScale = 1f;
        StopCoroutine("MeditationRoutine");
        Destroy(hitbox);
    }

    void Update()
    {
        if (GameManager.instance.meditationCount == 3) {
            GameManager.instance.meditationCount = 0;
            StartMeditation();
            GameManager.instance.isReady = true;
        }
    }

    IEnumerator MeditationRoutine() {
        while (true) {
            GameManager.instance.hitBoxMoveSpeed = 3f;
            SpawnHitBox();
            yield return new WaitForSecondsRealtime(3f);
        }
    }

    public void SpawnHitBox() {
        hitbox = Instantiate(prefabs, SpawnTransform.position, Quaternion.identity);   
    }
}