using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxSpawner : MonoBehaviour
{
    public GameObject prefabs;

    private GameObject hitbox;

    public Transform SpawnTransform;

    public int count = 0;
    // Start is called before the first frame update
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
        if (Input.GetButtonDown("Fire2")) {

            if (count == 0) {
                StartMeditation();
                count = 1;
            } else if (count == 1) {
                StopMeditation();
                count = 0;
            }
        }
    }

    IEnumerator MeditationRoutine() {
        while (true) {
            SpawnHitBox();

            yield return new WaitForSecondsRealtime(3f);
        }
    }
    public void SpawnHitBox() {
        hitbox = Instantiate(prefabs, SpawnTransform.position, Quaternion.identity);
    }
    
}
