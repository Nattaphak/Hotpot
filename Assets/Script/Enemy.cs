using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnbullet;

    public GameObject bullet;
    private int numBullet;
    private int RoundBullet;
    public float delayShoot = 4f;

    private int currentRound = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(ShootBullet());
    }

    public void SetRoundandNumberBullet(int BeforenumberBullet, int BeforeRoundBullet)
    {
        numBullet = BeforenumberBullet;
        RoundBullet = BeforeRoundBullet;
    }

    IEnumerator ShootBullet()
    {
        
        while (currentRound < RoundBullet)
        {
            currentRound++;

            for (int currentBullet = 0; currentBullet < numBullet; currentBullet++)
            {
                yield return new WaitForSeconds(delayShoot);

                Vector3 direction = (player.transform.position - spawnbullet.position).normalized;
                GameObject newBullet = Instantiate(bullet, spawnbullet.position, Quaternion.identity);
                newBullet.GetComponent<Bullet>().SetDirection(direction);

                yield return new WaitForSeconds(delayShoot);
            }

            yield return new WaitForSeconds(delayShoot);
        }

        Destroy(this.gameObject);
    }
}
