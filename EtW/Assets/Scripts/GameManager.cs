using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float hitBoxMoveSpeed = 1f;
    public bool isreinforced = false;
    public int meditationCount = 0;
    public bool isReady = false;
    public float stylishPoint = 0f;
    public bool fever = false;
    private float feverTimer = 5f;
    private float realTimer = 1f;

    [SerializeField]
    private TextMeshProUGUI stylishRank;
    
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        
    }

    void Update()
    {
        if (stylishPoint < 0f) {
            stylishPoint = 0f;
        }

        if (realTimer <= 0) {
            stylishPoint -= 0.3f;
            realTimer = 1f;
        } else {
            realTimer -= Time.deltaTime;
        }

        JudgeRank();

        if (fever) {
            feverTimer -= Time.deltaTime;
            if (feverTimer <= 0) {
                stylishPoint = 2f;
                fever = false;
            }
        }
    }

    public void JudgeRank() {
        if (stylishPoint < 2f) {
            stylishRank.SetText("D");
        } else if (stylishPoint >= 2f && stylishPoint < 4f) {
            stylishRank.SetText("C");
        } else if (stylishPoint >= 4f && stylishPoint < 6f) {
            stylishRank.SetText("B");
        } else if (stylishPoint >= 6f && stylishPoint < 8f) {
            stylishRank.SetText("A");
        } else if (stylishPoint >= 8f && stylishPoint < 10f) {
            stylishRank.SetText("S");
        } else if (stylishPoint >= 10f) {
            stylishRank.SetText("SS");
            fever = true;
        }
    }
}