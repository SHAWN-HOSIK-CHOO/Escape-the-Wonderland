using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitBox : MonoBehaviour
{
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

        transform.localScale += toSmall * GameManager.instance.hitBoxMoveSpeed * Time.unscaledDeltaTime;

        transform.position = player.transform.position;

        if (Input.GetButtonDown("Fire1")) {
            if (transform.localScale.x > 0.3f && transform.localScale.x < 0.6f) {
                Destroy(gameObject);
                GameManager.instance.isreinforced = true;
                spawner.StopMeditation();
                GameManager.instance.stylishPoint++;
                hit.SetCriticalText("Critical Hit");
            } else {
                Destroy(gameObject);
                spawner.StopMeditation();
                GameManager.instance.stylishPoint -= 0.5f;
                hit.SetCriticalText("Miss");
            }
        }


        if (gameObject.transform.localScale.x < 0) {
            Destroy(gameObject);
            spawner.StopMeditation();
            GameManager.instance.stylishPoint -= 0.5f;
            hit.SetCriticalText("Miss");
        }
    }
}
