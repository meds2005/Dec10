using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip scoreSound;  
    public AudioClip playClickSound;  

    public PlayerBehavior player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject startScreen;
    public GameObject gameName;
    public GameObject instructions;
    public GameObject credits;

    private int score;
    private bool hasGameStarted = false;

    public Transform scoreTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Application.targetFrameRate = 60;

        Time.timeScale = 0f;
        player.enabled = false;

        gameName.SetActive(true);
        startScreen.SetActive(true);   
        gameOver.SetActive(false);
        instructions.SetActive(true);
        playButton.SetActive(true);    
        credits.SetActive(true);
    }

    public void Play()
    {
        audioSource.PlayOneShot(playClickSound);
        hasGameStarted = true;       

        gameName.SetActive(false);
        startScreen.SetActive(false);        
        gameOver.SetActive(false);
        instructions.SetActive(false);
        playButton.SetActive(false);
        credits.SetActive(false);

        score = 0;
        scoreText.text = score.ToString();

        player.ResetPlayer();

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        for (int i = 0; i < pipes.Length; i++)
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
        audioSource.PlayOneShot(deathSound);

        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject p in projectiles)
        {
            Destroy(p);
        }

        if (hasGameStarted)
        {
            gameOver.SetActive(true);
            playButton.SetActive(true);
        }

        player.ResetPlayer();
        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        audioSource.PlayOneShot(scoreSound);

        StopAllCoroutines();
        StartCoroutine(ScorePop());
    }

    IEnumerator ScorePop()
    {
        Vector3 original = scoreTransform.localScale;
        scoreTransform.localScale = original * 1.3f;
        yield return new WaitForSeconds(0.1f);
        scoreTransform.localScale = original;
    }
}
