using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HiScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private string m_name = "";
    private int m_Points;

    private string hiScoreName;
    private int hiScore;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        if(DataManager.instance != null)
        {
            m_name = DataManager.instance.playerName;
            hiScore = DataManager.instance.hiScore;
            hiScoreName = DataManager.instance.hiScoreName;
            // Update Score Text
            AddPoint(0);
        }
        UpdateHiScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        if(m_name == "")
        {
            ScoreText.text = $"Score : {m_Points}";
        }
        else
        {
            ScoreText.text = m_name + $"'s Score : {m_Points}";
        }
        if(m_Points > hiScore)
        {
            hiScore = m_Points;
            hiScoreName = m_name;
            UpdateHiScore();
        }
    }

    private void UpdateHiScore()
    {
        HiScoreText.text = "Best Score: " + hiScoreName + " : " + hiScore;
    }

    public void GameOver()
    {
        m_GameOver = true;
        if(DataManager.instance != null)
        {
            DataManager.instance.hiScore = hiScore;
            DataManager.instance.hiScoreName = hiScoreName;
            DataManager.instance.Save();
        }
        GameOverText.SetActive(true);
    }
}
