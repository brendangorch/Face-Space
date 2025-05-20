using UnityEngine;

public class NinjaStar : MonoBehaviour
{
    // variables for projectile
    public float speed;
    private EdgeCollider2D m_playerBounds;
    private CapsuleCollider2D m_playerCollider;

    private Rigidbody2D m_rb;

    private void Start()
    {
        m_playerBounds = GameObject.FindWithTag("PlayerBounds").GetComponent<EdgeCollider2D>();
        m_playerCollider = GameObject.FindWithTag("Player").GetComponent<CapsuleCollider2D>();

        m_rb = GetComponent<Rigidbody2D>();

        // ignore collisions between player and player's projectiles
        Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), m_playerCollider);

        // get current mouse position
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = 0;

        // determine direction
        Vector2 direction = (currentMousePosition - transform.position).normalized;

        // set the velocity toward the mouse position
        m_rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the projectile collides with the player bounds
        if (collision == m_playerBounds)
        {
            // destroy the projectile
            GameObject.Destroy(gameObject);
        }
    }

}
