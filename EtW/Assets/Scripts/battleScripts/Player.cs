using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float moveSpeed = 10f;
    public float playerMaxHp;
    public float playerCurrentHp;
    public float playerMaxMp;
    public float playerCurrentMp;
    public float playerATK = 1f;
    public float playerDEF = 1f;
    public float reductionRate = 1f;
    private float _godModeTimer = 1f;
    private bool _isHit = false;

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

        playerMaxHp = PlayerManager.instance.playerStatHP;
        playerCurrentHp = playerMaxHp;
        playerMaxMp = PlayerManager.instance.playerStatMP;
        playerCurrentMp = playerMaxMp;
        playerATK = PlayerManager.instance.playerStatATK;
        playerDEF = PlayerManager.instance.playerStatDEF * reductionRate;
        StartCoroutine("MPRegeneration");
    }

    IEnumerator MPRegeneration() {
        while (true) {
            playerCurrentMp += 1f;
            yield return new WaitForSecondsRealtime(3f);
        }
    }



    void Update()
    {
        playerMaxHp = PlayerManager.instance.playerStatHP;
        playerMaxMp = PlayerManager.instance.playerStatMP;

        if (playerCurrentHp <= 0) {
            PlayerManager.instance.gameOver   = true;
            GameManager.Instance.IsPlayerDead = true;
            this.gameObject.SetActive(false);
        }


        if (playerCurrentMp > playerMaxMp) {
            playerCurrentMp = playerMaxMp;
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
        _playerRigidbody.velocity = velocity * moveSpeed;

        if (velocity.x != 0.0f)
        {
            bool flipped = velocity.x < 0.0f;
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, ( flipped ? 180.0f : 0.0f ), 0.0f));
        }

        animator.SetFloat("Speed",Mathf.Abs(velocity.magnitude * moveSpeed));
    }



    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Wolf") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            PlayerManager.instance.stylishPoint -= 1f;
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
            PlayerManager.instance.stylishPoint -= 1f;
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
            PlayerManager.instance.stylishPoint -= 2f;
            _isHit = true;
            _playerCollider.enabled = false;
            _playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
}