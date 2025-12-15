using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public GameObject popParticlesPrefab;

    public AudioClip popSound;
    private AudioSource audioSource;

    public float speed = 3f;
    public float stopDistance = 0.05f;  

    private bool reachedCenter = false;
    private Vector3 target = new Vector3(0f, 0.7f, 0f);  

    void Update()
    {
        if (reachedCenter) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) <= stopDistance)
        {
            reachedCenter = true;
        }
    }

    void OnMouseDown()
    {
        Instantiate(
            popParticlesPrefab,
            transform.position,
            Quaternion.identity
        );

        AudioSource.PlayClipAtPoint(popSound, transform.position);
        Destroy(gameObject);
    }
}
