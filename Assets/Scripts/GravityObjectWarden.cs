using UnityEngine;

public class GravityObjectWarden : MonoBehaviour
{
    private float fallingSpeed = 3f;
    private float spawnRangeX = 2.5f;

    void Start()
    {
        float randomSpawnX = Random.Range(-spawnRangeX, spawnRangeX);
        transform.position = new Vector3(randomSpawnX, Camera.main.orthographicSize + 1, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime);
        
        if (transform.position.y < -Camera.main.orthographicSize - 1)
        {
            Destroy(gameObject);
        }
    }
}