using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxScore = 300;
    [SerializeField] private float initialSpawnTime = 0.1f;
    [SerializeField] private float delayTime = 5.0f;

    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform player;
    [SerializeField] private Scorer scorer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Spawn), initialSpawnTime, delayTime);
    }

    private void Spawn()
    {
        if (scorer.GetScore() > maxScore || player == null)
        {
            Destroy(gameObject);
            return;
        }
        //choose 1 of 4 spawn locations and spawn an enemy there
        Transform spawnLocation = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        GameObject spawned = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnLocation.position, spawnLocation.rotation);

        // set the player as a target
        EnemyMovement enemy = spawned.GetComponent<EnemyMovement>();
        enemy.target = player;
        enemy.scorer = scorer;
    }

}
