using UnityEngine;

public class Enemy : MonoBehaviour
{
    // variables for all enemy types
    [Header("Enemy General References")]
    public int health;
    public float moveSpeed;
    private EdgeCollider2D playerBounds;
    private EdgeCollider2D enemyBounds;

    protected Rigidbody2D m_rb;
    protected Vector2 m_spawnPoint;
    protected Collider2D m_collider;

    protected GameObject m_player;
    protected CapsuleCollider2D m_playerCollider;

    private GameObject scoreManager;
    public int enemyType;

    private void Start()
    {
        scoreManager = GameObject.FindWithTag("ScoreManager");
        m_spawnPoint = transform.position;

        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        m_player = GameObject.FindWithTag("Player");
        m_playerCollider = GetComponent<CapsuleCollider2D>();

        playerBounds = GameObject.FindWithTag("PlayerBounds").GetComponent<EdgeCollider2D>();
        enemyBounds = GameObject.FindWithTag("EnemyBounds").GetComponent<EdgeCollider2D>();
        int enemyLayer = LayerMask.NameToLayer("Enemies");

        // turn off collisions between enemies
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer, true);
        // turn off collisions between enemies and player bounds
        Physics2D.IgnoreCollision(m_collider, playerBounds, true);

        // call move function
        Move();
    }

    // func for linear movement
    protected virtual void Move()
    {
        // direction towards center from enemy spawn point
        Vector2 direction = (Vector2.zero - m_spawnPoint).normalized;

        m_rb.velocity = direction * moveSpeed;
    }

    // func to take damage and check for death
    private void TakeDamage(int dmg)
    {
        // take away health
        health -= dmg;

        // increase score
        scoreManager.GetComponent<ScoreManager>().HitEnemy();

        // if no more health left
        if (health == 0)
        {
            // insert animation and sound effect for death here:

            // add score to player score
            scoreManager.GetComponent<ScoreManager>().KilledEnemy(enemyType);

            // destroy the enemy
            GameObject.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if enemy collides with its bounds
        if (collision == enemyBounds)
        {
            // destroy the enemy
            GameObject.Destroy(gameObject);
        }
        // if enemy collides with player's projectile
        else if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            // take 1 health away
            TakeDamage(1);
            
            // destroy the projectile
            GameObject.Destroy(collision.gameObject);
        }
        // if collision is with player
        else if (collision.gameObject.CompareTag("Player"))
        {
            // if collision occurs with enemy 5 when its health is greater than 10, deal more damage
            if (health > 10)
            {
                // destroy the enemy
                GameObject.Destroy(gameObject);

                // deal damage to player
                PlayerController pController = m_player.GetComponent<PlayerController>();
                pController.DealDamage(4);
            }
            else
            {
                // destroy the enemy
                GameObject.Destroy(gameObject);

                // deal damage to player
                PlayerController pController = m_player.GetComponent<PlayerController>();
                pController.DealDamage(2);
            }
            
        }
    }
}
