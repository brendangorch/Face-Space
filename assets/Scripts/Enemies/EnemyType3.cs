using UnityEngine;

public class EnemyType3 : Enemy
{
    public float spinSpeed;
    public GameObject projectilePrefab;
    public float attackCooldown;

    private float m_attackCooldownTimer = 0f;

    private GameObject m_projectilePoint;
    private GameObject m_projectileMovePoint;

    private void Update()
    {
        // rotate enemy (spin)
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);

        // increment attack timer
        m_attackCooldownTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // shoot at specified interval
        if (m_attackCooldownTimer >= attackCooldown)
        {
            Shoot();
            m_attackCooldownTimer = 0f;
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
