using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    protected EdgeCollider2D m_playerBounds;
    protected EdgeCollider2D m_exteriorBounds;
    protected GameObject m_player;
    protected CapsuleCollider2D m_playerCollider;

    protected void HandleCollisionIgnores()
    {
        m_playerBounds = GameObject.FindWithTag("PlayerBounds").GetComponent<EdgeCollider2D>();
        m_exteriorBounds = GameObject.FindWithTag("ExteriorBounds").GetComponent <EdgeCollider2D>();
        m_player = GameObject.FindWithTag("Player");
        m_playerCollider = m_player.GetComponent<CapsuleCollider2D>();


        int enemyLayer = LayerMask.NameToLayer("Enemies");
        int enemyProjectileLayer = LayerMask.NameToLayer("EnemyProjectiles");
        int playerProjectileLayer = LayerMask.NameToLayer("PlayerProjectiles");

        // Disable collisions between these layers
        Physics2D.IgnoreLayerCollision(enemyProjectileLayer, enemyLayer, true);
        Physics2D.IgnoreLayerCollision(enemyProjectileLayer, enemyProjectileLayer, true);
        Physics2D.IgnoreLayerCollision(enemyProjectileLayer, playerProjectileLayer, true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if collide with player bounds
        if (collision == m_playerBounds)
        {
            Destroy(gameObject);
        }
        // if collide with player
        else if (collision == m_playerCollider)
        {
            // destroy projectile
            Destroy(gameObject);

            // deal damage to player
            PlayerController pController = m_player.GetComponent<PlayerController>();
            pController.DealDamage(1);
            
        }
        else if (collision == m_exteriorBounds)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
