using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public static float SPAWNENEMYDELAY = 2;
    private float timeUntilSpawn = 0;
    private bool isRespawning = false;
    // Update is called once per frame
    void Update()
    {
        if (isRespawning)
        {
            timeUntilSpawn -= Time.deltaTime;
            if (timeUntilSpawn <= 0)
            {
                isRespawning = false;
                Instantiate(enemyPrefab, this.transform);
            }
        }
        if (!isRespawning && transform.childCount == 0)
        {
            StartRespawning(SPAWNENEMYDELAY - HealthSystem.DESTROYDELAY); 
        }
    }
    private void StartRespawning(float delay = 0)
    {
        isRespawning = true;
        timeUntilSpawn = delay;
    }
}
