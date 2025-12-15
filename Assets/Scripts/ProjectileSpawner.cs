using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float spawnInterval = 4f;
    private float timer;

    // How far outside the screen to spawn
    public float spawnOffset = 1f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnProjectile();
            timer = 0f;
        }
    }

    void SpawnProjectile()
    {
        Camera cam = Camera.main;

        // Get screen edges in world space
        float left   = cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - spawnOffset;
        float right  = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + spawnOffset;
        float bottom = cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - spawnOffset;
        float top    = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y + spawnOffset;

        // Pick a random side: 0=left, 1=right, 2=top, 3=bottom
        int side = Random.Range(0, 4);

        Vector3 spawnPos = Vector3.zero;

        switch (side)
        {
            case 0: // left
                spawnPos = new Vector3(left, Random.Range(bottom, top), 0);
                break;

            case 1: // right
                spawnPos = new Vector3(right, Random.Range(bottom, top), 0);
                break;

            case 2: // top
                spawnPos = new Vector3(Random.Range(left, right), top, 0);
                break;

            case 3: // bottom
                spawnPos = new Vector3(Random.Range(left, right), bottom, 0);
                break;
        }

        Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
    }
}
