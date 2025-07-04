/*
 * Author : GwangTae Jo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBoxSpawner : MonoBehaviour
{
    private GameObject hitbox;
    public GameObject prefabs;
    public Transform SpawnTransform;
    public Image meditationUI;
    public SpriteRenderer window;
    public Hit hit;
    private bool _meditationActivated = false;
    private float _meditationTimer = 10f;
    private float _meditationCoolTime;
    private bool _coolDown = false;

    void Start()
    {
    
    }

    public void StartMeditation() {
        // Time.timeScale = 0.1f;
        _meditationActivated = true;
        window.color = new Color(255, 255, 255, 0.3f);
        StartCoroutine("MeditationRoutine");
    }

    public void StopMeditation() {
        CoolDown();
        _meditationTimer = 10f;
        _meditationActivated = false;
        Time.timeScale = 1f;
        window.color = new Color(255, 255, 255, 0f);
        hit.SetCriticalText("");
        StopCoroutine("MeditationRoutine");
    }

    void Update()
    {
        if (PlayerManager.instance.stylishPoint >= 10) {
            StopMeditation();
        }
        // if (GameManager.instance.meditationCount == 2) {
        //     GameManager.instance.meditationCount = 0;
        //     StartMeditation();
        //     GameManager.instance.isReady = true;
        // }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (_meditationActivated && !_coolDown) {
                StopMeditation();
            } else if (!_meditationActivated && !_coolDown) {
                StartMeditation();
            }
        }

        if (_meditationActivated) {
            _meditationTimer -= Time.deltaTime;
            if (_meditationTimer <= 0) {
                StopMeditation();
            }
        }

        if (_coolDown) {
            _meditationCoolTime -= Time.deltaTime;
            meditationUI.fillAmount = _meditationCoolTime / 5f;
            //Debug.Log(_meditationCoolTime);
            if (_meditationCoolTime <= 0) {
                meditationUI.fillAmount = 1f;
                _coolDown = false;
            }
        }

    }

    IEnumerator MeditationRoutine() {
        while (true) {
            PlayerManager.instance.hitBoxMoveSpeed = 2f;
            SpawnHitBox();
            yield return new WaitForSecondsRealtime(1.5f);
        }
    }

    public void SpawnHitBox() {
        hitbox = Instantiate(prefabs, SpawnTransform.position, Quaternion.identity);   
    }

    public void CoolDown() {
        _meditationCoolTime = 5f;
        _coolDown = true;
    }
}