using UnityEngine;

public class EnemyType5 : Enemy
{
    // variables for movement
    private Vector2 m_targetPosition;

    // variables for attacking
    public float attackCooldown;
    private float m_attackCooldownTimer = 0f;

    // variables for projectiles
    public GameObject projectilePrefab; 
    public int numberOfProjectiles;
    public float projectileSpreadAngle;
    public float projectileRadius;

    private void Awake()
    {
        SetRandomTargetPosition();
    }

    private void Update()
    {
        // increment attack timer
        m_attackCooldownTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // move toward the target position
        Vector2 currentPosition = transform.position;
        Vector2 direction = (m_targetPosition - currentPosition).normalized;
        float distance = Vector2.Distance(currentPosition, m_targetPosition);

        // move only if the enemy hasn't reached the target
        if (distance > 0.1f)
        {
            m_rb.velocity = direction * moveSpeed;
        }
        else
        {
            // stop moving when the target is reached
            m_rb.velocity = Vector2.zero;

            // shoot at specified interval
            if (m_attackCooldownTimer >= attackCooldown)
            {
                Shoot();
                m_attackCooldownTimer = 0f;
            }
        }
    }

    private void SetRandomTargetPosition()
    {
        // get the camera's viewport bounds
        Camera camera = Camera.main;
        if (camera == null) return;

        // random point within the camera's view
        float randomX = Random.Range(camera.ScreenToWorldPoint(Vector3.zero).x + 2, camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, 0)).x - 2);
        float randomY = Random.Range(camera.ScreenToWorldPoint(Vector3.zero).y + 2, camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, 0)).y - 2);

        // set the target position
        m_targetPosition = new Vector2(randomX, randomY);
    }

    private void Shoot()
    {
        // get the projectile point child object
        Transform m_projectilePoint = transform.Find("ProjectilePoint");

        // instantiate multiple projectiles in a spread around the projectile point
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // calculate the angle for current projectile
            float angle = i * (projectileSpreadAngle / numberOfProjectiles); // evenly spread projectiles around the enemy

            // convert the angle to radians
            float radians = angle * Mathf.Deg2Rad;

            // calculate the spawn position based on the angle and radius
            float spawnX = Mathf.Cos(radians) * projectileRadius;
            float spawnY = Mathf.Sin(radians) * projectileRadius;
            // offset position by projectile point
            Vector2 spawnPosition = new Vector2(spawnX, spawnY) + (Vector2)m_projectilePoint.transform.position;

            // instantiate the projectile at the calculated spawn position
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

            // calculate the direction for the projectile to move
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

            ProjectileType5 projectileScript = projectile.GetComponent<ProjectileType5>();
            projectileScript.m_projectileMovePoint = spawnPosition + direction;


        }
    }
}
