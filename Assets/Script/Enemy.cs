using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnbullet;

    public GameObject bullet;
    public int numBullet = 3;
    public int RoundBullet = 2;
    public float delayShoot = 2f;

    private int currentRound = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(ShootBullet());
    }

    IEnumerator ShootBullet()
    {
        
        while (currentRound < RoundBullet)
        {
            currentRound++;

            for (int currentBullet = 0; currentBullet < numBullet; currentBullet++)
            {
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
