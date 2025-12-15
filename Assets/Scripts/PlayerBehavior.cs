using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public AudioClip bounceSound;
    private AudioSource audioSource;
    private bool playedThisCycle = false;

    public float jumpHeight = 1.2f;
    public float bounceDuration = 2.4f;

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        audioSource = FindFirstObjectByType<GameManager>().audioSource;
        startPosition = transform.position;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Normalize time 0 → 1
        float t = (timer % bounceDuration) / bounceDuration;

        float y;

        if (t < 0.5f)
        {
            // Fast up (ease out)
            y = Mathf.Pow(t * 2f, 0.35f);
        }
        else
        {
            // Hard fall (ease in)
            y = 1f - Mathf.Pow((t - 0.5f) * 2f, 1.4f);
        }

        if (t < 0.05f && !playedThisCycle)
        {
            audioSource.PlayOneShot(bounceSound, 0.3f);
            playedThisCycle = true;
        }

        if (t > 0.5f)
        {
            playedThisCycle = false;
        }
        transform.position = startPosition + Vector3.up * y * jumpHeight;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FindFirstObjectByType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
            FindFirstObjectByType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindFirstObjectByType<GameManager>().IncreaseScore();
        }
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
        timer = 0f;
    }
}
