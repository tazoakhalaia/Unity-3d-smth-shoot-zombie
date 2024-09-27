using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("EnemyPlayerParameter")]
    public GameObject enemy;
    public Transform player;

    [Header("EnemySpeed")]

    private readonly float maxEnemySpeed = 300;
    private readonly float minEenemySpeed = 200;
    private readonly float rotationSpeed = 5f;

    [Header("SpawnEnemy")]
    private readonly List<GameObject> enemies = new();

    void Start()
    {
        InvokeRepeating(nameof(RenderEnemy), 1.5f, 1.5f);
    }

    void Update()
    {
        foreach (GameObject e in enemies)
        {
            if (e != null)
            {
                ZombieManager zombieManager = e.GetComponent<ZombieManager>();
                Rigidbody rb = e.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(Vector3.up * -40, ForceMode.Impulse);
                }
                if (!zombieManager.isDead)
                {
                    Vector3 direct = (player.position - e.transform.position).normalized;
                    e.transform.position += Random.Range(minEenemySpeed, maxEnemySpeed) * Time.deltaTime * direct;
                    Quaternion lookRotation = Quaternion.LookRotation(direct);
                    e.transform.rotation = Quaternion.Slerp(e.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void RenderEnemy()
    {
        float randomX = Random.Range(300, 3500);
        Vector3 spawnPosition = new(randomX, transform.position.y, 4000);
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        newEnemy.transform.eulerAngles = new Vector3(0, 180, 0);
        enemies.Add(newEnemy);
    }
}
