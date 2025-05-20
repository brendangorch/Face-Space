using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables for player
    [Header("Player References")]
    public float moveSpeed;
    public GameObject projectilePrefab;
    public float shootCooldown;
    public int health;
    private int m_maxHealth;
    private int m_ownProjLayer = 11;

    private Rigidbody2D m_rb;
    private Collider2D m_collider;
    private Vector2 m_moveDirection;
    private bool m_isFacingRight;
    private float m_shootTimer;

    // dash variables
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    private float m_dashCooldownTimer;
    private bool m_isDashing;
    private float m_dashTimeLeft;
    private int m_enemyLayer = 9;
    private int m_projectileLayer = 10;

    // for wipe mechanic
    private GameObject[] m_allEnemies;
    private bool m_wipeUsed;
    private GameObject[] m_allProjectiles;

    // for pause
    public bool isPaused;
    public GameObject pauseCanvas;

    // for loss
    public GameObject lossCanvas;
    private bool m_gameOver;

    // for score
    public GameObject scoreManager;


    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        // initialize timers
        m_shootTimer = shootCooldown;

        m_maxHealth = health;

        // ignore collisions
        Physics2D.IgnoreLayerCollision(gameObject.layer, m_ownProjLayer, true);

        isPaused = false;
    }

    private void Update()
    {
        ProcessMoveInputs();

        // increment timers
        m_shootTimer += Time.deltaTime;
        m_dashCooldownTimer += Time.deltaTime;

        // check for dash input and cooldown
        if (Input.GetKeyDown(KeyCode.Space) && m_dashCooldownTimer >= dashCooldown && !m_isDashing && !isPaused && !m_gameOver)
        {
            StartDash();
        }

        // check for wipe input and cooldown
        if (Input.GetMouseButtonDown(1) && !m_wipeUsed && !isPaused && !m_gameOver)
        {
            WipeScreen();
        }

        // check for pause
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !m_gameOver)
        {
            // pause
            pauseCanvas.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;

        } else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && !m_gameOver)
        {
            // unpause
            pauseCanvas.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    void FixedUpdate()
    {
        if (m_isDashing)
        {
            Dash();
        }
        else
        {
            Move();
            FlipPlayer();
        }

        // if player presses left click and cooldown for shooting is up
        if (Input.GetMouseButton(0) && m_shootTimer >= shootCooldown && !isPaused && !m_gameOver)
        {
            SpawnProjectile();
            m_shootTimer = 0;
        }
    }


    #region Movement

    // func to get player input for movement
    private void ProcessMoveInputs()
    {
        // get inputs
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // set the move direction
        m_moveDirection = new Vector2(x, y).normalized;
    }

    // func to move the player
    private void Move()
    {
        // set the velocity of the rb
        m_rb.velocity = new Vector2(m_moveDirection.x * moveSpeed, m_moveDirection.y * moveSpeed);
        
    }

    // func to flip player
    private void FlipPlayer()
    {
        if (!m_isFacingRight && m_rb.velocity.x > 0)
        {
            // flip to face right
            transform.Rotate(0f, 180f, 0);
            m_isFacingRight = true;

        } else if (m_isFacingRight && m_rb.velocity.x < 0)
        {
            // flip to face left
            transform.Rotate(0f, -180f, 0);
            m_isFacingRight = false;
        }

    }

    private void StartDash()
    {
        // set dash state
        m_isDashing = true;
        m_dashTimeLeft = dashDuration; // reset dash timer
        m_dashCooldownTimer = 0f; // reset cooldown timer

        Physics2D.IgnoreLayerCollision(gameObject.layer, m_enemyLayer, true);
        Physics2D.IgnoreLayerCollision(gameObject.layer, m_projectileLayer, true);
    }

    private void Dash()
    {
        // apply dash speed in the movement direction
        if (m_moveDirection != Vector2.zero)
        {
            m_rb.velocity = m_moveDirection * dashSpeed;
        }

        // countdown the dash time
        m_dashTimeLeft -= Time.deltaTime;

        // end dash when the time is up
        if (m_dashTimeLeft <= 0f)
        {
            m_isDashing = false; // end dash
            Physics2D.IgnoreLayerCollision(gameObject.layer, m_enemyLayer, false);
            Physics2D.IgnoreLayerCollision(gameObject.layer, m_projectileLayer, false);
        }
    }
    #endregion Movement

    #region Attacking

    // func to spawn a projectile 
    private void SpawnProjectile()
    {
        // get the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // get direction from the player to the mouse click position
        Vector3 direction = (mousePosition - transform.position).normalized;

        // calculate the spawn position
        Vector3 spawnPosition = transform.position + direction * 0.8f;

        // spawn the projectile
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
    }


    #endregion

    #region Health System

    // func to deal damage to player
    public void DealDamage(int damage)
    {
        health -= damage;

        scoreManager.GetComponent<ScoreManager>().HitByEnemy(damage);

        // check if player's new health is zero or less
        if (health <= 0)
        {
            // end game
            lossCanvas.SetActive(true);
            Time.timeScale = 0f;
            m_gameOver = true;
        }


    }

    // func to increase health
    public void IncreaseHealth()
    {
        if (health < m_maxHealth)
        {
            health += 1;
        }
    }

    #endregion

    #region Wipe Mechanic

    private void WipeScreen()
    {
        m_wipeUsed = true;
        m_allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        m_allProjectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");

        // destroy all enemies
        foreach (GameObject enemy in m_allEnemies)
        {
            Destroy(enemy);
        }

        // destroy all enemy projectiles
        foreach (GameObject proj in m_allProjectiles)
        {
            Destroy(proj);
        }
    }

    #endregion
}
