using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;
    public float hitBoxMoveSpeed = 1f;
    public bool isreinforced = false;
    public int meditationCount = 0;
    public bool isReady = false;
    public float stylishPoint = 0f;
    public float increaseRate = 2f;
    public bool fever = false;
    public int LVP = 0;
    public int tempLVP = 0;
    public int SP = 0;
    public int playerStatATK;
    public int playerStatDEF;
    public int playerStatAGI;
    public int playerStatHP;
    public int playerStatMP;
    public float appliedATK;
    public float appliedDEF;
    public float appliedAGI;
    public float appliedHP;
    public float appliedMP;
    public float ATKupperLimit = 1.5f;
    public float DEFupperLimit = 1.5f;
    public float AGIupperLimit = 10f;
    public float enemySpeed = 1f;
    public bool checkHit = false;
    public bool gameOver = false;
    public string[] skill_select = new string[]{"", "", ""};
    public bool judgementCutActivated = false;
    private float _feverTimer = 5f;
    private float _realTimer = 1f;
    private bool _statusPanelOpen = false;

    [SerializeField]
    private TextMeshProUGUI stylishRank;

    [SerializeField]
    private Canvas ui;

    [SerializeField]
    private TextMeshProUGUI levelPoint_text;

    [SerializeField]
    private TextMeshProUGUI skillPoint_text;

    [SerializeField]
    private TextMeshProUGUI ATK_point;

    [SerializeField]
    private TextMeshProUGUI DEF_point;

    [SerializeField]
    private TextMeshProUGUI HP_point;

    [SerializeField]
    private TextMeshProUGUI MP_point;

    [SerializeField]
    private TextMeshProUGUI AGI_point;
    
    [SerializeField]
    private Player player;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        playerStatATK = 0;
        playerStatDEF = 0;
        playerStatAGI = 0;
        playerStatHP = 0;
        playerStatMP = 0;

        appliedATK = 1f + playerStatATK * 0.2f;
        appliedDEF = 1f + playerStatDEF * 0.05f;
        appliedAGI = 7f + playerStatAGI * 0.1f;
        appliedHP = 40f + playerStatHP * 2f;
        appliedMP = 100f + playerStatMP * 10f;
    }

    void Update()
    {
        if (tempLVP == 20) {
            SP++;
            tempLVP = 0;
        }

        levelPoint_text.SetText("point : " + LVP.ToString());
        skillPoint_text.SetText("skill point : " + SP.ToString());
        ATK_point.SetText(playerStatATK.ToString());
        DEF_point.SetText(playerStatDEF.ToString());
        AGI_point.SetText(playerStatAGI.ToString());
        HP_point.SetText(playerStatHP.ToString());
        MP_point.SetText(playerStatMP.ToString());

        if (stylishPoint < 0f) {
            stylishPoint = 0f;
        }

        if (_realTimer <= 0) {
            stylishPoint -= 0.1f;
            _realTimer = 1f;
        } else {
            _realTimer -= Time.deltaTime;
        }

        JudgeRank();

        if (fever) {
            _feverTimer -= Time.deltaTime;
            if (_feverTimer <= 0) {
                if (judgementCutActivated) {
                    GameObject[] wolfArray = GameObject.FindGameObjectsWithTag("Wolf");
                    int countOfWolf = wolfArray.Length;
                    LVP += countOfWolf;
                    foreach (GameObject tGO in wolfArray) {
                        Destroy(tGO);
                    }

                    GameObject[] elkArray = GameObject.FindGameObjectsWithTag("Elk");
                    int countOfElk = elkArray.Length;
                    LVP += countOfElk;
                    foreach (GameObject tGO in elkArray) {
                        Destroy(tGO);
                    }
                    player.playerCurrentMp -= 150f;
                }
                
                _feverTimer = 5f;
                stylishPoint = 2f;
                fever = false;
            }
        }

        if (gameOver) {
            //Time.timeScale = 0f;
            ui.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0f;
            ui.transform.GetChild(1).gameObject.SetActive(true);

        }

        if (Input.GetKeyDown(KeyCode.K)) {
            if (!_statusPanelOpen) {
                ui.transform.GetChild(2).gameObject.SetActive(true);
                _statusPanelOpen = true;
            } else {
                ui.transform.GetChild(2).gameObject.SetActive(false);
                _statusPanelOpen = false;
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