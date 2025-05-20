using UnityEngine;

public class HeartController : MonoBehaviour
{
    public float lifespan;

    void Start()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemies");
        int enemyProjLayer = LayerMask.NameToLayer("EnemyProjectiles");
        int playerProjLayer = LayerMask.NameToLayer("PlayerProjectiles");

        // turn off collisions between hearts with enemies, and all projectiles
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, true);
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyProjLayer, true);
        Physics2D.IgnoreLayerCollision(gameObject.layer, playerProjLayer, true);

        // destroy the heart once lifespan is up
        Destroy(gameObject, lifespan);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if heart collides with player
        if (collision.gameObject.CompareTag("Player"))
        {
            // give player health
            PlayerController pController = collision.gameObject.GetComponent<PlayerController>();
            pController.IncreaseHealth();

            // destroy the heart
            Destroy(gameObject);
        }
    }
}
