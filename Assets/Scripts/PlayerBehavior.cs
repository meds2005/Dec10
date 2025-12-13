using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 direction;

    public float gravity = -9.8f;
    public float strength = 7f;

    private float flapTimer = 0f;
    public float flapInterval = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = new Vector3(0f, 0f, 0f);

        // Reset movement
        direction = Vector3.zero;
        flapTimer = 0f;

        // Immediate starting jump
        direction = Vector3.up * strength;
    }

    // Update is called once per frame
    void Update()
    {
        // Count time
        flapTimer += Time.deltaTime;

        // Auto-flap every interval
        if (flapTimer >= flapInterval)
        {
            direction = Vector3.up * strength;   // same as pressing space
            flapTimer = 0f;                       // reset timer
        }
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FindFirstObjectByType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindFirstObjectByType<GameManager>().IncreaseScore();
        }
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;  // move bird to start
        direction = Vector3.zero;            // clear old falling speed
        flapTimer = 0f;                      // restart timer so flap happens on time
        direction = Vector3.up * strength;  
    }
}
