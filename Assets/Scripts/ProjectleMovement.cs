using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public AudioClip popSound;
    private AudioSource audioSource;

    public float speed = 3f;
    public float stopDistance = 0.05f;   // how close to get before stopping

    private bool reachedCenter = false;
    private Vector3 target = new Vector3(0f, 0.7f, 0f);   // ← your target

    void Update()
    {
        if (reachedCenter) return;

        // Move toward (0,0)
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        // When close enough, stop
        if (Vector3.Distance(transform.position, target) <= stopDistance)
        {
            reachedCenter = true;
        }
    }

    void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(popSound, transform.position);
        Destroy(gameObject);
    }
}
