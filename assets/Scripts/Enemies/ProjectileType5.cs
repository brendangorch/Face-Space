using UnityEngine;

public class ProjectileType5 : EnemyProjectile
{
    // variables for projectile
    private Rigidbody2D m_rb;
    public Vector3 m_projectileMovePoint;

    private void Start()
    {
        HandleCollisionIgnores();
        m_rb = GetComponent<Rigidbody2D>();

        MoveProjectile();
    }

    private void MoveProjectile()
    {
        // set direction
        Vector2 direction = (m_projectileMovePoint - transform.position).normalized;
        // move it
        m_rb.velocity = direction * speed;
    }
}
