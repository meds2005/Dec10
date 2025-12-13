using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerBehavior player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;

    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Application.targetFrameRate = 60;
        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameOver.SetActive(false);
        playButton.SetActive(false);

        player.ResetPlayer();
        
        Time.timeScale = 1f;
        player.enabled = true; 

        Pipes [] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        for (int i=0; i<pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
   }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);
        player.ResetPlayer();

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
