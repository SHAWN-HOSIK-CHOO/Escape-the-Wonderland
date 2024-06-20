using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private Rigidbody2D _playerRigidbody;
    private Vector2 _vector;
    private SpriteRenderer _playerSpriteRenderer;
    private BoxCollider2D _playerCollider;
    private Skill _skill;

    public bool isLeft = false;
    public float playerMaxHp;
    public float playerCurrentHp;
    public float playerMaxMp;
    public float playerCurrentMp;
    public float playerATK = 1f;
    public float playerDEF = 1f;
    public float playerAGI = 1f;
    public float reductionRate = 1f;
    private float _godModeTimer = 1f;
    private bool _isHit = false;
    private bool _start = true;

    public Light sspotLight;
    public Slider HP;
    public Slider MP;
    
    private void Awake() {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<BoxCollider2D>();
        _skill = GetComponent<Skill>();
    }

    void Start()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<BoxCollider2D>();
        _skill = GetComponent<Skill>();

        StartCoroutine("MPRegeneration");
        StartCoroutine("HPRegeneration");
    }

    IEnumerator MPRegeneration() {
        while (true) {
            playerCurrentMp += 2.5f;
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    IEnumerator HPRegeneration() {
        while (true) {
            playerCurrentHp += 0.3f;
            yield return new WaitForSecondsRealtime(1f);
        }
    }


    void Update()
    {
        if (_start) {
            playerATK = PlayerManager.instance.appliedATK;
            playerDEF = PlayerManager.instance.appliedDEF * reductionRate;
            playerAGI = PlayerManager.instance.appliedAGI;
            playerMaxHp = PlayerManager.instance.appliedHP;
            playerMaxMp = PlayerManager.instance.appliedMP;
            playerCurrentHp = playerMaxHp;
            playerCurrentMp = playerMaxMp;
            _start = false;
        }

        playerMaxHp = PlayerManager.instance.appliedHP;
        HP.value = playerCurrentHp / playerMaxHp;
        playerMaxMp = PlayerManager.instance.appliedMP;
        MP.value = playerCurrentMp / playerMaxMp;

        if (_skill.invincible) {
            playerDEF = 99f;
        } else {
            if (_skill.endure) {
                playerDEF = PlayerManager.instance.appliedDEF * reductionRate + 0.7f;
            } else {
                playerDEF = PlayerManager.instance.appliedDEF * reductionRate;
            }
        }



        if (playerCurrentHp <= 0) {
            GameManager.Instance.IsPlayerDead = true;
            _start = true;
        }


        if (playerCurrentMp > playerMaxMp) {
            playerCurrentMp = playerMaxMp;
        }

        if (playerCurrentHp > playerMaxHp) {
            playerCurrentHp = playerMaxHp;
        }

        
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("move")) {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
                animator.SetBool("idle", true);
            }
        }

        if (_isHit) {
            if (_godModeTimer <= 0f) {
                _playerCollider.enabled = true;
                _playerSpriteRenderer.color = new Color(1, 1, 1, 1);
                _godModeTimer = 1f;
                _isHit = false;
            } else {
                _godModeTimer -= Time.deltaTime;
            }
        }

        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        Vector3 velocity = Vector3.zero;
        velocity.x            = deltaX;
        velocity.y            = deltaY;
        _playerRigidbody.velocity = velocity * playerAGI;

        if (velocity.x != 0.0f)
        {
            bool       flipped = velocity.x < 0.0f;
            this.transform.rotation       = Quaternion.Euler(new Vector3(0.0f, ( flipped ? 180.0f : 0.0f ), 0.0f));
            sspotLight.transform.localPosition = new Vector3(sspotLight.transform.localPosition.x,
                                                        sspotLight.transform.localPosition.y, (flipped ? 20.0f : -20.0f) );
            sspotLight.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, ( flipped ? 180.0f : 0.0f ), 0.0f));
        }

        animator.SetFloat("Speed",Mathf.Abs(velocity.magnitude * playerAGI));
    }



    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Wolf") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            PlayerManager.instance.stylishPoint -= 0.3f;
            _isHit = true;
            _playerCollider.enabled = false;
            _playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        } else if (other.gameObject.tag == "Golem") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            PlayerManager.instance.stylishPoint -= 0.3f;
            _isHit = true;
            _playerCollider.enabled = false;
            _playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        } else if (other.gameObject.tag == "Eldlich") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            PlayerManager.instance.stylishPoint -= 0.3f;
            _isHit = true;
            _playerCollider.enabled = false;
            _playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        } else if (other.gameObject.tag == "Elk") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            PlayerManager.instance.stylishPoint -= 0.3f;
            _isHit = true;
            _playerCollider.enabled = false;
            _playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        } else if (other.gameObject.tag == "Woodgolem") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            PlayerManager.instance.stylishPoint -= 0.3f;
            _isHit = true;
            _playerCollider.enabled = false;
            _playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        } else if (other.gameObject.tag == "Graywolf") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            PlayerManager.instance.stylishPoint -= 0.3f;
            _isHit = true;
            _playerCollider.enabled = false;
            _playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        } else if (other.gameObject.tag == "Darknight") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            PlayerManager.instance.stylishPoint -= 0.3f;
            _isHit = true;
            _playerCollider.enabled = false;
            _playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
}