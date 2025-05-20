using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // variables for prefabs
    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject enemyType3;   
    public GameObject enemyType4;
    public GameObject enemyType5;
    public GameObject heart;

    private GameObject[] enemyTypes;

    public Transform enemyParent;

    // variables for edge colliders
    public EdgeCollider2D enemyBounds;
    public EdgeCollider2D playerBounds;

    // variables for spawn bounds
    private float m_leftOutsideBound;
    private float m_leftInsideBound;
    private float m_rightOutsideBound;
    private float m_rightInsideBound;
    private float m_topOutsideBound;
    private float m_topInsideBound;
    private float m_bottomOutsideBound;
    private float m_bottomInsideBound;

    // timer and wave number variables
    private float m_timer;
    private int m_waveNumber;
    private float m_heartTimer;
    private float m_heartCooldown = 12f;

    private bool m_waveOneDone = false;
    private bool m_waveTwoDone = false;
    private bool m_waveThreeDone = false;
    private bool m_waveFourDone = false;
    private bool m_waveFiveDone = false;

    private bool m_waveFiveEnemiesAllSpawned;

    public GameObject scoreManager;

    private void Start()
    {
        m_waveNumber = 1;

        // get edge collider points
        List<Vector2> enemyColliderPoints = new List<Vector2>();
        enemyBounds.GetPoints(enemyColliderPoints);
        List<Vector2> playerColliderPoints = new List<Vector2>();
        playerBounds.GetPoints(playerColliderPoints);

        // enemy type list
        enemyTypes = new GameObject[] { enemyType1, enemyType2, enemyType3, enemyType4, enemyType5 };

        // set the bounds for spawning
        m_leftOutsideBound = enemyColliderPoints[0].x;
        m_leftInsideBound = playerColliderPoints[0].x;

        m_rightOutsideBound = enemyColliderPoints[2].x;
        m_rightInsideBound = playerColliderPoints[2].x;

        m_topOutsideBound = enemyColliderPoints[1].y;
        m_topInsideBound = playerColliderPoints[1].y;

        m_bottomOutsideBound = enemyColliderPoints[0].y;
        m_bottomInsideBound = playerColliderPoints[0].y;
    }

    private void Update()
    {
        // increment timers
        m_timer += Time.deltaTime;
        m_heartTimer += Time.deltaTime;

        // end check - all enemies finished spawning and all enemies cleared from screen
        if (m_waveFiveEnemiesAllSpawned && enemyParent.childCount == 0)
        {
            // open end scene:

            // save score here:
        }
        

        // heart spawning
        if (m_heartTimer > m_heartCooldown)
        {
            // get the camera's viewport bounds
            Camera camera = Camera.main;

            // random point within the camera's view
            float randomX = Random.Range(camera.ScreenToWorldPoint(Vector3.zero).x + 2, camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, 0)).x - 2);
            float randomY = Random.Range(camera.ScreenToWorldPoint(Vector3.zero).y + 2, camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, 0)).y - 2);

            // set the target position
            Vector2 m_heartSpawnPoint = new Vector2(randomX, randomY);

            // spawn a heart randomly
            Instantiate(heart, m_heartSpawnPoint, Quaternion.identity);

            m_heartTimer = 0;
        }

        if (m_timer > 3 && !m_waveOneDone)
        {
            SpawnWave();
            m_waveOneDone = true;
            Debug.Log("wave 1 started");
        }

        else if (m_timer > 24 && !m_waveTwoDone)
        {
            SpawnWave();
            m_waveTwoDone = true;
            Debug.Log("wave 2 started");
        }

        else if (m_timer > 40 && !m_waveThreeDone)
        {
            SpawnWave();
            m_waveThreeDone = true;
            Debug.Log("wave 3 started");

        }

        else if (m_timer > 58 && !m_waveFourDone)
        {
            SpawnWave();
            m_waveFourDone = true;
            Debug.Log("wave 4 started");

        }

        else if (m_timer > 78 && !m_waveFiveDone)
        {
            SpawnWave();
            m_waveFiveDone = true;
            Debug.Log("wave 5 started");

        }
    }

    private void SpawnWave()
    {
        // wave 1
        if (m_waveNumber == 1)
        {
            StartCoroutine(Wave1Handler());
        }

        // wave 2
        else if (m_waveNumber == 2)
        {
            StartCoroutine(Wave2Handler());
        }

        // wave 3
        else if (m_waveNumber == 3) 
        {
            StartCoroutine(Wave3Handler());
        }

        // wave 4
        else if (m_waveNumber == 4)
        {
            StartCoroutine(Wave4Handler());
        }

        // wave 5 
        else if (m_waveNumber == 5)
        {
            StartCoroutine(Wave5Handler());
        }

        m_waveNumber += 1;
    }

    #region Waves 1-5 functions
    // helper functions for waves

    private IEnumerator Wave1Handler()
    {
        // spawn enemy along top
        Instantiate(enemyType1, GetRandomPosition(1), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(5f);

        // spawn enemy along left
        Instantiate(enemyType1, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right
        Instantiate(enemyType1, GetRandomPosition(2), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(6f);

        // spawn enemy along left
        Instantiate(enemyType1, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right
        Instantiate(enemyType1, GetRandomPosition(2), Quaternion.identity, enemyParent);

        // spawn enemy along top
        Instantiate(enemyType1, GetRandomPosition(1), Quaternion.identity, enemyParent);;

        // spawn enemy along bottom
        Instantiate(enemyType1, GetRandomPosition(3), Quaternion.identity, enemyParent);

        scoreManager.GetComponent<ScoreManager>().WaveCompleted(1);
    }

    private IEnumerator Wave2Handler()
    {
        // spawn enemy along bottom
        Instantiate(enemyType2, GetRandomPosition(3), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(4f); 

        // spawn enemy along bottom
        Instantiate(enemyType2, GetRandomPosition(3), Quaternion.identity, enemyParent);

        // spawn enemy along top
        Instantiate(enemyType1, GetRandomPosition(1), Quaternion.identity, enemyParent);

        // spawn enemy along left
        Instantiate(enemyType1, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right
        Instantiate(enemyType1, GetRandomPosition(2), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(5f);

        // spawn enemy along left
        Instantiate(enemyType2, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right 
        Instantiate(enemyType2, GetRandomPosition(2), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(1f);

        // spawn enemy along bottom
        Instantiate(enemyType1, GetRandomPosition(3), Quaternion.identity, enemyParent);

        // spawn enemy along top
        Instantiate(enemyType1, GetRandomPosition(1), Quaternion.identity, enemyParent);

        // spawn enemy along left
        Instantiate(enemyType2, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right 
        Instantiate(enemyType2, GetRandomPosition(2), Quaternion.identity, enemyParent);

        scoreManager.GetComponent<ScoreManager>().WaveCompleted(2);
    }

    private IEnumerator Wave3Handler()
    {
        // spawn enemy along bottom
        Instantiate(enemyType3, GetRandomPosition(3), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(5f);

        // spawn enemy along bottom
        Instantiate(enemyType3, GetRandomPosition(3), Quaternion.identity, enemyParent);

        // spawn enemy along top
        Instantiate(enemyType3, GetRandomPosition(1), Quaternion.identity, enemyParent);

        // spawn enemy along left
        Instantiate(enemyType1, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right
        Instantiate(enemyType1, GetRandomPosition(2), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(3);

        // spawn enemy along bottom
        Instantiate(enemyType2, GetRandomPosition(3), Quaternion.identity, enemyParent);

        // spawn enemy along top
        Instantiate(enemyType2, GetRandomPosition(1), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(3);

        // spawn enemy along left
        Instantiate(enemyType1, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right
        Instantiate(enemyType1, GetRandomPosition(2), Quaternion.identity, enemyParent);

        // spawn enemy along top
        Instantiate(enemyType1, GetRandomPosition(1), Quaternion.identity, enemyParent);

        // spawn enemy along bottom
        Instantiate(enemyType1, GetRandomPosition(3), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(3);

        // spawn enemy along bottom
        Instantiate(enemyType3, GetRandomPosition(3), Quaternion.identity, enemyParent);

        // spawn enemy along top
        Instantiate(enemyType3, GetRandomPosition(1), Quaternion.identity, enemyParent);

        scoreManager.GetComponent<ScoreManager>().WaveCompleted(3);
    }

    private IEnumerator Wave4Handler()
    {
        // spawn enemy along bottom
        Instantiate(enemyType4, GetRandomPosition(3), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(4f);

        // spawn enemy along left
        Instantiate(enemyType4, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right
        Instantiate(enemyType4, GetRandomPosition(2), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(2f);

        // spawn enemy along bottom
        Instantiate(enemyType3, GetRandomPosition(3), Quaternion.identity, enemyParent);

        // spawn enemy along top
        Instantiate(enemyType3, GetRandomPosition(1), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(1f);

        // spawn enemy along bottom
        Instantiate(enemyType2, GetRandomPosition(3), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(1f);

        // spawn enemy along top
        Instantiate(enemyType2, GetRandomPosition(1), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(3f);

        // spawn enemy along left
        Instantiate(enemyType1, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right
        Instantiate(enemyType1, GetRandomPosition(2), Quaternion.identity, enemyParent);

        // spawn enemy along top
        Instantiate(enemyType1, GetRandomPosition(1), Quaternion.identity, enemyParent);

        // spawn enemy along bottom
        Instantiate(enemyType4, GetRandomPosition(3), Quaternion.identity, enemyParent);

        scoreManager.GetComponent<ScoreManager>().WaveCompleted(4);
    }

    private IEnumerator Wave5Handler()
    {
        // spawn enemy along bottom
        Instantiate(enemyType5, GetRandomPosition(3), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(8f);

        // spawn enemy along bottom
        Instantiate(enemyType5, GetRandomPosition(3), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(6f);

        // spawn enemy along left
        Instantiate(enemyType2, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right
        Instantiate(enemyType2, GetRandomPosition(2), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(5f);

        // spawn enemy along top
        Instantiate(enemyType4, GetRandomPosition(1), Quaternion.identity, enemyParent);

        // spawn enemy along bottom
        Instantiate(enemyType4, GetRandomPosition(3), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(3f);

        // spawn enemy along left
        Instantiate(enemyType3, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right
        Instantiate(enemyType3, GetRandomPosition(2), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(2f);

        // spawn enemy along bottom
        Instantiate(enemyType5, GetRandomPosition(3), Quaternion.identity, enemyParent);

        // spawn enemy along left
        Instantiate(enemyType1, GetRandomPosition(4), Quaternion.identity, enemyParent);

        // spawn enemy along right
        Instantiate(enemyType1, GetRandomPosition(2), Quaternion.identity, enemyParent);

        // spawn enemy along top
        Instantiate(enemyType1, GetRandomPosition(1), Quaternion.identity, enemyParent);

        // spawn enemy along bottom
        Instantiate(enemyType1, GetRandomPosition(3), Quaternion.identity, enemyParent);

        yield return new WaitForSeconds(1f);

        scoreManager.GetComponent<ScoreManager>().BeatLevel();

        m_waveFiveEnemiesAllSpawned = true;

    }

    #endregion

    #region Random Functions

    private Vector2 GetRandomPosition(int spot)
    {
        float randomX = 0;
        float randomY = 0;

        // 1 = top
        if (spot == 1)
        {
            // random position along top bounds
            randomX = Random.Range(m_leftOutsideBound + 1, m_rightOutsideBound - 1);
            randomY = Random.Range(m_topInsideBound + 1, m_topOutsideBound - 1);
            return new Vector2(randomX, randomY);
        }
        // 2 = right
        else if (spot == 2)
        {
            // random position along right bounds 
            randomX = Random.Range(m_rightInsideBound + 1, m_rightOutsideBound - 1);
            randomY = Random.Range(m_bottomOutsideBound + 1, m_topOutsideBound - 1);
            return new Vector2(randomX, randomY);
        }
        // 3 = bottom
        else if (spot == 3) 
        {
            // random position along bottom bounds 
            randomX = Random.Range(m_leftOutsideBound + 1, m_rightOutsideBound - 1);
            randomY = Random.Range(m_bottomOutsideBound + 1, m_bottomInsideBound - 1);
            return new Vector2(randomX, randomY);
        }
        // 4 = left
        else if (spot == 4)
        {
            // random position along left bounds 
            randomX = Random.Range(m_leftOutsideBound + 1, m_leftInsideBound - 1);
            randomY = Random.Range(m_bottomOutsideBound + 1, m_topOutsideBound - 1);
            return new Vector2(randomX, randomY);
        }
        else
        {
            return Vector2.zero;
        }
    }

    private GameObject GetRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemyTypes.Length);

        // return random enemy
        return enemyTypes[randomIndex];
    }

    #endregion
}
