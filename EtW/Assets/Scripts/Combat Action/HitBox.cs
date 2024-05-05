using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitBox : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Hit hit;

    private HitBoxSpawner spawner;

    private Player player;

    void Start()
    {
        hit = FindObjectOfType<Hit>();
        spawner = FindObjectOfType<HitBoxSpawner>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        Vector3 toSmall = new Vector3(-1f, -1f, 0f);

        transform.localScale += toSmall * moveSpeed * Time.unscaledDeltaTime;

        transform.position = player.transform.position;

        if (Input.GetButtonDown("Fire1")) {
            if (transform.localScale.x > 0.3f && transform.localScale.x < 0.6f) {
                Destroy(gameObject);
                GameManager.instance.isreinforced = true;
                spawner.StopMeditation();
                spawner.count = 0;
                hit.SetCriticalText("Critical Hit");
            } else {
                Destroy(gameObject);
                spawner.StopMeditation();
                spawner.count = 0;
                hit.SetCriticalText("Miss");
            }
        }


        if (gameObject.transform.localScale.x < 0) {
            Destroy(gameObject);
            hit.SetCriticalText("Miss");
        }
    }
}
