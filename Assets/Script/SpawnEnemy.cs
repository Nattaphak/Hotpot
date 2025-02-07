using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> spawnPoint;
    public int BeforenumBullet = 1;

    [SerializeField] private GameObject enemy;

    public float maxTimeTospawn = 5f;
    public int currentRound = 1;
    public float timeToSpawn = 0;

    void Update()
    {
        timeToSpawn += Time.deltaTime;

        if (timeToSpawn >= maxTimeTospawn)
        {
            StartCoroutine(SpawnEnemyRoutine());
            timeToSpawn = 0;
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        int enemiesToSpawn = Mathf.Min(currentRound, 6);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, spawnPoint.Count);
            GameObject spawnLocation = spawnPoint[randomIndex];

            GameObject newEnemy = Instantiate(enemy, spawnLocation.transform.position, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().SetRoundandNumberBullet(BeforenumBullet, currentRound);
            
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(2f);

        if (currentRound < 6) 
        {
            currentRound++;
            maxTimeTospawn += 5f;
        }
        else if (currentRound == 6)
        {
            maxTimeTospawn -= 2f;
        }
    }
}
