using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private string player_name;
    private bool m_GameOver = false;

    private int hits = 0;
    private int totalBricks = 0;
    private bool win = false;

    public DataHandler.SaveData newScore;
    string HighScorePlayer;
    int HighScore;

    int difficulty;



    // Start is called before the first frame update
    void Start()
    {
        difficulty = DataHandler.Instance.Difficulty;
        newScore = DataHandler.Instance.newplayer;
        if(DataHandler.Instance.scoreData.Count > 0)
		{
             HighScorePlayer = DataHandler.Instance.scoreData.First().PlayerName;
             HighScore = DataHandler.Instance.scoreData.First().score;
        }
        else
		{
            HighScore = 0;
            HighScorePlayer = newScore.PlayerName;

        }

        player_name = DataHandler.Instance.newplayer.PlayerName;
        ScoreText.text = $" SCORE : {m_Points}";
        HighScoreText.text = $"High Score : {HighScorePlayer} : {HighScore}";


        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        int totalBricks = 0;

        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                totalBricks++;
            }
        }
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
                Ball.AddForce(forceDir * 2.0f*difficulty, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                GameOver();
            }
        }
    }

    void AddPoint(int point)
    {
        
        m_Points += point;
        ScoreText.text = $"{player_name} : {m_Points}";
        hits++;

        if (hits == totalBricks)
        { 
            win = true;
            GameOver();
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
		if (win == true)
		{
            GameOverText.GetComponent<Text>().text = "WIN";
		}
        GameOverText.SetActive(true);

        newScore.score = m_Points;
        newScore.rank = 0;
        DataHandler.Instance.UpdateRanking(newScore);
        //SceneManager.LoadScene(2);
    }


}
