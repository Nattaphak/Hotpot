using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnbullet;
    public float timeBullet;

    public GameObject bullet;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        timeBullet += Time.deltaTime;
        if(timeBullet >= 5)
        {
            ShootBullet();
            timeBullet = 0;
        }
    }

    void ShootBullet()
    {
        Vector3 direction = (player.transform.position - spawnbullet.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject newBullet = Instantiate(bullet, spawnbullet.transform.position, Quaternion.identity);

        newBullet.GetComponent<Bullet>().SetDirection(direction);
    }
}
