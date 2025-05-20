using UnityEngine;

public class EnemyType4 : Enemy
{
    // variables for movement
    public float playerMoveThreshold = 1.0f;
    public float stopDistance;
    public float spinSpeed;
    private Vector2 lastPlayerPosition;

    // variables for attacking
    public GameObject projectilePrefab;
    public float attackCooldown;
    private float m_attackCooldownTimer = 0f;
    private GameObject m_projectilePoint;
    private GameObject m_projectileMovePoint;

    private void Awake()
    {
        if (m_player != null)
        {
            lastPlayerPosition = m_player.transform.position;
        }
    }

    private void Update()
    {
        // rotate enemy (spin)
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);

        // increment attack timer
        m_attackCooldownTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float distanceMoved = Vector2.Distance(lastPlayerPosition, m_player.transform.position);
        if (distanceMoved > playerMoveThreshold || distanceMoved == 0)
        {
            // update the player's last position
            lastPlayerPosition = m_player.transform.position;

            // move
            Move();
        }

        // shoot at specified interval
        if (m_attackCooldownTimer >= attackCooldown)
        {
            Shoot();
            m_attackCooldownTimer = 0f;
        }

    }

    // override the move func 
    protected override void Move()
    {

        // direction to the player
        Vector2 direction = (m_player.transform.position - transform.position).normalized;

        // distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, m_player.transform.position);

        // move only if the distance is greater than the stopDistance
        if (distanceToPlayer > stopDistance)
        {
            m_rb.velocity = direction * moveSpeed;
        }
        else
        {
            // stop moving when within the desired distance
            m_rb.velocity = Vector2.zero;
        }
    }

    #region Attacking

    private void Shoot()
    {
        // get the projectile point child object
        m_projectilePoint = transform.Find("ProjectilePoint").gameObject;
        // get the point where the projectile should move to
        m_projectileMovePoint = transform.Find("ProjectileMovePoint").gameObject;

        // instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, m_projectilePoint.transform.position, Quaternion.identity);

        // set the move point of the projectile
        ProjectileType3 projectileScript = projectile.GetComponent<ProjectileType3>();
        projectileScript.m_projectileMovePoint = m_projectileMovePoint.transform.position;
    }

    #endregion

}
