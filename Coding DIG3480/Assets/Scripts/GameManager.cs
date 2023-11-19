using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPreFab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPreFab;
    public GameObject coin;
    public GameObject[] thingsThatSpawn;
    public GameObject gameOverSet;
    public int score;
    public int lives;
    public int cloudsMove;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI powerupText;
    private int numOfLives;
    private bool isGameOver;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPreFab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemyOne", 1.0f, 2.0f);
        InvokeRepeating("SpawnSomething", 2.0f, Random.Range(2f, 3f));
        cloudsMove = 1;
        score = 0;
        lives = 3;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: 3";
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.R) && isGameOver)
       {
        SceneManager.LoadScene("Game");
       }
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-13, 13), 7.5f, 0), Quaternion.Euler(0, 0, 180));
    }

    void CreateSky()
    {
        for (int i = 0; i < 50; i++)
        {
            Instantiate(cloudPreFab, new Vector3(Random.Range(-13f, 13f), Random.Range(-7.5f, 7.5f), 0), Quaternion.identity);
        }
    }


    void SpawnSomething()
    {
        int tempInt;
        tempInt = Random.Range(0, 3);
        Instantiate(thingsThatSpawn[tempInt], new Vector3(Random.Range(-13, 13), Random.Range(-3.47f, 0), 0), Quaternion.identity);
    }

    
     public void GameOver()
        {
            CancelInvoke();
            cloudsMove = 0;
            GetComponent<AudioSource>().Stop();
            gameOverSet.SetActive(true);
            isGameOver = true;
        }

        public void EarnScore(int scoreToAdd)
        {
            score = score + scoreToAdd;
            scoreText.text = "Score" + score;
        }

    public void LivesChange(int currentLife)
    {
        livesText.text = "Lives:  " + currentLife;
    }    

    public void PowerupChange(string whatPowerup)
    {
        powerupText.text = whatPowerup;
    }


    








}
