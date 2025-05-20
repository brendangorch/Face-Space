using UnityEngine;

public class ProjectileType1 : EnemyProjectile
{
    // variables for projectile
    private Rigidbody2D m_rb;
    private Vector2 direction;
    
    // variables for enemy
    public Vector2 m_enemySpawnPoint;

    private void Start()
    {
        HandleCollisionIgnores();
        m_rb = GetComponent<Rigidbody2D>();

        MoveProjectile();
    }

    // func to move projectile
    private void MoveProjectile()
    {
        // set direction
        Vector2 direction = (Vector2.zero - m_enemySpawnPoint).normalized;
        // move it
        m_rb.velocity = direction * speed;
    }

}
