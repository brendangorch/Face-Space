using UnityEngine;

public class EnemyType1 : Enemy
{
    public GameObject projectilePrefab;
    public float attackCooldown;

    private float m_attackCooldownTimer = 0f;

    private void Update()
    {
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
        // calculate direction towards center from enemy spawn point
        Vector3 direction = (Vector2.zero - m_spawnPoint).normalized;

        Vector3 spawnPosition = transform.position + direction * 2f;

        // spawn the projectile
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // set the enemy spawn point of the projectile
        ProjectileType1 projectileScript = projectile.GetComponent<ProjectileType1>();
        projectileScript.m_enemySpawnPoint = m_spawnPoint;
       
    }

    #endregion
}
