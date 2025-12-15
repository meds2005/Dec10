using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    public AudioClip bounceSound;
    private AudioSource audioSource;
    private bool playedThisCycle = false;

    public float jumpHeight = 1.2f;
    public float bounceDuration = 2.4f;

    private Vector3 startPosition;
    private float timer;

    public Sprite[] flapSprites;   // size = 3
    public float flapSpeed = 0.2f;

    private SpriteRenderer spriteRenderer;
    private int flapIndex = 0;
    private float flapTimer = 0f;

    private Coroutine flashRoutine;

    void Start()
    {
        audioSource = FindFirstObjectByType<GameManager>().audioSource;
        startPosition = transform.position;
        timer = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        float t = (timer % bounceDuration) / bounceDuration;

        float y;

        if (t < 0.5f)
        {
            y = Mathf.Pow(t * 2f, 0.35f);
        }
        else
        {
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

        flapTimer += Time.deltaTime;

        if (flapTimer >= flapSpeed)
        {
            flapTimer = 0f;
            flapIndex = (flapIndex + 1) % flapSprites.Length;
            spriteRenderer.sprite = flapSprites[flapIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            StartCoroutine(FlashRoutine());
            FindFirstObjectByType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Projectile")
        {
            StartCoroutine(FlashRoutine());
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

    IEnumerator FlashRoutine()
    {
        Color original = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.05f);

        spriteRenderer.color = original;
    }
}
