using UnityEngine;

public class EnemyType2 : Enemy
{
    private void FixedUpdate()
    {
        Move();
    }

    // override the move function
    protected override void Move()
    {
        // simply move enemy towards the player
        transform.position = Vector2.MoveTowards(this.transform.position, m_player.transform.position, moveSpeed * Time.deltaTime);
    }
}
