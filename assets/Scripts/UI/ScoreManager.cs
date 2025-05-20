using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int m_playerScore;

    public TMP_Text scoreText;

    private void Start()
    {
        m_playerScore = 0;
        Debug.Log(MainMenuController.playerName);
    }

    #region Updating Score Functions

    // to increase score when player hits an enemy
    public void HitEnemy()
    {
        m_playerScore++;
        scoreText.text = "Score: " + m_playerScore;
    }

    // to increase score when player kills an enemy
    public void KilledEnemy(int enemyType)
    {
        if (enemyType == 1) 
        {
            m_playerScore += 5;
        }
        else if (enemyType == 2) 
        {
            m_playerScore += 5;
        }
        else if (enemyType == 3)
        {
            m_playerScore += 7;

        }
        else if (enemyType == 4) 
        {
            m_playerScore += 7;

        }
        else if (enemyType == 5)
        {
            m_playerScore += 10;

        }
        scoreText.text = "Score: " + m_playerScore;
    }

    // to increase score if player beats level
    public void BeatLevel()
    {
        m_playerScore += 100;
        scoreText.text = "Score: " + m_playerScore;
    }

    // to increase score after a wave is completed
    public void WaveCompleted(int waveNumber)
    {
        if (waveNumber == 1)
        {
            m_playerScore += 25;
        }
        else if (waveNumber == 2)
        {
            m_playerScore += 25;

        }
        else if (waveNumber == 3)
        {
            m_playerScore += 25;

        }
        else if (waveNumber == 4)
        {
            m_playerScore += 25;

        }

        scoreText.text = "Score: " + m_playerScore;
    }

    // to reduce score when hit by enemy
    public void HitByEnemy(int heartsLost)
    {
        if (heartsLost == 1) 
        {
            m_playerScore -= 3;
        }
        else if (heartsLost == 2)
        {
            m_playerScore -= 5;
        }
        else if (heartsLost == 4)
        {
            m_playerScore -= 8;
        }
        scoreText.text = "Score: " + m_playerScore;
    }


    #endregion
}
