using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public GameObject hazard;
    public GameObject shipHazard;
    public int spawnCount;
    public float spawnWait;
    public float startSpawn;
    public float waveWait;
    private int spawnLevels;
    public int asteroidLevels;
    public int bossWait;

    public Text scoreText;
    public Text gameOverText;
    public Text restartText;
    public Text quitText;
    public int score;

    private bool gameOver;
    private bool restart;

    void Update()
    {

        if(restart == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
                Debug.Log("Oyun Kapandı");
            }
        }
    }
    IEnumerator SpawnValues()
    {
        while(true)
        {
            spawnLevels = asteroidLevels;
            yield return new WaitForSeconds(startSpawn);
            while (spawnLevels > 0)
            {

                for (int i = 0; i < spawnCount; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-3, 3), 0, 10);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
                yield return new WaitForSeconds(waveWait);
                if (gameOver == true)
                {
                    EndOptions();
                    break;
                }
                spawnLevels -= 1;
            }
            if (gameOver == true)
            {
                break;
            }
            yield return new WaitForSeconds(bossWait);
            shipHazard.SetActive(true);
            yield return new WaitUntil(() => shipHazard.activeSelf == false);
        }
        
    }

    private void EndOptions()
    {
        restartText.text = "Press 'R' for restart";
        quitText.text = "Press 'Q' for quit";
        restart = true;
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score : " + score;

    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }


    void Start()
    {
        gameOverText.text = "";
        restartText.text = "";
        quitText.text = "";
        gameOver = false;
        restart = false;
        StartCoroutine(SpawnValues());

    }

}
